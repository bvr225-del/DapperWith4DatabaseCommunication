using System.Globalization;

namespace DapperWith4DatabaseCommunication.MiddleWares
{
    //Exception class is the base class for all exceptions in C#. By creating a custom exception class that inherits from Exception, you can define your own exception types and add additional properties or methods as needed.
    public class AppException : Exception
    {
        //this is one constructor for the AppException class that takes no parameters and simply calls the base constructor of the Exception class. This allows you to create an instance of AppException without providing any additional information.
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        protected AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
