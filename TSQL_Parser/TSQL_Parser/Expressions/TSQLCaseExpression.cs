using System;
using System.Collections.Generic;

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

		public void AddWhenExpression(TSQLExpression when)
		{
			if (when == null) throw new ArgumentException("Should not be null", nameof(when));
			if (when.Tokens.Count == 0) throw new ArgumentException("Should have tokens", nameof(when));

			((List<TSQLExpression>)WhenExpressions).Add(when);
		}

		public bool IsSimpleCaseExpression { get; set; }

		public TSQLExpression InputExpression { get; set; }

		public IReadOnlyList<TSQLExpression> WhenExpressions { get; } = new List<TSQLExpression>();
	}


}
