using System;
using UnityEngine;

public class OqoDateTime
{
    private int _year;

    public int Year
    {
        get
        {
            return _year;
        }
    }

    private int _month;

    public int Month
    {
        get
        {
            return _month;
        }
    }

    private int _day;

    public int Day
    {
        get
        {
            return _day;
        }
    }

    private int _hour;

    public int Hour
    {
        get
        {
            return _hour;
        }
    }

    private int _minute;

    public int Minute
    {
        get
        {
            return _minute;
        }
    }

    private float _seconds;

    public float Seconds
    {
        get
        {
            return _seconds;
        }
    }

    public static int MinutesPerHour = 60;
    public static int HoursPerDay = 30;
    public static int DaysPerMonth = 22;
    public static int MonthsPerYear = 30;
    public static int MinutesPerDay = (HoursPerDay * MinutesPerHour);
    public static int MinutesPerMonth = (DaysPerMonth * MinutesPerDay);
    public static int MinutesPerYear = (MonthsPerYear * MinutesPerMonth);

    public event TimeEventHandler HoursAdvanced;
    public event TimeEventHandler DaysAdvanced;
    public event TimeEventHandler MonthsAdvanced;
    public event TimeEventHandler YearsAdvanced;

    public OqoDateTime(int minutes = 0)
    {
        SetTime(minutes);
        Validate();
    }

    public void AddSeconds(float seconds)
    {
        _seconds += seconds;
        Validate();
    }

    public void SetTime(int minutes)
    {
        _minute = minutes;
        Validate();
    }

    public int GetTime()
    {
        var time = (_year * MinutesPerYear) + (_month * MinutesPerMonth) + (_day * MinutesPerDay) + (_hour * MinutesPerHour) + _minute;
        return time;
    }

    public float DayProgress()
    {
        float totMinutes = _minute + (_hour * MinutesPerHour) + (_seconds / 60);
        var ret = totMinutes / MinutesPerDay;
        return ret;
    }

    public void Validate()
    {
        if (_seconds >= 60)
        {
            var addMinutes = (int)_seconds / 60;
            _minute += addMinutes;
            _seconds -= addMinutes * 60;
        }
        if (_minute >= MinutesPerHour)
        {
            var addHour = _minute / MinutesPerHour;
            _hour += addHour;
            _minute -= addHour * MinutesPerHour;
            if (addHour > 0) HoursAdvanced?.Invoke(addHour);
        }
        if (_hour >= HoursPerDay)
        {
            var addDay = _hour / HoursPerDay;
            _day += addDay;
            _hour -= addDay * HoursPerDay;
            if (addDay > 0) DaysAdvanced?.Invoke(addDay);
        }
        if (_day > DaysPerMonth)
        {
            var addMonth = _day / DaysPerMonth;
            _month += addMonth;
            _day -= addMonth * DaysPerMonth;
            if (addMonth > 0) MonthsAdvanced?.Invoke(addMonth);
        }
        if (_month > MonthsPerYear)
        {
            var addYear = _month / MonthsPerYear;
            _year += addYear;
            _month -= addYear * MonthsPerYear;
            if (addYear > 0) YearsAdvanced?.Invoke(addYear);
        }
    }

    public override string ToString()
    {
        return $"{string.Format("{0:00}", _hour)}:{string.Format("{0:00}", _minute)} {string.Format("{0:00}", _day + 1)}/{string.Format("{0:00}", _month + 1)}/{string.Format("{0:00}", _year + 1)}";
    }
}

public delegate void TimeEventHandler(int increment);