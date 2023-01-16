/*
 * Purpose: Construct a practical, triangle solver program that provides its users with a menu of triangle options and performs whatever calculation
 * Input: menuChoice, adjacent, opposite, hypotenuse
 * Output: hypotenuse, theta, invalidTriangle, invalid menu choice
 * Written by: Reonel Duque
 * Written for: Allan Anderson
 * Section: A02
 * Last Modified Date: Sept. 22, 2022
 */
namespace CPSC1012_Lab2_ReonelDuque
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // declare variables
            double hypotenuse = 0,
                adjacent = 0,
                opposite = 0,
                theta = 0,
                angle = 0;
            char menuChoice;

            // get input from user
            Console.WriteLine("Solve a right triangle for the missing side and angle");
            Console.WriteLine("Select from one of the following options");
            Console.WriteLine("\t1. Given Adjacent and Opposite");
            Console.WriteLine("\t2. Given Adjacent and Hypotenuse");
            Console.WriteLine("\t3. Given Adjacent and Hypotenuse");
            Console.Write("Option: ");
            menuChoice = char.Parse(Console.ReadLine().Substring(0, 1));

            // getting opposite, adjacent, or hypotenuse based on user's choice
            // will state invalid triangle if hypotenuse isn't greater than opposite and adjacent
            switch (menuChoice)
            {
                case '1':
                    Console.Write("Side Adjacent = ");
                    adjacent = double.Parse(Console.ReadLine());
                    Console.Write("Side Opposite = ");
                    opposite = double.Parse(Console.ReadLine());
                    hypotenuse = Math.Sqrt((adjacent * adjacent) + (opposite * opposite));
                    theta = Math.Atan(opposite / adjacent);
                    angle = theta * (180 / Math.PI);
                    break;
                case '2':
                    Console.Write("Side Adjacent = ");
                    adjacent = double.Parse(Console.ReadLine());
                    Console.Write("Side Hypotenuse = ");
                    hypotenuse = double.Parse(Console.ReadLine());
                    if (hypotenuse > adjacent)
                    {
                        opposite = Math.Sqrt((hypotenuse * hypotenuse) - (adjacent * adjacent));
                        theta = Math.Acos(adjacent / hypotenuse);
                        angle = theta * (180 / Math.PI);
                    }
                    else
                    {
                        Console.WriteLine("Invalid triangle, Goodbye");
                    }
                    break;
                case '3':
                    Console.Write("Side Opposite = ");
                    opposite = double.Parse(Console.ReadLine());
                    Console.Write("Side Hypotenuse = ");
                    hypotenuse = double.Parse(Console.ReadLine());
                    if (hypotenuse > opposite)
                    {
                        adjacent = Math.Sqrt((hypotenuse * hypotenuse) - (opposite * opposite));
                        theta = Math.Asin(opposite / hypotenuse);
                        angle = theta * (180 / Math.PI);
                    }
                    else
                    {
                        Console.WriteLine("Invalid triangle, Goodbye");
                    }
                    break;
                default:
                    Console.Write("INVALID Selection, Goodbye ...");
                    break;
            }

            // display results if its a valid triangle
            if (hypotenuse > adjacent && hypotenuse > opposite)
            {
                switch (menuChoice)
                {
                    case '1':
                        Console.WriteLine($"Hypotenuse = {hypotenuse:f2}, Angle = {angle:f2}");
                        Console.Write("Goodbye");
                        break;
                    case '2':
                        Console.WriteLine($"Opposite = {opposite:f2}, Angle = {angle:f2}");
                        Console.Write("Goodbye");
                        break;
                    case '3':
                        Console.WriteLine($"Adjacent = {adjacent:f2}, Angle = {angle:f2}");
                        Console.Write("Goodbye");
                        break;
                    default:
                        break;
                }
            }
            //keeps console open
            Console.ReadLine();
        }
    }
}