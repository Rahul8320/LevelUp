using FactoryMethod.Biryani;
using FactoryMethod.Store;
using FactoryMethod.Zone;

Console.WriteLine("Welcome to Biryani App! \n");

BiryaniStore kolkataStore = new KolkataStyleBiryaniStore();
IBiryani biryani = kolkataStore.OrderBiryani(BiryaniType.Chicken);
Console.WriteLine($"Rahul's order : {biryani.GetName()}. \n");

BiryaniStore hyderabadStore = new HyderabadStyleBiryaniStore();
biryani = hyderabadStore.OrderBiryani(BiryaniType.Mutton);
Console.WriteLine($"Rahul's order : {biryani.GetName()}. \n");

Console.WriteLine();


Zone zone = ZoneFactory.CreateZone("US/Central");
Console.WriteLine($"Zone: {zone.DisplayName}, Offset: {zone.Offset}");
