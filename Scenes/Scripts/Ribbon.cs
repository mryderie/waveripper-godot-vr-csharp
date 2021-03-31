using Godot;

public class Ribbon : Spatial
{
	protected Controller LeftController;
	protected Controller RightController;
	protected ImmediateGeometry RibbonMesh;

	// these points will be used for collision detection with the wave
	public Vector3[] RibbonPoints { get; protected set; }

	public override void _Ready()
	{
		LeftController = GetNode<Controller>("LeftController");
		RightController = GetNode<Controller>("RightController");
		RibbonMesh = GetNode<ImmediateGeometry>("RibbonMesh");
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);

		var ribbonCurve = new Curve3D();

		// based on the gap between the two controllers, the control points move further away from the origin as the controllers are moved apart
		// without dynamically adjusting the control points, the curve would get straighter as the controllers moved apart.
		var gapWidth = (RightController.GlobalTransform.origin - LeftController.GlobalTransform.origin).Length();

		// control point locations are offset from the origin point (i.e not global coordinates)
		ribbonCurve.AddPoint(LeftController.RibbonGlobalOrigin,
							AdjustControlPoint(LeftController.ControlPointInOffset, gapWidth),
							AdjustControlPoint(LeftController.ControlPointOutOffset, gapWidth));

		ribbonCurve.AddPoint(RightController.RibbonGlobalOrigin,
							AdjustControlPoint(RightController.ControlPointInOffset, gapWidth),
							AdjustControlPoint(RightController.ControlPointOutOffset, gapWidth));


		// figure out a vector at 90 degrees to origin and control point. Vector will be used to offset one side of the ribbon, and give it a consistent width and orientation
		// need this for both controllers, and then gradually rotate from one to the other over the course of the ribbon length

		// Curve points pass through the centre of the Ribbon, as it appears on-screen.
		// To get the vertices to draw, need to offset from curvePoints for the front and back edges of the Ribbon, as below
		RibbonPoints = ribbonCurve.Tessellate();

		// will interpolate between these edges to give the ribbon a smooth "twist" over its length
		var leftRibbonFrontEdgeOffset = LeftController.RibbonFrontEdgeOffset;
		var leftRibbonBackEdgeOffset = LeftController.RibbonBackEdgeOffset;

		var rightRibbonFrontEdgeOffset = RightController.RibbonFrontEdgeOffset;
		var rightRibbonBackEdgeOffset = RightController.RibbonBackEdgeOffset;

		var triStripPoints = new Vector3[RibbonPoints.Length * 2];
		var curvePointLengthFloat = (float)RibbonPoints.Length;
		for (var i = 0; i < RibbonPoints.Length; i++)
		{
			// Offset front and back from curvePoints, as curvePoints pass through the centre of the ribbon
			var frontEdgePoint = RibbonPoints[i] + leftRibbonFrontEdgeOffset.LinearInterpolate(rightRibbonFrontEdgeOffset, i / curvePointLengthFloat);
			var backEdgePoint = RibbonPoints[i] + leftRibbonBackEdgeOffset.LinearInterpolate(rightRibbonBackEdgeOffset, i / curvePointLengthFloat);

			triStripPoints[i * 2] = frontEdgePoint;
			triStripPoints[(i * 2) + 1] = backEdgePoint;
		}

		RibbonMesh.Clear(); // without Clear, triangles from previous loops accumulate on screen
		RibbonMesh.Begin(Mesh.PrimitiveType.TriangleStrip);
		for (var i = 0; i < triStripPoints.Length; i++)
		{
			// pass vertices to Normal function in anti-clockwise order
			// this will vary depending on which side of the ribbon the vertex is added, hence the i % 2... condition. i.e.:
			// - odd number: n, n-1, n-2
			// - even number: n-2, n-1, n
			var normal = i < 3 ? GetTriangleNormal(triStripPoints[2], triStripPoints[1], triStripPoints[0])
								: (i % 2 == 0 ? GetTriangleNormal(triStripPoints[i], triStripPoints[i - 1], triStripPoints[i - 2])
									: GetTriangleNormal(triStripPoints[i - 2], triStripPoints[i - 1], triStripPoints[i]));

			RibbonMesh.SetNormal(normal);
			RibbonMesh.AddVertex(triStripPoints[i]);
		}
		RibbonMesh.End();
	}

	protected Vector3 AdjustControlPoint(Vector3 controlPointOffset, float gapWidth)
	{
		return controlPointOffset.Normalized() * gapWidth * 0.7f;
	}

	protected Vector3 GetTriangleNormal(Vector3 a, Vector3 b, Vector3 c)
	{
		// find the surface normal given 3 vertices
		var side1 = b - a;
		var side2 = c - a;
		var normal = side1.Cross(side2);
		return normal;
	}
}
