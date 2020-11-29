using NUnit.Framework;
using System.Collections.Generic;
using TSQL;
using TSQL.Statements;

namespace Tests.Statements
{
	[TestFixture(Category = "Statement Parsing")]
	public class MergeStatementTests
	{
		[Test]
		public void MergeStatement_StandardMerge_UsingValues()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				MERGE INTO [t].[a] AS [Target]
				USING (VALUES
				  (1,N'v1',NULL)
				 ,(2,N'v2',NULL)
				) AS [Source] ([ID],[Val])
				ON ([Target].[ID] = [Source].[ID])
				WHEN MATCHED AND (
					NULLIF([Source].[Val], [Target].[Val]) IS NOT NULL OR NULLIF([Target].[Val], [Source].[Val]) IS NOT NULL) THEN
				 UPDATE SET
				  [Target].[Val] = [Source].[Val],
				WHEN NOT MATCHED BY TARGET THEN
				 INSERT([ID],[Val])
				 VALUES([Source].[ID],[Source].[Val])
				WHEN NOT MATCHED BY SOURCE THEN
				 DELETE
				OUTPUT
				  $ACTION AS [Action]
				 ,DELETED.[ID]
				 ,DELETED.[Val]
				 ,INSERTED.[ID]
				 ,INSERTED.[Val];",
				includeWhitespace: true);
			TSQLMergeStatement merge = statements[0] as TSQLMergeStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Merge, statements[0].Type);
			Assert.AreEqual(195, merge.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.MERGE, merge.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual(TSQLKeywords.INTO, merge.Tokens[2].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[3].AsWhitespace.Text);
			Assert.AreEqual("t", merge.Tokens[4].AsIdentifier.Name);
			Assert.AreEqual(2, merge.Merge.Tokens.Count);
			Assert.AreEqual(10, merge.Into.Tokens.Count);
			Assert.AreEqual(34, merge.Using.Tokens.Count);
			Assert.AreEqual(14, merge.On.Tokens.Count);
			Assert.AreEqual(3, merge.When.Count);
			Assert.AreEqual(64, merge.When[0].Tokens.Count);
			Assert.AreEqual(30, merge.When[1].Tokens.Count);
			Assert.AreEqual(14, merge.When[2].Tokens.Count);
			Assert.AreEqual(27, merge.Output.Tokens.Count);
		}

		[Test]
		public void MergeStatement_StandardMerge_UsingSelect()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				Merge into myTargetTable as tab1 
				using(select * from mySourceTable) as tab2 
				   on tab1.PrimaryID=tab2.PrimaryID 
				when matched then 
				   update set 
				   tab1.someColumn1=tab2.someColumn1, 
				   tab1.someColumn2=tab2.someColumn2, 
				   tab1.someColumn3=tab2.someColumn3, 
				   tab1.someColumn4=tab2.someColumn4, 
				   tab1.someColumn5=tab2.someColumn5 
				when not matched then 
				   insert values(tab2.PrimaryID,tab2.someColumn1,tab2.someColumn2,tab2.someColumn3,tab2.someColumn4,tab2.someColumn5); ",
				includeWhitespace: true);
			TSQLMergeStatement merge = statements[0] as TSQLMergeStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Merge, merge.Type);
			Assert.AreEqual(125, merge.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.MERGE, merge.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual(TSQLKeywords.INTO, merge.Tokens[2].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[3].AsWhitespace.Text);
			Assert.AreEqual("myTargetTable", merge.Tokens[4].AsIdentifier.Name);
			Assert.AreEqual(2, merge.Merge.Tokens.Count);
			Assert.AreEqual(8, merge.Into.Tokens.Count);
			Assert.AreEqual(15, merge.Using.Tokens.Count);
			Assert.AreEqual(10, merge.On.Tokens.Count);
			Assert.AreEqual(2, merge.When.Count);
			Assert.AreEqual(54, merge.When[0].Tokens.Count);
			Assert.AreEqual(36, merge.When[1].Tokens.Count);
			Assert.IsNull(merge.Output);
		}

		[Test]
		public void MergeStatement_StandardMerge_UsingSelectFromCTE()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				WITH myCTE (cteColumn1, cteColumn2)
				AS
				(
					SELECT column1, column2
					FROM cteSourceTable
					WHERE column2 IS NOT NULL
				)
				MERGE INTO [t].[a] AS [Target]
				USING (
				  SELECT cteColumn1, cteColumn2
				  FROM myCTE
				) AS [Source]
				ON ([Target].[ID] = [Source].[cteColumn1])
				WHEN MATCHED AND (
					NULLIF([Source].[cteColumn2], [Target].[Val]) IS NOT NULL OR NULLIF([Target].[Val], [Source].[cteColumn2]) IS NOT NULL) THEN
				 UPDATE SET
				  [Target].[Val] = [Source].[cteColumn2]
				WHEN NOT MATCHED BY TARGET THEN
				 INSERT([ID],[Val])
				 VALUES([Source].[cteColumn1],[Source].[cteColumn2])
				WHEN NOT MATCHED BY SOURCE THEN
				 DELETE
				OUTPUT
				  $ACTION AS [Action]
				 ,DELETED.[ID]
				 ,DELETED.[Val]
				 ,INSERTED.[ID]
				 ,INSERTED.[Val];",
				includeWhitespace: true);
			TSQLMergeStatement merge = statements[0] as TSQLMergeStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Merge, statements[0].Type);
			Assert.AreEqual(219, merge.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.WITH, merge.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(TSQLKeywords.MERGE, merge.Tokens[38].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[39].AsWhitespace.Text);
			Assert.AreEqual(TSQLKeywords.INTO, merge.Tokens[40].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[41].AsWhitespace.Text);
			Assert.AreEqual("t", merge.Tokens[42].AsIdentifier.Name);
			Assert.AreEqual(38, merge.With.Tokens.Count);
			Assert.AreEqual(2, merge.Merge.Tokens.Count);
			Assert.AreEqual(10, merge.Into.Tokens.Count);
			Assert.AreEqual(21, merge.Using.Tokens.Count);
			Assert.AreEqual(14, merge.On.Tokens.Count);
			Assert.AreEqual(3, merge.When.Count);
			Assert.AreEqual(63, merge.When[0].Tokens.Count);
			Assert.AreEqual(30, merge.When[1].Tokens.Count);
			Assert.AreEqual(14, merge.When[2].Tokens.Count);
			Assert.AreEqual(27, merge.Output.Tokens.Count);
		}

		[Test]
		public void MergeStatement_StandardMerge_WhenNotMatched()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				MERGE INTO [t].[a] AS [Target]
				USING (VALUES
				  (1,N'v1',NULL)
				 ,(2,N'v2',NULL)
				) AS [Source] ([ID],[Val])
				ON ([Target].[ID] = [Source].[ID])
				WHEN MATCHED AND (
					NULLIF([Source].[Val], [Target].[Val]) IS NOT NULL OR NULLIF([Target].[Val], [Source].[Val]) IS NOT NULL) THEN
				 UPDATE SET
				  [Target].[Val] = [Source].[Val],
				WHEN NOT MATCHED THEN
				 INSERT([ID],[Val])
				 VALUES([Source].[ID],[Source].[Val])
				WHEN NOT MATCHED BY SOURCE THEN
				 DELETE
				OUTPUT
				  $ACTION AS [Action]
				 ,DELETED.[ID]
				 ,DELETED.[Val]
				 ,INSERTED.[ID]
				 ,INSERTED.[Val];",
				includeWhitespace: true);
			TSQLMergeStatement merge = statements[0] as TSQLMergeStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Merge, statements[0].Type);
			Assert.AreEqual(191, merge.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.MERGE, merge.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual(TSQLKeywords.INTO, merge.Tokens[2].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[3].AsWhitespace.Text);
			Assert.AreEqual("t", merge.Tokens[4].AsIdentifier.Name);
			Assert.AreEqual(2, merge.Merge.Tokens.Count);
			Assert.AreEqual(10, merge.Into.Tokens.Count);
			Assert.AreEqual(34, merge.Using.Tokens.Count);
			Assert.AreEqual(14, merge.On.Tokens.Count);
			Assert.AreEqual(3, merge.When.Count);
			Assert.AreEqual(64, merge.When[0].Tokens.Count);
			Assert.AreEqual(26, merge.When[1].Tokens.Count);
			Assert.AreEqual(14, merge.When[2].Tokens.Count);
			Assert.AreEqual(27, merge.Output.Tokens.Count);
		}

		[Test]
		public void MergeStatement_StandardMerge_NoData()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				MERGE INTO [t].[a] AS [Target]
				USING (
				  SELECT [ID],[Val] FROM [t].[a] WHERE 1 = 0 -- Empty dataset
				) AS [Source] ([ID],[Val])
				ON ([Target].[ID] = [Source].[ID])
				WHEN MATCHED AND (
					NULLIF([Source].[Val], [Target].[Val]) IS NOT NULL OR NULLIF([Target].[Val], [Source].[Val]) IS NOT NULL) THEN
				 UPDATE SET
				  [Target].[Val] = [Source].[Val],
				WHEN NOT MATCHED BY TARGET THEN
				 INSERT([ID],[Val])
				 VALUES([Source].[ID],[Source].[Val])
				WHEN NOT MATCHED BY SOURCE THEN
				 DELETE
				OUTPUT
				  $ACTION AS [Action]
				 ,DELETED.[ID]
				 ,DELETED.[Val]
				 ,INSERTED.[ID]
				 ,INSERTED.[Val];",
				includeWhitespace: true);
			TSQLMergeStatement merge = statements[0] as TSQLMergeStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Merge, statements[0].Type);
			Assert.AreEqual(199, merge.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.MERGE, merge.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual(TSQLKeywords.INTO, merge.Tokens[2].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[3].AsWhitespace.Text);
			Assert.AreEqual("t", merge.Tokens[4].AsIdentifier.Name);
			Assert.AreEqual(2, merge.Merge.Tokens.Count);
			Assert.AreEqual(10, merge.Into.Tokens.Count);
			Assert.AreEqual(38, merge.Using.Tokens.Count);
			Assert.AreEqual(14, merge.On.Tokens.Count);
			Assert.AreEqual(3, merge.When.Count);
			Assert.AreEqual(64, merge.When[0].Tokens.Count);
			Assert.AreEqual(30, merge.When[1].Tokens.Count);
			Assert.AreEqual(14, merge.When[2].Tokens.Count);
			Assert.AreEqual(27, merge.Output.Tokens.Count);
		}

		[Test]
		public void MergeStatement_SpGenerateMerge()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				---
				--MERGE generated by 'sp_generate_merge' stored procedure
				--Originally by Vyas (http://vyaskn.tripod.com/code): sp_generate_inserts (build 22)
				--Adapted for SQL Server 2008+ by Daniel Nolan (https://twitter.com/dnlnln)

				SET NOCOUNT ON

				SET IDENTITY_INSERT [t].[a] ON

				MERGE INTO [t].[a] AS [Target]
				USING (VALUES
				  (1,N'v1',NULL)
				 ,(2,N'v2',NULL)
				) AS [Source] ([ID],[Val])
				ON ([Target].[ID] = [Source].[ID])
				WHEN MATCHED AND (
					NULLIF([Source].[Val], [Target].[Val]) IS NOT NULL OR NULLIF([Target].[Val], [Source].[Val]) IS NOT NULL) THEN
				 UPDATE SET
				  [Target].[Val] = [Source].[Val],
				WHEN NOT MATCHED BY TARGET THEN
				 INSERT([ID],[Val])
				 VALUES([Source].[ID],[Source].[Val])
				WHEN NOT MATCHED BY SOURCE THEN
				 DELETE;

				DECLARE @mergeError int
				 , @mergeCount int
				SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
				IF @mergeError != 0
				 BEGIN
				 PRINT 'ERROR OCCURRED IN MERGE FOR [t].[a]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
				 END
				ELSE
				 BEGIN
				 PRINT '[t].[a] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
				 END
				GO



				SET IDENTITY_INSERT [t].[a] OFF
				SET NOCOUNT OFF
				GO",
				includeWhitespace: true);
			TSQLMergeStatement merge = statements[2] as TSQLMergeStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(17, statements.Count);

			Assert.AreEqual(TSQLStatementType.Merge, statements[2].Type);
			Assert.AreEqual(TSQLStatementType.Select, statements[4].Type);
			Assert.AreEqual(6, statements[0].Tokens.Count);
			Assert.AreEqual(10, statements[1].Tokens.Count);
			Assert.AreEqual(167, merge.Tokens.Count);
			Assert.AreEqual(12, statements[3].Tokens.Count);
			Assert.AreEqual(15, statements[4].Tokens.Count);
			Assert.AreEqual(8, statements[5].Tokens.Count);
			Assert.AreEqual(2, statements[6].Tokens.Count);
			Assert.AreEqual(17, statements[7].Tokens.Count);
			Assert.AreEqual(2, statements[8].Tokens.Count);
			Assert.AreEqual(2, statements[9].Tokens.Count);
			Assert.AreEqual(2, statements[10].Tokens.Count);
			Assert.AreEqual(17, statements[11].Tokens.Count);
			Assert.AreEqual(2, statements[12].Tokens.Count);
			Assert.AreEqual(2, statements[13].Tokens.Count);
			Assert.AreEqual(10, statements[14].Tokens.Count);
			Assert.AreEqual(6, statements[15].Tokens.Count);
			Assert.AreEqual(1, statements[16].Tokens.Count);
			Assert.AreEqual(TSQLKeywords.MERGE, merge.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual(TSQLKeywords.INTO, merge.Tokens[2].AsKeyword.Keyword);
			Assert.AreEqual(" ", merge.Tokens[3].AsWhitespace.Text);
			Assert.AreEqual("t", merge.Tokens[4].AsIdentifier.Name);
			Assert.AreEqual(2, merge.Merge.Tokens.Count);
			Assert.AreEqual(10, merge.Into.Tokens.Count);
			Assert.AreEqual(34, merge.Using.Tokens.Count);
			Assert.AreEqual(14, merge.On.Tokens.Count);
			Assert.AreEqual(3, merge.When.Count);
			Assert.AreEqual(64, merge.When[0].Tokens.Count);
			Assert.AreEqual(30, merge.When[1].Tokens.Count);
			Assert.AreEqual(13, merge.When[2].Tokens.Count);
			Assert.IsNull(merge.Output);
		}
	}
}