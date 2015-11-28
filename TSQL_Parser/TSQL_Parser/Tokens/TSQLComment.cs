using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public abstract class TSQLComment : TSQLToken
	{
		public TSQLComment(
			int beginPostion,
			string text,
			TokenType type) :
			base(
				beginPostion,
				text,
				type)
		{

		}

		public string Comment
		{
			get;

			protected set;
		}
	}
}
