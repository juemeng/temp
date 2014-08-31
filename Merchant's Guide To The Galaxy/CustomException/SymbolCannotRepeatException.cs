using System;

namespace Merchant_s_Guide_To_The_Galaxy.CustomException
{
    public class SymbolCannotRepeatException : Exception
    {
        public override string Message
        {
            get { return "Some symbols cannot repeat, please check again"; }
        }
    }
}