using System;

namespace StudentApp.Api.Exceptions
{
    public class StudentException : Exception
    {
        public StudentException(string message) : base(message)
        {
        }

        public StudentException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}