using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant_s_Guide_To_The_Galaxy.Rules
{
    public class RuleStartWithLargest:IRule
    {
        public string Process(string symbols)
        {
            var symbolsArr = symbols.Split('+');
            for (var i = 0; i < symbolsArr.Length; i++)
            {
                var symbol = char.Parse(symbolsArr[i]);
                var value = Constant.RomanSymbolNumbers[symbol];
                var lessThanNext = false;
                if (i < symbolsArr.Length - 1)
                {
                    var nextValue = Constant.RomanSymbolNumbers[char.Parse(symbolsArr[i + 1])];
                    lessThanNext = value < nextValue;
                }
                if (lessThanNext)
                {
                    if (symbol == 'I' || symbol == 'X' || symbol == 'C')
                    {
                        var newSymbols = symbols.ToCharArray().ToList();
                        if (i == 0)
                        {
                            newSymbols.Insert(0, '-');
                        }
                        else
                        {
                            newSymbols[i * 2 - 1] = '-';
                        }
                        symbols = new string(newSymbols.ToArray());
                    }
                }
            }
            return symbols;
        }
    }
}
