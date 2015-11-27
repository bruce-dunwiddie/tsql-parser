using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLKeyword : TSQLToken
	{
		public TSQLKeyword(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text)
		{
			Keyword = TSQLKeywords.Parse(text);
		}

		public TSQLKeywords Keyword
		{
			get;
			private set;
		}

		public static bool operator ==(
			TSQLKeyword a,
			TSQLKeyword b)
		{
			return
				(object)a != null &&
				a.Equals(b);
		}

		public static bool operator !=(
			TSQLKeyword a,
			TSQLKeyword b)
		{
			return
				(object)a != null &&
				!a.Equals(b);
		}

		private bool Equals(TSQLKeyword obj)
		{
			return
				(
					ReferenceEquals(this, obj)
				) ||
				(
					(object)obj != null &&
					base.Equals(obj) &&
					Keyword == obj.Keyword
				);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TSQLKeyword);
		}

		public override int GetHashCode()
		{
			// http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416
			unchecked // Overflow is fine, just wrap
			{
				int hash = 17;
				hash = hash * 486187739 + base.GetHashCode();
				hash = hash * 486187739 + Keyword.GetHashCode();
				return hash;
			}
		}
	}
}
