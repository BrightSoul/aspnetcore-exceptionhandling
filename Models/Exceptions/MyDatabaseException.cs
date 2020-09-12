using System;

namespace ExceptionDemo.Models.Exceptions
{
    public class MyDatabaseException : Exception
    {
        public MyDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}