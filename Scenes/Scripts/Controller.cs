using Godot;
using System;

public class Controller : ARVRController
{
	protected Game Game;

	// Point from which to begin drawing the ribbon
	protected Position3D RibbonOrigin;
	protected Position3D RibbonFrontEdge;
	protected Position3D RibbonBackEdge;

	// Bezier curve control point on the handle side of the ribbon origin
	// On the Left controller, this will be the "in" point, as the curve is drawn from left controller to right.
	// On the Right controller, this will be the "out" point.
	protected Position3D ControlPointHandle;

	// Bezier curve control point on the ribbon side of the ribbon origin
	// On the Left controller, this will be the "out" point, as the curve is drawn from left controller to right.
	// On the Right controller, this will be the "in" point.
	protected Position3D ControlPointRibbon;
	
	public override void _Ready()
	{
		Game = GetNode<Game>("/root/Game");

		RibbonOrigin = GetNode<Position3D>("RibbonOrigin");
		RibbonFrontEdge = GetNode<Position3D>("RibbonFrontEdge");
		RibbonBackEdge = GetNode<Position3D>("RibbonBackEdge");

		ControlPointHandle = GetNode<Position3D>("ControlPointHandle");
		ControlPointRibbon = GetNode<Position3D>("ControlPointRibbon");
	}

	public Vector3 RibbonGlobalOrigin
	{
		get
		{
			return RibbonOrigin.GlobalTransform.origin;
		}
	}

	public Vector3 RibbonFrontEdgeOffset
	{
		get
		{
			return RibbonFrontEdge.GlobalTransform.origin - RibbonOrigin.GlobalTransform.origin;
		}
	}

	public Vector3 RibbonBackEdgeOffset
	{
		get
		{
			return RibbonBackEdge.GlobalTransform.origin - RibbonOrigin.GlobalTransform.origin;
		}
	}

	public Vector3 ControlPointInOffset
	{
		get
		{
			if (ControllerId == 1) // left controller
			{
				return ControlPointHandle.GlobalTransform.origin - RibbonOrigin.GlobalTransform.origin;
			}
			else // right controller
			{
				return ControlPointRibbon.GlobalTransform.origin - RibbonOrigin.GlobalTransform.origin;
			}
		}
	}

	public Vector3 ControlPointOutOffset
	{
		get
		{
			if (ControllerId == 1) // left controller
			{
				return ControlPointRibbon.GlobalTransform.origin - RibbonOrigin.GlobalTransform.origin;
			}
			else // right controller
			{
				return ControlPointHandle.GlobalTransform.origin - RibbonOrigin.GlobalTransform.origin;
			}
		}
	}

	private void OnArVrControllerButtonPressed(int buttonIndex)
	{
		switch (buttonIndex)
		{
			case 1:
			case 7:
				Game.StartGame();
				break;
		}
	}
}
