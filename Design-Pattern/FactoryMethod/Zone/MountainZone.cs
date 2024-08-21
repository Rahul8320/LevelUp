namespace FactoryMethod.Zone;

public sealed class MountainZone : Zone
{
    public MountainZone()
    {
        DisplayName = ZoneId.US_Mountain.ToZoneName();
        Offset = -7;
    }
}
