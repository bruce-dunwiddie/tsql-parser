using System;

namespace TSQL.Tokens
{
	public class TSQLIdentifier : TSQLToken
	{
		public TSQLIdentifier(
			int beginPostion,
			string text) :
			base(
				beginPostion,
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

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.Identifier;
			}
		}

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
