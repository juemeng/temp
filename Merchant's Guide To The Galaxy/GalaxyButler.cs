using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Merchant_s_Guide_To_The_Galaxy.Converter;
using Merchant_s_Guide_To_The_Galaxy.CustomException;
using Merchant_s_Guide_To_The_Galaxy.Rules;

namespace Merchant_s_Guide_To_The_Galaxy
{
    public class GalaxyButler
    {
        private readonly IRomanSymbolConverter _romanSymbolConverter;

        public GalaxyButler()
        {
            _romanSymbolConverter = new RomanSymbolConverter();
        }

        public void ProcessInput(string path)
        {
            var goodsNameSymbols = new Dictionary<string, char>();
            var goodsNamePrices = new Dictionary<string, decimal>();
            using (var streamReader = new StreamReader(path))
            {
                while (streamReader.Peek() >= 0)
                {
                    var line = streamReader.ReadLine();
                    
                    if (line.EndsWith("?"))
                    {
                        if (goodsNameSymbols.Count == 0) throw new NotFindAnySymbolException();
                        if (goodsNamePrices.Count == 0) throw new NotFindAnyPriceException();

                        var howMuch = "how much is";
                        var howMany = "how many Credits is";

                        if (line.StartsWith(howMuch))
                        {
                            Console.WriteLine(HowMuchOutPut(line,howMuch,goodsNameSymbols));
                        }
                        else if (line.StartsWith(howMany))
                        {
                            Console.WriteLine(HowManyOutPut(line,howMany,goodsNameSymbols,goodsNamePrices));
                        }
                        else
                        {
                            throw new NotKnowMeansException();
                        }
                    }
                    else
                    {
                        InitData(line,goodsNameSymbols,goodsNamePrices);
                    }
                }
            }
        }

        private int GetGoodsNumberFromSymbols(string[] symbols)
        {
            var rules = GetRules();
            return _romanSymbolConverter.ConvertToNumber(string.Join("",symbols), rules);
        }

        private IEnumerable<IRule> GetRules()
        {
            yield return new RuleSymbolOnlyRepeatThreeTimes();
            yield return new RuleStartWithLargest();
        }

        private void InitData(string line, Dictionary<string, char> goodsNameSymbols,
            Dictionary<string, decimal> goodsNamePrices)
        {
            var contentArr = line.Split(' ');
            if (contentArr.Count(text => text == "is") != 1) throw new ParseTextException();

            for (var i = 0; i < contentArr.Length; i++)
            {
                if (contentArr[i] != "is") continue;
                char value;
                if (char.TryParse(contentArr[i + 1].Trim(), out value))
                {
                    if (!Constant.RomanSymbolNumbers.Keys.Contains(value)) throw new InvalidSymbolException();
                    goodsNameSymbols.Add(contentArr[0], value);
                }
                else
                {
                    var goodsName = contentArr[i - 1];
                    var goodsInfo = line.Substring(0, line.IndexOf(goodsName, StringComparison.Ordinal)).Trim();

                    if (goodsNameSymbols.Count == 0) throw new NotFindAnySymbolException();

                    goodsInfo = goodsNameSymbols.Aggregate(goodsInfo, (current, kv) => current.Replace(kv.Key, kv.Value.ToString(CultureInfo.InvariantCulture)));
                    var goodsNumber = GetGoodsNumberFromSymbols(goodsInfo.Split(' '));

                    decimal totalPrice;
                    var priceText = contentArr[i + 1].Split(' ').First().Trim();
                    if (!decimal.TryParse(priceText, out totalPrice)) throw new InvalidGoodsPriceException();
                    goodsNamePrices.Add(goodsName, totalPrice / goodsNumber);
                }
            }
        }

        private string HowMuchOutPut(string line, string howMuch,Dictionary<string, char> goodsNameSymbols)
        {
            var expression = line.Substring(howMuch.Length, line.Length - howMuch.Length - 1).Trim();
            var text = expression.Clone();
            foreach (var name in expression.Split(' '))
            {
                if (goodsNameSymbols.Keys.Contains(name))
                {
                    expression = expression.Replace(name, goodsNameSymbols[name].ToString());
                }
            }
            var number = _romanSymbolConverter.ConvertToNumber(expression, GetRules());
            return string.Format("{0} is {1}", text, number.ToString("F"));
        }

        private string HowManyOutPut(string line, string howMany, 
            Dictionary<string, char> goodsNameSymbols, 
            Dictionary<string, decimal> goodsNamePrices)
        {

            var goodsNames = line.Replace("?", "").Trim();
            var goodsNameList = goodsNames.Split(' ').ToList();
            var goodsName = goodsNameList.Last().Trim();
            goodsNameList.RemoveAt(goodsNameList.Count - 1);
            var preExpression = string.Join(" ", goodsNameList);

            var expression = preExpression.Substring(howMany.Length, preExpression.Length - howMany.Length).Trim();
            var text = expression.Clone();
            foreach (var name in expression.Split(' '))
            {
                if (goodsNameSymbols.Keys.Contains(name))
                {
                    expression = expression.Replace(name, goodsNameSymbols[name].ToString());
                }
            }
            var number = _romanSymbolConverter.ConvertToNumber(expression, GetRules());
            var price = goodsNamePrices[goodsName]*number;
            return string.Format("{0} {1} is {2} Credits",text, goodsName, price.ToString("F"));
        }
    }
}
