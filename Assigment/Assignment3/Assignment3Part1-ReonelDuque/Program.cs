//Purpose: Design and write a modularized menu-driven program that allows user to select one of two games (Craps and Pig) to play or to quit the program.
//Input: menuChoice, betAmount, 'Y', 'N', pointTotal, 'R', 'H'
//Output: Dice rolls, win or lost for game of craps, win or lose for game of pig
//Written by: Reonel Duque
//Written for: Allan Anderson
//Section: A02
//Last Modified Date: November 15, 2022
namespace Assignment3Part1_ReonelDuque
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //declare main method variables
            bool exit = false;
            
            //do loop for main
            do
            {
                int menuChoice;
                menuChoice = ProgramMenu();
                switch (menuChoice)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        PlayCraps();
                        break;
                    case 2:
                        PlayPig();
                        break;
                    default:
                        Console.WriteLine("Error: Try Again");
                        break;
                }
            } while (!exit);
        }

        static int ProgramMenu()
        {
            //declare variables for ProgramMenu
            int choice;

            //displays the menu
            Console.WriteLine("|-------------------|");
            Console.WriteLine("|  CPSC1012 Casino  |");
            Console.WriteLine("|-------------------|");
            Console.WriteLine("| 1. Play Craps     |");
            Console.WriteLine("| 2. Play Pig       |");
            Console.WriteLine("| 0. Exit Program   |");
            Console.WriteLine("|-------------------|");

            //calls GetSafeInt to get proper INT
            choice = GetSafeInt("Enter your menu number choice > ", 0);

            return choice;
        }//end of ProgramMenu

        static int GetSafeInt(string prompt, int minValue)
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

                    if (number >= minValue)
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

        static char GetSafeChar(string prompt, char option1, char option2)
        {
            //declare variables for GetSafeChar
            bool isValid = false;
            char option = ' ';

            //do try loop for validation
            do
            {
                try
                {
                    Console.Write(prompt);
                    option = char.Parse(Console.ReadLine().ToUpper());
                    if (option == option1 || option == option2)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Input a single character. Try again.");
                }
            } while (!isValid);

            return option;
        }//end of GetSafeChar

        static double GetSafeDouble(string prompt, int minValue)
        {
            bool isValid = false;
            double number = 0;

            do
            {
                try
                {
                    Console.Write(prompt);
                    number = double.Parse(Console.ReadLine());

                    if (number >= minValue)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Value must be >= $0");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                }
            } while (!isValid);

            return number;
        }//end of GetSafeInt

        static void PlayCraps()
        {
            //declare variables for PlayCraps
            bool playEnding = false,
                pointStatement;
            int point,
                result;

            double betAmount,
                betTotal = 0;
            Die die1 = new Die();
            Die die2 = new Die();
            char playAgain;

            Console.WriteLine("-----------------");
            Console.WriteLine("| Game of Craps |");
            Console.WriteLine("-----------------");

            //do loop if player wants to keep playing
            do
            {
                point = -1;
                bool gameEnd = false;
                pointStatement = false;

                //calls GetSafeInt for a valid int
                betAmount = GetSafeDouble("Enter amount to bet: ", 0);
                
                //while loop will keep dice rolling until results are 2,3,7,11 or 12 which are loss or win conditions
                while (gameEnd == false)
                {
                    die1.Roll();
                    die2.Roll();
                    result = die1.Facevalue + die2.Facevalue;
                    Console.WriteLine($"You rolled {die1.Facevalue} + {die2.Facevalue} = {result}");
                    if (pointStatement == false)
                    {
                        if (result == 7 || result == 11)
                        {
                            Console.WriteLine($"You win {betAmount:c}");
                            betTotal += betAmount;
                            gameEnd = true;
                        }
                        else if (result == 2 || result == 3 || result == 12)
                        {
                            Console.WriteLine($"You lost {betAmount:c}");
                            betTotal -= betAmount;
                            gameEnd = true;
                        }
                        else
                        {
                            point = result;
                            Console.WriteLine($"Point is {point}");
                            pointStatement = true;
                        }
                        
                    }
                    else
                    {
                        if (result == 7)
                        {
                            Console.WriteLine($"You lost {betAmount:c}");
                            betTotal -= betAmount;
                            gameEnd = true;
                        }
                        else if (result == point)
                        {
                            Console.WriteLine($"You win {betAmount:c}");
                            betTotal += betAmount;
                            gameEnd = true;
                        }
                    }
                }

                //asks player to play again
                playAgain = GetSafeChar("Do you want to play again (y/n): ", 'Y', 'N');
                if (playAgain == 'N')
                {
                    Console.WriteLine($"Your net winning is {betTotal:c}");
                    playEnding = true;
                }
            } while (!playEnding);
        }//end of PlayCarps()

        static void PlayPig()
        {
            //declare variables for PlayPig
            int defaultPointTotal = 100,
                pointTotal = -1,
                playerTotal = 0,
                computerTotal = 0;
            bool isValid,
                gameEnd;

            char option;

            Die die1 = new Die();

            Console.WriteLine("---------------");
            Console.WriteLine("| Game of Pig |");
            Console.WriteLine("---------------");

            option = GetSafeChar("Would you like to choose point total? (Y/N): ", 'Y','N');
            if (option == 'Y')
            {
                pointTotal = GetSafeInt("Enter the point total to play for: ", 0);
            }
            else if (option == 'N')
            {
                Console.WriteLine("Default of 100 points is set.");
                pointTotal = defaultPointTotal;
            }

            gameEnd = false;
            //do loop will keep going until someone wins
            do
            {   
                //check to see if computer won
                if (gameEnd == false)
                {
                    if (computerTotal >= pointTotal)
                    {
                        Console.WriteLine("Computer Wins");
                        gameEnd = true;
                    }
                    else
                    {
                        //calls for PlayerTurn while it's the player's turn
                        playerTotal += PlayerTurn(pointTotal - playerTotal);
                        Console.WriteLine($"\nYour total points: {playerTotal}");
                        Console.WriteLine($"Computer total poitns: {computerTotal}\n");
                    }
                }
                //check to see if player won
                if (gameEnd == false)
                    if (playerTotal >= pointTotal)
                    {
                        Console.WriteLine("You Win");
                        gameEnd = true;
                    }

                    else
                    {
                        //calls for ComputerTurn while it's the computer's turn
                        computerTotal += ComputerTurn(pointTotal - computerTotal);
                        Console.WriteLine($"\nYour total points: {playerTotal}");
                        Console.WriteLine($"Computer total points: {computerTotal}\n");
                }
            } while (!gameEnd);
        }//end of PlayPig

        static int PlayerTurn(int pointTotal)
        {
            //declare variables for PlayerTurn
            int pointTurn = 0;
            bool turnEnd = false;
            char playOption = ' ';

            Die die1 = new Die();

            Console.WriteLine("It's your turn.");

            //do loop will keep going until player wins, player rolls a 1, or player holds
            do
            {
                die1.Roll();
                Console.WriteLine($"You rolled a {die1.Facevalue}");
                pointTurn += die1.Facevalue;
                
                while (pointTurn < pointTotal && die1.Facevalue != 1 && playOption != 'H')
                {
                    //calls GetSafeChar for a valid input
                    playOption = GetSafeChar("Enter r to roll or h to hold (r/h): ", 'R', 'H');
                    if (playOption == 'R')
                    {
                        die1.Roll();
                        Console.WriteLine($"You rolled a {die1.Facevalue}");
                        pointTurn += die1.Facevalue;
                    }
                }

                if (die1.Facevalue == 1)
                {
                    pointTurn = 0;
                    Console.WriteLine($"Your turn score is {pointTurn}");
                    turnEnd = true;
                }
                else if (playOption == 'H')
                {
                    Console.WriteLine("You HOLD");
                    Console.WriteLine($"Your turn score is {pointTurn}");
                    turnEnd = true;
                }
                else if (pointTurn >= pointTotal)
                {
                    Console.WriteLine($"Your turn point total is {pointTurn}");
                    turnEnd = true;
                }

            } while (!turnEnd);
        
            return pointTurn;
        }//end of PlayerTurn

        static int ComputerTurn(int pointTotal)
        {
            //declare variables
            int pointTurn = 0;
            bool turnEnd = false;

            Die die1 = new Die();

            Console.WriteLine("It's the computer's turn.");

            //do loop will keep going until computer wins, rolls a 1, or gets a cumulative point turn of 10 or more
            do
            {
                die1.Roll();
                Console.WriteLine($"Computer rolled a {die1.Facevalue}");
                pointTurn += die1.Facevalue;

                while (pointTurn < pointTotal && die1.Facevalue != 1 && pointTurn < 10)
                {
                    die1.Roll();
                    Console.WriteLine($"Computer rolled a {die1.Facevalue}");
                    pointTurn += die1.Facevalue;
                }

                if (die1.Facevalue == 1)
                {
                    pointTurn = 0;
                    Console.WriteLine($"Computer turn score is {pointTurn}");
                    turnEnd = true;
                }
                else if (pointTurn >= 10)
                {
                    Console.WriteLine("Computer HOLD");
                    Console.WriteLine($"Computer turn score is {pointTurn}");
                    turnEnd = true;
                }
                else if (pointTurn >= pointTotal)
                {
                    Console.WriteLine($"Computer turn point total is {pointTurn}");
                    turnEnd = true;
                }

            } while (!turnEnd);

            return pointTurn;
        }//end of PlayerTurn
    }
}