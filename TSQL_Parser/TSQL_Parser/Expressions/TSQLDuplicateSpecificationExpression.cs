using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions
{
	public class TSQLDuplicateSpecificationExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.DuplicateSpecification;
			}
		}

		public TSQLDuplicateSpecificationType DuplicateSpecificationType { get; internal set; }

		public TSQLExpression InnerExpression { get; internal set; }

		public enum TSQLDuplicateSpecificationType
		{
			Distinct,
			All
		}
	}
}
