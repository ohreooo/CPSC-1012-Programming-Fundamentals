/*
 * Purpose: Compute the wind-chill factor for winds of 10, 25, and 40 miles per hour from the temperature the user inputs, and list the wind-chill factors.
 * Input: The temperature in fahrenheit
 * Output: The Wind-Chill Factor with two decimal places
 * Written by: Reonel Duque
 * Written for: Allan Anderson
 * Section: A02
 * Last Modified Date: Sept. 15, 2022
 */

namespace Chilly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // declare variables
            double temperature,
                windChill10,
                windChill25,
                windChill40;
            
            // input the temperature
            Console.Write("Enter the temperature (in degrees Fahrenheit): ");
            temperature = double.Parse(Console.ReadLine());

            // calculate the wind-chill factor
            windChill10 = 35.74 + (0.6215 * temperature) - (35.75 * (Math.Pow(10, 0.16))) + (0.4275 * temperature * (Math.Pow(10, 0.16)));
            windChill25 = 35.74 + (0.6215 * temperature) - (35.75 * (Math.Pow(25, 0.16))) + (0.4275 * temperature * (Math.Pow(25, 0.16)));
            windChill40 = 35.74 + (0.6215 * temperature) - (35.75 * (Math.Pow(40, 0.16))) + (0.4275 * temperature * (Math.Pow(40, 0.16)));


            // display the results
            Console.WriteLine($"Wind: 10, Wind-Chill factor: {windChill10:f2}");
            Console.WriteLine($"Wind: 25, Wind-Chill factor: {windChill25:f2}");
            Console.WriteLine($"Wind: 40, Wind-Chill factor: {windChill40:f2}");

            // keep the console open
            Console.ReadLine();
        }
    }
}