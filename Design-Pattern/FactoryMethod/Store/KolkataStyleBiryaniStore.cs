using FactoryMethod.Biryani;
using FactoryMethod.Biryani.Kolkata;

namespace FactoryMethod.Store;

public sealed class KolkataStyleBiryaniStore : BiryaniStore
{
    public override IBiryani CreateBiryani(BiryaniType biryaniType)
    {
        return biryaniType switch
        {
            BiryaniType.Mutton => new KolkataStyleMuttonBiryani(),
            BiryaniType.Chicken => new KolkataStyleChickenBiryani(),
            BiryaniType.Egg => new KolkataStyleEggBiryani(),
            _ => new KolkataStyleAluBiryani(),
        };
    }
}