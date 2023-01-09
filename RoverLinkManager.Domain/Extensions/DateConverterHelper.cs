namespace RoverLinkManager.Domain.Extensions;
public static class DateConverterHelper
{
    /// <summary>
    /// Returns TimeZone adjusted time for a given from a Utc or local time.
    /// Date is first converted to UTC then adjusted.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="timeZoneId"></param>
    /// <returns></returns>
    public static DateTime ToLocalTime(this DateTime time, string timeZoneId = "Eastern Standard Time")
    {
        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return time.ToLocalTime(tzi);
    }

    /// <summary>
    /// Returns TimeZone adjusted time for a given from a Utc or local time.
    /// Date is first converted to UTC then adjusted.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="timeZoneId"></param>
    /// <returns></returns>
    public static DateTime ToLocalTime(this DateTime time, TimeZoneInfo tzi)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(time, tzi);
    }
}
