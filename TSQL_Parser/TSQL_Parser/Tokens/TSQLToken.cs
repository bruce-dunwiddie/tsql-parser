using System;

namespace TSQL.Tokens
{
	public abstract class TSQLToken
	{
		protected TSQLToken(
			int beginPostion,
			string text,
			TokenType type
			)
		{
			BeginPostion = beginPostion;
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			Text = text;
			Type = type;
		}

		public int BeginPostion
		{
			get;
			private set;
		}

		public int EndPosition
		{
			get
			{
				return BeginPostion + Length - 1;
			}
		}

		public int Length
		{
			get
			{
				return Text.Length;		
			}
		}

		public string Text
		{
			get;
			private set;
		}

		public TokenType Type
		{
			get;
			private set;
		}

		public static bool operator ==(
			TSQLToken a,
			TSQLToken b)
		{
			return
				(object)a != null &&
				a.Equals(b);
        }

		public static bool operator !=(
			TSQLToken a,
			TSQLToken b)
		{
			return
				(object)a == null ||
				!a.Equals(b);
		}

		private bool Equals(TSQLToken obj)
		{
			return
				(
					ReferenceEquals(this, obj)
				) ||
				(
					(object) obj != null &&
					BeginPostion == obj.BeginPostion &&
					EndPosition == obj.EndPosition &&
					Text == obj.Text &&
					GetType() == obj.GetType()
				);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TSQLToken);
		}

		public override int GetHashCode()
		{
			// http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416
			unchecked // Overflow is fine, just wrap
			{
				int hash = 17;
				hash = hash * 486187739 + BeginPostion.GetHashCode();
				hash = hash * 486187739 + Text.GetHashCode();
				hash = hash * 486187739 + Type.GetHashCode();
				return hash;
			}
		}
	}
}
