using System;

namespace TSQL.Tokens
{
	public abstract class TSQLComment : TSQLToken
	{
		internal protected TSQLComment(
			int beginPosition,
			string text) :
			base(
				beginPosition,
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
