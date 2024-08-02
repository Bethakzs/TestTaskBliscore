using System;
using System.Collections.Generic;

public class Solution
{
    public static int Process(double mass, List<Destination> stations)
    {
        double totalFuel = 0;

        foreach (var station in stations)
        {
            double gravity = GetGravity(station.TargetSpaceObject);
            double fuel = station.OperationType == OperationType.Launch ? CalculateLaunchFuel(mass, gravity) : CalculateLandingFuel(mass, gravity);
            totalFuel += fuel;
            mass += fuel;
        }

        return (int)Math.Floor(totalFuel);
    }

    private static double GetGravity(TargetSpaceObject target)
    {
        switch (target)
        {
            case TargetSpaceObject.Earth:
                return 9.807;
            case TargetSpaceObject.Moon:
                return 1.62;
            case TargetSpaceObject.Mars:
                return 3.711;
            default:
                throw new ArgumentException("Unavaible target");
        }
    }

    private static double CalculateLaunchFuel(double mass, double gravity)
    {
        double totalFuel = 0;
        while (true)
        {
            double fuel = Math.Floor(mass * gravity * 0.042 - 33);
            if (fuel <= 0) break;
            totalFuel += fuel;
            mass = fuel;
        }
        return totalFuel;
    }

    private static double CalculateLandingFuel(double mass, double gravity)
    {
        double totalFuel = 0;

        while (true)
        {
            double fuel = Math.Floor(mass * gravity * 0.033 - 42);

            if (fuel <= 0) break;
            totalFuel += fuel;
            mass = fuel;
        }
        return totalFuel;
    }

    public static void Main(string[] args)
    {
        var stations1 = new List<Destination>
        {
            new Destination { OperationType = OperationType.Launch, TargetSpaceObject = TargetSpaceObject.Earth },
            new Destination { OperationType = OperationType.Land, TargetSpaceObject = TargetSpaceObject.Moon },
            new Destination { OperationType = OperationType.Launch, TargetSpaceObject = TargetSpaceObject.Moon },
            new Destination { OperationType = OperationType.Land, TargetSpaceObject = TargetSpaceObject.Earth }
        };
        double mass1 = 28801;
        int fuel1 = Process(mass1, stations1);
        Console.WriteLine(fuel1);  // 51898

        var stations2 = new List<Destination>
        {
            new Destination { OperationType = OperationType.Launch, TargetSpaceObject = TargetSpaceObject.Earth },
            new Destination { OperationType = OperationType.Land, TargetSpaceObject = TargetSpaceObject.Mars },
            new Destination { OperationType = OperationType.Launch, TargetSpaceObject = TargetSpaceObject.Mars },
            new Destination { OperationType = OperationType.Land, TargetSpaceObject = TargetSpaceObject.Earth }
        };
        double mass2 = 14606;
        int fuel2 = Process(mass2, stations2);
        Console.WriteLine(fuel2);  // 33388

        var stations3 = new List<Destination>
        {
            new Destination { OperationType = OperationType.Launch, TargetSpaceObject = TargetSpaceObject.Earth },
            new Destination { OperationType = OperationType.Land, TargetSpaceObject = TargetSpaceObject.Moon },
            new Destination { OperationType = OperationType.Launch, TargetSpaceObject = TargetSpaceObject.Moon },
            new Destination { OperationType = OperationType.Land, TargetSpaceObject = TargetSpaceObject.Mars },
            new Destination { OperationType = OperationType.Launch, TargetSpaceObject = TargetSpaceObject.Mars },
            new Destination { OperationType = OperationType.Land, TargetSpaceObject = TargetSpaceObject.Earth }
        };
        double mass3 = 75432;
        int fuel3 = Process(mass3, stations3);
        Console.WriteLine(fuel3);  // 212161
    }
}

public enum TargetSpaceObject
{
    Earth,
    Moon,
    Mars
}

public enum OperationType
{
    Launch,
    Land
}

public class Destination
{
    public OperationType OperationType { get; set; }
    public TargetSpaceObject TargetSpaceObject { get; set; }
}
