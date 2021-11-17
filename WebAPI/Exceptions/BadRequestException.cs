using System;

namespace WebAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {
            
        }
    }
}