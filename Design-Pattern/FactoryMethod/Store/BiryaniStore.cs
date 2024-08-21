using FactoryMethod.Biryani;

namespace FactoryMethod.Store;

public abstract class BiryaniStore
{
    public abstract IBiryani CreateBiryani(BiryaniType biryaniType);

    public IBiryani OrderBiryani(BiryaniType biryaniType)
    {
        IBiryani biryani = CreateBiryani(biryaniType);

        Console.WriteLine($"Making a {biryani.GetName()}");

        biryani.Prepare();
        biryani.Cook();
        biryani.Box();

        return biryani;
    }
}
