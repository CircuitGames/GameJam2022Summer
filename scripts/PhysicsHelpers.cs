using System;

public class PhysHelp
{
    // maximum frame interval, start slowing down the game if we exceed this
    const float m_deltaContraintThreshold = 1 / 24f;
    public static void constrainDelta ( ref float delta )
    {
        delta = Math.Min( delta, m_deltaContraintThreshold );
    }
    public static float constrainDelta ( float delta )
    {
        return Math.Min( delta, m_deltaContraintThreshold );
    }
}