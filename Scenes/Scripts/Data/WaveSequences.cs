using Godot;

public static class WaveSequences
{
	// waves need to arrive at optimal cutting point in sync to the music
	// waves will move at a constant speed - 1m/s
	// for high-tempo tunes, waves will be closer together

	const float Degrees45Rad = Mathf.Pi / 4;
	const float Degrees90Rad = Mathf.Pi / 2;
	const float Degrees135Rad = Mathf.Pi * 0.75f;
	const float Degrees180Rad = Mathf.Pi;

	public static Sequence SequenceA = new Sequence(
			"res://Tracks/128bpm_click_track.ogg",
			0.875f, // intro length -  length of 4 beats, minus the 1 second it takes the wave to move from the emision point to the cut point
			0.46875f, // 128bpm
			new []
			{
				// Intro
				new WaveParams(0, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),


				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(4, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16),

				new WaveParams(4, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(4, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16),

				new WaveParams(4, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(4, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(4, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(4, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(4, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),


				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),


				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 32),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 32),






				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: -Degrees45Rad),


				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: -Degrees45Rad),


				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalUp, RightControlPointRotation.DiagonalUp, 32, rotationRateRad: -Degrees45Rad),

				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 16, rotationRateRad: Degrees45Rad),
				new WaveParams(2, LeftControlPointRotation.DiagonalDown, RightControlPointRotation.DiagonalDown, 32, rotationRateRad: -Degrees45Rad),


			}
		);
}
