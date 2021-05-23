using NPL_M_A013.Exception;
using NPL_M_A013.Ulti;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NPL_M_A013.Model
{
    class Airport
    {
        private string id;
        public string Id
        {
            get => id;
            set
            {
                if (!Regex.IsMatch(value, Validation.GetRegexId(RegexType.AirportId)))
                    throw new NotMatchPropertyRequireException("Id is not match Airport id's format.");
                else id = value;
            }
        }
        public string Name { get; set; }
        public double RunwaySize { get; set; }
        public int MaxFixedwingParkingPlace { get; set; }
        public List<string> FixedwingIds { get; set; }
        public int MaxRotatedwingParkingPlace { get; set; }
        public List<string> HelicopterIds { get; set; }

    }
}
