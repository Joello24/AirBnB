﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.Write(message);
        }
    }
}
