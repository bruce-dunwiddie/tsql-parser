using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public abstract class TSQLToken
	{
		protected TSQLToken(
			int beginPostion,
			int endPosition,
			string text)
		{
			BeginPostion = beginPostion;
			EndPosition = endPosition;
			Text = text;
		}

		public int BeginPostion
		{
			get;
			private set;
		}

		public int EndPosition
		{
			get;
			private set;
		}

		public int Length
		{
			get
			{
				return EndPosition - BeginPostion + 1;		
			}
		}

		public string Text
		{
			get;
			private set;
		}
	}
}
