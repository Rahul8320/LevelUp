using WeatherStationApp.Demo;

Console.WriteLine("Welcome to the Weather Station App!");

SimpleSubject subject = new();
SimpleObserver observer = new(subject);

observer.Display();
subject.SetValue(26);
subject.SetValue(49);