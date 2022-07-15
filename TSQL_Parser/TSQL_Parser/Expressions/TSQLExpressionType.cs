using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Expressions
{
	public enum TSQLExpressionType
	{
		/// <summary>
		///		i.e. a CASE/WHEN expression
		/// </summary>
		Case,

		Column,

		Function,

		Subquery,

		/// <summary>
		///		e.g. @productId
		/// </summary>
		Variable,

		/// <summary>
		///		e.g. * or p.*
		/// </summary>
		Multicolumn,

		Operation,

		/// <summary>
		///		i.e. an expression surrounded by parenthesis, but not containing a subquery
		/// </summary>
		Grouped,

		/// <summary>
		///		e.g. 'Active' or 3.14
		/// </summary>
		Constant,

		/// <summary>
		///		e.g. AND, OR, or NOT
		/// </summary>
		Logical,

		/// <summary>
		///		e.g. @ProductID = p.id
		/// </summary>
		VariableAssignment,

		/// <summary>
		///		e.g. 123.45 AS INT (only used as an argument to CAST function)
		/// </summary>
		ValueAsType,

		/// <summary>
		///		e.g. NULL
		/// </summary>
		Null,

		/// <summary>
		///		e.g. DISTINCT or ALL
		/// </summary>
		DuplicateSpecification
	}
}
