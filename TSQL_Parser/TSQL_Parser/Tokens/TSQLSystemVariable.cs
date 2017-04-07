using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLSystemVariable : TSQLVariable
	{
		public TSQLSystemVariable(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text)
		{

		}

		/*

		@@CONNECTIONS
		@@MAX_CONNECTIONS
		@@CPU_BUSY
		@@ERROR  
		@@IDENTITY
		@@IDLE
		@@IO_BUSY
		@@LANGID  
		@@LANGUAGE
		@@MAXCHARLEN
		@@PACK_RECEIVED  
		@@PACK_SENT
		@@PACKET_ERRORS
		@@ROWCOUNT  
		@@SERVERNAME 
		@@SPID
		@@TEXTSIZE 
		@@TIMETICKS
		@@TOTAL_ERRORS
		@@TOTAL_READ / @@TOTAL_WRITE
		@@TRANCOUNT
		@@VERSION  

		*/

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.SystemVariable;
			}
		}

#pragma warning restore 1591
	}
}
