namespace TSQL.Clauses
{
	public class TSQLOutputClause : TSQLClause
	{
		internal TSQLOutputClause()
		{

		}

		public TSQLIntoClause Into { get; internal set; }
	}
}