using NUnit.Framework;

namespace Calculator_tests
{
    public class NonIntegerTest
    {

        Calculator.Calculator calculator;
        [SetUp]
        public void Setup()
        {
            calculator = new Calculator.Calculator();
        }

        [Test]
        public void FloatMulInt()
        {
            string Calculation = "3.5*3";
            float answer = 10.5f;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }

        [Test]
        public void NegPlusNeg()
        {
            string Calculation = "-53+-24";
            float answer = -77;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }

        [Test]
        public void FloatDiv()
        {
            string Calculation = "10/3";
            float answer = 3.333f;

            string resultString =calculator.Calculate(Calculation).ToString("#.###");

            float result = float.Parse(resultString);

            Assert.That(result, Is.EqualTo(answer));
        }


        [Test]
        public void BracketsNegMulFloatDiv()
        {
            string Calculation = "(-20*1.8)/2";
            float answer = -18;

            float result = calculator.Calculate(Calculation);

            Assert.That(result, Is.EqualTo(answer));
        }

        [Test]
        public void NegfloatMinus()
        {
            string Calculation = "-12.315 - 42";
            float answer = -54.315f;

            string resultString = calculator.Calculate(Calculation).ToString("#.###");

            float result = float.Parse(resultString);

            Assert.That(result, Is.EqualTo(answer));
        }
    }
}
