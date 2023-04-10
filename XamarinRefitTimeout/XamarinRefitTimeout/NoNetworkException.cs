using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinRefitTimeout
{
    public class NoNetworkException : Exception
    {
        public NoNetworkException(string message) : base(message)
        {
        }
    }
}
