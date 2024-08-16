using DuckSimulatorApp.Duck;

namespace DuckSimulatorApp.Drone;

public class DroneAdapter(IDrone drone) : IDuck
{
    public void Fly()
    {
        drone.Spin_Rotors();
        drone.Take_Off();
    }

    public void Quack()
    {
        drone.Beep();
    }
}
