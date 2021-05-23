using NPL_M_A013.Exception;
using NPL_M_A013.Model;
using NPL_M_A013.Ulti;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPL_M_A013.Control
{
    class FixedwingControl
    {
        public List<Fixedwing> Fixedwings { get; set; }

        internal void DisplayFixedwingPlane(AirportControl airportControl)
        {
            if (Fixedwings.Count > 0)
            {
                foreach (var item in Fixedwings)
                {
                    Airport airport = airportControl.GetAirportParkingByFixedwing(item.Id);
                    if (airport != null)
                        Console.WriteLine($"Fixedwing {item.Id} model {item.Model}. Parking at {airport.Id} - {airport.Name}");
                    else
                        Console.WriteLine($"Fixedwing {item.Id} model {item.Model}. Not parking at anywhere.");
                }
            }
            else
                Console.WriteLine("There is no Fixedwing airplane in here yet.");
        }

        internal void AddFixedwingToAnAirport(ref AirportControl airportControl, ref bool isDataChange)
        {
            Fixedwing fixedwing;
            Console.Write("\n1. New fixedwing plane." +
                "\n2. Existed fixedwing plane." +
                "\n3. Back." +
                "\nYour choice: ");
            switch (Validation.InputInRange(1, 3))
            {
                case 1:
                    fixedwing = GetFixedwingFromInput();
                    if (fixedwing != null)
                    {
                        Fixedwings.Add(fixedwing);
                        NewFixedwingToAirport(fixedwing, ref airportControl);
                        isDataChange = true;
                    }
                    break;
                case 2:
                    if (Fixedwings.Count > 0)
                    {
                        fixedwing = FindFixedwingById();
                        if (fixedwing != null)
                        {
                            var airportId = FindAirportParking(fixedwing.Id, airportControl);
                            if (!string.IsNullOrEmpty(airportId))
                            {
                                Console.Write($"Fixedwing plane {fixedwing.Id} is parking at airport {airportId}." +
                                    $"\nRemove it from there? (Y/N):");
                                if (Validation.CheckContinue())
                                {
                                    airportControl.RemoveFixedwingFromAirport(fixedwing.Id, airportId);
                                    NewFixedwingToAirport(fixedwing, ref airportControl);
                                    isDataChange = true;
                                }
                            }
                            else
                            {
                                airportControl.RemoveFixedwingFromAirport(fixedwing.Id, airportId);
                                NewFixedwingToAirport(fixedwing, ref airportControl);
                                isDataChange = true;
                            }
                        }
                    }
                    else
                        Console.WriteLine("There is no fixedwing in data yet.");
                    break;
                default:
                    isDataChange = false;
                    return;
            }


        }

        private void NewFixedwingToAirport(Fixedwing fixedwing, ref AirportControl airportControl)
        {
            Console.WriteLine($"Choose airport id to add fixedwing plane {fixedwing.Id}.");
            var airport = airportControl.GetAirport();
            if (airport != null)
                airportControl.AddNewFixedwingToAirport(fixedwing, ref airport);
        }

        private Fixedwing FindFixedwingById()
        {
            while (true)
            {
                Console.Write("Enter fixedwing id: ");
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.FixedwingId));
                foreach (var item in Fixedwings)
                {
                    if (item.Id == id)
                        return item;
                }
                Console.WriteLine("Fixedwing plane is not found. Do you want to continue? (Y/N): ");
                if (!Validation.CheckContinue())
                    return null;
            }
        }

        private Fixedwing GetFixedwingFromInput()
        {
            while (true)
            {
                Console.Write("Enter id: ");
                var id = GetFixedwingId();

                Console.Write("Enter model: ");
                var model = Console.ReadLine();

                Console.Write("Cruise speed: ");
                var cruiseSpeed = Validation.InputPositiveDouble();

                Console.Write("Min need runway size: ");
                var minNeedRunwaySize = Validation.InputPositiveDouble();

                Console.Write("Empty weight: ");
                var emptyWeight = Validation.InputPositiveDouble();

                Console.Write("Max takeoff weight: ");
                var maxTakeoffWeight = Validation.InputPositiveDouble();

                var planeType = ChoosePlaneType();

                try
                {
                    return new Fixedwing()
                    {
                        Id = id,
                        Model = model,
                        CruiseSpeed = cruiseSpeed,
                        MinNeedRunwaySize = minNeedRunwaySize,
                        EmptyWeight = emptyWeight,
                        MaxTakeoffWeight = maxTakeoffWeight,
                        PlaneType = planeType
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

        private string GetFixedwingId()
        {
            while (true)
            {
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.FixedwingId));
                if (CheckIsUniquePlaneId(id))
                    return id;
                Console.Write("This id is not unique." +
                    "\nEnter again: ");
            }
        }

        private bool CheckIsUniquePlaneId(string id)
        {
            foreach (var item in Fixedwings)
            {
                if (item.Id == id) return false;
            }
            return true;
        }

        private FixedwingType ChoosePlaneType(string currentType = "")
        {
            Console.WriteLine("Fixedwing plane type " +
                (string.IsNullOrEmpty(currentType) ? currentType : $"(current type: {currentType})") +
                ":");
            var totalEnum = Enum.GetNames(typeof(FixedwingType)).Length;
            for (int i = 0; i < totalEnum; i++)
            {
                Console.WriteLine($"{i}. {(FixedwingType)i}.");
            }

            return (FixedwingType)Validation.InputInRange(0, totalEnum - 1);
        }

        internal void RemoveFixedwingFromAnAirport(ref AirportControl airportControl, ref bool isRemoveSuccess)
        {
            while (true)
            {
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.FixedwingId));

                foreach (var item in Fixedwings)
                {
                    if (item.Id == id)
                    {
                        var airportId = FindAirportParking(id, airportControl);
                        if (!string.IsNullOrEmpty(airportId))
                        {
                            Console.Write($"You sure to remove fixedwing plane {id} from airport {airportId}? (Y/N):");
                            if (Validation.CheckContinue())
                            {
                                airportControl.RemoveFixedwingFromAirport(id, airportId);
                                isRemoveSuccess = true;
                                return;
                            }
                        }
                    }
                }

                Console.Write("Fixedwing plane's not found." +
                    "\nDo you want to countinue? (Y/N): ");
                if (!Validation.CheckContinue())
                {
                    isRemoveSuccess = false;
                    return;
                }
            }
        }


        private string FindAirportParking(string id, AirportControl airportControl)
        {
            foreach (var item in airportControl.Airports)
            {
                foreach (var fixedwingId in item.FixedwingIds)
                {
                    if (fixedwingId == id)
                        return item.Id;
                }
            }
            return string.Empty;
        }

        internal void EditFixedwingInformation(ref bool isUpdateSuccess)
        {
            var fixedwing = FindFixedwingById();
            if (fixedwing != null)
            {
                while (true)
                {
                    try
                    {
                        fixedwing.PlaneType = ChoosePlaneType(fixedwing.PlaneType.ToString());
                        Console.Write($"Update fixedwing min needed runway size (current: {fixedwing.MinNeedRunwaySize}): ");
                        fixedwing.MinNeedRunwaySize = Validation.InputPositiveDouble();
                        isUpdateSuccess = true;
                        return;
                    }
                    catch (NotMatchPropertyRequireException e)
                    {
                        Console.WriteLine(e.Message +
                            "\nDo you want to continue?");
                        if (!Validation.CheckContinue())
                        {
                            isUpdateSuccess = false;
                            return;
                        }
                    }
                }
            }
        }
    }
}
