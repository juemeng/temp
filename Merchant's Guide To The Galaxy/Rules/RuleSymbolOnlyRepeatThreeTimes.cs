using System.Linq;
using Merchant_s_Guide_To_The_Galaxy.CustomException;

namespace Merchant_s_Guide_To_The_Galaxy.Rules
{
    public class RuleSymbolOnlyRepeatThreeTimes:IRule
    {
        public string Process(string symbols)
        {

            if (Tools.Tools.CheckRepeatFouth(symbols,'I','X','C')) throw new SymbolRepeatFouthException();
            if (Tools.Tools.CheckRepeat(symbols, 'D', 'L', 'V')) throw new SymbolCannotRepeatException();
                

            var expression = string.Join("+", symbols.ToCharArray());
            return expression;
        }
    }
}
