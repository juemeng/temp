using System;

namespace Merchant_s_Guide_To_The_Galaxy.CustomException
{
    public class InvalidSymbolException : Exception
    {
        public override string Message
        {
            get { return "Invalid roman symbol input"; }
        }
    }
}