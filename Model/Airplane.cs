using NPL_M_A013.Exception;

namespace NPL_M_A013.Model
{
    abstract class Airplane
    {
        private string model;
        public virtual string Id { get; set; }
        public string Model
        {
            get => model;
            set
            {
                if (value.Length > 40)
                    throw new NotMatchPropertyRequireException("Input model is too long.");
                else model = value;
            }
        }
        public double CruiseSpeed { get; set; }
        public double EmptyWeight { get; set; }
        public virtual double MaxTakeoffWeight { get; set; }
        public abstract void Fly();
    }
}
