using System;
using System.Linq;
using TSQL.Expressions;

namespace TSQL
{
    public class TSQLParseException: Exception
    {
        public TSQLParseException(string message): base(message)
        {

        }

        public TSQLParseException(string message, TSQLExpression expr) : base(
            message + " \n..." + string.Join(" ", expr.Tokens.Select(t => t.Text)) + "[error]")
        {

        }
    }
}