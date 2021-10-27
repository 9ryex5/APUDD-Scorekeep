using System;

public static class Helpers
{
    public static string TimeSpanToString(TimeSpan _ts)
    {
        return (_ts.Hours > 0 ? _ts.Hours.ToString("00") + ":" : string.Empty) + (_ts.Minutes > 0 ? _ts.Minutes.ToString("00") + ":" : string.Empty) + _ts.Seconds.ToString("00");
    }
}