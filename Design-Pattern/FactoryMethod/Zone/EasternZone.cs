namespace FactoryMethod.Zone;

public sealed class EasternZone : Zone
{
    public EasternZone()
    {
        DisplayName = ZoneId.US_Eastern.ToZoneName();
        Offset = -5;
    }
}
