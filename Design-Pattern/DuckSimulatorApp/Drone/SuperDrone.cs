namespace DuckSimulatorApp.Drone;

public sealed class SuperDrone : IDrone
{
    public void Beep()
    {
        Console.WriteLine("Super Drone is Beeping...");
    }

    public void Spin_Rotors()
    {
        Console.WriteLine("Super Drone Rotors are spinning...");
    }

    public void Take_Off()
    {
        Console.WriteLine("Super Drone is Taking Off...");
    }
}
