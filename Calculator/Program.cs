

using System.Text;

namespace Calculator
{
    public class Calculator
    {
        static void Main(string[] args)
        {

            do {

                Calculator calc = new Calculator();
                string answer = calc.Calculate(Console.ReadLine()).ToString();
                Console.WriteLine(answer);

            } while (true);
        }


        Queue<string> outPut = new Queue<string>();
        char[] OperatorChars = { '+', '-', '*', '/', '^', '(',')' };
        int[] OperatorPresidenc = { 2,2,3,3,4,              0,0};

        public float Calculate(string Input)
        {
            Input = Input.Replace(" ", ""); // strip white space out
            outPut= parseToReverseHungarionNotation(Input);

            

            return calculate(outPut);
        }

        private float calculate(Queue<string> calculation)
        {
            Stack<float> stack = new Stack<float>();

            while(calculation.Count > 0) 
            {
                string Token = calculation.Dequeue();

                float TokenAsInt;
                if(float.TryParse(Token, out TokenAsInt))
                {
                    stack.Push(TokenAsInt);
                }
                else //we assume its a command
                {
                    float b = stack.Pop();
                    float a = stack.Pop();

                    float answer = parseTokens(a, b, Token.ToCharArray()[0]);
                    stack.Push(answer);
                }
            }

            return stack.Pop();
        }

        private float parseTokens(float A, float B, char commandChar)
        {
            float answer = 0;
            switch(commandChar)
            {
                case '+':
                    answer = A + B;
                    break;
                case '-':
                    answer = A - B;
                    break;
                case '*':
                    answer = A * B;
                    break;
                case '/':
                    answer = A / B;
                    break;
                case '^':
                    answer = (int)Math.Pow(A, B);
                    break;
                default:
                    //not a command????
                    break;

            }

            return answer;
        }

        private string parseStringForNegatives(string input)
        {
            StringBuilder sb = new StringBuilder(input);
            if (sb[0] == '-')
                sb[0] = '!';


            sb =sb.Replace("+-", "+!");
            sb =sb.Replace("--", "-!");
            sb =sb.Replace("*-", "*!");
            sb =sb.Replace("/-", "/!");
            sb = sb.Replace("^-", "^!");
            sb = sb.Replace("(-", "(!");
            sb = sb.Replace(")-", ")!");


            return sb.ToString();
        }

        private Queue<string> parseToReverseHungarionNotation(string input)
        {
            input = parseStringForNegatives(input);


            Queue<string> tokenNumbers = new Queue<string>(input.Split(OperatorChars));//might want to check for - + * and no numbers

           


            Queue<string> output = new Queue<string>();
            Stack<string> OperandStack = new Stack<string>();



            while (input.Length>0)
            {
                string currentTokenNumber = tokenNumbers.Dequeue();
                
                input = input.Substring(currentTokenNumber.Length);

                if (currentTokenNumber != "")
                {
                    currentTokenNumber = currentTokenNumber.Replace("!", "-");
                    output.Enqueue(currentTokenNumber);
                }

               
                if (input.Length > 0)
                {
                    string operatorToken = input.Substring(0, 1);
                    input = input.Remove(0, 1);

                    if (operatorToken != "(" && OperandStack.Count > 0)
                    {
                        
                        if (operatorToken == ")")
                        {
                            while(OperandStack.Peek() !="(")
                                output.Enqueue(OperandStack.Pop());
                            OperandStack.Pop(); // pop the "(" off the stack, we dont need it anymore
                        }
                        else
                        {
                            int index = Array.IndexOf(OperatorChars, operatorToken.ToCharArray()[0]);
                            int PresidenceOfCurrentToken = OperatorPresidenc[index];

                            index = Array.IndexOf(OperatorChars, OperandStack.Peek().ToCharArray()[0]);
                            int PresidenceOfLastToken = OperatorPresidenc[index];
                            
                            if(PresidenceOfCurrentToken == PresidenceOfLastToken)
                            {
                                output.Enqueue(OperandStack.Pop());

                            }
                            else if (PresidenceOfCurrentToken < PresidenceOfLastToken)
                                while (OperandStack.Count > 0)
                                    output.Enqueue(OperandStack.Pop());
                        }
                    }

                    if(operatorToken!=")")
                        OperandStack.Push(operatorToken);


                }

            }

            while(OperandStack.Count > 0)
                output.Enqueue(OperandStack.Pop());

            return output;
        }

    }

}