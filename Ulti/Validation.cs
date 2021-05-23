using NPL_M_A013.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NPL_M_A013.Ulti
{
    static class Validation
    {
        const string AIRPORT_ID_REGEX = @"^AP[a-zA-Z0-9]{5}$";
        const string FIXEDWING_ID_REGEX = @"^FW[a-zA-Z0-9]{5}$";
        const string HELICOPTER_REGEX = @"^RW[a-zA-Z0-9]{5}$";

        static internal int InputInRange(int min, int max)
        {
            while (true)
            {
                int num;
                if (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.Write("Your input must be a number." +
                        "\nPlease enter again: ");
                    continue;
                }
                if (num < min || num > max)
                    Console.Write($"Your input must be a number, between {min}, {max}." +
                        "\nPlease enter again: ");
                else
                    return num;
            }
        }

        static internal string InputStringMatchRegex(string regex)
        {
            while (true)
            {
                var str = Console.ReadLine();
                if (Regex.IsMatch(str, regex))
                    return str;

                Console.Write("Your input is invalid." +
                    "\nPlease enter again: ");
            }
        }

        static internal string GetRegexId(RegexType type)
        {
            string regex;
            switch (type)
            {
                case RegexType.AirportId:
                    regex = AIRPORT_ID_REGEX;
                    break;
                case RegexType.FixedwingId:
                    regex = FIXEDWING_ID_REGEX;
                    break;
                case RegexType.HelicopterId:
                    regex = HELICOPTER_REGEX;
                    break;
                default:
                    regex = string.Empty;
                    break;
            }
            return regex;

        }

        internal static void DisplayWaiting()
        {
            Console.WriteLine("Press enter to contitnue.");
            Console.ReadLine();
        }

        static internal bool CheckAirportExist(List<Airport> airports, string id, out Airport airport)
        {
            foreach (var item in airports)
            {
                if (item.Id == id)
                {
                    airport = item;
                    return true;
                }
            }
            airport = null;
            return false;
        }

        internal static bool CheckContinue()
        {
            switch (InputStringMatchRegex(@"^[ynYN]$"))
            {
                case "y":
                case "Y":
                    return true;
                case "n":
                case "N":
                    return false;
                default:
                    return false;
            }
        }

        internal static int InputPositiveInteger()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var num))
                {
                    if (num > 0)
                        return num;
                    else
                        Console.Write("Please enter positive number." +
                            "\nEnter again: ");
                }
                Console.Write("Please enter a number." +
                    "\nEnter again: ");
            }
        }

        internal static double InputPositiveDouble()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out var num))
                {
                    if (num > 0)
                        return num;
                    else
                        Console.Write("Please enter positive number." +
                            "\nEnter again: ");
                }
                Console.Write("Please enter a number." +
                    "\nEnter again: ");
            }
        }

    }
    public enum RegexType
    {
        AirportId,
        FixedwingId,
        HelicopterId
    }

}
