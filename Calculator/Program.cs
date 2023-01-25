using System.Text;

namespace Calculator
{
    public class Calculator
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            do {
                //string hex =Console.ReadLine();
                //int value = Convert.ToInt32(hex, 16);
                //Console.WriteLine(value);

                //Console.WriteLine("is hex?");
                //if(Console.ReadLine().ToLower().Contains('y'))
                //    calc.isHex= true;

                Console.Write("Enter the floating point precision (blank For Default):");
                string precision = Console.ReadLine();


                Console.WriteLine("Please enter the calculation");
                string? input = Console.ReadLine();
                if (input != null)
                {
                    string answer;
                    if (precision.Length >0)
                        answer = calc.Calculate(input).ToString("n"+precision);
                    else
                        answer = calc.Calculate(input).ToString();
                    Console.WriteLine(answer);
                }

            } while (true); //TODO catch ESC, end prog
        }


        bool isHex = false;
        
        readonly char[] OperatorChars = { '+', '-', '*', '/', '^', '(',')', '!', 's', 'c', 'l', 't' };
        readonly int[] OperatorPresidenc = { 2,2,3,3,4,              0,0,    5,   5,   5,   5,   5};

        public float Calculate(string Input)
        {
            // strip white space out
            Input = Input.Replace(" ", ""); 

            Queue<string> outPut = parseToReverseHungarionNotation(Input);

            return ProcessCalculation(outPut);
        }

        private float ProcessCalculation(Queue<string> calculationQueue)
        {
            Stack<float> OutputStack = new Stack<float>();

            while(calculationQueue.Count > 0) 
            {
                string Token = calculationQueue.Dequeue();

                float TokenAsFloat;
                if(float.TryParse(Token, out TokenAsFloat))
                {
                    
                    OutputStack.Push(TokenAsFloat);
                }
                else //we assume its a command
                {
                    float answer;
                    if (Token == "!" || Token == "s" || Token == "c"|| Token == "t"|| Token == "l")
                    {
                        answer = ComputeFuncitonOperation(OutputStack.Pop(), Token.ToCharArray()[0]);
                    }
                    else
                    {
                        float OperandB = OutputStack.Pop();
                        float OperandA = OutputStack.Pop();

                        answer = ComputeOperation(OperandA, OperandB, Token.ToCharArray()[0]);
                        
                    }
                    OutputStack.Push(answer);
                }
            }

            return OutputStack.Pop();
        }

        private float Factoral(float input)
        {
            int result = 1;
            for (int i = (int)input; i > 0; i--)
                result *= i;
            return result;  
        }



        private float ComputeFuncitonOperation(float OperandA, char Operator)
        {
            float answer = 0;
            switch (Operator)
            {
                case '!':
                    answer = Factoral(OperandA);
                    break;
                case 's':
                    answer = (float)Math.Sin(OperandA);
                    break;
                case 'l':
                    answer = (float)Math.Log(OperandA);
                    break;
                case 'c':
                    answer = (float)Math.Cos(OperandA);
                    break;
                case 't':
                    answer = (float)Math.Tan(OperandA);
                    break;

                default:
                    //not a command????
                    break;

            }

            return answer;
        }

        /// <summary>
        /// takes the operands and operator and calculates the result
        /// </summary>
        /// <param name="OperandA"></param>
        /// <param name="OperandB"></param>
        /// <param name="Operator"></param>
        /// <returns>the result of the operation</returns>
        private float ComputeOperation(float OperandA, float OperandB, char Operator)
        {
            float answer = 0;
            switch(Operator)
            {
                case '+':
                    answer = OperandA + OperandB;
                    break;
                case '-':
                    answer = OperandA - OperandB;
                    break;
                case '*':
                    answer = OperandA * OperandB;
                    break;
                case '/':
                    answer = OperandA / OperandB;
                    break;
                case '^':
                    answer = (float)Math.Pow(OperandA, OperandB);
                    break;
                
                default:
                    //not a command????
                    break;

            }

            return answer;
        }



        /// <summary>
        /// replaces - with £ to denote negatve numbers, only when - is used for negative numbers and not as operation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string parseStringForNegatives(string input)
        {

            //used a string builder as its easier to replace substrings and its more efficient.
            StringBuilder InputStringBuilder = new StringBuilder(input);

            //if first char is a -, replace with £ to denote negative number/
            if (InputStringBuilder[0] == '-')
                InputStringBuilder[0] = '£';

            //TODO automate the construction of the strings and loop. 
            InputStringBuilder = InputStringBuilder.Replace("+-", "+£");
            InputStringBuilder = InputStringBuilder.Replace("--", "-£");
            InputStringBuilder = InputStringBuilder.Replace("*-", "*£");
            InputStringBuilder = InputStringBuilder.Replace("/-", "/£");
            InputStringBuilder = InputStringBuilder.Replace("^-", "^£");
            InputStringBuilder = InputStringBuilder.Replace("(-", "(£");
            InputStringBuilder = InputStringBuilder.Replace(")-", ")£");


            return InputStringBuilder.ToString();
        }

        private string ReplaceFunctions(string input)
        {
            input = input.ToLower();
            //used a string builder as its easier to replace substrings and its more efficient.
            StringBuilder InputStringBuilder = new StringBuilder(input);

            InputStringBuilder = InputStringBuilder.Replace("sin", "s");
            InputStringBuilder = InputStringBuilder.Replace("cos", "c");
            InputStringBuilder = InputStringBuilder.Replace("tan", "t");
            InputStringBuilder = InputStringBuilder.Replace("log", "l");
            

            return InputStringBuilder.ToString();
        }

        private Queue<string> parseToReverseHungarionNotation(string input)
        {
            

            input = parseStringForNegatives(input);
            input = ReplaceFunctions(input);

            //split input string by the operator char list, so we get the Operands
            Queue<string> operandTokens = new Queue<string>(input.Split(OperatorChars));//might want to check for - + * and no numbers
            

            Queue<string> output = new Queue<string>();
            Stack<string> OperandStack = new Stack<string>();



            while (input.Length>0)
            {
                string operandTokenNumber = operandTokens.Dequeue();
                
                //remove the chars for the current of the current number, from the input.
                input = input.Substring(operandTokenNumber.Length);

                if (operandTokenNumber != "")//check current token is no blank. they sometimes creep in from the string split.
                {
                    //replace ! with - to preserve the sign of the number
                    operandTokenNumber = operandTokenNumber.Replace("£", "-");
                    output.Enqueue(operandTokenNumber);
                }

               
                if (input.Length > 0)
                {
                    //get operator 
                    string operatorToken = input.Substring(0, 1);
                    //remove operator from input
                    input = input.Remove(0, 1);

                    //if token is (, we need to push it to the stack and do nothing this ireraation.
                    if (operatorToken != "(" && OperandStack.Count > 0)
                    {
                        
                        //if operator is ), pop all, from operandStack, untill we find the (
                        if (operatorToken == ")")
                        {
                            while(OperandStack.Peek() !="(")
                                output.Enqueue(OperandStack.Pop());
                            OperandStack.Pop(); // pop the "(" off the stack, we dont need it anymore
                        }
                        else
                        {
                            int PresidenceOfCurrentToken = GetPresidenceForOperator(operatorToken);
                            int PresidenceOfLastToken = GetPresidenceForOperator(OperandStack.Peek());

                            //if presidenc of currint operator is == to last operator
                            //pop operator stack to output, to preserve order of operations
                            if (PresidenceOfCurrentToken == PresidenceOfLastToken)
                            {
                                output.Enqueue(OperandStack.Pop());
                            }
                            //else if presidence is greater, pop lower presidence to preserve BODMAS
                            else if (PresidenceOfCurrentToken < PresidenceOfLastToken)
                                while (OperandStack.Count > 0)
                                    output.Enqueue(OperandStack.Pop());
                        }
                    }

                    //push operator to output stack, inc (
                    if(operatorToken!=")")
                        OperandStack.Push(operatorToken);


                }

            }

            //pop all remaning operators to output
            while(OperandStack.Count > 0)
                output.Enqueue(OperandStack.Pop());

            return output;
        }

        /// <summary>
        /// jooks up the presidence value, of the given operatorToken, in the OperatoPresidenc array
        /// </summary>
        /// <param name="operatorToken"></param>
        /// <returns>the presidence of the given operator</returns>
        private int GetPresidenceForOperator(string operatorToken)
        {
            int index = Array.IndexOf(OperatorChars, operatorToken.ToCharArray()[0]);
            int PresidenceOfCurrentToken = OperatorPresidenc[index];
            return PresidenceOfCurrentToken;
        }
    }

}