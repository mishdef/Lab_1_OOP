using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    internal class Car
    {
        string _markAndModel;
        Color _color;
        float _horsePower;
        decimal _weight;
        double _milage;
        double _fuelCapacity;
        double _currentFuel;
        DateTime _productionDate;
        double _fuelConsumptionPer100km; //new

        bool _isStarted = false;
        double _currentSpeed = 0;
        double _maxSpeed;

        public string MarkAndModel { get { return _markAndModel; } set { _markAndModel = value; } }
        public Color Color { get { return _color; } set { _color = value; } }
        public float HorsePower { get { return _horsePower; } set { _horsePower = value; } }
        public decimal Weight { get { return _weight; } set { _weight = value; } }
        public double Milage { get { return _milage; } set { _milage = value; } }
        public double FuelCapacity { get { return _fuelCapacity; } set { _fuelCapacity = value; } }
        public double CurrentFuel { get { return _currentFuel; } set { _currentFuel = value; } }
        public double CurrentSpeed { get { return _currentSpeed; } set { _currentSpeed = value; } }
        public DateTime ProductionDate { get { return _productionDate; } set { _productionDate = value; } }
        public double MaxSpeed { get { return _maxSpeed; } set { _maxSpeed = value; } }
        public double FuelConsumptionPer100km { get { return _fuelConsumptionPer100km; } set { _fuelConsumptionPer100km = value; } } //new

        public Car(string markAndModel, Color color, float horsePower, decimal weight, double milage, double fuelConsumptionPer100km, double fuelCapacity, DateTime productiDate)
        {
            _markAndModel = markAndModel;
            _color = color;
            _horsePower = horsePower;
            _weight = weight;
            _milage = milage;
            _fuelConsumptionPer100km = fuelConsumptionPer100km; //new
            _fuelCapacity = fuelCapacity;
            _currentFuel = fuelCapacity;

            _maxSpeed = _horsePower / (float)_weight * 1000;
            if (_maxSpeed < 50) _maxSpeed = 50;
            _productionDate = productiDate;

        }

        public string StartEnige()
        {
            if (_isStarted)
            {
                return "The car is already started.";
            }
            if (_currentFuel <= 0)
            {
                return "Cannot start engine. The fuel tank is empty.";
            }
            _isStarted = true;
            return "The car has started.";
        }

        public string StopEngine()
        {
            if (_isStarted)
            {
                _isStarted = false;
                _currentSpeed = 0;
                return "The car has stopped.";
            }
            else
            {
                return "The car is already stopped.";
            }
        }

        public string SpeedUp(double increment)
        {
            if (!_isStarted)
            {
                return "The car is not started. Please start the engine first.";
            }
            if (_currentFuel <= 0)
            {
                StopEngine();
                return "Out of fuel! The car stopped. Please refuel.";
            }
            if (increment < 0)
            {
                return "Increment must be a positive value.";
            }

            _currentSpeed += increment;
            if (_currentSpeed > _maxSpeed)
            {
                _currentSpeed = _maxSpeed;
                return $"The car has reached its maximum speed of {_maxSpeed:F2} km/h."; //new
            }
            return $"The car's current speed is {_currentSpeed:F2} km/h.";
        }

        public string SlowDown(double decrement)
        {
            if (!_isStarted)
            {
                return "The car is not started. Please start the engine first.";
            }
            if (decrement < 0)
            {
                return "Decrement must be a positive value.";
            }
            _currentSpeed -= decrement;
            if (_currentSpeed < 0)
            {
                _currentSpeed = 0;
                return "The car has come to a complete stop.";
            }
            return $"The car's current speed is {_currentSpeed:F2} km/h.";
        }

        public string Refuel(double amount)
        {
            if (amount <= 0)
            {
                return "Refuel amount must be positive.";
            }

            double fuelAdded = 0;
            if (_currentFuel + amount > _fuelCapacity)
            {
                fuelAdded = _fuelCapacity - _currentFuel;
                _currentFuel = _fuelCapacity;
                return $"Refueled {fuelAdded:F2} liters. The tank is now full: {_currentFuel:F2} / {_fuelCapacity} liters.";
            }
            else
            {
                _currentFuel += amount;
                fuelAdded = amount;
                return $"Refueled {fuelAdded:F2} liters. Current fuel: {_currentFuel:F2} / {_fuelCapacity} liters.";
            }
        }

        public string RideCar(double distanceDrivenKM)
        {
            if (!_isStarted)
            {
                return "Cannot ride. The car is not started. Please start the engine first.";
            }
            if (_currentSpeed <= 0)
            {
                return "Cannot ride. The car is not moving. Increase speed first.";
            }
            if (distanceDrivenKM <= 0)
            {
                return "Driving distance must be positive.";
            }
            if (_currentFuel <= 0)
            {
                StopEngine(); 
                return "Out of fuel! The car has stopped. Please refuel.";
            }


            double litersPerKM = _fuelConsumptionPer100km / 100.0; //new

            double fuelConsumed = litersPerKM * distanceDrivenKM; //new

            double timeInMinutes = (distanceDrivenKM / _currentSpeed) * 60.0; //new


            if (fuelConsumed > _currentFuel)
            {
                double actualDistancePossible = _currentFuel / litersPerKM;

                double actualTimePossible = (actualDistancePossible / _currentSpeed) * 60.0;

                _milage += actualDistancePossible;
                _currentFuel = 0;
                StopEngine();
                return $"Ran out of fuel after driving for {actualTimePossible:F2} minutes and {actualDistancePossible:F2} km. " +
                       $"The car stopped. Total milage: {_milage:F2} km. Please refuel.";
            }
            else
            {
                _currentFuel -= fuelConsumed;
                _milage += distanceDrivenKM;
                return $"Drove for {timeInMinutes:F2} minutes ({distanceDrivenKM:F2} km) at {_currentSpeed:F2} km/h. " +
                       $"Fuel consumed: {fuelConsumed:F2} liters. Remaining fuel: {_currentFuel:F2} / {_fuelCapacity} liters. " +
                       $"Total milage: {_milage:F2} km.";
            }
        }

        public override string ToString()
        {
            return $"Car: {_markAndModel}, Color: {_color}, HorsePower: {_horsePower}, Weight: {_weight}, Milage: {_milage:F2} km, " +
                   $"MaxSpeed: {_maxSpeed:F2} km/h, Fuel: {_currentFuel:F2}/{_fuelCapacity} liters.";
        }
    }
}