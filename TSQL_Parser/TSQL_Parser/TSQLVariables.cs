using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL
{
	public struct TSQLVariables
	{
		private static Dictionary<string, TSQLVariables> variableLookup =
			new Dictionary<string, TSQLVariables>(StringComparer.InvariantCultureIgnoreCase);

		public static readonly TSQLVariables None = new TSQLVariables("");

#pragma warning disable 1591

		public static readonly TSQLVariables CONNECTIONS = new TSQLVariables("@@CONNECTIONS");
		public static readonly TSQLVariables MAX_CONNECTIONS = new TSQLVariables("@@MAX_CONNECTIONS");
		public static readonly TSQLVariables CPU_BUSY = new TSQLVariables("@@CPU_BUSY");
		public static readonly TSQLVariables ERROR = new TSQLVariables("@@ERROR");
		public static readonly TSQLVariables IDENTITY = new TSQLVariables("@@IDENTITY");
		public static readonly TSQLVariables IDLE = new TSQLVariables("@@IDLE");
		public static readonly TSQLVariables IO_BUSY = new TSQLVariables("@@IO_BUSY");
		public static readonly TSQLVariables LANGID = new TSQLVariables("@@LANGID");
		public static readonly TSQLVariables LANGUAGE = new TSQLVariables("@@LANGUAGE");
		public static readonly TSQLVariables MAXCHARLEN = new TSQLVariables("@@MAXCHARLEN");
		public static readonly TSQLVariables PACK_RECEIVED = new TSQLVariables("@@PACK_RECEIVED");
		public static readonly TSQLVariables PACK_SENT = new TSQLVariables("@@PACK_SENT");
		public static readonly TSQLVariables PACKET_ERRORS = new TSQLVariables("@@PACKET_ERRORS");
		public static readonly TSQLVariables ROWCOUNT = new TSQLVariables("@@ROWCOUNT");
		public static readonly TSQLVariables SERVERNAME = new TSQLVariables("@@SERVERNAME");
		public static readonly TSQLVariables SPID = new TSQLVariables("@@SPID");
		public static readonly TSQLVariables TEXTSIZE = new TSQLVariables("@@TEXTSIZE");
		public static readonly TSQLVariables TIMETICKS = new TSQLVariables("@@TIMETICKS");
		public static readonly TSQLVariables TOTAL_ERRORS = new TSQLVariables("@@TOTAL_ERRORS");
		public static readonly TSQLVariables TOTAL_READ = new TSQLVariables("@@TOTAL_READ");
		public static readonly TSQLVariables TOTAL_WRITE = new TSQLVariables("@@TOTAL_WRITE");
		public static readonly TSQLVariables TRANCOUNT = new TSQLVariables("@@TRANCOUNT");
		public static readonly TSQLVariables VERSION = new TSQLVariables("@@VERSION");

#pragma warning restore 1591

		private readonly string Variable;

		private TSQLVariables(
			string variable)
		{
			Variable = variable;
			if (variable.Length > 0)
			{
				variableLookup[variable] = this;
			}
		}

		public static TSQLVariables Parse(
			string token)
		{
			if (
				!string.IsNullOrEmpty(token) &&
				variableLookup.ContainsKey(token))
			{
				return variableLookup[token];
			}
			else
			{
				return TSQLVariables.None;
			}
		}

		public static bool IsVariable(
			string token)
		{
			if (!string.IsNullOrWhiteSpace(token))
			{
				return variableLookup.ContainsKey(token);
			}
			else
			{
				return false;
			}
		}

		public bool In(params TSQLVariables[] variables)
		{
			return
				variables != null &&
				variables.Contains(this);
		}

#pragma warning disable 1591

		public static bool operator ==(
			TSQLVariables a,
			TSQLVariables b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(
			TSQLVariables a,
			TSQLVariables b)
		{
			return !(a == b);
		}

		public bool Equals(TSQLVariables obj)
		{
			return Variable == obj.Variable;
		}

		public override bool Equals(object obj)
		{
			if (obj is TSQLVariables)
			{
				return Equals((TSQLVariables)obj);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return Variable.GetHashCode();
		}

#pragma warning restore 1591
	}
}
