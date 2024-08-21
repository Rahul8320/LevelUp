namespace FactoryMethod.Zone;

public sealed class ZoneFactory
{
    public static Zone CreateZone(string zone)
    {
        var zoneId = ZoneIdExtension.ToZoneId(zone);

        return zoneId switch
        {
            ZoneId.US_Eastern => new EasternZone(),
            ZoneId.US_Central => new CentralZone(),
            ZoneId.US_Mountain => new MountainZone(),
            ZoneId.US_Pacific => new PacificZone(),
            _ => throw new Exception("Invalid Zone ID"),
        };
    }
}
