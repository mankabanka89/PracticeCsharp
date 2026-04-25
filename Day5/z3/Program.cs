using System;
using System.Collections.Generic;

public abstract class Vehicle
{
    public string Model { get; set; }
    public int Year { get; set; }

    protected Vehicle(string model, int year)
    {
        Model = model;
        Year = year;
    }

    public abstract void DisplayInfo();
}

public interface IElectric
{
    int BatteryCapacity { get; set; }
    void ChargeBattery();
    int GetRange();
}

public interface IDiesel
{
    double EngineVolume { get; set; }
    double FuelConsumption { get; set; }
    void Refuel();
    double CalculateRange();
}

public class ElectricCar : Vehicle, IElectric
{
    public int BatteryCapacity { get; set; }
    public int EfficiencyPerKw { get; set; }

    public ElectricCar(string model, int year, int batteryCapacity, int efficiencyPerKw)
        : base(model, year)
    {
        BatteryCapacity = batteryCapacity;
        EfficiencyPerKw = efficiencyPerKw;
    }

    public void ChargeBattery()
    {
        Console.WriteLine($"Электромобиль {Model} заряжается. Ёмкость батареи: {BatteryCapacity} кВт·ч");
    }

    public int GetRange()
    {
        return BatteryCapacity * EfficiencyPerKw;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Электромобиль: {Model}, {Year} г.в., запас хода: {GetRange()} км");
    }
}

public class Truck : Vehicle, IDiesel
{
    public double EngineVolume { get; set; }
    public double FuelConsumption { get; set; }
    public double FuelTankCapacity { get; set; }

    public Truck(string model, int year, double engineVolume, double fuelConsumption, double fuelTankCapacity)
        : base(model, year)
    {
        EngineVolume = engineVolume;
        FuelConsumption = fuelConsumption;
        FuelTankCapacity = fuelTankCapacity;
    }

    public void Refuel()
    {
        Console.WriteLine($"Грузовик {Model} заправляется дизельным топливом. Объём бака: {FuelTankCapacity} л");
    }

    public double CalculateRange()
    {
        return FuelTankCapacity / FuelConsumption * 100;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Грузовик: {Model}, {Year} г.в., объём двигателя: {EngineVolume} л, запас хода: {CalculateRange():F1} км");
    }
}

public static class Program
{
    public static void Main()
    {
        Vehicle[] vehicles = new Vehicle[]
        {
            new ElectricCar("Tesla Model 3", 2023, 75, 6),
            new Truck("Kamaz 5490", 2021, 12.0, 25.5, 500),
            new ElectricCar("Nissan Leaf", 2022, 62, 5.5),
            new Truck("Volvo FH", 2023, 13.0, 28.0, 600),
            new ElectricCar("BMW i4", 2024, 80, 6.2),
            new Truck("Scania S500", 2022, 12.8, 26.5, 550)
        };

        Console.WriteLine("Все транспортные средства:");
        Console.WriteLine("==========================");
        foreach (Vehicle vehicle in vehicles)
        {
            vehicle.DisplayInfo();
        }

        Console.WriteLine("\nПоиск дизельных транспортных средств:");
        Console.WriteLine("=====================================");

        List<Vehicle> dieselVehicles = new List<Vehicle>();

        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle is IDiesel)
            {
                dieselVehicles.Add(vehicle);
            }
        }

        if (dieselVehicles.Count == 0)
        {
            Console.WriteLine("Дизельные транспортные средства не найдены.");
        }
        else
        {
            foreach (Vehicle vehicle in dieselVehicles)
            {
                vehicle.DisplayInfo();
            }
        }

        Console.WriteLine($"\nВсего найдено дизельных машин: {dieselVehicles.Count}");
    }
}