
using NUnit.Framework;

namespace Calculator_tests
{
    public class OrderOfOperationsTest
    {
        Calculator.Calculator calculator;
        [SetUp]
        public void Setup()
        {
            calculator = new Calculator.Calculator();
        }


        [Test]
        public void PlusMul()
        {
            string Calculation = "1+2*3";
            float answer = 7;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }

        [Test]
        public void BracketsPlusMul()
        {
            string Calculation = "(1+2)*3";
            float answer = 9;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }

        [Test]
        public void PlusMinusPlus()
        {
            string Calculation = "6+3-2+12";
            float answer = 19;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
        [Test]
        public void MulPlus()
        {
            string Calculation = "2*15+23";
            float answer = 53;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
        [Test]
        public void MinusPow()
        {
            string Calculation = "10-3^2";
            float answer = 1;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }
    }
}
