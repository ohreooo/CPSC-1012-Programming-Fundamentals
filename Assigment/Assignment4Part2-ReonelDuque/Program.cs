/*
    Purpose: Write a program that allows an administrator to manage the questions for a
    multiple-choice quiz. When the program is run, it should check to see if a data file exists. If the data file
    exists, then the multiple-choice questions should be loaded from the data file into an array/list in
    memory. If the data file does not exist, start the program with no multiple-choice questions in memory.
    The program should then present a menu that allows the administrator to list all multiple-choice items
    (question, answer, and value) in the database, add a new multiple-choice question item, or delete an
    existing multiple-choice question item. Upon exiting the program, the multiple-choice data in memory
    should be stored in a text file.
    Input:   QuizQuestions.csv, userchoice input, userquestion input, useranswer input
    Output:  QuizQuestion.csv
    Written by: Reonel Duque
    Written for: Allan Anderson
    Section: A02
    Last Modified Date: November 30, 2022 
*/
namespace Assignment4Part2_ReonelDuque
{
    internal class Program
    {
        const string InputPathAndFile = @"D:\CPSC1012\VisualStudio2022Code\Assignments\Assignment4\Assignment4Part2-ReonelDuque\QuizQuestions.csv";
        static void Main(string[] args)
        {
            //declare variables
            int choice;
            bool exit = false;
            List<MultipleChoiceQuestion> questionList = new List<MultipleChoiceQuestion>();
            if (File.Exists(InputPathAndFile))
            {
                do
                {
                    choice = ProgramMenu();
                    switch (choice)
                    {
                        case 1:
                            ListMultipleQuestion(questionList);
                            break;
                        case 2:
                            AddMultipleQuestion(questionList);
                            break;
                        case 3:
                            RemoveQuestion(questionList);
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Error: Try Again");
                            break;
                    }
                } while (!exit);
                WriteListToFile(questionList);
                Console.WriteLine($"The Multiple-Choice Questions has been saved to the file {InputPathAndFile}");
            }
            else
            {
                Console.WriteLine($"The file, {InputPathAndFile}, does not exist");
            }
        }

        static int ProgramMenu()
        {
            int choice;

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("| Multiple-Choice Quiz Administration |");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1. List Multiple Choice Questions");
            Console.WriteLine("2. Add Multiple Choice Questions");
            Console.WriteLine("3. Delete Multiple Choice Questions");
            Console.WriteLine("4. Quit");
            choice = GetSafeInt("Your choice: ", 1, 4);
            return choice;
        }//end of ProgramMenu

        static void LoadFromFile(List<MultipleChoiceQuestion> multipleQuestions)
        {
            string input;
            StreamReader reader = null;

            multipleQuestions.Clear();

            try
            {
                //configure the reader
                reader = File.OpenText(InputPathAndFile);

                //loop to the end of the file
                while ((input = reader.ReadLine()) != null)
                {
                    //split the input on the ','
                    string[] parts = input.Split(',');

                    //add the new MultipleQuestions to the List<MultipleChoiceQuestions>
                     multipleQuestions.Add(new MultipleChoiceQuestion(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]));
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            finally
            {
                reader.Close();
            }
        }//end of LoadFromFile

        static void DisplayMultipleQuestion(List<MultipleChoiceQuestion> multipleQuestions)
        {
            int index = 1;
            Console.WriteLine("");
            if (multipleQuestions.Count == 0)
            {
                Console.WriteLine("No questions to list");
            }
            else
            {
                foreach (MultipleChoiceQuestion question in multipleQuestions)
                {
                    Console.WriteLine($"{index}. {question}");
                    index++;
                }
            }
        }//end of DisplayMultipleQuestion

        static void ListMultipleQuestion(List<MultipleChoiceQuestion> multipleQuestions)
        {
            LoadFromFile(multipleQuestions);
            DisplayMultipleQuestion(multipleQuestions);
        }//end of ListmultipleQuestion

        static void AddToList(List<MultipleChoiceQuestion> multipleQuestions)
        {
            //declare variables for AddToList
            bool exit;
            int location;
            string newQuestion;
            string[] choices = new string[4];

            do
            {
                exit = true;
                newQuestion = GetSafeString("\nEnter the Multiple-Choice Question text:\n", 9);
                foreach (MultipleChoiceQuestion question in multipleQuestions)
                {
                    if (newQuestion.ToUpper() == question.Question.ToUpper())
                    {
                        Console.WriteLine("Question already exists");
                        exit = false;
                    }
                }
            } while (!exit);

            for (int index = 0; index < 4; index++)
            {
                do
                {
                    choices[index] = GetSafeString($"Enter answer #{index + 1}\n", 1);
                    location = SearchArray(choices, 4, choices[index]);
                    if(location != -1 && index != location)
                    {
                        Console.WriteLine("Choice already exists. Try a different choice");
                    }
                } while (location != -1 && index != location);
            }

            int answer = GetSafeInt("Enter the correct answer choice (1-4):\n", 1, 4);

            multipleQuestions.Add(new MultipleChoiceQuestion(newQuestion, choices[0], choices[1], choices[2], choices[3], answer.ToString()));
        }//end of AddToList

        static void WriteListToFile(List<MultipleChoiceQuestion> multipleQuestions)
        {
            StreamWriter writer = null;
            try
            {
                writer = File.CreateText(InputPathAndFile);

                foreach (MultipleChoiceQuestion question in multipleQuestions)
                {
                    writer.WriteLine($"{question.Question},{question.Choice1},{question.Choice2},{question.Choice3},{question.Choice4},{question.Answer}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            finally
            {
                writer.Close();
            }
        }//end of WriteListtoFile

        static void RemoveQuestion(List<MultipleChoiceQuestion> multipleQuestions)
        {
            //declare variables for removequestion
            int location;
            int index = 1;
            if (multipleQuestions.Count == 0)
            {
                Console.WriteLine("No questions to delete");
            }
            else
            {
                Console.WriteLine("Item#\tQuestion");
                Console.WriteLine("-----\t----------------------------------");

                foreach (MultipleChoiceQuestion question in multipleQuestions)
                {
                    Console.WriteLine($"{index}\t{question.Question}");
                    index++;
                }
                location = GetSafeInt("Enter the item number to delete: ", 1, multipleQuestions.Count);
                multipleQuestions.RemoveAt(location - 1);
                Console.WriteLine($"Item #{location} has been deleted from the database.");
                WriteListToFile(multipleQuestions);
            }
        }//end of RemoveQuestion
        static void AddMultipleQuestion(List<MultipleChoiceQuestion> multipleQuestions)
        {
            AddToList(multipleQuestions);
            WriteListToFile(multipleQuestions);
        }//end of AddMultipleQuestion
        static int SearchArray(string[] choices, int size, string searchValue)
        {
            //declare variables
            int location = -1; //value of -1 indicates the searchName was not found

            for (int index = 0; index < size; index++)
            {
                if (searchValue.ToUpper() == choices[index].ToUpper())
                {
                    location = index;
                    index = size;
                }
            }
            return location;
        }//end of SearchArray
        static int GetSafeInt(string prompt, int minValue, int maxValue)
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

                    if (number >= minValue && number <= maxValue)
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

        static string GetSafeString(string prompt, int minValue)
        {
            //declare variables for GetSafeString
            bool isValid = false;
            string text;

            //do loop for validation
            do
            {
                Console.Write(prompt);
                text = Console.ReadLine();
                if (text.Length >= minValue)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine($"The name entered, {text}, is too short (min 2 letters) ... try again!");
                }
            } while (!isValid);
            return text;
        }//end of GetSafeString
    }
}