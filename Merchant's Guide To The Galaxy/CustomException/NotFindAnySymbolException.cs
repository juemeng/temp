﻿using System;

namespace Merchant_s_Guide_To_The_Galaxy.CustomException
{
    public class NotFindAnySymbolException : Exception
    {
        public override string Message
        {
            get { return "please input valid sysbols first"; }
        }
    }
}