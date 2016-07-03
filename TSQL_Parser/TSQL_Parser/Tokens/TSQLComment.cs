using System;

namespace TSQL.Tokens
{
	public abstract class TSQLComment : TSQLToken
	{
		public TSQLComment(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text)
		{

		}

		public string Comment
		{
			get;

			protected set;
		}
	}
}
