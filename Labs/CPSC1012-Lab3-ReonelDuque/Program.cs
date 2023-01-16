//Purpose: construct a practical program to calculate compound interest. 
//Input: startingBalance, interestRate, savingYears
//Output: startingBalance
//Written by: Reonel Duque
//Written for: Allan Anderson
//Section: A02
//Last Modified Date:October 6, 2022

namespace CPSC1012_Lab3_ReonelDuque
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare variables
            double startingBalance = 0,
                interestRate = 0,
                monthlyInterestRate,
                monthCount = 1;
            double savingYears = 0,
                count = 0;
            bool inputNumbers;
            
            //validation loop for input
            do
            {
                inputNumbers = false;
                //getting input for starting Balance, interestRate, and savingYears
                try
                {
                    Console.Write("Enter your starting balance: ");
                    startingBalance = double.Parse(Console.ReadLine());
                    Console.Write("Enter your interest rate(in decimal percent): ");
                    interestRate = double.Parse(Console.ReadLine());
                    Console.Write("How many years to save: ");
                    savingYears = int.Parse(Console.ReadLine());
                    //checks to see if all input is positive numbers
                    if (startingBalance >= 0 && interestRate >= 0 && savingYears >= 0)
                    {
                        inputNumbers = true;
                    }
                    else
                    {
                        Console.WriteLine("Please input positive numbers");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Please input a number");
                }
            } while (!inputNumbers);

            //calculating monthly interestRate
            monthlyInterestRate = (interestRate) / 12;

            //calculating for starting balance per year and displaying balance
            for(; count <= savingYears; count++)
            {
                Console.WriteLine($"Year: {count} {startingBalance:c}");
                //calculating balance per month
                for (; monthCount <= 12; monthCount++)
                {
                    startingBalance += (startingBalance * monthlyInterestRate);
                }
                //reset the month after every year
                monthCount = 1;  
            }
        }
    }
}
