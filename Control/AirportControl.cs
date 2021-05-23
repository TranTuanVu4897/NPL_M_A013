using NPL_M_A013.Exception;
using NPL_M_A013.Model;
using NPL_M_A013.Ulti;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPL_M_A013.Control
{
    class AirportControl
    {
        public List<Airport> Airports { get; set; }

        internal Airport GetAirportParkingByHelicopter(string id)
        {
            foreach (var item in Airports)
            {
                foreach (var plane in item.HelicopterIds)
                {
                    if (plane == id) return item;
                }
            }
            return null;
        }

        internal Airport GetAirportParkingByFixedwing(string id)
        {
            foreach (var item in Airports)
            {
                foreach (var plane in item.FixedwingIds)
                {
                    if (plane == id) return item;
                }
            }
            return null;
        }

        internal void DisplayAllAirport()
        {
            Console.WriteLine("\n***Airport:");
            if (Airports.Count > 0)
            {
                Airports.Sort((a1, a2) => a1.Id.CompareTo(a2.Id));
                for (int i = 0; i < Airports.Count; i++)
                {
                    Console.WriteLine($"{Airports[i].Id}: {Airports[i].Name}.");
                }
            }
            else
                Console.WriteLine("There is no plane is this system yet.");
            Validation.DisplayWaiting();
        }

        internal Airport GetAirport()
        {
            Airport airport = null;
            while (airport == null)
            {
                Console.Write("Enter aiport id: ");
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.AirportId));
                airport = GetAirportById(id);
                if (airport != null)
                    return airport;

                Console.WriteLine($"Airport {id} not found. Continue? (Y/N):");
                if (!Validation.CheckContinue()) break;
            }
            return airport;
        }

        internal void RemoveFixedwingFromAirport(string fixedwingId, string airportId)
        {
            Airport airport = null;
            foreach (var item in Airports)
            {
                if (item.Id == airportId)
                {
                    airport = item;
                    break;
                }
            }
            if (airport != null)
                airport.FixedwingIds.Remove(fixedwingId);
        }

        private Airport GetAirportById(string id)
        {
            foreach (var item in Airports)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        internal void RemoveHelicopterFromAirport(string helicopterId, string airportId)
        {
            Airport airport = null;
            foreach (var item in Airports)
            {
                if (item.Id == airportId)
                {
                    airport = item;
                    break;
                }
            }
            if (airport != null)
                airport.HelicopterIds.Remove(helicopterId);
        }

        internal void AddNewAirport(ref bool isAddSuccess)
        {
            while (true)
            {
                Console.Write("Enter airport id: ");
                var id = GetAirportId();

                var name = GetAirportName();
                var runwaySize = GetRunwaySize();

                Console.Write("Enter Max fixedwing plane parking place: ");
                var maxFixedwing = Validation.InputPositiveInteger();

                Console.Write("Enter Max helicopter plane parking place: ");
                var maxHelicopter = Validation.InputPositiveInteger();

                var listFixedwing = new List<string>();
                var listHelicopter = new List<string>();

                Airport airport;

                try
                {
                    airport = new Airport
                    {
                        Id = id,
                        Name = name,
                        RunwaySize = runwaySize,
                        MaxFixedwingParkingPlace = maxFixedwing,
                        MaxRotatedwingParkingPlace = maxHelicopter,
                        FixedwingIds = listFixedwing,
                        HelicopterIds = listHelicopter
                    };

                    Airports.Add(airport);
                    isAddSuccess = true;
                    break;
                }
                catch (NotMatchPropertyRequireException e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Do you want to re input? (Y/N): ");
                    if (!Validation.CheckContinue())
                    {
                        isAddSuccess = false;
                        break;
                    }
                }
            }
        }

        private string GetAirportId()
        {
            while (true)
            {
                var id = Validation.InputStringMatchRegex(Validation.GetRegexId(RegexType.AirportId));
                if (CheckIsUnique(id))
                    return id;
                Console.Write("This id is not unique." +
                    "\nEnter again: ");
            }
        }

        private bool CheckIsUnique(string id)
        {
            foreach (var item in Airports)
            {
                if (item.Id == id) return false;
            }
            return true;
        }

        internal void AddNewHelicopterToAirport(string helicopterId, ref Airport airport)
        {
            if (airport.HelicopterIds.Count + 1 <= airport.MaxRotatedwingParkingPlace)
                airport.HelicopterIds.Add(helicopterId);
            else
                Console.WriteLine($"Fail to add {helicopterId} to airport {airport.Name}." +
                    $"\nAirport {airport.Name} - {airport.Id} is not enough space for one more helicopter plane.");
        }

        internal void AddNewFixedwingToAirport(Fixedwing fixedwing, ref Airport airport)
        {
            if (airport.FixedwingIds.Count + 1 > airport.MaxFixedwingParkingPlace)
                Console.WriteLine($"Fail to add {fixedwing.Id} to airport {airport.Name}." + 
                    $"\nAirport {airport.Name} - {airport.Id} is not enough space for one more fixedwing plane.");
            else if (airport.RunwaySize < fixedwing.MinNeedRunwaySize)
                Console.WriteLine($"Fail to add {fixedwing.Id} to airport {airport.Name}." +
                    $"\nAirport runway size is not enought." +
                    $"\nAirport runway size: {airport.RunwaySize}" +
                    $"\nFixedwing min needed size: {fixedwing.MinNeedRunwaySize}");
            else
                airport.FixedwingIds.Add(fixedwing.Id);
        }

        private double GetRunwaySize()
        {
            double[] size = { 3.255, 4.0, 4.198, 5.231 };
            Console.WriteLine("Please choose runway size: ");
            for (int i = 0; i < size.Length; i++)
            {
                Console.WriteLine($"{i}. {size[i]}.");
            }
            return size[Validation.InputInRange(0, size.Length - 1)];
        }

        private string GetAirportName()
        {
            string[] name = { "Guangzhou Baiyun International Airport", "Hartsfield–Jackson Atlanta International Airport",
                "Tokyo Haneda Airport", "Los Angeles International Airport" };
            Console.WriteLine("Please choose airport name: ");
            for (int i = 0; i < name.Length; i++)
            {
                Console.WriteLine($"{i}. {name[i]}.");
            }
            return name[Validation.InputInRange(0, name.Length - 1)];
        }

        internal void DeleteAnAirport(ref bool isDeleteSuccess)
        {
            Console.WriteLine("Please enter Airport you want to remove: ");
            Airport airport = GetAirport();
            if (airport != null)
            {
                Airports.Remove(airport);
                isDeleteSuccess = true;
            }
            else
                isDeleteSuccess = false;
        }
    }
}
