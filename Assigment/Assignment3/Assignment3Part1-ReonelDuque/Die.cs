//Purpose: Die class to imitate a die on the computer
//Input: N/A
//Output: N/A
//Written by: Reonel Duque
//Written for: Allan Anderson
//Section: A02
//Last Modified Date: November 13, 2022
namespace Assignment3Part1_ReonelDuque
{
    internal class Die
    {
        // private member fields
        private int _sides;
        private int _facevalue;

        // public Accessors & Mutators
        public int Sides
        {
            get { return _sides; }
            set
            {
                if (value >= 4)
                {
                    _sides = value;
                }
                else
                {
                    throw new Exception("Invalid sides for a Die.");
                }
            }
        }// end of Sides

        public int Facevalue
        {
            get { return _facevalue; }
            set
            {
                if (value > 0 && value <= Sides)
                {
                    _facevalue = value;
                }
                else
                {
                    throw new Exception("Invalid face value for a Die.");
                }
            }

        }// end of Facevalue

        // constructors
        public Die()
        {
            Sides = 6;
            Facevalue = 1;
        }

        public Die(int sides)
        {
            Sides = sides;
        }

        // class methods
        public void Roll()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            Facevalue = random.Next(1, Sides + 1);
        }// end of Roll

        public int AddDie(Die die2)
        {
            return Facevalue + die2.Facevalue;
        }// end of AddDie
    }
}