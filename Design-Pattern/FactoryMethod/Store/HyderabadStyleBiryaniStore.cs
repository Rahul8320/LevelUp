using FactoryMethod.Biryani;
using FactoryMethod.Biryani.Hyderabad;

namespace FactoryMethod.Store;

public sealed class HyderabadStyleBiryaniStore : BiryaniStore
{
    public override IBiryani CreateBiryani(BiryaniType biryaniType)
    {
        return biryaniType switch
        {
            BiryaniType.Mutton => new HyderabadStyleMuttonBiryani(),
            BiryaniType.Chicken => new HyderabadStyleChickenBiryani(),
            BiryaniType.Egg => new HyderabadStyleEggBiryani(),
            _ => new HyderabadStyleAluBiryani(),
        };
    }
}
