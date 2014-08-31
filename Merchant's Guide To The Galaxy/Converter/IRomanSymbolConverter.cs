using System.Collections.Generic;
using Merchant_s_Guide_To_The_Galaxy.Rules;

namespace Merchant_s_Guide_To_The_Galaxy.Converter
{
    public interface IRomanSymbolConverter
    {
        int ConvertToNumber(string symbols, IEnumerable<IRule> rules);
    }
}
