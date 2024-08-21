namespace FactoryMethod.Zone;

public static class ZoneIdExtension
{
    private static readonly Dictionary<string, ZoneId> _zoneNameToIdMaps = new(){
        { "US/Eastern", ZoneId.US_Eastern },
        { "US/Central", ZoneId.US_Central },
        { "US/Mountain", ZoneId.US_Mountain },
        { "US/Pacific", ZoneId.US_Pacific }
    };
    public static string ToZoneName(this ZoneId zoneId)
    {
        return zoneId.ToString().Replace("_", "/");
    }

    public static ZoneId? ToZoneId(string zoneName)
    {
        if (_zoneNameToIdMaps.TryGetValue(zoneName, out var zoneId))
        {
            return zoneId;
        }

        return null;
    }
}
