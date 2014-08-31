using System;

namespace Merchant_s_Guide_To_The_Galaxy.CustomException
{
    public class ParseTextException : Exception
    {
        public override string Message
        {
            get { return "Program cannot parse input text!"; }
        }
    }
}