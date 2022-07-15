using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Elements;

namespace TSQL.Expressions
{
	public abstract class TSQLExpression : TSQLElement
	{
		public abstract TSQLExpressionType Type
		{
			get;
		}

		// TODO: populate Expressions list for all expressions recursively

		//public List<TSQLExpression> Expressions
		//{
		//	get;

		//	internal set;
		//}

		public TSQLCaseExpression AsCase
		{
			get
			{
				return this as TSQLCaseExpression;
			}
		}

		public TSQLColumnExpression AsColumn
		{
			get
			{
				return this as TSQLColumnExpression;
			}
		}

		public TSQLFunctionExpression AsFunction
		{
			get
			{
				return this as TSQLFunctionExpression;
			}
		}

		public TSQLSubqueryExpression AsSubquery
		{
			get
			{
				return this as TSQLSubqueryExpression;
			}
		}

		public TSQLVariableExpression AsVariable
		{
			get
			{
				return this as TSQLVariableExpression;
			}
		}

		public TSQLMulticolumnExpression AsMulticolumn
		{
			get
			{
				return this as TSQLMulticolumnExpression;
			}
		}

		public TSQLOperationExpression AsOperation
		{
			get
			{
				return this as TSQLOperationExpression;
			}
		}

		public TSQLGroupedExpression AsGrouped
		{
			get
			{
				return this as TSQLGroupedExpression;
			}
		}

		public TSQLConstantExpression AsConstant
		{
			get
			{
				return this as TSQLConstantExpression;
			}
		}

		//public TSQLLogicalExpression AsLogical
		//{
		//	get
		//	{
		//		return this as TSQLLogicalExpression;
		//	}
		//}

		public TSQLVariableAssignmentExpression AsVariableAssignment
		{
			get
			{
				return this as TSQLVariableAssignmentExpression;
			}
		}

		public TSQLValueAsTypeExpression AsValueAsType
		{
			get
			{
				return this as TSQLValueAsTypeExpression;
			}
		}

		public TSQLNullExpression AsNull
		{
			get
			{
				return this as TSQLNullExpression;
			}
		}

		public TSQLDuplicateSpecificationExpression AsDuplicateSpecification
		{
			get
			{
				return this as TSQLDuplicateSpecificationExpression;
			}
		}
	}
}
