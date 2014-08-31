using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant_s_Guide_To_The_Galaxy.Rules
{
    public interface IRule
    {
        string Process(string symbols);
    }
}
