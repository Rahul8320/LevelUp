using Builder.Builders;
using Builder.Directors;
using Builder.Models;

Console.WriteLine("Builder Design Pattern!");
Console.WriteLine();

CarBuilder carBuilder = new();
Director.ConstructSportsCar(carBuilder);
Car car = carBuilder.GetProduct();
car.Details();

Console.WriteLine();

Director.ConstructEVCar(carBuilder);
Car evCar = carBuilder.GetProduct();
evCar.Details();

Console.WriteLine();

CarManualBuilder builder = new();
Director.ConstructSUVCar(builder);
Manual manual = builder.GetProduct();

manual.Details();