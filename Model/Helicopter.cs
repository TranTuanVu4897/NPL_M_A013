using NPL_M_A013.Exception;
using NPL_M_A013.Ulti;
using System;
using System.Text.RegularExpressions;

namespace NPL_M_A013.Model
{
    class Helicopter : Airplane
    {
        protected double maxTakeoffWeight;
        public override string Id
        {
            get => base.Id;
            set
            {
                if (!Regex.IsMatch(value, Validation.GetRegexId(RegexType.HelicopterId)))
                    throw new NotMatchPropertyRequireException("Id is not match Helicopter id's format.");

                base.Id = value;
            }
        }
        public override double MaxTakeoffWeight
        {
            get => maxTakeoffWeight;
            set
            {
                if (value > 1.5 * EmptyWeight)
                    throw new NotMatchPropertyRequireException("Max takeoff weight is over allowed");
                
                maxTakeoffWeight = value;
            }
        }
        public int Range { get; set; }

        public override void Fly()
        {
            Console.WriteLine("rotated wing");
        }
    }
}
