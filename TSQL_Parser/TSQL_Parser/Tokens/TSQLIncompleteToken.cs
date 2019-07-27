using System;

namespace TSQL.Tokens
{
	public abstract class TSQLIncompleteToken : TSQLToken
	{
		internal protected TSQLIncompleteToken(
			int beginPosition,
			string text) :
			base(
				beginPosition, 
				text)
		{

		}

		public override bool IsComplete
		{
			get
			{
				return false;
			}
		}
	}
}
