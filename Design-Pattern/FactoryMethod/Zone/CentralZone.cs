namespace FactoryMethod.Zone;

public sealed class CentralZone : Zone
{
    public CentralZone()
    {
        DisplayName = ZoneId.US_Central.ToZoneName();
        Offset = -6;
    }
}
