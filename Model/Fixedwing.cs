using NPL_M_A013.Exception;
using NPL_M_A013.Ulti;
using System;
using System.Text.RegularExpressions;

namespace NPL_M_A013.Model
{
    class Fixedwing : Airplane
    {
        public FixedwingType PlaneType { get; set; }
        public override string Id
        {
            get => base.Id;
            set
            {
                if (!Regex.IsMatch(value, Validation.GetRegexId(RegexType.FixedwingId)))
                    throw new NotMatchPropertyRequireException("Id is not match Fixedwing id's format.");

                base.Id = value;
            }
        }
        public double MinNeedRunwaySize { get; set; }

        public override void Fly()
        {
            Console.WriteLine("fixed wing");
        }
    }
    public enum FixedwingType
    {
        CAG,
        LGR,
        PRV
    }
}
