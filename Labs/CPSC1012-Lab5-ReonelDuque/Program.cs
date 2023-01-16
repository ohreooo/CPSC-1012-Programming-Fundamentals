//Purpose:  construct a practical program to calculate the grades as percentages, class average, and how many students passed and failed the assessment. The data of student grades will be loaded from a file.
//Input: no input from users
//Output: display of the quiz total, the mark and percentage of each line of the file, the class average, number of passes and number of fails
//Written by: Reonel Duque
//Written for: Allan Anderson
//Section: A02
//Last Modified Date: November 18, 2022
namespace CPSC1012_Lab5_ReonelDuque
{
    internal class Program
    {
        //declare path
        const string PathAndFile = @"F:\CPSC1012\VisualStudio2022Code\Labs\CPSC1012-Lab5-ReonelDuque\QuizMarks.txt";
        static void Main(string[] args)
        {
            //declare main variables
            const int PhysicalSize = 25;
            int pass = 0,
                fail = 0,
                logicalSize = 0;
            double total = 0,
                average = 0;
            double[] grades = new double[PhysicalSize];

            //display the beginning
            Console.WriteLine("Welcome to the Quiz Mark Calculator");
            Console.WriteLine("===================================\n\n");

            //grabs all information from file
            logicalSize = LoadFromFile(grades, PhysicalSize);
            total = GetTotal(grades, logicalSize);
            pass = ShowMarkAndPassRate(grades, logicalSize, total);
            average = GetAverage(grades, logicalSize, total);

            //display average, pass and fail
            Console.WriteLine($"The class average is {average:f2}%");
            Console.WriteLine($"There were {pass} passes and {(logicalSize - 1) - pass} fails.");

        }//end of Main

        static int ShowMarkAndPassRate(double[] grades, int logicalSize, double total)
        {
            //declare variables for ShowMarkAndPassRate
            int pass = 0;
            double grade;
            Console.WriteLine($"Quiz marks:  Quiz Total = {total}");
            Console.WriteLine($"{"Mark",-5} {"Percentage", -10}");
            for (int index = 1; index < logicalSize; index++)
            {
                //calculates percentage form of mark
                grade = (grades[index] / total) * 100;
                Console.WriteLine($"{grades[index], -5:f1}{grade,10:f2}%");
                
                //check for count of passing
                if ( grade > 50)
                {
                    pass++;
                }
            }
            return pass;
        }//end of ShowMarkAndPassRate

        static double GetAverage(double[] grades, int logicalSize, double total)
        {
            //declare GetAverage variables
            double average = 0;
            for (int index = 1; index < logicalSize; index++)
            {
                //adds all marks
                average += grades[index];
            }

            //calculates average - 1 because first one is the quiz total
            average = ((average/(logicalSize - 1))/total) * 100;
            return average;
        }//end of GetAverage

        static double GetTotal(double[] grades, int logicalSize)
        {
            //declare GetTotal variables
            double total;

            //first one seems to be quiz total always
            total = grades[0];

            return total;
        }//end of GetTotal
        static int LoadFromFile(double[] grades, int size)
        {
            //declare LoadFromFile variables
            int grade,
                logicalSize = 0;
            string input;
            if (File.Exists(PathAndFile))
            {
                // Setup the StreamReader
                StreamReader reader = null;

                // Use a try-catch-finally to read & display file contents
                try
                {
                    // Open the file for reading
                    reader = File.OpenText(PathAndFile);
                    //Use a while loop to loop through the file
                    while ((input = reader.ReadLine()) != null && logicalSize < size) //READING A FILE IS ALWAYS THIS CODE, BEST SAVE IT
                    {
                        grades[logicalSize] = double.Parse(input);
                        logicalSize++;
                    }
                }
                catch (Exception ex)
                {
                    //Display any exceptions
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    // Close the StreamReader
                    reader.Close();
                }
                // end of file read and display
            }
            else
            {
                // Information message if the file does not exist
                Console.WriteLine($"The file, {PathAndFile}, does not exist");
            }

            return logicalSize;

        }//end of LoadFromFile
    }
}