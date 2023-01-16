/*
Purpose: design and write a modularized menu-driven program that allows the user to play Lotto MAX with EXTRA or to play Lotto 6/49 with EXTRA.
Inputs: choice to generate or enter the lottery numbers (g/e), menuChoice, numbers for the lottery winning numbers
Outputs: array of lottery numbers(generated or user inputted), number of matched numbers, prize according to matched numbers, number of matched digits, prize according to matched digits
Written by: Reonel Duque
Written for: Allan Anderson
Section No: A02
Last modified: November 22, 2022
*/
namespace Assignment3Part2_ReonelDuque
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //declare main method variables
            int[] maxWinningNumbers = new int[7],
            sixFortyNineWinningNumbers = new int[6],
            maxPlayNumbers = new int[7],
            sixFortyNinePlayNumbers = new int[6];
            Random random = new Random();

            //initializing values for lottery numbers and bonus
            int maxBonus = random.Next(1, 50),
                sixFortyNineBonus = random.Next(1, 49),
                extraNumber = random.Next(1000000, 9999999);
            LoadArrayRandom(maxWinningNumbers, 7, 50);
            LoadArrayRandom(sixFortyNineWinningNumbers, 6, 49);

            //method that will display menu and handle inputs
            MenuandLotto(maxWinningNumbers, sixFortyNineWinningNumbers, maxPlayNumbers, sixFortyNinePlayNumbers, maxBonus, sixFortyNineBonus, extraNumber);
        }//end of Main

        //method for displaying menu and playing lotto
        static void MenuandLotto(int[] maxWinningNumbers, int[] sixFortyNineWinningNumbers, int[] maxPlayNumbers, int[] sixFortyNinePlayNumbers, int maxBonus, int sixFortyNineBonus, int extraNumber)
        {
            //declare MenuandLotto variables
            bool exit = false;
            int menuChoice;

            do
            {
                menuChoice = ProgramMenu();
                switch (menuChoice)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        maxBonus = WinningNumbers("MAX winning", maxWinningNumbers, maxBonus, 7, 50);
                        break;
                    case 2:
                        sixFortyNineBonus = WinningNumbers("6/49 winning", sixFortyNineWinningNumbers, sixFortyNineBonus, 6, 49);
                        break;
                    case 3:
                        extraNumber = ChangeExtraNumber(extraNumber);
                        break;
                    case 4:
                        PlayLotto(maxWinningNumbers, maxPlayNumbers, maxBonus, 7, "MAX quick pick", extraNumber);
                        break;
                    case 5:
                        PlayLotto(sixFortyNineWinningNumbers, sixFortyNinePlayNumbers, sixFortyNineBonus, 6, "6/49 quick pick", extraNumber);
                        break;
                    default:
                        Console.WriteLine("Error: Try Again");
                        break;
                }
            } while (!exit);
        }//end of MenuandLotto

        //method for displaying programmenu
        static int ProgramMenu()
        {
            //declare variables for ProgramMenu
            int choice;

            //displays the menu
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("|           CPSC1012 Lotto Centre            |");
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("| 1. Change Lotto MAX winning numbers        |");
            Console.WriteLine("| 2. Change Lotto 6/49 winning numbers       |");
            Console.WriteLine("| 3. Change Lotto EXTRA winning numbers      |");
            Console.WriteLine("| 4. Play Lotto MAX                          |");
            Console.WriteLine("| 5. Play Lotto 6/49                         |");
            Console.WriteLine("| 0. Exit Program                            |");
            Console.WriteLine("|--------------------------------------------|");

            //calls GetSafeInt to get proper INT
            choice = GetSafeInt("Enter your menu number choice > ", 0, 5);

            return choice;
        }//end of ProgramMenu

        //method for getting an int within range
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

        //method for changing the lottery numbers
        static int WinningNumbers(string prompt, int[] winningNumbers, int bonus, int size, int max)
        {
            //declare variables for WinningNumbers
            int location = -1,
                input;
            char option;
            Random random = new Random();

            SortAndDisplayWinningNumbers(winningNumbers, size, "The current", prompt);
            Console.WriteLine($"(Bonus: {bonus})");
            option = GetSafeChar("Would you like to generate or enter the winning numbers (g/e): ", 'G', 'E');
            Array.Clear(winningNumbers);
            if (option == 'G')
            {
                LoadArrayRandom(winningNumbers, size, max);
                bonus = BonusCheck(winningNumbers, bonus, location, size, max, option);
                SortAndDisplayWinningNumbers(winningNumbers, size, "The new", prompt);
                Console.WriteLine($"(Bonus: {bonus})");
            }
            else if (option == 'E')
            {
                for (int index = 0; index < size; index++)
                {
                    do
                    {
                        input = GetSafeInt($"Enter number {index + 1}: ", 1, max);
                        location = SearchArray(winningNumbers, size, input);
                        if (location != -1 && index != location)
                        {
                            Console.WriteLine("Number already exists. Try a different number.");
                        }
                    } while (location != -1 && index != location);
                    winningNumbers[index] = input;
                }
                bonus = BonusCheck(winningNumbers, bonus, location, size, max, option);
                SortAndDisplayWinningNumbers(winningNumbers, size, "The new", prompt);
                Console.WriteLine($"(Bonus: {bonus})");
            }
            return bonus;
        }//end of WinningNumbers

        //method for sorting and displaying the winning numbers
        static void SortAndDisplayWinningNumbers(int[] winningNumbers, int size, string prompt1, string prompt2)
        {
            SelectionSort(winningNumbers, size);
            Console.Write($"{prompt1} Lotto {prompt2} numbers are: ");
            DisplayArray(winningNumbers, size);
        }//end of SortAndDisplayWinningNumbers

        //method for actually playing lotto
        static void PlayLotto(int[] winningNumbers, int[] playNumbers, int bonus, int size, string prompt, int extraNumber)
        {
            //declare variables for PlayLotto
            int maxExtraNumber,
                arraysMatch;
            bool bonusMatch;
            Random random = new Random();

            maxExtraNumber = random.Next(1000000, 9999999);
            SortAndDisplayWinningNumbers(winningNumbers, size, "The current", prompt.Substring(0, 4));
            Console.WriteLine($"(Bonus: {bonus})");
            Console.WriteLine("The current Lotto EXTRA number is {0000000}\n", extraNumber);
            LoadArrayRandom(playNumbers, size, 50);
            SortAndDisplayWinningNumbers(playNumbers, size, "Your", prompt);
            Console.WriteLine("\nYour Lotto EXTRA number is {0000000}\n", maxExtraNumber);
            arraysMatch = CompareWinningNumbers(winningNumbers, playNumbers, size);
            if (prompt == "MAX quick pick")
            {
                bonusMatch = BonusMatched(playNumbers, size, bonus);
                if (bonusMatch == false)
                {
                    LottoMaxMatchPrize(arraysMatch);
                }
                else
                {
                    LottoMaxMatchPrizeBonus(arraysMatch);
                }
            }
            else
            {
                bonusMatch = BonusMatched(playNumbers, size, bonus);
                if (bonusMatch == false)
                {
                    Lotto649Prize(arraysMatch);
                }
                else
                {
                    Lotto649PrizeBonus(arraysMatch);
                }
            }
            LottoExtraPrize(CompareExtraNumbers(extraNumber, maxExtraNumber, size));
        }//end of PlayLotto

        //method for displaying the extra prize
        static void LottoExtraPrize(int extraNumsMatch)
        {
            Console.WriteLine($"Your Lotto EXTRA Match: Last {extraNumsMatch} digit(s)");
            Console.Write("Your Lotto EXTRA Prize: ");
            switch (extraNumsMatch)
            {
                case 2:
                    Console.WriteLine("$10\n");
                    break;
                case 3:
                    Console.WriteLine("$50\n");
                    break;
                case 4:
                    Console.WriteLine("$100\n");
                    break;
                case 5:
                    Console.WriteLine("$1,000\n");
                    break;
                case 6:
                    Console.WriteLine("$100,000\n");
                    break;
                case 7:
                    Console.WriteLine("$250,000\n");
                    break;
                default:
                    Console.WriteLine("$0\n");
                    break;
            }
        }//end of LottoExtraPrize

        //method to ocheck if the bonus was matched
        static bool BonusMatched(int[] playNumbers, int size, int bonus)
        {
            //declare variables
            bool bonusPoint = false;

            for (int index = 0; index < size; index++)
            {
                if (playNumbers[index] == bonus)
                {
                    bonusPoint = true;
                }
            }
            return bonusPoint;
        }//end of BonusMatched

        //method to display prize based on matches
        static void LottoMaxMatchPrize(int arraysMatch)
        {
            //declare variables
            string winnings;

            Console.WriteLine($"Your Lotto Max Match: {arraysMatch} / 7");

            switch (arraysMatch)
            {
                case 3:
                    winnings = "Free Play";
                    break;
                case 4:
                    winnings = "$20";
                    break;
                case 5:
                    winnings = "Share of 3.5% of Pools Fund";
                    break;
                case 6:
                    winnings = "Share of 2.5% of Pools Fund";
                    break;
                case 7:
                    winnings = "Win or share Jackpot of at least $10 Million or 87.25% of Pools Fund";
                    break;
                default:
                    winnings = "$0";
                    break;
            }
            Console.WriteLine($"Your Lotto Max Prize: {winnings}");
        }//end of LottoMaxMatchPrize

        //method to display prize for MAX based on matches and if bonus was matched
        static void LottoMaxMatchPrizeBonus(int arraysMatch)
        {
            //declare variables
            string winnings;

            Console.WriteLine($"Your Lotto Max Match: {arraysMatch} / 7");
            switch (arraysMatch)
            {
                case 3:
                    winnings = "$20";
                    break;
                case 4:
                    winnings = "Share of 2.75% of Pools Fund";
                    break;
                case 5:
                    winnings = "Share of 1.5% of Pools Fund";
                    break;
                default:
                    winnings = "$0";
                    break;
            }
            Console.WriteLine($"Your Lotto Max Prize: {winnings}");
        }//end of LottoMaxMatchPrizeBonus

        //method to display prize for 6/49 based on matches if bonus was not matched
        static void Lotto649Prize(int arraysMatch)
        {
            //declare variables
            string winnings;

            Console.WriteLine($"Your Lotto 6/49 Match: {arraysMatch} / 6");
            switch (arraysMatch)
            {
                case 2:
                    winnings = "Free Play";
                    break;
                case 3:
                    winnings = "$10";
                    break;
                case 4:
                    winnings = "Share of 9.5% of the Pools Fund";
                    break;
                case 5:
                    winnings = "Share of 5% of the Pools Fund";
                    break;
                case 6:
                    winnings = "Win or share Jackpot (79.5% of the Pools Fund";
                    break;
                default:
                    winnings = "$0";
                    break;
            }
            Console.WriteLine($"Your Lotto 6/49 Prize: {winnings}");
        }//end of Lotto649Prize

        //method to display prize for 6/49 based on matches if bonus was matched
        static void Lotto649PrizeBonus(int arraysMatch)
        {
            //declare variables
            string winnings;

            Console.WriteLine($"Your Lotto 6/49 Match: {arraysMatch} / 6");
            switch (arraysMatch)
            {
                case 2:
                    winnings = "$5";
                    break;
                case 5:
                    winnings = "Share of 6% of the Pools Fund";
                    break;
                default:
                    winnings = "$0";
                    break;
            }
            Console.WriteLine($"Your Lotto 6/49 Prize: {winnings}");
        }//end of Lotto649PrizeBonus

        //method that compares winningnumbers and playingnumbers
        static int CompareWinningNumbers(int[] winningNumbers, int[]playingNumbers, int size)
        {
            //declare variables
            int number = 0;

            for (int index = 0; index < size; index++)
            {
                for (int scan = 0; scan < size; scan++)
                {
                    if (playingNumbers[index] == winningNumbers[scan])
                    {
                        number++;
                    }
                }
            }
            return number;
        }//end of CompareWinningNumbers

        //method that compares the winningextranumbers and playextranumbers
        static int CompareExtraNumbers(int extraNumbers, int playExtraNums, int size)
        {
            //declare variables
            string extraNumber = extraNumbers.ToString(),
                playExtraNum = playExtraNums.ToString();
            int number = 0;

            for (int index = size - 1; index >= 0; index--)
            {
                if (playExtraNum[index] == extraNumber[index])
                {
                    number++;
                }
                else
                {
                    index = -1;
                }
            }
            return number;
        }//end of CompareExtraNumbers

        //method to check if the number to be put in bonus already exists in the winning numbers array
        static int BonusExists(int[] winningNumbers, int bonus, int size)
        {
            //declare variables
            int location = -1;

            for (int index = 0; index < size; index++)
            {
                if (bonus == winningNumbers[index])
                {
                    location = index;
                    index = size;
                }
            }
            return location;
        }//end of BonusExists

        //will change the bonus
        static int BonusCheck(int[] winningNumbers, int bonus, int location, int size, int max, char option)
        {
            //declare variables
            Random random = new Random();

            if (option == 'G')
            {
                do
                {
                    bonus = random.Next(1, max + 1);
                    location = BonusExists(winningNumbers, bonus, size);
                } while (location >= 0);

            }
            if (option == 'E')
            {
                do
                {
                    bonus = GetSafeInt("Enter bonus number: ", 1, max);
                    location = BonusExists(winningNumbers, bonus, size);
                    if (location != -1)
                    {
                        Console.WriteLine("Number already exists. Try a different number.");
                    }
                } while (location != -1);
            }
            return bonus;
        }//end of BonusCheck
        
        //method that changes the EXTRA winning numbers
        static int ChangeExtraNumber(int extraNumber)
        {
            //declare variables
            Random random = new Random();

            Console.WriteLine($"The current Lotto EXTRA number is: {extraNumber}");
            extraNumber = random.Next(1000000, 9999999);
            Console.WriteLine($"The new Lotto EXTRA number is: {extraNumber}");
            return extraNumber;
        }//end of ChangeExtraNumber

        //method that sorts the array in increasing numerical order
        static void SelectionSort(int[] winningNumbers, int size)
        {
            //declare vari8ables
            int minIndex, minValue;
            int temp;

            for (int startScan = 0; startScan < size - 1; startScan++)
            {
                //assume, for now, the first element has the smallest value
                minValue = winningNumbers[startScan];
                //now look at the rest of the array
                for (int index = startScan; index < size; index++)
                {
                    if (winningNumbers[index] < minValue)
                    {
                        minValue = winningNumbers[index];
                        minIndex = index;
                        //now swap
                        temp = winningNumbers[minIndex];
                        winningNumbers[minIndex] = winningNumbers[startScan];
                        winningNumbers[startScan] = temp;
                    }//end if
                }//end inner for
            }//end outer for
        }//end of SelectionSort

        //method that loads random number into the array
        static void LoadArrayRandom(int[] winningNumbers, int size, int max)
        {
            //declare variables
            int location;
            Random random = new Random();

            for (int index = 0; index < size; index++)
            {
                do
                {
                    winningNumbers[index] = random.Next(1, max + 1);
                    location = SearchArray(winningNumbers, size, winningNumbers[index]);
                } while (location != -1 && index != location);
            }
        }//end of LoadArrayRandom

        //method to display the numbers in the array
        static void DisplayArray(int[] winningNumbers, int size)
        {
            for (int index = 0; index < size; index++)
            {
                if (index + 1 == size)
                {
                    Console.Write($"{winningNumbers[index]} ");
                }
                else
                {
                    Console.Write($"{winningNumbers[index]}, ");
                }
            }
        }//end of DisplayArray

        //method that searches the array
        static int SearchArray(int[] winningNumbers, int size, int searchValue)
        {
            //declare variables
            int location = -1; //value of -1 indicates the searchName was not found

            for (int index = 0; index < size; index++)
            {
                if (searchValue == winningNumbers[index])
                {
                    location = index;
                    index = size;
                }
            }
            return location;
        }//end of SearchArray

        //method that gets a character that is wanted
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
    }
}