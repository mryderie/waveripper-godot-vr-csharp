using Godot;
using System.Collections.Generic;
using System.Linq;

public class WaveEmitter : Spatial
{
	protected Position3D WaveSpawnPoint;
	protected Tween WaveMovementTween;
	protected Sequence WaveSequence;
	protected List<Wave> WaveInstances = new List<Wave>();
	protected AudioStreamPlayer AudioStreamPlayer;

	protected PackedScene WaveScene = ResourceLoader.Load<PackedScene>("res://Scenes/Wave.tscn");
	protected Vector3[] RibbonPoints;

	private bool Active = false;
	private float NextEmissionTime;
	private const float WARP_TIME = 0.25f;

	public override void _Ready()
	{
		WaveSpawnPoint = GetNode<Position3D>("WaveSpawnPoint");
		WaveMovementTween = GetNode<Tween>("WaveMovementTween");
		AudioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

	public void RunSequence(Sequence sequence)
	{
		WaveSequence = sequence;

		// load and play the audio track
		var audioStream = ResourceLoader.Load<AudioStreamOGGVorbis>(sequence.TrackPath);
		audioStream.Loop = false;
		AudioStreamPlayer.Stream = audioStream;
		AudioStreamPlayer.Play();
		
		Active = true;
		NextEmissionTime = sequence.IntroDelay;
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);

		// check if the wave nearest the player has passed the global z-axis zero point
		// At that point it is not longer cutable, and must be removed
		var nearestWave = WaveInstances.FirstOrDefault();
		if(nearestWave != null
			&& nearestWave.GlobalTransform.origin.z >= 1)
		{
			// wave should have faded-out by now, so remove it from the scene.
			WaveInstances.Remove(nearestWave);

			nearestWave.Remove();
			nearestWave.QueueFree();
		}
		else if (nearestWave != null
			&& nearestWave.GlobalTransform.origin.z >= 0)
		{
			nearestWave.SetForRemoval();
		}

		// check for a collision with the ribbon
		// Check all waves within 1m of global origin for cuts
		foreach (var waveInstance in WaveInstances)
		{
			if (waveInstance.GlobalTransform.origin.z > -1) // only check for cuts when wave is near player origin
			{
				waveInstance.SetActive();

				if (waveInstance.WaveCut(RibbonPoints))
				{
					// todo - if all the segments are cut, play a special sound effect?
				}
			}
		}

		// check if it's time to emit another wave
		// Previously attempted to do this with a Timer, and also with OS.ticks() method, but both fell out of sync with audio over duration of track.
		// Using GetPlaybackPosition() to time wave emission seems to be effective
		// todo - investigate method here: https://docs.godotengine.org/en/stable/tutorials/audio/sync_with_audio.html#using-the-sound-hardware-clock-to-sync
		if (Active
			&& AudioStreamPlayer.GetPlaybackPosition() >= NextEmissionTime - WARP_TIME)
		{
			// Initialise a new wave
			var waveInstance = WaveScene.Instance() as Wave;
			WaveSpawnPoint.AddChild(waveInstance);

			waveInstance.Initialise(WaveSequence.GetNextWave());
			WaveInstances.Add(waveInstance);

			// "warp" the wave in quickly...
			WaveMovementTween.InterpolateProperty(waveInstance, "translation:z", -30, 0, WARP_TIME);

			// ...then move the wave towards the player at a reasonable speed (n.b. this tween starts after a delay)
			var animationDuration = 3f;
			WaveMovementTween.InterpolateProperty(waveInstance, "translation:z", 0, 3, animationDuration, delay: WARP_TIME);

			if (waveInstance.RotationRateRad != 0)
			{
				// some waves will also rotate as they move toward the player (n.b. this tween starts after a delay)
				var rotationStart = waveInstance.Rotation.z;
				var rotationEnd = waveInstance.Rotation.z + (waveInstance.RotationRateRad * animationDuration);
				WaveMovementTween.InterpolateProperty(waveInstance, "rotation:z", rotationStart, rotationEnd, animationDuration, delay: WARP_TIME);
			}

			WaveMovementTween.Start();

			var nextWaveDelay = WaveSequence.GetNextWaveDelay();
			if (nextWaveDelay.HasValue)
			{
				NextEmissionTime += nextWaveDelay.Value;
			}
			else
			{
				// no waves left in sequence
				Active = false;
			}
		}
	}

	public void SetRibbonPoints(Vector3[] ribbonPoints)
	{
		RibbonPoints = ribbonPoints;
	}
}
