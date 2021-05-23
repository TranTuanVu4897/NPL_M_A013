using NPL_M_A013.Exception;
using NPL_M_A013.Model;
using NPL_M_A013.Ulti;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPL_M_A013.Control
{
    class HelicopterControl
    {
        public List<Helicopter> Helicopters { get; set; }

        internal void DisplayHelicopterPlane(AirportControl airportControl)
        {
            if (Helicopters.Count > 0)
            {
                foreach (var item in Helicopters)
                {
                    Airport airport = airportControl.GetAirportParkingByHelicopter(item.Id);
                    if (airport != null)
                        Console.WriteLine($"Helicopter {item.Id} model {item.Model}. Parking at {airport.Id} - {airport.Name}");
                    else
                        Console.WriteLine($"Helicopter {item.Id} model {item.Model}. Not parking at anywhere.");
                }
            }
            else
                Console.WriteLine("There is no Helicopter airplane in here yet.");
        }

        internal void AddHelicopterToAnAirport(ref AirportControl airportControl, ref bool isDataChange)
        {
            Helicopter helicopter;
            Console.Write("\n1. New helicopter plane." +
                "\n2. Existed helicopter plane." +
                "\n3. Back." +
                "\nYour choice: ");
            switch (Validation.InputInRange(1, 3))
            {
                case 1:
                    helicopter = GetHelicopterFromInput();
                    if (helicopter != null)
                    {
                        Helicopters.Add(helicopter);
                        NewHelicopterToAirport(helicopter, ref airportControl);
                        isDataChange = true;
                    }
                    break;
                case 2:
                    helicopter = FindHelicopterById();
                    if (helicopter != null)
                    {
                        var airportId = FindAirportParking(helicopter.Id, airportControl);
                        if (!string.IsNullOrEmpty(airportId))
                        {
                            Console.Write($"Helicopter plane {helicopter.Id} is parking at airport {airportId}." +
                                $"\nRemove it from there? (Y/N):");
                            if (Validation.CheckContinue())
                            {
                                airportControl.RemoveHelicopterFromAirport(helicopter.Id, airportId);
                                NewHelicopterToAirport(helicopter, ref airportControl);
                                isDataChange = true;
                            }
                        }
                    }
                    break;
                default:
                    isDataChange = false;
                    return;
            }

        }

        private string FindAirportParking(string id, AirportControl airportControl)
        {
            foreach (var item in airportControl.Airports)
            {
                foreach (var helicopterId in item.HelicopterIds)
                {
                    if (helicopterId == id)
                        return item.Id;
                }
            }
            return string.Empty;
        }

        private Helicopter FindHelicopterById()
        {
            while (true)
            {
                Console.Write("Enter helicopter id: ");
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.HelicopterId));
                foreach (var item in Helicopters)
                {
                    if (item.Id == id)
                        return item;
                }
                Console.WriteLine("Helicopter plane is not found. Do you want to continue? (Y/N): ");
                if (!Validation.CheckContinue())
                    return null;
            }
        }

        private void NewHelicopterToAirport(Helicopter helicopter, ref AirportControl airportControl)
        {
            Console.WriteLine($"Choose airport id to add helicopter plane {helicopter.Id}.");
            var airport = airportControl.GetAirport();
            if (airport != null)
                airportControl.AddNewHelicopterToAirport(helicopter.Id, ref airport);
        }

        private Helicopter GetHelicopterFromInput()
        {
            while (true)
            {
                Console.Write("Enter id: ");
                var id = GetHelicopterId();

                Console.Write("Enter model: ");
                var model = Console.ReadLine();

                Console.Write("Cruise speed: ");
                var cruiseSpeed = Validation.InputPositiveDouble();

                Console.Write("Range: ");
                var range = Validation.InputPositiveInteger();

                Console.Write("Empty weight: ");
                var emptyWeight = Validation.InputPositiveDouble();

                Console.Write("Max takeoff weight: ");
                var maxTakeoffWeight = Validation.InputPositiveDouble();

                try
                {
                    return new Helicopter()
                    {
                        Id = id,
                        Model = model,
                        CruiseSpeed = cruiseSpeed,
                        Range = range,
                        EmptyWeight = emptyWeight,
                        MaxTakeoffWeight = maxTakeoffWeight
                    };
                }
                catch (NotMatchPropertyRequireException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Do you want to re input? (Y/N)");
                    if (!Validation.CheckContinue())
                        return null;
                }
            }
        }

        private string GetHelicopterId()
        {
            while (true)
            {
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.HelicopterId));
                if (CheckIsUniquePlaneId(id))
                    return id;
                Console.Write("This id is not unique." +
                    "\nEnter again: ");
            }
        }

        private bool CheckIsUniquePlaneId(string id)
        {
            foreach (var item in Helicopters)
            {
                if (item.Id == id) return false;
            }
            return true;
        }

        internal void RemoveHelicopterFromAnAirport(ref AirportControl airportControl, ref bool isRemoveSuccess)
        {
            while (true)
            {
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.HelicopterId));

                foreach (var item in Helicopters)
                {
                    if (item.Id == id)
                    {
                        var airportId = FindAirportParking(id, airportControl);
                        if (!string.IsNullOrEmpty(airportId))
                        {
                            Console.Write($"You sure to remove helicopter plane {id} from airport {airportId}? (Y/N):");
                            if (Validation.CheckContinue())
                            {
                                airportControl.RemoveHelicopterFromAirport(id, airportId);
                                isRemoveSuccess = true;
                                return;
                            }
                        }
                    }
                }

                Console.Write("Helicopter plane's not found." +
                    "\nDo you want to countinue? (Y/N): ");
                if (!Validation.CheckContinue())
                {
                    isRemoveSuccess = false;
                    return;
                }
            }
        }
    }
}
