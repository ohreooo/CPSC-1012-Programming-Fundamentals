/*
 * Purpose: Create a system to reflect the costs of financially sponsoring a Unicorn at the
            Unicorn Rescue Society
 * Input: donorName, unicornName, donationType, monthAmount, sideWidth, backWidth, gateHeight, gateWidth, gateOption, paintOption, mealOption 
 * Output: donationTotal, donorName, unicornName, wallCost, gateCost, paintCost, mealCost, totalCost, unlimitedMessage
 * Written by: Reonel Duque
 * Written for: Allan Anderson
 * Section: A02
 * Last Date Modified: Sept. 23, 2022
 */

namespace Unicorn_Rescue_Society
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // declaring variables for user, unicorn and donation info
            string donorName,
                unlimitedMessage = "",
                unicornName;
            char donationType;
            double donationAmount = 0,
                    donationTotal = 0;
            int monthAmount;

            // declaring variables for pen accomodations
            double sideWidth = 0,
                   backWidth = 0,
                   gateWidth = 0,
                   gateHeight = 0,
                   wallCost = 0;

            // declaring variables for gate style
            char gateOption,
                changePaint,
                paintOption;
            string paintMessage = "";
            double gateCost = 0,
                paintCost = 0;

            // declaring variables for meal upgrade
            string mealMessage = "";
            int mealCost = 0;
            char mealOption,
                upgradeOption;

            //declaring variable for total cost
            double totalCost;

            // getting input for name and donation data
            Console.WriteLine("***** Welcome to the Unicorn Rescue Society Sponsor Estimator *****\n");

            Console.Write("What is the sponsor's name? ");
            donorName = Console.ReadLine();

            Console.Write("What would you like to call the unicorn? ");
            unicornName = Console.ReadLine();

            Console.WriteLine("\nAll sponsors must make a donation");
            Console.WriteLine("What type would you like to make?");
            Console.WriteLine("\tU - Unlimited");
            Console.WriteLine("\tM - Monthly");
            Console.WriteLine("\tO - One Time");
            Console.Write("Option: ");
            donationType = char.Parse(Console.ReadLine().ToUpper());

            // determining donation option and cost
            switch (donationType)
            {
                case 'U':
                    Console.Write("How much per month? ");
                    donationTotal = double.Parse(Console.ReadLine());
                    unlimitedMessage = $" plus a monthly donation of {donationTotal:c}";
                    break;
                case 'M':
                    Console.Write("How much per month? ");
                    donationAmount = double.Parse(Console.ReadLine());
                    Console.Write("How many month? ");
                    monthAmount = int.Parse(Console.ReadLine());
                    donationTotal = (donationAmount * monthAmount);
                    break;
                case 'O':
                    Console.Write("Enter the one time donation amount: ");
                    donationTotal = double.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            //displaying donation total
            Console.WriteLine($"\nThe total donation amount is {donationTotal:c}");

            //getting input for pen accomodations
            Console.WriteLine("\n***** Pen Accomodations *****");

            Console.Write("What is the width of the 2 walls (in feet)? ");
            sideWidth = double.Parse(Console.ReadLine());
            Console.Write("What is the width of the back wall (in feet)? ");
            backWidth = double.Parse(Console.ReadLine());
            Console.Write("What is the width of the gate? ");
            gateWidth = double.Parse(Console.ReadLine());
            Console.Write("What is the height of the gate (in feet) ");
            gateHeight = double.Parse(Console.ReadLine());

            // calculating cost for wall
            wallCost = 4 * ((2 * (12 * sideWidth)) + (12 * backWidth) + ((backWidth - gateWidth) * 12));

            // getting input for gate style
            Console.WriteLine("\n***** Gate Style *****");
            Console.WriteLine("Available gates (prices per square foot): ");
            Console.WriteLine("\tW - Wooden ($3)");
            Console.WriteLine("\tS - Silver ($8)");
            Console.WriteLine("\tG - Gold ($15)");
            Console.Write("Option: ");
            gateOption = char.Parse(Console.ReadLine().ToUpper());

            // determining gate option
            switch (gateOption)
            {
                case 'W':
                    gateCost = 3;
                    break;
                case 'S':
                    gateCost = 8;
                    break;
                case 'G':
                    gateCost = 12;
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            // calculating gate cost
            gateCost = gateCost * (gateHeight * gateWidth);

            // getting input for gate paint option
            Console.Write("\nWould you like to change the gate paint (Y/N)? ");
            changePaint = char.Parse(Console.ReadLine().ToUpper());

            // determining paint cost
            if (changePaint == 'Y')
            {
                Console.WriteLine("Available paints");
                Console.WriteLine("\tM - Mood: Changes colour based on mood ($200)");
                Console.WriteLine("\tA - Magic: Changes colour several times a day ($300)");
                Console.WriteLine("\tR - Reflective: Reflects like a mirror ($150)");
                Console.Write("Option: ");
                paintOption = char.Parse(Console.ReadLine().ToUpper());

                switch (paintOption)
                {
                    case 'M':
                        paintCost = 200;
                        paintMessage = "$200.00";
                        break;
                    case 'A':
                        paintCost = 300;
                        paintMessage = "$300.00";
                        break;
                    case 'R':
                        paintCost = 150;
                        paintMessage = "$150.00";
                        break;
                    default:
                        Console.WriteLine("Invalid Selection");
                        break;
                }
            }
            else
            {
                paintMessage = "Original";
                paintCost = 0;
            }
            
            // getting inputs for meal
            Console.WriteLine("\n***** Meal Upgrade *****");
            Console.Write("Would you like a meal upgrade (Y/N)? ");
            mealOption = char.Parse(Console.ReadLine().ToUpper());

            //determining cost
            if (mealOption == 'Y')
            {
                Console.WriteLine("Available meal upgrades:");
                Console.WriteLine("\tR - Add rainbow cookie treats ($1000)");
                Console.WriteLine("\tS - Special appetizers ($500)");
                Console.Write("Option: ");
                upgradeOption = char.Parse(Console.ReadLine().ToUpper());
                switch (upgradeOption)
                {
                    case 'R':
                        mealCost = 1000;
                        mealMessage = "$1000.00";
                        break;
                    case 'S':
                        mealCost = 500;
                        mealMessage = "$500.00";
                        break;
                    default:
                        Console.WriteLine("Invalid Selection");
                        break;
                }
            }
            else
            {
                mealMessage = "None";
                mealCost = 0;
            }

            //display results
            Console.WriteLine("\n***** Summary *****");
            Console.WriteLine($"Donor\t\t\t{donorName}");
            Console.WriteLine($"Unicorn Name\t\t{unicornName}");
            if (donationType == 'U')
            {
                totalCost = wallCost + gateCost + paintCost + mealCost;
            }
            else
            {
                totalCost = donationTotal + wallCost + gateCost + paintCost + mealCost;
                Console.WriteLine($"Donation Amount\t\t{donationTotal:c}");
            }
            Console.WriteLine($"Wall Cost\t\t{wallCost:c}");
            Console.WriteLine($"Gate Cost\t\t{gateCost:c}");
            Console.WriteLine($"Gate Paint Cost\t\t{paintMessage}");
            Console.WriteLine($"Meal Upgrade\t\t{mealMessage}");

            Console.WriteLine($"\n{donorName}, the total cost to sponsor {unicornName} is {totalCost:c}{unlimitedMessage}");

            //keeps console open
            Console.ReadLine();   
        }
    }
}