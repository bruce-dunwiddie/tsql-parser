using System;

namespace TSQL
{
    public class TSQLParseException: Exception
    {
        public TSQLParseException(string message): base(message)
        {
            
        }
    }
}