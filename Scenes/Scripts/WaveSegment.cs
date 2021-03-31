using Godot;
using System;

public class WaveSegment : Spatial
{
	public const float SEGMENT_WIDTH = 0.025f;
	protected CollisionQuad CollisionQuad;
	protected MeshInstance MeshInstance;
	protected SpatialMaterial ActiveMaterial;
	protected Tween CutTween;
	protected Particles CutParticlesUp;
	protected Particles CutParticlesDown;
	protected AudioStreamPlayer CutSoundPlayer;
	protected Timer CutSoundTimer;
	protected const float CUT_REMOVAL_TIME = 0.2f;
	protected bool isCut;

	public override void _Ready()
	{
		base._Ready();

		var collisionQuadA = GetNode<Position3D>("CollisionQuad/A");
		var collisionQuadB = GetNode<Position3D>("CollisionQuad/B");
		var collisionQuadC = GetNode<Position3D>("CollisionQuad/C");
		var collisionQuadD = GetNode<Position3D>("CollisionQuad/D");
		
		MeshInstance = GetNode<MeshInstance>("MeshInstance");
		ActiveMaterial = ResourceLoader.Load<SpatialMaterial>("res://Scenes/Materials/SegmentActiveMaterial.tres");

		CutTween = GetNode<Tween>("CutTween");
		CutParticlesUp = GetNode<Particles>("CutParticlesUp");
		CutParticlesDown = GetNode<Particles>("CutParticlesDown");
		CutSoundPlayer = GetNode<AudioStreamPlayer>("CutSoundPlayer");
		CutSoundTimer = GetNode<Timer>("CutSoundTimer");

		CollisionQuad = new CollisionQuad(collisionQuadA, collisionQuadB, collisionQuadC, collisionQuadD);
	}
	public bool CheckCut(Vector3[] ribbonPoints)
	{
		if (ribbonPoints == null
			|| isCut) // already cut, no need to check again....
			return isCut;

		for (var i = 0; i < ribbonPoints.Length - 1; i++)
		{
			isCut = CollisionQuad.LineSegmentIntersects(ribbonPoints[i], ribbonPoints[i + 1]);

			if (isCut)
			{
				ChangeToCutAppearance();

				// add some random delay to the playback so the sound effects from all the segments don't overlap
				CutSoundTimer.Start((float)GD.RandRange(0, 0.25));
				
				break;
			}
		}

		return isCut;
	}

	protected void ChangeToCutAppearance()
	{
		// shrink
		CutTween.InterpolateProperty(MeshInstance,
										"scale:y",
										1,
										0,
										CUT_REMOVAL_TIME);

		// todo - fade-out not working
		// ...and fade out
		CutTween.InterpolateProperty(ActiveMaterial,
										"albedo_color:a",
										1,
										0,
										CUT_REMOVAL_TIME);

		CutTween.Start();

		CutParticlesUp.Emitting = true;
		CutParticlesDown.Emitting = true;
	}

	public void SetActive()
	{
		MeshInstance.MaterialOverride = ActiveMaterial;
	}

	internal void SetForRemoval(float fadeOutTime)
	{
		if (!isCut)
		{
			// todo - fade-out not working
			CutTween.InterpolateProperty(ActiveMaterial,
											"albedo_color:a",
											1,
											0,
											fadeOutTime);

			CutTween.Start();
		}
	}

	private void OnCutSoundTimerTimeout()
	{
		CutSoundPlayer.Play();
	}
}
