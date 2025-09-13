using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    internal class Car
    {
        public string markAndModel;
        public Color color;
        public float horsePower;
        public decimal weight;
        public double milage;
        public double fuelCapacity;
        public double currentFuel;
        public DateTime productionDate;
        public double fuelConsumptionPer100km; //new

        bool isStarted = false;
        public double currentSpeed = 0;
        public double maxSpeed;


        public string StartEnige()
        {
            if (isStarted)
            {
                return "The car is already started.";
            }
            if (currentFuel <= 0)
            {
                return "Cannot start engine. The fuel tank is empty.";
            }
            isStarted = true;
            return "The car has started.";
        }

        public string StopEngine()
        {
            if (isStarted)
            {
                isStarted = false;
                currentSpeed = 0;
                return "The car has stopped.";
            }
            else
            {
                return "The car is already stopped.";
            }
        }

        public string SpeedUp(double increment)
        {
            if (!isStarted)
            {
                return "The car is not started. Please start the engine first.";
            }
            if (currentFuel <= 0)
            {
                StopEngine();
                return "Out of fuel! The car stopped. Please refuel.";
            }
            if (increment < 0)
            {
                return "Increment must be a positive value.";
            }

            currentSpeed += increment;
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
                return $"The car has reached its maximum speed of {maxSpeed:F2} km/h."; //new
            }
            return $"The car's current speed is {currentSpeed:F2} km/h.";
        }

        public string SlowDown(double decrement)
        {
            if (!isStarted)
            {
                return "The car is not started. Please start the engine first.";
            }
            if (decrement < 0)
            {
                return "Decrement must be a positive value.";
            }
            currentSpeed -= decrement;
            if (currentSpeed < 0)
            {
                currentSpeed = 0;
                return "The car has come to a complete stop.";
            }
            return $"The car's current speed is {currentSpeed:F2} km/h.";
        }

        public string Refuel(double amount)
        {
            if (amount <= 0)
            {
                return "Refuel amount must be positive.";
            }

            double fuelAdded = 0;
            if (currentFuel + amount > fuelCapacity)
            {
                fuelAdded = fuelCapacity - currentFuel;
                currentFuel = fuelCapacity;
                return $"Refueled {fuelAdded:F2} liters. The tank is now full: {currentFuel:F2} / {fuelCapacity} liters.";
            }
            else
            {
                currentFuel += amount;
                fuelAdded = amount;
                return $"Refueled {fuelAdded:F2} liters. Current fuel: {currentFuel:F2} / {fuelCapacity} liters.";
            }
        }

        public string RideCar(double distanceDrivenKM)
        {
            if (!isStarted)
            {
                return "Cannot ride. The car is not started. Please start the engine first.";
            }
            if (currentSpeed <= 0)
            {
                return "Cannot ride. The car is not moving. Increase speed first.";
            }
            if (distanceDrivenKM <= 0)
            {
                return "Driving distance must be positive.";
            }
            if (currentFuel <= 0)
            {
                StopEngine(); 
                return "Out of fuel! The car has stopped. Please refuel.";
            }


            double litersPerKM = fuelConsumptionPer100km / 100.0; //new

            double fuelConsumed = litersPerKM * distanceDrivenKM; //new

            double timeInMinutes = (distanceDrivenKM / currentSpeed) * 60.0; //new


            if (fuelConsumed > currentFuel)
            {
                double actualDistancePossible = currentFuel / litersPerKM;

                double actualTimePossible = (actualDistancePossible / currentSpeed) * 60.0;

                milage += actualDistancePossible;
                currentFuel = 0;
                StopEngine();
                return $"Ran out of fuel after driving for {actualTimePossible:F2} minutes and {actualDistancePossible:F2} km. " +
                       $"The car stopped. Total milage: {milage:F2} km. Please refuel.";
            }
            else
            {
                currentFuel -= fuelConsumed;
                milage += distanceDrivenKM;
                return $"Drove for {timeInMinutes:F2} minutes ({distanceDrivenKM:F2} km) at {currentSpeed:F2} km/h. " +
                       $"Fuel consumed: {fuelConsumed:F2} liters. Remaining fuel: {currentFuel:F2} / {fuelCapacity} liters. " +
                       $"Total milage: {milage:F2} km.";
            }
        }

        public override string ToString()
        {
            return $"Car: {markAndModel}, Color: {color}, HorsePower: {horsePower}, Weight: {weight}, Milage: {milage:F2} km, CurrentSpeed: {currentSpeed:F2} km/h ," +
                   $"MaxSpeed: {maxSpeed:F2} km/h, Fuel: {currentFuel:F2}/{fuelCapacity} liters. Fuel per 100km: {fuelConsumptionPer100km:F2} liters.";
        }
    }
}