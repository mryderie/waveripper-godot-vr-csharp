using Godot;

public class Game : Node
{
	protected Ribbon Ribbon;
	protected WaveEmitter WaveEmitter;
	protected Timer GameStartTimer;

	public override void _Ready()
	{
		base._Ready();

		var vr = ARVRServer.FindInterface("OpenVR");

		if (vr != null && vr.Initialize())
		{
			GetViewport().Arvr = true;

			OS.VsyncEnabled = false;
			Engine.IterationsPerSecond = 90;
		}

		Ribbon = GetNode<Ribbon>("ARVROrigin/Ribbon");
		WaveEmitter = GetNode<WaveEmitter>("WaveEmitter");
		GameStartTimer = GetNode<Timer>("GameStartTimer");
		//GameStartTimer.Start();
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);

		WaveEmitter.SetRibbonPoints(Ribbon.RibbonPoints);
	}

	// temporary...
	private void GameStartTimerTimeout()
	{
		StartGame();
	}

	public void StartGame()
	{
		GD.Print("Game start...");
		WaveEmitter.RunSequence(WaveSequences.SequenceA);
	}
}
