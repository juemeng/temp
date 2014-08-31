using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant_s_Guide_To_The_Galaxy.Tools
{
    public static class Tools
    {
        public static bool CheckRepeatFouth(string symbols, params char[] chars)
        {
            var flag = false;
            for (var i = 0; i < chars.Length; i++)
            {
                var index = symbols.IndexOf(chars[i]);
                if (index == symbols.Length - 1 || index == symbols.Length - 2 || index == symbols.Length - 3)
                {
                    flag = false;
                    break;
                }
                flag = symbols[index + 1] == chars[i] && symbols[index + 2] == chars[i] && symbols[index + 3] == chars[i];
                if (flag) break;
            }
            return flag;
        }

        public static bool CheckRepeat(string symbols, params char[] chars)
        {
            return chars.Any(t => symbols.Count(c => c == t) > 1);
        }

        public static int CalculateExpression(string expression)
        {
            try
            {
                return Convert.ToInt32(new DataTable().Compute(expression, null));
            }
            catch (Exception)
            {
                throw new Exception("Invalid expression");
            }
            
        }
    }
}
