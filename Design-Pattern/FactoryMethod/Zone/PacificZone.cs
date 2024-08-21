namespace FactoryMethod.Zone;

public sealed class PacificZone : Zone
{
    public PacificZone()
    {
        DisplayName = ZoneId.US_Pacific.ToZoneName();
        Offset = -8;
    }
}
