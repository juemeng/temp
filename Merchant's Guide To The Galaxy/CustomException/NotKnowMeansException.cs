using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant_s_Guide_To_The_Galaxy.CustomException
{
    public class NotKnowMeansException:Exception
    {
        public override string Message
        {
            get { return "I have no idea what you are talking about"; }
        }
    }
}
