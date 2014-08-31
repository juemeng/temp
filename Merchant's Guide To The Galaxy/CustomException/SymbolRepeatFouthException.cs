using System;

namespace Merchant_s_Guide_To_The_Galaxy.CustomException
{
    public class SymbolRepeatFouthException : Exception
    {
        public override string Message
        {
            get { return "Some symbols cannot repeat more than three times, please check again"; }
        }
    }
}