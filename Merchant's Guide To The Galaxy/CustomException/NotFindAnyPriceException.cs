using System;

namespace Merchant_s_Guide_To_The_Galaxy.CustomException
{
    public class NotFindAnyPriceException : Exception
    {
        public override string Message
        {
            get { return "please input valid price first"; }
        }
    }
}