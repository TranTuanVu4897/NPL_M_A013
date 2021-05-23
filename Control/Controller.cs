using NPL_M_A013.Model;
using NPL_M_A013.Ulti;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPL_M_A013.Control
{
    class Controller
    {

        AirportControl airportControl;
        readonly HelicopterControl helicopterControl;
        readonly FixedwingControl fixedwingControl;
        readonly ReadJson readJson;
        readonly WriteJson writeJson;


        public Controller()
        {
            readJson = new ReadJson();
            writeJson = new WriteJson();
            airportControl = new AirportControl()
            {
                Airports = readJson.GetAirportDatas()
            };
            helicopterControl = new HelicopterControl()
            {
                Helicopters = readJson.GetHelicopterDatas()
            };
            fixedwingControl = new FixedwingControl()
            {
                Fixedwings = readJson.GetFixedwingDatas()
            };
        }

        public void MainFunction()
        {
            var loop = true;

            while (loop)
            {
                Console.WriteLine("-------------------------------------------------" +
                    "\nWelcome to plane management system." +
                    "\nPlease choose your action: " +
                    "\n1. Aiport mangement." +
                    "\n2. Fixedwing management." +
                    "\n3. Helicopter management." +
                    "\n4. Exit.");
                switch (Validation.InputInRange(1, 5))
                {
                    case 1:
                        AirportManagement();
                        break;
                    case 2:
                        FixedwingManagement();
                        break;
                    case 3:
                        HelicopterManagement();
                        break;
                    case 4:
                        loop = false;
                        break;
                }
            }
        }

        private void HelicopterManagement()
        {
            while (true)
            {
                Console.WriteLine("\nHelicopter maangement:" +
                    "\n1. Display helicopter plane." +
                    "\n2. Add Helicopter to an airport." +
                    "\n3. Remove Helicopter from an airport." +
                    "\n4. Back." +
                    "\nYour choice:");
                switch (Validation.InputInRange(1, 4))
                {
                    case 1:
                        helicopterControl.DisplayHelicopterPlane(airportControl);
                        Validation.DisplayWaiting();
                        break;

                    case 2:
                        var isDataChange = false;
                        helicopterControl.AddHelicopterToAnAirport(ref airportControl, ref isDataChange);
                        if (isDataChange)
                        {
                            writeJson.WriteAirportDatas(airportControl.Airports);
                            writeJson.WriteHelicopterDatas(helicopterControl.Helicopters);
                            helicopterControl.Helicopters = readJson.GetHelicopterDatas();
                            airportControl.Airports = readJson.GetAirportDatas(); 
                            Console.WriteLine("==========^*^==========" +
                                     "\nSuccess add new helicopter.");
                        }
                        else
                            Console.WriteLine("==========^*^==========" +
                                "\nFail to add new helicopter. Please try again.");

                        Validation.DisplayWaiting();
                        break;

                    case 3:
                        if (helicopterControl.Helicopters.Count > 0)
                        {
                            var isRemoveSuccess = false;
                            helicopterControl.RemoveHelicopterFromAnAirport(ref airportControl, ref isRemoveSuccess);
                            if (isRemoveSuccess)
                            {
                                writeJson.WriteAirportDatas(airportControl.Airports);
                                writeJson.WriteHelicopterDatas(helicopterControl.Helicopters);
                                helicopterControl.Helicopters = readJson.GetHelicopterDatas();
                                airportControl.Airports = readJson.GetAirportDatas();
                                Console.WriteLine("==========^*^==========" +
                                    "\nSuccess remove helicopter.");
                            }
                            else
                                Console.WriteLine("==========^*^==========" +
                                    "\nFail to remove helicopter. Please try again.");
                        }
                        else
                            Console.WriteLine("Helicopter data is empty. Please add new helicopter to do other action.");

                        Validation.DisplayWaiting();
                        break;

                    case 4:
                        return;
                }
            }

        }

        private void FixedwingManagement()
        {
            while (true)
            {
                Console.WriteLine("\nFixedwing maangement:" +
                    "\n1. Display fixedwing plane." +
                    "\n2. Add Fixedwing to an airport." +
                    "\n3. Remove Fixedwing from an airport." +
                    "\n4. Edit Fixedwing information." +
                    "\n5. Back." +
                    "\nYour choice:");
                switch (Validation.InputInRange(1, 5))
                {
                    case 1:
                        fixedwingControl.DisplayFixedwingPlane(airportControl);
                        Validation.DisplayWaiting();
                        break;

                    case 2:
                        var isDataChange = false;
                        fixedwingControl.AddFixedwingToAnAirport(ref airportControl, ref isDataChange);
                        if (isDataChange)
                        {
                            writeJson.WriteAirportDatas(airportControl.Airports);
                            writeJson.WriteFixedwingDatas(fixedwingControl.Fixedwings);
                            fixedwingControl.Fixedwings = readJson.GetFixedwingDatas();
                            airportControl.Airports = readJson.GetAirportDatas();
                            Console.WriteLine("==========^*^==========" +
                                    "\nSuccess add fixedwing.");
                        }
                        else
                            Console.WriteLine("==========^*^==========" +
                                "\nFail to add new fixedwing. Please try again.");

                        Validation.DisplayWaiting();
                        break;

                    case 3:
                        if (fixedwingControl.Fixedwings.Count > 0)
                        {
                            var isRemoveSuccess = false;
                            fixedwingControl.RemoveFixedwingFromAnAirport(ref airportControl, ref isRemoveSuccess);
                            if (isRemoveSuccess)
                            {
                                writeJson.WriteAirportDatas(airportControl.Airports);
                                writeJson.WriteFixedwingDatas(fixedwingControl.Fixedwings);
                                fixedwingControl.Fixedwings = readJson.GetFixedwingDatas();
                                airportControl.Airports = readJson.GetAirportDatas();
                                Console.WriteLine("==========^*^==========" +
                                    "\nSuccess remove fixedwing.");
                            }
                            else
                                Console.WriteLine("==========^*^==========" +
                                    "\nFail to remove fixedwing. Please try again.");
                        }
                        else
                            Console.WriteLine("Fixedwing data is empty. Please add new fixedwing to do other action.");

                        Validation.DisplayWaiting();
                        break;

                    case 4:
                        if (fixedwingControl.Fixedwings.Count > 0)
                        {
                            var isUpdateSuccess = false;
                            fixedwingControl.EditFixedwingInformation(ref isUpdateSuccess);
                            if (isUpdateSuccess)
                            {
                                writeJson.WriteFixedwingDatas(fixedwingControl.Fixedwings);
                                fixedwingControl.Fixedwings = readJson.GetFixedwingDatas();
                                Console.WriteLine("==========^*^==========" +
                                    "\nSuccess update fixedwing.");
                            }
                            else
                                Console.WriteLine("==========^*^==========" +
                                    "\nFail to update fixedwing information. Please try again.");
                        }
                        else
                            Console.WriteLine("Fixedwing data is empty. Please add new fixedwing to do other action.");

                        Validation.DisplayWaiting();
                        break;

                    case 5:
                        return;
                }
            }

        }

        private void AirportManagement()
        {
            while (true)
            {
                Console.Write("\nAirport Management:" +
                    "\n1. Display all airport." +
                    "\n2. Choose an airport." +
                    "\n3. Add new Airport." +
                    "\n4. Delete an airport." +
                    "\n5. Back." +
                    "\nYour choice: ");
                switch (Validation.InputInRange(1, 5))
                {
                    case 1:
                        airportControl.DisplayAllAirport();
                        break;

                    case 2:
                        if (airportControl.Airports.Count > 0)
                        {
                            var airport = airportControl.GetAirport();
                            if (airport != null)
                                Console.WriteLine($"Airport status: {airport.Id} - {airport.Name}" +
                                    $"\nTotal Fixedwing: {airport.FixedwingIds.Count}/{airport.MaxFixedwingParkingPlace}" +
                                    $"\nTotal Helicopter: {airport.HelicopterIds.Count}/{airport.MaxFixedwingParkingPlace}");
                        }
                        else
                            Console.WriteLine("Airport data is empty. Please add new airport to do other action.");
                        Validation.DisplayWaiting();
                        break;

                    case 3:
                        bool isAddSuccess = false;
                        airportControl.AddNewAirport(ref isAddSuccess);

                        if (isAddSuccess)
                        {
                            writeJson.WriteAirportDatas(airportControl.Airports);
                            airportControl.Airports = readJson.GetAirportDatas();
                            Console.WriteLine("==========^*^==========" +
                                "\nSuccess add airport to data.");
                        }
                        else
                            Console.WriteLine("==========^*^==========" +
                                "\nFail to add new airport. Please try again.");
                        Validation.DisplayWaiting();
                        break;

                    case 4:
                        if (airportControl.Airports.Count > 0)
                        {
                            bool isDeleteSuccess = false;
                            airportControl.DeleteAnAirport(ref isDeleteSuccess);

                            if (isDeleteSuccess)
                            {
                                writeJson.WriteAirportDatas(airportControl.Airports);
                                airportControl.Airports = readJson.GetAirportDatas();
                                Console.WriteLine("==========^*^==========" +
                                    "\nSuccess delete airport.");
                            }
                            else
                                Console.WriteLine("==========^*^==========" +
                                    "\nFail to delete airport. Please try again.");
                        }
                        else
                            Console.WriteLine("Airport data is empty. Please add new airport to do other action.");
                        Validation.DisplayWaiting();
                        break;

                    default:
                        return;
                }
            }
        }
    }
}
