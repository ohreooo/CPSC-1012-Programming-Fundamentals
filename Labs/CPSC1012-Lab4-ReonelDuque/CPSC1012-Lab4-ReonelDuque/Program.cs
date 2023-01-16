//Purpose: Create a program that will allow the user to enter the original value of an item and the number of years you wish to calculate the depreciated value
//Input: input, amount, years, sum(not user input, just input for a method)
//Output: a table of values
//Written by: Reonel Duque
//Written for: Allan Anderson
//Section: A02
//Last Modified Date: October 27, 2022
namespace CPSC1012_Lab4_ReonelDuque
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare variables for the main method
            int years = -1,
                sum;
            double amount = -1;
            char input;
            bool validInput,
                validYears,
                validAmount,
                quit = false;

            //do loop until user decides to quit
            do
            {
                //validation loop for userinput
                do
                {
                    DisplayMenu();
                    validInput = char.TryParse(Console.ReadLine().ToUpper(), out input);
                    if (!validInput)
                    {
                        Console.WriteLine("Please enter a single character");
                    }
                } while (!validInput);

                //switch 
                switch (input)
                {
                    //asks input for amount and years
                    case 'A':
                        Console.Write("Enter amount to be depreciated: ");
                        validAmount = double.TryParse(Console.ReadLine(), out amount);
                        while (!validAmount || amount < 0)
                        {
                            Console.WriteLine("Please enter valid amount");
                            Console.Write("Enter amount to be depreciated: ");
                            validAmount = double.TryParse(Console.ReadLine(), out amount);
                        }

                        Console.Write("Enter number of years for depreciation: ");
                        validYears = int.TryParse(Console.ReadLine(), out years);
                        while (!validYears || years < 0)
                        {
                            Console.WriteLine("Please enter valid years");
                            Console.Write("Enter number of years for depreciation: ");
                            validYears = int.TryParse(Console.ReadLine(), out years);
                        }
                        break;

                    //would use DisplayDepreciation() if there is a valid amount and year
                    case 'B':
                        if (!ValidInput(amount, years))
                        {
                            Console.WriteLine("You must enter an amount and number of years...");
                            break;
                        }
                        else
                        {
                            DisplayDepreciation(amount, years, sum = SumOfDigits(years));
                        }
                        break;

                    //quit option
                    case 'Q':
                        Console.WriteLine("Good-bye and thanks for using the program.");
                        quit = true;
                        break;
                    
                    default:
                        Console.WriteLine("Please enter a valid input");
                        break;
                }
            } while (!quit);
        }//end of main

        //adds the sum of each year
        static int SumOfDigits(int years)
        {
            int sum = 0,
                count = 1;
            for (; count <= years; count++)
            {
                sum += count;
            }
            return sum;
        }//end of SumOfDigits

        //calculates and displays the depreciation value per year
        static void DisplayDepreciation(double amount, int years, int sum)
        {
            int count = 0;
            double depreciation;

            Console.WriteLine("Year \tDepreciation");
            for (; count < years; count++)
            {
                depreciation = ((years - count) * amount) / sum;
                Console.WriteLine($"{count + 1}\t{depreciation:c}");
            }
        }//end of DisplayDepreciation

        //Displays the Menu
        static void DisplayMenu()
        {
            Console.WriteLine("This program computes depreciation tables using the sum-of-years-digits method.");
            Console.WriteLine("\tA. Enter a new amount and number of years.");
            Console.WriteLine("\tB. Display depreciation table.");
            Console.WriteLine("\tQ. Quit");
            Console.Write("Choice: ");
        }//end of DisplayMenu

        //checks if both amount and years is not less than 0
        static bool ValidInput(double amount, int years)
        {
            return (amount >= 0 && years >= 0);
        }//end of ValidInput
    }
}