public class Sequence
{
    public Sequence(string trackPath, float introDelay, float beatDuration, WaveParams[] waves, int nextWaveIndex = 0)
    {
        TrackPath = trackPath;
        IntroDelay = introDelay;
        BeatDuration = beatDuration;
        Waves = waves;
        NextWaveIndex = nextWaveIndex;
    }
    
    /// <summary>
    /// Resource path to the music track that will play during the sequence
    /// </summary>
    public string TrackPath { get; protected set; }

    /// <summary>
    /// The delay in seconds until the first wave is ready to cut
    /// </summary>
    public float IntroDelay { get; protected set; }

    /// <summary>
    /// Duration in seconds of a single beat in the music track. Used to control the spacing between waves. (n.b. assumes tempo remains the same throughout track)
    /// </summary>
    public float BeatDuration { get; protected set; }

    /// <summary>
    /// Array index of the next wave to show. Usually starts at zero.
    /// </summary>
    public int NextWaveIndex { get; protected set; }
    
    /// <summary>
    /// Defines the series of waves that will be presented over the course of the track sequence.
    /// </summary>
    public WaveParams[] Waves { get; protected set; }

    public WaveParams GetNextWave()
    {
        // todo - temporary
        if (NextWaveIndex == Waves.Length)
            NextWaveIndex = 0;
        
        var wave = Waves[NextWaveIndex];
        NextWaveIndex++;

        return wave;
    }

    /// <summary>
    /// Get the time in seconds until the next wave should appear.
    /// </summary>
    /// <returns>Time in seconds</returns>
    public float? GetNextWaveDelay()
    {
        // todo - temporary?
        if (NextWaveIndex == Waves.Length)
            return null;

        return Waves[NextWaveIndex].BeatGap * BeatDuration;
    }
}