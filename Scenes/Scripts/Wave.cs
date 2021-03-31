using Godot;

public class Wave : Spatial
{
	protected AudioStreamPlayer CutSoundPlayer;
	protected Timer CutSoundTimer;
	protected bool AllSegmentsCut = false;
	protected bool WaveActive = false;
	protected bool WaveSetForRemoval = false;
	protected PackedScene WaveSegmentScene = ResourceLoader.Load<PackedScene>("res://Scenes/WaveSegment.tscn");
	protected WaveSegment[] WaveSegments;
	public float RotationRateRad { get; protected set; }
	protected const float FADE_OUT_TIME = 0.5f;

	public override void _Ready()
	{
		base._Ready();

		CutSoundPlayer = GetNode<AudioStreamPlayer>("CutSoundPlayer");
		CutSoundTimer = GetNode<Timer>("CutSoundTimer");
	}

	public void Initialise(WaveParams waveParams)
	{
		// Instantiate the wave segments, and set the initial wave rotation

		WaveSegments = new WaveSegment[waveParams.SegmentCount];
		var waveWidth = WaveSegment.SEGMENT_WIDTH * waveParams.SegmentCount;

		var leftOrigin = new Vector3(-waveWidth / 2, 0, 0);
		var leftControlPointInOrigin = new Vector3(-0.2f, 0, 0).Rotated(new Vector3(0, 0, 1), waveParams.LeftControlRotationRad);
		var leftControlPointOutOrigin = new Vector3(0.2f, 0, 0).Rotated(new Vector3(0, 0, 1), waveParams.LeftControlRotationRad);

		var rightOrigin = new Vector3(waveWidth / 2, 0, 0);
		var rightControlPointInOrigin = new Vector3(0.2f, 0, 0).Rotated(new Vector3(0, 0, 1), waveParams.RightControlRotationRad);
		var rightControlPointOutOrigin = new Vector3(-0.2f, 0, 0).Rotated(new Vector3(0, 0, 1), waveParams.RightControlRotationRad);

		var curve = new Curve3D();
		var gap = rightOrigin - leftOrigin;

		// control point locations are relative to the curve point origin (i.e not global coordinates)
		curve.AddPoint(leftOrigin,
						SetControlPoint(gap.Length(), leftControlPointInOrigin),
						SetControlPoint(gap.Length(), leftControlPointOutOrigin));

		curve.AddPoint(rightOrigin,
						SetControlPoint(gap.Length(), rightControlPointInOrigin),
						SetControlPoint(gap.Length(), rightControlPointOutOrigin));

		// instantiate wave segments
		var segmentFraction = (float)1 / waveParams.SegmentCount;
		for (var i = 0; i < waveParams.SegmentCount; i++)
		{
			var waveSegment = WaveSegmentScene.Instance() as WaveSegment;

			// xOffset moves from leftOrigin to rightOrigin in a number of steps equal to the segment count
			var xOffset = leftOrigin.x + (rightOrigin.x - leftOrigin.x) * (i * segmentFraction);
			var yOffset = curve.Interpolate(0, i * segmentFraction + (segmentFraction / 2));

			waveSegment.Translation = new Vector3(xOffset, yOffset.y, 0);

			AddChild(waveSegment);
			WaveSegments[i] = waveSegment;
		}

		Rotation = new Vector3(0, 0, waveParams.InitialRotationRad);
		RotationRateRad = waveParams.RotationRateRad;
	}

	public void SetActive()
	{
		if(!WaveActive)
		{
			// change material to indicate wave can be cut
			foreach (var segment in WaveSegments)
				segment.SetActive();

			WaveActive = true;
		}
	}

	public void SetForRemoval()
	{
		if (!WaveSetForRemoval) // only execute once
		{
			WaveSetForRemoval = true;

			// change material to fade out the wave
			foreach (var segment in WaveSegments)
			{
				segment.SetForRemoval(FADE_OUT_TIME);
			}
		}
	}

	public bool WaveCut(Vector3[] ribbonPoints)
	{
		if (AllSegmentsCut)
			return true; // If already cut, don't check again

		AllSegmentsCut = true;
		foreach (var waveSegment in WaveSegments)
		{
			var segmentCut = waveSegment.CheckCut(ribbonPoints);
			
			if (!segmentCut)
				AllSegmentsCut = false;
		}

		if (AllSegmentsCut)
			// delay so it's not muffled by overlapping with the Segment Cut sound effects
			CutSoundTimer.Start(0.25f);

		return AllSegmentsCut;
	}

	private void OnCutSoundTimerTimeout()
	{
		CutSoundPlayer.Play();
	}

	public void Remove()
	{
		// todo - start some fade-out/decay animation on the wave

		foreach (var waveSegment in WaveSegments)
			waveSegment.QueueFree();
	}

	protected Vector3 SetControlPoint(float gap, Vector3 controlPoint)
	{
		return controlPoint.Normalized() * gap * 0.7f;
	}
}
