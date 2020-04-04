namespace TSQL.Statements
{
	public enum TSQLStatementType
	{
#pragma warning disable 1591

		Select,
		Insert,
		Update,
		Delete,
		//Execute,
		Merge,
		Unknown

#pragma warning restore 1591
	}
}