using System;

public static class Helpers
{
    public static string TimeSpanToString(TimeSpan _ts, bool _hide0hour = false)
    {
        return (_hide0hour ? (_ts.Hours > 0 ? _ts.Hours.ToString("00") + ":" : string.Empty) : _ts.Hours.ToString("00") + ":")
         + _ts.Minutes.ToString("00") + ":"
         + _ts.Seconds.ToString("00");
    }

    public static string DateTimeToString(DateTime _dt, bool _date, bool _showSeconds = true)
    {
        return _date ? _dt.Year.ToString() + "/" + _dt.Month.ToString() + "/" + _dt.Day.ToString()
        : _dt.Hour.ToString("00") + ":" + _dt.Minute.ToString("00");
    }
}