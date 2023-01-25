using NUnit.Framework;
using System.Collections.Generic;


namespace Calculator_tests
{
    public  class JagexTests
    {
        Calculator.Calculator calculator;

        struct Test
        {
            public float Answer;
            public string Calculation;
            public Test(string calc, float a) 
            {
                Answer= a; 
                Calculation= calc;
            }
        }

        List<Test> Tests = new List<Test>(); 

        [SetUp]
        public void Setup()
        {
            calculator = new Calculator.Calculator();
            string FileContents = System.IO.File.ReadAllText("../../../tests.txt");

            string[] Lines = FileContents.Split('\n');

            foreach(string line in Lines) 
            {
                if (line == "")
                    continue;
                string LineNoWhiteSpace = line.Replace(" ", "");
                LineNoWhiteSpace = LineNoWhiteSpace.Replace("\r", "");

                string[] SplitLine = LineNoWhiteSpace.Split(':');

                float answer = float.Parse(SplitLine[1]);

                Tests.Add(new Test(SplitLine[0], answer));
            }

        }


        [Test]
        public void JagexTest()
        {
            foreach (Test test in Tests)
            {

                float result = calculator.Calculate(test.Calculation);

                Assert.That(result, Is.EqualTo(test.Answer));
            }
        }
    }
}
