//Purpose:  Write a program that will allow the user to enter the time in minutes and the distance travelled in kilometers. These values will be validated using methods in your main program code to be > 0. Once basic validation has passed, these values will be passed to the class constructor. If there is an invalid value for a class property (minimum minutes = 10 and minimum kilometers = 2), throw an exception and exit the program. If the values are valid the main program code will call a class level method to calculate and return the fare
//Input: Minutes, Distance
//Output: TaxiFare
//Written by: Reonel Duque
//Written for: Allan Anderson
//Section: A02
//Last Modified Date: December 03, 2022
namespace CPSC1012_Lab6_ReonelDuque
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare variables
            TaxiFare fare;

            //main algorithm
            Console.WriteLine("Welcome to the Taxi Fare Calculator");
            try
            {
                fare = new TaxiFare(GetSafeInt("Enter the time, in whole minutes, the trip took (min. 10 minutes): "), GetSafeInt("Enter the distance, in whole kilometers, the trip was (min. 2 km): "));
                Console.WriteLine($"The fare for the passenger(s) is {fare.FareCalculate():c}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }//EOM

        static int GetSafeInt(string prompt)
        {
            //declare variables for GetSafeInt
            bool isValid = false;
            int number = 0;
            string choice = "";

            //do try loop for validation
            do
            {
                try
                {
                    Console.Write(prompt);
                    choice = Console.ReadLine();
                    number = int.Parse(choice);

                    if (number > 0)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"{number} is out of range!");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"{choice} is not a valid choice. Try again.");
                }
            } while (!isValid);

            return number;
        }//end of GetSafeInt
    }
}