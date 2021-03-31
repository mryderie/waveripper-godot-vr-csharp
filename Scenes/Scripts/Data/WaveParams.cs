using Godot;

public class WaveParams
{
    public WaveParams(int beatGap,
                    LeftControlPointRotation leftRotation,
                    RightControlPointRotation rightRotation,
                    int segmentCount = 16,
                    float initialRotationRad = 0,
                    float rotationRateRad = 0)
    {
        BeatGap = beatGap;
        LeftRotation = leftRotation;
        RightRotation = rightRotation;
        SegmentCount = segmentCount;
        InitialRotationRad = initialRotationRad;
        RotationRateRad = rotationRateRad;
    }
    
    /// <summary>
    /// Multiple of BeatDuration in Sequence to set the number of beats between this wave, and the preceeding wave.
    /// </summary>
    public int BeatGap { get; protected set; }

    public LeftControlPointRotation LeftRotation { get; protected set; }
    public RightControlPointRotation RightRotation { get; protected set; }

    public float LeftControlRotationRad
    {
        get
        {
            return Mathf.Deg2Rad((int)LeftRotation);
        }
    }

    public float RightControlRotationRad
    {
        get
        {
            return Mathf.Deg2Rad((int)RightRotation);
        }
    }

    /// <summary>
    /// Width of wave in number of segements, 16 to 32 is a reasonable range. 
    /// </summary>
    public int SegmentCount { get; protected set; }

    /// <summary>
    /// The initial rotation in radians of the wave about the z-axis (positive or negative)
    /// </summary>
    public float InitialRotationRad { get; protected set; }

    /// <summary>
    /// Rate of wave rotation in radians per second about the z-axis. (zero for no rotation, or positive or negative)
    /// </summary>
    public float RotationRateRad { get; protected set; }
}
