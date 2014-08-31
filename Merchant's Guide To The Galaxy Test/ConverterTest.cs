using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Merchant_s_Guide_To_The_Galaxy.Converter;
using Merchant_s_Guide_To_The_Galaxy.CustomException;
using Merchant_s_Guide_To_The_Galaxy.Rules;
using NUnit.Framework;

namespace Merchant_s_Guide_To_The_Galaxy_Test
{
    [TestFixture]
    public class ConverterTest
    {
        private IRomanSymbolConverter _symbolConverter;

        [SetUp]
        public void SetUp()
        {
            _symbolConverter = new RomanSymbolConverter();
        }

        [Test]
        public void ConvertToNumberTest()
        {
            //Arrange
            var symbols = "IVX";
            var symbols1 = "MCMXLIV";
            var rules = new List<IRule>
            {
                new RuleSymbolOnlyRepeatThreeTimes(),
                new RuleStartWithLargest()
            };

            //Act
            var number = _symbolConverter.ConvertToNumber(symbols, rules);
            var number1 = _symbolConverter.ConvertToNumber(symbols1, rules);

            //Assert
            number.Should().Be(14);
            number1.Should().Be(1944);
        }

        [Test]
        [ExpectedException(typeof(SymbolRepeatFouthException), 
        ExpectedMessage = "Some symbols cannot repeat more than three times, please check again")]
        public void ConvertToNumberFouthRepeatExceptionTest()
        {
            //Arrange
            var symbols = "IVXXXX";
            var rules = new List<IRule>
            {
                new RuleSymbolOnlyRepeatThreeTimes(),
                new RuleStartWithLargest()
            };

            //Act
            _symbolConverter.ConvertToNumber(symbols, rules);

            //Assert
        }

        [Test]
        [ExpectedException(typeof(SymbolCannotRepeatException),
        ExpectedMessage = "Some symbols cannot repeat, please check again")]
        public void ConvertToNumberCannotRepeatExceptionTest()
        {
            //Arrange
            var symbols = "MCDD";
            var rules = new List<IRule>
            {
                new RuleSymbolOnlyRepeatThreeTimes(),
                new RuleStartWithLargest()
            };

            //Act
            _symbolConverter.ConvertToNumber(symbols, rules);

            //Assert
        }
    }
}
