using System;

namespace AutoSmartTechAPI.UserComm
{
    public static class ExceptionsAndLogging
    {
        public static Exception NullExceptionsLogging(object message)
        {
             Exception exception = null; 
            if (message == null )
            {
                Console.WriteLine(message + " is null");
                throw  new  ArgumentNullException(nameof(message));
            }
            return exception;
        }

        public static void CatchExceptionAndLogging(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw ex;
            
        }
    }
}
