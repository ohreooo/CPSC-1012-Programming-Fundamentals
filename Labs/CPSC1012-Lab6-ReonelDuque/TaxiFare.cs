

namespace CPSC1012_Lab6_ReonelDuque
{
    internal class TaxiFare
    {
        //private member fields
        private int _minutes;
        private int _distance;

        //public member fields
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (value >= 10)
                {
                    _minutes = value;
                }
                else
                {
                    throw new Exception("Invalid trip minutes");
                }
            }
        }

        public int Distance
        {
            get { return _distance; }
            set
            {
                if (value >= 2)
                {
                    _distance = value;
                }
                else
                {
                    throw new Exception("Invalid trip kilometers");
                }
            }
        }

        //constructor
        public TaxiFare(int minutes, int distance)
        {
            Minutes = minutes;
            Distance = distance;
        }

        //class method
        public double FareCalculate()
        {
            return ((Minutes * 0.25) + (Distance * 1.25)) * 1.10;
        }
    }
}
