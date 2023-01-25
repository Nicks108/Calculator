
using NUnit.Framework;

namespace Calculator_tests
{
    public class BasicPoeratorTest
    {

        Calculator.Calculator calculator;
        [SetUp]
        public void Setup()
        {
            calculator = new Calculator.Calculator();
        }

        

        [Test]
        public void BasicOperatorsPlusTest()
        {
            string Calculation = "2+5";
            float answer = 7;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
        [Test]
        public void BasicOperatorsMinuseTest()
        {
            string Calculation = "8-3";
            float answer = 5;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
        [Test]
        public void BasicOperatorsMultTest()
        {
            string Calculation = "5*4";
            float answer = 20;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
        [Test]
        public void BasicOperatorsDivTest()
        {
            string Calculation = "8/2";
            float answer = 4;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
        [Test]
        public void BasicOperatorsPowerTest()
        {
            string Calculation = "4^2";
            float answer = 16;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
    }
}