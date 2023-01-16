//Purpose: Create a game where player rolls a virtual 6-sided die and the unicorns move down the track. The player is against the computer. The character that crosses the finish line first wins.
//Input: playerCharacter, "Enter", trackLength, playOption
//Output: racetrack, playerCharacter, playerWinCount, computerwinCount, tieCount
//Written by: Reonel Duque
//Written for: Allan Anderson
//Section: A02
//Last Modified Date:October 20, 2022

namespace UnicorntheVideoGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // setup for random numbers
            Random random = new Random();

            // declare variables
            int trackLength = 0,
                trackCount,
                playerCurrentPosition,
                computerCurrentPosition,
                playerWinCount = 0,
                computerWinCount = 0,
                tieCount = 0;

            char playerCharacter = ' ';
            string enterToRoll;

            char playOption;

            bool validCharacter,
                playAgain,
                winnerValid,
                validPlayAgain;

            // do loop for the entire game, will loop if player inputs "y" to play again, will exit if "n"
            do
            {
                playAgain = false;
                bool validNumber = false;
                Console.WriteLine("Welcome to the Unicorn Racing Game!");

                // gets input for length of track, will loop if number is not greater than 1 or isn't an integer
                do
                {
                    try
                    {
                        Console.Write("Enter the length of the track: ");
                        trackLength = int.Parse(Console.ReadLine());
                        if (trackLength > 1)
                        {
                            validNumber = true;
                        }
                        else
                        {
                            Console.WriteLine("Input Number must be >1");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error: input must be an integer");
                    }
                } while (!validNumber);

                // gets input for the character for unicorn, checking that it isn't empty or just a space
                do
                {
                    validCharacter = false;
                    try
                    {
                        Console.Write("Enter your unicorn character (use a single character): ");
                        playerCharacter = char.Parse(Console.ReadLine());
                        if (playerCharacter == ' ')
                        {
                            Console.WriteLine("Do not use an empty space for character");
                            Console.Write("Enter your unicorn character (use a single character): ");
                            playerCharacter = char.Parse(Console.ReadLine());
                        }
                        else
                        {
                            validCharacter = true;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error! Input a valid character");
                    }
                } while (!validCharacter);


                computerCurrentPosition = 1;
                playerCurrentPosition = 1;

                // loop for the actual unicorn race, will keep looping if "enter" is pressed while there is no winner
                do
                {
                    winnerValid = false;
                    Console.Clear();

                    // for loop to display the top border of the track
                    for (trackCount = 1; trackCount <= trackLength; trackCount++)
                    {
                        Console.Write($"=");
                    }

                    // displays the unicorns
                    Console.WriteLine("\n");
                    Console.WriteLine($"{playerCharacter}".PadLeft(playerCurrentPosition));
                    Console.WriteLine("\n\n");
                    Console.WriteLine("*".PadLeft(computerCurrentPosition));
                    Console.WriteLine("");

                    // for loop to display the bottom border of the track
                    for (trackCount = 1; trackCount <= trackLength; trackCount++)
                    {
                        Console.Write("=");
                    }

                    // check to see if someone has passed the tracks
                    if (playerCurrentPosition > trackLength || computerCurrentPosition > trackLength)
                    {

                        // playerwins if player's position is greater than computer position, and vice versa, if equal, then tie
                        if (playerCurrentPosition > computerCurrentPosition)
                        {
                            winnerValid = true;
                            Console.WriteLine("\nPlayer Wins!");
                            playerWinCount++;
                        }
                        else if (playerCurrentPosition == computerCurrentPosition)
                        {
                            winnerValid = true;
                            Console.WriteLine("\nTie!");
                            tieCount++;
                        }
                        else
                        {
                            winnerValid = true;
                            Console.WriteLine("\nComputer Wins!");
                            computerWinCount++;
                        }
                    }
                    else
                    {
                        // asks input to only press "enter"
                        Console.Write("\nHit Enter to roll ");
                        enterToRoll = Console.ReadLine();

                        // checks to see if there is no input other than "enter"
                        while (enterToRoll.Length != 0)
                        {
                            Console.Write("Hit Enter to roll(no other keyboard input) ");
                            enterToRoll = Console.ReadLine();
                        }
                    }

                    // random generator for rolling the dice
                    playerCurrentPosition += random.Next(1, 7);
                    computerCurrentPosition += random.Next(1, 7);
                } while (!winnerValid);

                validPlayAgain = false;
                
                // do loop to check if play again input is valid
                do
                {
                    try
                    {
                        Console.Write("Would you like to play again? ");
                        playOption = char.Parse(Console.ReadLine().ToUpper());
                        switch (playOption)
                        {
                            case 'Y':
                                playAgain = true;
                                validPlayAgain = true;
                                break;
                            case 'N':
                                playAgain = false;
                                validPlayAgain = true;
                                break;
                            default:
                                Console.WriteLine("Please input 'y' or 'n'.");
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error: input must be a single character");
                    }
                } while (!validPlayAgain);
            } while (playAgain);

            //display summary
            Console.WriteLine($"You won {playerWinCount} game(s).");
            Console.WriteLine($"You lost {computerWinCount} game(s).");
            Console.WriteLine($"You tied {tieCount} game(s).");
        }
    }
}