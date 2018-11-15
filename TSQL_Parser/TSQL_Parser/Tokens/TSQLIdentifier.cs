using System;

namespace TSQL.Tokens
{
	public class TSQLIdentifier : TSQLToken
	{
		internal TSQLIdentifier(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			if (Text.StartsWith("["))
			{
				Name = Text
					.Substring(1, Text.Length - 2)
					.Replace("]]", "]");
			}
			// check for quoted identifiers here just in case
			else if (Text.StartsWith("\""))
			{
				Name = Text
					.Substring(1, Text.Length - 2)
					.Replace("\"\"", "\"");
			}
			else if (Text.StartsWith("N\""))
			{
				Name = Text
					.Substring(2, Text.Length - 3)
					.Replace("\"\"", "\"");
			}
			else
			{
				Name = Text;
			}
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.Identifier;
			}
		}

#pragma warning restore 1591

		/// <summary>
		///		Unescaped value for the name of the identifier.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}
	}
}
