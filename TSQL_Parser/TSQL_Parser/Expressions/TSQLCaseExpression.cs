using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Expressions
{
	/// <summary>
	/// -- Simple CASE expression:
	/// CASE input_expression
	/// WHEN when_expression THEN result_expression [ ...n ]
	/// [ ELSE else_result_expression ]
	/// END
	///
	/// -- Searched CASE expression:
	/// CASE
	/// WHEN Boolean_expression THEN result_expression [ ...n ]
	/// [ ELSE else_result_expression ]
	/// END
	/// </summary>
	public class TSQLCaseExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Case;
			}
		}

		public bool IsSimpleCaseExpression { get; set; }
	}


}
