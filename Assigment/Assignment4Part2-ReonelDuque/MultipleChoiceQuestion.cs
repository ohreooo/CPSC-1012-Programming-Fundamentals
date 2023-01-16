namespace Assignment4Part2_ReonelDuque
{
    internal class MultipleChoiceQuestion
    {
        //private member fields
        private string _question;
        private string _choice1;
        private string _choice2;
        private string _choice3;
        private string _choice4;
        private string _answer;

        //public member fields
        public string Question
        {
            get { return _question; }
            set
            {
                if(value.Length >= 9)
                {
                    _question = value;
                }
                else
                {
                    throw new Exception("Invalid Question");
                }
            }
        }//end of Question

        public string Choice1
        {
            get { return _choice1; }
            set
            {
                if(value.Length > 0)
                {
                    _choice1 = value;
                }
                else
                {
                    throw new Exception("Invalid Choice");
                }
            }
        }//end of Choice1

        public string Choice2
        {
            get { return _choice2; }
            set
            {
                if (value.Length > 0)
                {
                    _choice2 = value;
                }
                else
                {
                    throw new Exception("Invalid Choice");
                }
            }
        }//end of Choice2
        public string Choice3
        {
            get { return _choice3; }
            set
            {
                if (value.Length > 0)
                {
                    _choice3 = value;
                }
                else
                {
                    throw new Exception("Invalid Choice");
                }
            }
        }//end of Choice3
        public string Choice4
        {
            get { return _choice4; }
            set
            {
                if (value.Length > 0)
                {
                    _choice4 = value;
                }
                else
                {
                    throw new Exception("Invalid Choice");
                }
            }
        }//end of Choice4

        public string Answer
        {
            get { return _answer; }
            set
            {
                if(int.Parse(value) > 0 && int.Parse(value) <= 4)
                {
                    _answer = value;
                }
                else
                {
                    throw new Exception("Invalid Answer");
                }
            }
        }//end of Answer

        //Greedy Constructor
        public MultipleChoiceQuestion(string question, string choice1, string choice2, string choice3, string choice4, string answer)
        {
            Question = question;
            Choice1 = choice1;
            Choice2 = choice2;
            Choice3 = choice3;
            Choice4 = choice4;
            Answer = answer;
        }//end of MultipleChoiceQuestion

        //ClassMethod
        public override string ToString()
        {
            return $"{Question}\n  1. {Choice1}\n  2. {Choice2}\n  3. {Choice3}\n  4. {Choice4}\nCorrect Answer: {Answer}\n";
        }
    }
}
