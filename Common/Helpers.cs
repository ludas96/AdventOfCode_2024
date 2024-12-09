using System.Diagnostics;

namespace Common;

public static class StopWatchHelpers
{
    public static float TicksToMs(float ticks)
    {
        long nanosecPerTick = (1000L*1000L*1000L) / Stopwatch.Frequency;
        return (ticks * nanosecPerTick) / 1_000_000;
    }
}