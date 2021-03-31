using Godot;

public readonly struct CollisionQuad
{
	private readonly Position3D _p1;
	private readonly Position3D _p2;
	private readonly Position3D _p3;
	private readonly Position3D _p4;

	public CollisionQuad(Position3D p1, Position3D p2, Position3D p3, Position3D p4)
	{
		_p1 = p1;
		_p2 = p2;
		_p3 = p3;
		_p4 = p4;
	}

	public bool LineSegmentIntersects(Vector3 from, Vector3 to)
	{
		return Geometry.SegmentIntersectsTriangle(from, to, _p1.GlobalTransform.origin, _p2.GlobalTransform.origin, _p3.GlobalTransform.origin) != null
				|| Geometry.SegmentIntersectsTriangle(from, to, _p1.GlobalTransform.origin, _p3.GlobalTransform.origin, _p4.GlobalTransform.origin) != null;
	}
}
