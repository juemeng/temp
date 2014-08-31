using System;
using System.Collections.Generic;
using System.Linq;
using Merchant_s_Guide_To_The_Galaxy.Rules;

namespace Merchant_s_Guide_To_The_Galaxy.Converter
{
    public class RomanSymbolConverter:IRomanSymbolConverter
    {
        public int ConvertToNumber(string symbols,IEnumerable<IRule> rules)
        {
            symbols = symbols.Replace(" ", "");
            symbols = rules.Aggregate(symbols, (current, rule) => rule.Process(current));
            foreach (var c in symbols.Where(c => Constant.RomanSymbolNumbers.Keys.Contains(c)))
            {
                symbols = symbols.Replace(c.ToString(), Constant.RomanSymbolNumbers[c].ToString());
            }
            return Tools.Tools.CalculateExpression(symbols);
        }
    }
}
