έ
iC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLOptionClauseParser.cs
	namespace 	
TSQL
 
. 
Clauses 
. 
Parsers 
{		 
internal

 	
class


 "
TSQLOptionClauseParser

 &
:

' (
ITSQLClauseParser

) :
{ 
public 
TSQLOptionClause	 
Parse 
(  
ITSQLTokenizer  .
	tokenizer/ 8
)8 9
{ 
TSQLOptionClause 
option 
= 
new  
TSQLOptionClause! 1
(1 2
)2 3
;3 4
if 
( 
! 
	tokenizer 
. 
Current 
. 
	IsKeyword #
(# $
TSQLKeywords$ 0
.0 1
OPTION1 7
)7 8
)8 9
{ 
throw 	
new
  
ApplicationException "
(" #
$str# 5
)5 6
;6 7
} 
option 	
.	 

Tokens
 
. 
Add 
( 
	tokenizer 
. 
Current &
)& '
;' (
while 
(	 

	tokenizer 
. 
MoveNext 
( 
) 
&& 
! 
	tokenizer 
. 
Current 
. 
IsCharacter "
(" #
TSQLCharacters# 1
.1 2
OpenParentheses2 A
)A B
)B C
{ 
option 

.
 
Tokens 
. 
Add 
( 
	tokenizer 
.  
Current  '
)' (
;( )
} 
do 
{ 
if   
(   
	tokenizer   
.   
Current   
!=   
null   !
)  ! "
{!! 
option"" 
."" 
Tokens"" 
."" 
Add"" 
("" 
	tokenizer""  
.""  !
Current""! (
)""( )
;"") *
}## 
}$$ 
while$$ 

($$ 
	tokenizer%% 
.%% 
MoveNext%% 
(%% 
)%% 
&&%% 
!&& 
	tokenizer&& 
.&& 
Current&& 
.&& 
IsCharacter&& "
(&&" #
TSQLCharacters&&# 1
.&&1 2
CloseParentheses&&2 B
)&&B C
)&&C D
;&&D E
if(( 
((( 
	tokenizer(( 
.(( 
Current(( 
!=(( 
null((  
)((  !
{)) 
option** 

.**
 
Tokens** 
.** 
Add** 
(** 
	tokenizer** 
.**  
Current**  '
)**' (
;**( )
	tokenizer++ 
.++ 
MoveNext++ 
(++ 
)++ 
;++ 
},, 
return.. 	
option..
 
;.. 
}// 

TSQLClause11 
ITSQLClauseParser11 
.11 
Parse11 $
(11$ %
ITSQLTokenizer11% 3
	tokenizer114 =
)11= >
{22 
return33 	
Parse33
 
(33 
	tokenizer33 
)33 
;33 
}44 
}55 
}66 δ
gC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLWithClauseParser.cs
	namespace 	
TSQL
 
. 
Clauses 
. 
Parsers 
{ 
internal 	
class
  
TSQLWithClauseParser $
:% &
ITSQLClauseParser' 8
{		 
public

 
TSQLWithClause

	 
Parse

 
(

 
ITSQLTokenizer

 ,
	tokenizer

- 6
)

6 7
{ 
throw 
new	 #
NotImplementedException $
($ %
)% &
;& '
} 

TSQLClause 
ITSQLClauseParser 
. 
Parse $
($ %
ITSQLTokenizer% 3
	tokenizer4 =
)= >
{ 
return 	
Parse
 
( 
	tokenizer 
) 
; 
} 
} 
} δ
dC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\ITSQLClauseParser.cs
	namespace 	
TSQL
 
. 
Clauses 
. 
Parsers 
{		 
internal

 	
	interface


 
ITSQLClauseParser

 %
{ 

TSQLClause 
Parse 
( 
ITSQLTokenizer !
	tokenizer" +
)+ ,
;, -
} 
} ¤G
gC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLFromClauseParser.cs
	namespace

 	
TSQL


 
.

 
Clauses

 
.

 
Parsers

 
{ 
internal 	
class
  
TSQLFromClauseParser $
:% &
ITSQLClauseParser' 8
{ 
public 
TSQLFromClause	 
Parse 
( 
ITSQLTokenizer ,
	tokenizer- 6
)6 7
{ 
TSQLFromClause 
from 
= 
new 
TSQLFromClause +
(+ ,
), -
;- .
if 
( 
! 
	tokenizer 
. 
Current "
." #
	IsKeyword# ,
(, -
TSQLKeywords- 9
.9 :
FROM: >
)> ?
)? @
{ 
throw 
new  
ApplicationException .
(. /
$str/ ?
)? @
;@ A
} 
from 
. 
Tokens 
. 
Add 
( 
	tokenizer %
.% &
Current& -
)- .
;. /
int 
nestedLevel 
= 
$num 
;  
while 
(	 

	tokenizer 
. 
MoveNext 
( 
) 
&& 
! 
( 
	tokenizer   
.   
Current   
.   
Type   
==   
TSQLTokenType   ,
.  , -
	Character  - 6
&&  7 9
	tokenizer!! 
.!! 
Current!! 
.!! 
AsCharacter!! "
.!!" #
	Character!!# ,
==!!- /
TSQLCharacters!!0 >
.!!> ?
	Semicolon!!? H
)"" 
&&"" 
!## 
(## 
nestedLevel$$ 
==$$ 
$num$$ 
&&$$ 
	tokenizer%% 
.%% 
Current%% 
.%% 
Type%% 
==%% 
TSQLTokenType%% ,
.%%, -
	Character%%- 6
&&%%7 9
	tokenizer&& 
.&& 
Current&& 
.&& 
AsCharacter&& "
.&&" #
	Character&&# ,
==&&- /
TSQLCharacters&&0 >
.&&> ?
CloseParentheses&&? O
)'' 
&&'' 
((( 
nestedLevel)) 
>)) 
$num)) 
||)) 
	tokenizer** 
.** 
Current** 
.** 
Type** 
!=** 
TSQLTokenType** ,
.**, -
Keyword**- 4
||**5 7
(++ 
	tokenizer,, 
.,, 
Current,, 
.,, 
Type,, 
==,, 
TSQLTokenType,,  -
.,,- .
Keyword,,. 5
&&,,6 8
	tokenizer-- 
.-- 
Current-- 
.-- 
	AsKeyword-- !
.--! "
Keyword--" )
.--) *
In--* ,
(.. 
TSQLKeywords// 
.// 
JOIN// 
,// 
TSQLKeywords00 
.00 
ON00 
,00 
TSQLKeywords11 
.11 
INNER11 
,11 
TSQLKeywords22 
.22 
LEFT22 
,22 
TSQLKeywords33 
.33 
RIGHT33 
,33 
TSQLKeywords44 
.44 
OUTER44 
,44 
TSQLKeywords55 
.55 
CROSS55 
,55 
TSQLKeywords66 
.66 
FULL66 
,66 
TSQLKeywords77 
.77 
AS77 
,77 
TSQLKeywords88 
.88 
PIVOT88 
,88 
TSQLKeywords99 
.99 
UNPIVOT99 
,99 
TSQLKeywords:: 
.:: 
WITH:: 
,:: 
TSQLKeywords;; 
.;; 
MERGE;; 
,;; 
TSQLKeywords<< 
.<< 
TABLESAMPLE<< 
,<<  
TSQLKeywords== 
.== 
FOR== 
,== 
TSQLKeywords>> 
.>> 
FROM>> 
,>> 
TSQLKeywords?? 
.?? 
BETWEEN?? 
,?? 
TSQLKeywords@@ 
.@@ 
AND@@ 
,@@ 
TSQLKeywordsAA 
.AA 
INAA 
,AA 
TSQLKeywordsBB 
.BB 

REPEATABLEBB 
,BB 
TSQLKeywordsCC 
.CC 
ALLCC 
)DD 
)EE 
)FF 
)FF 
{GG 
fromHH 
.HH 	
TokensHH	 
.HH 
AddHH 
(HH 
	tokenizerHH 
.HH 
CurrentHH %
)HH% &
;HH& '
ifJJ 
(JJ 
	tokenizerJJ 
.JJ 
CurrentJJ 
.JJ 
TypeJJ 
==JJ !
TSQLTokenTypeJJ" /
.JJ/ 0
	CharacterJJ0 9
)JJ9 :
{KK 
TSQLCharactersLL 
	characterLL 
=LL 
	tokenizerLL  )
.LL) *
CurrentLL* 1
.LL1 2
AsCharacterLL2 =
.LL= >
	CharacterLL> G
;LLG H
ifNN 
(NN 	
	characterNN	 
==NN 
TSQLCharactersNN $
.NN$ %
OpenParenthesesNN% 4
)NN4 5
{OO 
nestedLevelQQ 
++QQ 
;QQ 
ifSS 
(SS	 

	tokenizerSS
 
.SS 
MoveNextSS 
(SS 
)SS 
)SS 
{TT 
ifUU 	
(UU
 
	tokenizerVV 
.VV 
CurrentVV 
.VV 
TypeVV 
==VV !
TSQLTokenTypeVV" /
.VV/ 0
KeywordVV0 7
&&VV8 :
	tokenizerWW 
.WW 
CurrentWW 
.WW 
	AsKeywordWW #
.WW# $
KeywordWW$ +
==WW, .
TSQLKeywordsWW/ ;
.WW; <
SELECTWW< B
)WWB C
{XX 
TSQLSelectStatementYY 
selectStatementYY +
=YY, -
newYY. 1%
TSQLSelectStatementParserYY2 K
(YYK L
)YYL M
.YYM N
ParseYYN S
(YYS T
	tokenizerYYT ]
)YY] ^
;YY^ _
from[[ 
.[[ 
Tokens[[ 
.[[ 
AddRange[[ 
([[ 
selectStatement[[ ,
.[[, -
Tokens[[- 3
)[[3 4
;[[4 5
if]] 

(]] 
	tokenizer^^	 
.^^ 
Current^^ 
!=^^ 
null^^ "
&&^^# %
	tokenizer__	 
.__ 
Current__ 
.__ 
Type__ 
==__  "
TSQLTokenType__# 0
.__0 1
	Character__1 :
&&__; =
	tokenizer``	 
.`` 
Current`` 
.`` 
AsCharacter`` &
.``& '
	Character``' 0
==``1 3
TSQLCharacters``4 B
.``B C
CloseParentheses``C S
)``S T
{aa 	
nestedLevelbb	 
--bb 
;bb 
fromcc	 
.cc 
Tokenscc 
.cc 
Addcc 
(cc 
	tokenizercc "
.cc" #
Currentcc# *
)cc* +
;cc+ ,
}dd 	
}ee 
elseff 
ifff 
(ff 
	tokenizerff 
.ff 
Currentff !
.ff! "
IsCharacterff" -
(ff- .
TSQLCharactersgg 
.gg 
CloseParenthesesgg '
)gg' (
)gg( )
{hh 
nestedLevelii 
--ii 
;ii 
fromjj 
.jj 
Tokensjj 
.jj 
Addjj 
(jj 
	tokenizerjj !
.jj! "
Currentjj" )
)jj) *
;jj* +
}kk 
elsell 
{mm 
fromnn 
.nn 
Tokensnn 
.nn 
Addnn 
(nn 
	tokenizernn !
.nn! "
Currentnn" )
)nn) *
;nn* +
}oo 
}pp 
}qq 
elserr 	
ifrr
 
(rr 
	characterrr 
==rr 
TSQLCharactersrr )
.rr) *
CloseParenthesesrr* :
)rr: ;
{ss 
nestedLeveltt 
--tt 
;tt 
}uu 
}vv 
}ww 
returnyy 	
fromyy
 
;yy 
}zz 

TSQLClause|| 
ITSQLClauseParser|| 
.|| 
Parse|| $
(||$ %
ITSQLTokenizer||% 3
	tokenizer||4 =
)||= >
{}} 
return~~ 	
Parse~~
 
(~~ 
	tokenizer~~ 
)~~ 
;~~ 
} 
}
€€ 
} Ζ>
jC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLGroupByClauseParser.cs
	namespace

 	
TSQL


 
.

 
Clauses

 
.

 
Parsers

 
{ 
internal 	
class
 #
TSQLGroupByClauseParser '
:( )
ITSQLClauseParser* ;
{ 
public 
TSQLGroupByClause	 
Parse  
(  !
ITSQLTokenizer! /
	tokenizer0 9
)9 :
{ 
TSQLGroupByClause 
groupBy 
= 
new "
TSQLGroupByClause# 4
(4 5
)5 6
;6 7
if 
( 
! 
	tokenizer 
. 
Current 
. 
	IsKeyword #
(# $
TSQLKeywords$ 0
.0 1
GROUP1 6
)6 7
)7 8
{ 
throw 	
new
  
ApplicationException "
(" #
$str# 4
)4 5
;5 6
} 
groupBy 

.
 
Tokens 
. 
Add 
( 
	tokenizer 
.  
Current  '
)' (
;( )
int 
nestedLevel 
= 
$num 
; 
while 
(	 

	tokenizer 
. 
MoveNext 
( 
) 
&& 
! 
	tokenizer 
. 
Current 
. 
IsCharacter "
(" #
TSQLCharacters# 1
.1 2
	Semicolon2 ;
); <
&&= ?
! 
( 
nestedLevel   
==   
$num   
&&   
	tokenizer!! 
.!! 
Current!! 
.!! 
IsCharacter!! "
(!!" #
TSQLCharacters!!# 1
.!!1 2
CloseParentheses!!2 B
)!!B C
)"" 
&&"" 
(## 
nestedLevel$$ 
>$$ 
$num$$ 
||$$ 
	tokenizer%% 
.%% 
Current%% 
.%% 
Type%% 
!=%% 
TSQLTokenType%% ,
.%%, -
Keyword%%- 4
||%%5 7
(&& 
	tokenizer'' 
.'' 
Current'' 
.'' 
Type'' 
=='' 
TSQLTokenType''  -
.''- .
Keyword''. 5
&&''6 8
	tokenizer(( 
.(( 
Current(( 
.(( 
	AsKeyword(( !
.((! "
Keyword((" )
.(() *
In((* ,
()) 
TSQLKeywords** 
.** 
BY** 
,** 
TSQLKeywords++ 
.++ 
NULL++ 
,++ 
TSQLKeywords,, 
.,, 
CASE,, 
,,, 
TSQLKeywords-- 
.-- 
WHEN-- 
,-- 
TSQLKeywords.. 
... 
THEN.. 
,.. 
TSQLKeywords// 
.// 
ELSE// 
,// 
TSQLKeywords00 
.00 
AND00 
,00 
TSQLKeywords11 
.11 
OR11 
,11 
TSQLKeywords22 
.22 
BETWEEN22 
,22 
TSQLKeywords33 
.33 
EXISTS33 
,33 
TSQLKeywords44 
.44 
END44 
,44 
TSQLKeywords55 
.55 
IN55 
,55 
TSQLKeywords66 
.66 
IS66 
,66 
TSQLKeywords77 
.77 
NOT77 
,77 
TSQLKeywords88 
.88 
OVER88 
,88 
TSQLKeywords99 
.99 
LIKE99 
,99 
TSQLKeywords:: 
.:: 
ALL:: 
,:: 
TSQLKeywords;; 
.;; 
WITH;; 
,;; 
TSQLKeywords<< 
.<< 
DISTINCT<< 
)== 
)>> 
)?? 
)?? 
{@@ 
groupByAA 
.AA 
TokensAA 
.AA 
AddAA 
(AA 
	tokenizerAA  
.AA  !
CurrentAA! (
)AA( )
;AA) *
ifCC 
(CC 
	tokenizerCC 
.CC 
CurrentCC 
.CC 
TypeCC 
==CC !
TSQLTokenTypeCC" /
.CC/ 0
	CharacterCC0 9
)CC9 :
{DD 
TSQLCharactersEE 
	characterEE 
=EE 
	tokenizerEE  )
.EE) *
CurrentEE* 1
.EE1 2
AsCharacterEE2 =
.EE= >
	CharacterEE> G
;EEG H
ifGG 
(GG 	
	characterGG	 
==GG 
TSQLCharactersGG $
.GG$ %
OpenParenthesesGG% 4
)GG4 5
{HH 
nestedLevelJJ 
++JJ 
;JJ 
ifLL 
(LL	 

	tokenizerLL
 
.LL 
MoveNextLL 
(LL 
)LL 
)LL 
{MM 
ifNN 	
(NN
 
	tokenizerNN 
.NN 
CurrentNN 
.NN 
	IsKeywordNN &
(NN& '
TSQLKeywordsNN' 3
.NN3 4
SELECTNN4 :
)NN: ;
)NN; <
{OO 
TSQLSelectStatementPP 
selectStatementPP +
=PP, -
newPP. 1%
TSQLSelectStatementParserPP2 K
(PPK L
)PPL M
.PPM N
ParsePPN S
(PPS T
	tokenizerPPT ]
)PP] ^
;PP^ _
groupByRR 
.RR 
TokensRR 
.RR 
AddRangeRR 
(RR  
selectStatementRR  /
.RR/ 0
TokensRR0 6
)RR6 7
;RR7 8
ifTT 

(TT 
	tokenizerTT 
.TT 
CurrentTT 
.TT 
IsCharacterTT )
(TT) *
TSQLCharactersTT* 8
.TT8 9
CloseParenthesesTT9 I
)TTI J
)TTJ K
{UU 	
nestedLevelVV	 
--VV 
;VV 
groupByWW	 
.WW 
TokensWW 
.WW 
AddWW 
(WW 
	tokenizerWW %
.WW% &
CurrentWW& -
)WW- .
;WW. /
}XX 	
}YY 
elseZZ 
ifZZ 
(ZZ 
	tokenizerZZ 
.ZZ 
CurrentZZ !
.ZZ! "
IsCharacterZZ" -
(ZZ- .
TSQLCharacters[[ 
.[[ 
CloseParentheses[[ '
)[[' (
)[[( )
{\\ 
nestedLevel]] 
--]] 
;]] 
groupBy^^ 
.^^ 
Tokens^^ 
.^^ 
Add^^ 
(^^ 
	tokenizer^^ $
.^^$ %
Current^^% ,
)^^, -
;^^- .
}__ 
else`` 
{aa 
groupBybb 
.bb 
Tokensbb 
.bb 
Addbb 
(bb 
	tokenizerbb $
.bb$ %
Currentbb% ,
)bb, -
;bb- .
}cc 
}dd 
}ee 
elseff 	
ifff
 
(ff 
	characterff 
==ff 
TSQLCharactersff )
.ff) *
CloseParenthesesff* :
)ff: ;
{gg 
nestedLevelhh 
--hh 
;hh 
}ii 
}jj 
}kk 
returnmm 	
groupBymm
 
;mm 
}nn 

TSQLClausepp 
ITSQLClauseParserpp 
.pp 
Parsepp $
(pp$ %
ITSQLTokenizerpp% 3
	tokenizerpp4 =
)pp= >
{qq 
returnrr 	
Parserr
 
(rr 
	tokenizerrr 
)rr 
;rr 
}ss 
}tt 
}uu Η;
iC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLHavingClauseParser.cs
	namespace

 	
TSQL


 
.

 
Clauses

 
.

 
Parsers

 
{ 
internal 	
class
 "
TSQLHavingClauseParser &
:' (
ITSQLClauseParser) :
{ 
public 
TSQLHavingClause	 
Parse 
(  
ITSQLTokenizer  .
	tokenizer/ 8
)8 9
{ 
TSQLHavingClause 
having 
= 
new  
TSQLHavingClause! 1
(1 2
)2 3
;3 4
if 
( 
! 
	tokenizer 
. 
Current 
. 
	IsKeyword #
(# $
TSQLKeywords$ 0
.0 1
HAVING1 7
)7 8
)8 9
{ 
throw 	
new
  
ApplicationException "
(" #
$str# 5
)5 6
;6 7
} 
having 	
.	 

Tokens
 
. 
Add 
( 
	tokenizer 
. 
Current &
)& '
;' (
int 
nestedLevel 
= 
$num 
; 
while 
(	 

	tokenizer 
. 
MoveNext 
( 
) 
&& 
! 
	tokenizer 
. 
Current 
. 
IsCharacter "
(" #
TSQLCharacters# 1
.1 2
	Semicolon2 ;
); <
&&= ?
! 
( 
nestedLevel   
==   
$num   
&&   
	tokenizer!! 
.!! 
Current!! 
.!! 
IsCharacter!! "
(!!" #
TSQLCharacters!!# 1
.!!1 2
CloseParentheses!!2 B
)!!B C
)"" 
&&"" 
(## 
nestedLevel$$ 
>$$ 
$num$$ 
||$$ 
	tokenizer%% 
.%% 
Current%% 
.%% 
Type%% 
!=%% 
TSQLTokenType%% ,
.%%, -
Keyword%%- 4
||%%5 7
(&& 
	tokenizer'' 
.'' 
Current'' 
.'' 
Type'' 
=='' 
TSQLTokenType''  -
.''- .
Keyword''. 5
&&''6 8
	tokenizer(( 
.(( 
Current(( 
.(( 
	AsKeyword(( !
.((! "
Keyword((" )
.(() *
In((* ,
()) 
TSQLKeywords** 
.** 
NULL** 
,** 
TSQLKeywords++ 
.++ 
CASE++ 
,++ 
TSQLKeywords,, 
.,, 
WHEN,, 
,,, 
TSQLKeywords-- 
.-- 
THEN-- 
,-- 
TSQLKeywords.. 
... 
ELSE.. 
,.. 
TSQLKeywords// 
.// 
AND// 
,// 
TSQLKeywords00 
.00 
OR00 
,00 
TSQLKeywords11 
.11 
BETWEEN11 
,11 
TSQLKeywords22 
.22 
EXISTS22 
,22 
TSQLKeywords33 
.33 
END33 
,33 
TSQLKeywords44 
.44 
IN44 
,44 
TSQLKeywords55 
.55 
IS55 
,55 
TSQLKeywords66 
.66 
NOT66 
,66 
TSQLKeywords77 
.77 
LIKE77 
)88 
)99 
):: 
):: 
{;; 
having<< 

.<<
 
Tokens<< 
.<< 
Add<< 
(<< 
	tokenizer<< 
.<<  
Current<<  '
)<<' (
;<<( )
if>> 
(>> 
	tokenizer>> 
.>> 
Current>> 
.>> 
Type>> 
==>> !
TSQLTokenType>>" /
.>>/ 0
	Character>>0 9
)>>9 :
{?? 
TSQLCharacters@@ 
	character@@ 
=@@ 
	tokenizer@@  )
.@@) *
Current@@* 1
.@@1 2
AsCharacter@@2 =
.@@= >
	Character@@> G
;@@G H
ifBB 
(BB 	
	characterBB	 
==BB 
TSQLCharactersBB $
.BB$ %
OpenParenthesesBB% 4
)BB4 5
{CC 
nestedLevelEE 
++EE 
;EE 
ifGG 
(GG	 

	tokenizerGG
 
.GG 
MoveNextGG 
(GG 
)GG 
)GG 
{HH 
ifII 	
(II
 
	tokenizerII 
.II 
CurrentII 
.II 
	IsKeywordII &
(II& '
TSQLKeywordsII' 3
.II3 4
SELECTII4 :
)II: ;
)II; <
{JJ 
TSQLSelectStatementKK 
selectStatementKK +
=KK, -
newKK. 1%
TSQLSelectStatementParserKK2 K
(KKK L
)KKL M
.KKM N
ParseKKN S
(KKS T
	tokenizerKKT ]
)KK] ^
;KK^ _
havingMM 
.MM 
TokensMM 
.MM 
AddRangeMM 
(MM 
selectStatementMM .
.MM. /
TokensMM/ 5
)MM5 6
;MM6 7
ifOO 

(OO 
	tokenizerOO 
.OO 
CurrentOO 
.OO 
IsCharacterOO )
(OO) *
TSQLCharactersOO* 8
.OO8 9
CloseParenthesesOO9 I
)OOI J
)OOJ K
{PP 	
nestedLevelQQ	 
--QQ 
;QQ 
havingRR	 
.RR 
TokensRR 
.RR 
AddRR 
(RR 
	tokenizerRR $
.RR$ %
CurrentRR% ,
)RR, -
;RR- .
}SS 	
}TT 
elseUU 
ifUU 
(UU 
	tokenizerUU 
.UU 
CurrentUU !
.UU! "
IsCharacterUU" -
(UU- .
TSQLCharactersVV 
.VV 
CloseParenthesesVV '
)VV' (
)VV( )
{WW 
nestedLevelXX 
--XX 
;XX 
havingYY 
.YY 
TokensYY 
.YY 
AddYY 
(YY 
	tokenizerYY #
.YY# $
CurrentYY$ +
)YY+ ,
;YY, -
}ZZ 
else[[ 
{\\ 
having]] 
.]] 
Tokens]] 
.]] 
Add]] 
(]] 
	tokenizer]] #
.]]# $
Current]]$ +
)]]+ ,
;]], -
}^^ 
}__ 
}`` 
elseaa 	
ifaa
 
(aa 
	characteraa 
==aa 
TSQLCharactersaa )
.aa) *
CloseParenthesesaa* :
)aa: ;
{bb 
nestedLevelcc 
--cc 
;cc 
}dd 
}ee 
}ff 
returnhh 	
havinghh
 
;hh 
}ii 

TSQLClausekk 
ITSQLClauseParserkk 
.kk 
Parsekk $
(kk$ %
ITSQLTokenizerkk% 3
	tokenizerkk4 =
)kk= >
{ll 
returnmm 	
Parsemm
 
(mm 
	tokenizermm 
)mm 
;mm 
}nn 
}oo 
}pp κ
gC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLIntoClauseParser.cs
	namespace 	
TSQL
 
. 
Clauses 
. 
Parsers 
{		 
internal

 	
class


  
TSQLIntoClauseParser

 $
:

% &
ITSQLClauseParser

' 8
{ 
public 
TSQLIntoClause	 
Parse 
( 
ITSQLTokenizer ,
	tokenizer- 6
)6 7
{ 
TSQLIntoClause 
into 
= 
new 
TSQLIntoClause +
(+ ,
), -
;- .
if 
( 
! 
	tokenizer 
. 
Current "
." #
	IsKeyword# ,
(, -
TSQLKeywords- 9
.9 :
INTO: >
)> ?
)? @
{ 
throw 
new  
ApplicationException .
(. /
$str/ ?
)? @
;@ A
} 
into 
. 
Tokens 
. 
Add 
( 
	tokenizer %
.% &
Current& -
)- .
;. /
while 
( 
	tokenizer 
. 
MoveNext 
( 
) 
&& 
( 
	tokenizer 
. 
Current 
. 
Type 
== 
TSQLTokenType ,
., -

Identifier- 7
||8 :
	tokenizer 
. 
Current 
. 
IsCharacter "
(" #
TSQLCharacters# 1
.1 2
Period2 8
)8 9
||: <
	tokenizer 
. 
Current 
. 
Type 
== 
TSQLTokenType ,
., -

Whitespace- 7
||8 :
	tokenizer 
. 
Current 
. 
Type 
== 
TSQLTokenType ,
., -
SingleLineComment- >
||? A
	tokenizer 
. 
Current 
. 
Type 
== 
TSQLTokenType ,
., -
MultilineComment- =
) 
) 
{   
into!! 
.!! 	
Tokens!!	 
.!! 
Add!! 
(!! 
	tokenizer!! 
.!! 
Current!! %
)!!% &
;!!& '
}"" 
return$$ 	
into$$
 
;$$ 
}%% 

TSQLClause'' 
ITSQLClauseParser'' 
.'' 
Parse'' $
(''$ %
ITSQLTokenizer''% 3
	tokenizer''4 =
)''= >
{(( 
return)) 	
Parse))
 
()) 
	tokenizer)) 
))) 
;)) 
}** 
}++ 
},, ?
jC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLOrderByClauseParser.cs
	namespace

 	
TSQL


 
.

 
Clauses

 
.

 
Parsers

 
{ 
internal 	
class
 #
TSQLOrderByClauseParser '
:( )
ITSQLClauseParser* ;
{ 
public 
TSQLOrderByClause	 
Parse  
(  !
ITSQLTokenizer! /
	tokenizer0 9
)9 :
{ 
TSQLOrderByClause 
orderBy 
= 
new "
TSQLOrderByClause# 4
(4 5
)5 6
;6 7
if 
( 
! 
	tokenizer 
. 
Current 
. 
	IsKeyword #
(# $
TSQLKeywords$ 0
.0 1
ORDER1 6
)6 7
)7 8
{ 
throw 	
new
  
ApplicationException "
(" #
$str# 4
)4 5
;5 6
} 
orderBy 

.
 
Tokens 
. 
Add 
( 
	tokenizer 
.  
Current  '
)' (
;( )
int 
nestedLevel 
= 
$num 
; 
while 
(	 

	tokenizer 
. 
MoveNext 
( 
) 
&& 
! 
	tokenizer 
. 
Current 
. 
IsCharacter "
(" #
TSQLCharacters# 1
.1 2
	Semicolon2 ;
); <
&&= ?
! 
( 
nestedLevel   
==   
$num   
&&   
	tokenizer!! 
.!! 
Current!! 
.!! 
IsCharacter!! "
(!!" #
TSQLCharacters!!# 1
.!!1 2
CloseParentheses!!2 B
)!!B C
)"" 
&&"" 
(## 
nestedLevel$$ 
>$$ 
$num$$ 
||$$ 
	tokenizer%% 
.%% 
Current%% 
.%% 
Type%% 
!=%% 
TSQLTokenType%% ,
.%%, -
Keyword%%- 4
||%%5 7
(&& 
	tokenizer'' 
.'' 
Current'' 
.'' 
Type'' 
=='' 
TSQLTokenType''  -
.''- .
Keyword''. 5
&&''6 8
	tokenizer(( 
.(( 
Current(( 
.(( 
	AsKeyword(( !
.((! "
Keyword((" )
.(() *
In((* ,
()) 
TSQLKeywords** 
.** 
BY** 
,** 
TSQLKeywords++ 
.++ 
NULL++ 
,++ 
TSQLKeywords,, 
.,, 
CASE,, 
,,, 
TSQLKeywords-- 
.-- 
WHEN-- 
,-- 
TSQLKeywords.. 
... 
THEN.. 
,.. 
TSQLKeywords// 
.// 
ELSE// 
,// 
TSQLKeywords00 
.00 
AND00 
,00 
TSQLKeywords11 
.11 
OR11 
,11 
TSQLKeywords22 
.22 
BETWEEN22 
,22 
TSQLKeywords33 
.33 
EXISTS33 
,33 
TSQLKeywords44 
.44 
END44 
,44 
TSQLKeywords55 
.55 
IN55 
,55 
TSQLKeywords66 
.66 
IS66 
,66 
TSQLKeywords77 
.77 
NOT77 
,77 
TSQLKeywords88 
.88 
OVER88 
,88 
TSQLKeywords99 
.99 
LIKE99 
,99 
TSQLKeywords:: 
.:: 
ASC:: 
,:: 
TSQLKeywords;; 
.;; 
DESC;; 
,;; 
TSQLKeywords<< 
.<< 
FETCH<< 
,<< 
TSQLKeywords== 
.== 
COLLATE== 
)>> 
)?? 
)@@ 
)@@ 
{AA 
orderByBB 
.BB 
TokensBB 
.BB 
AddBB 
(BB 
	tokenizerBB  
.BB  !
CurrentBB! (
)BB( )
;BB) *
ifDD 
(DD 
	tokenizerDD 
.DD 
CurrentDD 
.DD 
TypeDD 
==DD !
TSQLTokenTypeDD" /
.DD/ 0
	CharacterDD0 9
)DD9 :
{EE 
TSQLCharactersFF 
	characterFF 
=FF 
	tokenizerFF  )
.FF) *
CurrentFF* 1
.FF1 2
AsCharacterFF2 =
.FF= >
	CharacterFF> G
;FFG H
ifHH 
(HH 	
	characterHH	 
==HH 
TSQLCharactersHH $
.HH$ %
OpenParenthesesHH% 4
)HH4 5
{II 
nestedLevelKK 
++KK 
;KK 
ifMM 
(MM	 

	tokenizerMM
 
.MM 
MoveNextMM 
(MM 
)MM 
)MM 
{NN 
ifOO 	
(OO
 
	tokenizerOO 
.OO 
CurrentOO 
.OO 
	IsKeywordOO &
(OO& '
TSQLKeywordsOO' 3
.OO3 4
SELECTOO4 :
)OO: ;
)OO; <
{PP 
TSQLSelectStatementQQ 
selectStatementQQ +
=QQ, -
newQQ. 1%
TSQLSelectStatementParserQQ2 K
(QQK L
)QQL M
.QQM N
ParseQQN S
(QQS T
	tokenizerQQT ]
)QQ] ^
;QQ^ _
orderBySS 
.SS 
TokensSS 
.SS 
AddRangeSS 
(SS  
selectStatementSS  /
.SS/ 0
TokensSS0 6
)SS6 7
;SS7 8
ifUU 

(UU 
	tokenizerUU 
.UU 
CurrentUU 
.UU 
IsCharacterUU )
(UU) *
TSQLCharactersUU* 8
.UU8 9
CloseParenthesesUU9 I
)UUI J
)UUJ K
{VV 	
nestedLevelWW	 
--WW 
;WW 
orderByXX	 
.XX 
TokensXX 
.XX 
AddXX 
(XX 
	tokenizerXX %
.XX% &
CurrentXX& -
)XX- .
;XX. /
}YY 	
}ZZ 
else[[ 
if[[ 
([[ 
	tokenizer[[ 
.[[ 
Current[[ !
.[[! "
IsCharacter[[" -
([[- .
TSQLCharacters\\ 
.\\ 
CloseParentheses\\ '
)\\' (
)\\( )
{]] 
nestedLevel^^ 
--^^ 
;^^ 
orderBy__ 
.__ 
Tokens__ 
.__ 
Add__ 
(__ 
	tokenizer__ $
.__$ %
Current__% ,
)__, -
;__- .
}`` 
elseaa 
{bb 
orderBycc 
.cc 
Tokenscc 
.cc 
Addcc 
(cc 
	tokenizercc $
.cc$ %
Currentcc% ,
)cc, -
;cc- .
}dd 
}ee 
}ff 
elsegg 	
ifgg
 
(gg 
	charactergg 
==gg 
TSQLCharactersgg )
.gg) *
CloseParenthesesgg* :
)gg: ;
{hh 
nestedLevelii 
--ii 
;ii 
}jj 
}kk 
}ll 
returnnn 	
orderBynn
 
;nn 
}oo 

TSQLClauseqq 
ITSQLClauseParserqq 
.qq 
Parseqq $
(qq$ %
ITSQLTokenizerqq% 3
	tokenizerqq4 =
)qq= >
{rr 
returnss 	
Parsess
 
(ss 
	tokenizerss 
)ss 
;ss 
}tt 
}uu 
}vv @
iC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLSelectClauseParser.cs
	namespace

 	
TSQL


 
.

 
Clauses

 
.

 
Parsers

 
{ 
internal 	
class
 "
TSQLSelectClauseParser &
:' (
ITSQLClauseParser) :
{ 
public 
TSQLSelectClause	 
Parse 
(  
ITSQLTokenizer  .
	tokenizer/ 8
)8 9
{ 
TSQLSelectClause 
select 
= 
new  
TSQLSelectClause! 1
(1 2
)2 3
;3 4
if 
( 
! 
	tokenizer 
. 
Current 
. 
	IsKeyword #
(# $
TSQLKeywords$ 0
.0 1
SELECT1 7
)7 8
)8 9
{ 
throw 	
new
  
ApplicationException "
(" #
$str# 5
)5 6
;6 7
} 
select 	
.	 

Tokens
 
. 
Add 
( 
	tokenizer 
. 
Current &
)& '
;' (
int 
nestedLevel 
= 
$num 
; 
while!! 
(!!	 

	tokenizer"" 
."" 
MoveNext"" 
("" 
)"" 
&&"" 
!## 
	tokenizer## 
.## 
Current## 
.## 
IsCharacter## "
(##" #
TSQLCharacters### 1
.##1 2
	Semicolon##2 ;
)##; <
&&##= ?
!$$ 
($$ 
nestedLevel%% 
==%% 
$num%% 
&&%% 
	tokenizer&& 
.&& 
Current&& 
.&& 
IsCharacter&& "
(&&" #
TSQLCharacters&&# 1
.&&1 2
CloseParentheses&&2 B
)&&B C
)'' 
&&'' 
((( 
nestedLevel)) 
>)) 
$num)) 
||)) 
	tokenizer** 
.** 
Current** 
.** 
Type** 
!=** 
TSQLTokenType** ,
.**, -
Keyword**- 4
||**5 7
(++ 
	tokenizer,, 
.,, 
Current,, 
.,, 
Type,, 
==,, 
TSQLTokenType,,  -
.,,- .
Keyword,,. 5
&&,,6 8
	tokenizer-- 
.-- 
Current-- 
.-- 
	AsKeyword-- !
.--! "
Keyword--" )
.--) *
In--* ,
(.. 
TSQLKeywords// 
.// 
ALL// 
,// 
TSQLKeywords00 
.00 
AS00 
,00 
TSQLKeywords11 
.11 
DISTINCT11 
,11 
TSQLKeywords22 
.22 
PERCENT22 
,22 
TSQLKeywords33 
.33 
TOP33 
,33 
TSQLKeywords44 
.44 
WITH44 
,44 
TSQLKeywords55 
.55 
NULL55 
,55 
TSQLKeywords66 
.66 
CASE66 
,66 
TSQLKeywords77 
.77 
WHEN77 
,77 
TSQLKeywords88 
.88 
THEN88 
,88 
TSQLKeywords99 
.99 
ELSE99 
,99 
TSQLKeywords:: 
.:: 
AND:: 
,:: 
TSQLKeywords;; 
.;; 
OR;; 
,;; 
TSQLKeywords<< 
.<< 
BETWEEN<< 
,<< 
TSQLKeywords== 
.== 
EXISTS== 
,== 
TSQLKeywords>> 
.>> 
END>> 
,>> 
TSQLKeywords?? 
.?? 
IN?? 
,?? 
TSQLKeywords@@ 
.@@ 
IS@@ 
,@@ 
TSQLKeywordsAA 
.AA 
NOTAA 
,AA 
TSQLKeywordsBB 
.BB 
OVERBB 
,BB 
TSQLKeywordsCC 
.CC 
IDENTITYCC 
,CC 
TSQLKeywordsDD 
.DD 
LIKEDD 
)EE 
)FF 
)GG 
)GG 
{HH 
selectII 

.II
 
TokensII 
.II 
AddII 
(II 
	tokenizerII 
.II  
CurrentII  '
)II' (
;II( )
ifKK 
(KK 
	tokenizerKK 
.KK 
CurrentKK 
.KK 
TypeKK 
==KK !
TSQLTokenTypeKK" /
.KK/ 0
	CharacterKK0 9
)KK9 :
{LL 
TSQLCharactersMM 
	characterMM 
=MM 
	tokenizerMM  )
.MM) *
CurrentMM* 1
.MM1 2
AsCharacterMM2 =
.MM= >
	CharacterMM> G
;MMG H
ifOO 
(OO 	
	characterOO	 
==OO 
TSQLCharactersOO $
.OO$ %
OpenParenthesesOO% 4
)OO4 5
{PP 
nestedLevelRR 
++RR 
;RR 
ifTT 
(TT	 

	tokenizerTT
 
.TT 
MoveNextTT 
(TT 
)TT 
)TT 
{UU 
ifVV 	
(VV
 
	tokenizerVV 
.VV 
CurrentVV 
.VV 
	IsKeywordVV &
(VV& '
TSQLKeywordsWW 
.WW 
SELECTWW 
)WW 
)WW 
{XX 
TSQLSelectStatementYY 
selectStatementYY +
=YY, -
newYY. 1%
TSQLSelectStatementParserYY2 K
(YYK L
)YYL M
.YYM N
ParseYYN S
(YYS T
	tokenizerYYT ]
)YY] ^
;YY^ _
select[[ 
.[[ 
Tokens[[ 
.[[ 
AddRange[[ 
([[ 
selectStatement[[ .
.[[. /
Tokens[[/ 5
)[[5 6
;[[6 7
if]]  "
(]]# $
	tokenizer]]$ -
.]]- .
Current]]. 5
.]]5 6
IsCharacter]]6 A
(]]A B
TSQLCharacters^^	 
.^^ 
CloseParentheses^^ (
)^^( )
)^^) *
{__  !
nestedLevel``$ /
--``/ 1
;``1 2
selectaa$ *
.aa* +
Tokensaa+ 1
.aa1 2
Addaa2 5
(aa5 6
	tokenizeraa6 ?
.aa? @
Currentaa@ G
)aaG H
;aaH I
}bb  !
}cc 
elsedd 
ifdd 
(dd 
	tokenizerdd 
.dd 
Currentdd !
.dd! "
IsCharacterdd" -
(dd- .
TSQLCharactersee 
.ee 
CloseParenthesesee '
)ee' (
)ee( )
{ff 
nestedLevelgg 
--gg 
;gg 
selecthh 
.hh 
Tokenshh 
.hh 
Addhh 
(hh 
	tokenizerhh #
.hh# $
Currenthh$ +
)hh+ ,
;hh, -
}ii 
elsejj 
{kk 
selectll 
.ll 
Tokensll 
.ll 
Addll 
(ll 
	tokenizerll #
.ll# $
Currentll$ +
)ll+ ,
;ll, -
}mm 
}nn 
}oo 
elsepp 	
ifpp
 
(pp 
	characterpp 
==pp 
TSQLCharacterspp )
.pp) *
CloseParenthesespp* :
)pp: ;
{qq 
nestedLevelrr 
--rr 
;rr 
}ss 
}tt 
}uu 
returnww 	
selectww
 
;ww 
}xx 

TSQLClausezz 
ITSQLClauseParserzz 
.zz 
Parsezz $
(zz$ %
ITSQLTokenizerzz% 3
	tokenizerzz4 =
)zz= >
{{{ 
return|| 	
Parse||
 
(|| 
	tokenizer|| 
)|| 
;|| 
}}} 
}~~ 
} Ή;
hC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\Parsers\TSQLWhereClauseParser.cs
	namespace

 	
TSQL


 
.

 
Clauses

 
.

 
Parsers

 
{ 
internal 	
class
 !
TSQLWhereClauseParser %
:& '
ITSQLClauseParser( 9
{ 
public 
TSQLWhereClause	 
Parse 
( 
ITSQLTokenizer -
	tokenizer. 7
)7 8
{ 
TSQLWhereClause 
where 
= 
new 
TSQLWhereClause .
(. /
)/ 0
;0 1
if 
( 
! 
	tokenizer 
. 
Current "
." #
	IsKeyword# ,
(, -
TSQLKeywords- 9
.9 :
WHERE: ?
)? @
)@ A
{ 
throw 
new  
ApplicationException .
(. /
$str/ @
)@ A
;A B
} 
where 
. 
Tokens 
. 
Add 
( 
	tokenizer &
.& '
Current' .
). /
;/ 0
int 
nestedLevel 
= 
$num 
;  
while 
(	 

	tokenizer 
. 
MoveNext 
( 
) 
&& 
! 
	tokenizer 
. 
Current 
. 
IsCharacter "
(" #
TSQLCharacters# 1
.1 2
	Semicolon2 ;
); <
&&= ?
! 
( 
nestedLevel   
==   
$num   
&&   
	tokenizer!! 
.!! 
Current!! 
.!! 
IsCharacter!! "
(!!" #
TSQLCharacters!!# 1
.!!1 2
CloseParentheses!!2 B
)!!B C
)"" 
&&"" 
(## 
nestedLevel$$ 
>$$ 
$num$$ 
||$$ 
	tokenizer%% 
.%% 
Current%% 
.%% 
Type%% 
!=%% 
TSQLTokenType%% ,
.%%, -
Keyword%%- 4
||%%5 7
(&& 
	tokenizer'' 
.'' 
Current'' 
.'' 
Type'' 
=='' 
TSQLTokenType''  -
.''- .
Keyword''. 5
&&''6 8
	tokenizer(( 
.(( 
Current(( 
.(( 
	AsKeyword(( !
.((! "
Keyword((" )
.(() *
In((* ,
()) 
TSQLKeywords** 
.** 
NULL** 
,** 
TSQLKeywords++ 
.++ 
CASE++ 
,++ 
TSQLKeywords,, 
.,, 
WHEN,, 
,,, 
TSQLKeywords-- 
.-- 
THEN-- 
,-- 
TSQLKeywords.. 
... 
ELSE.. 
,.. 
TSQLKeywords// 
.// 
AND// 
,// 
TSQLKeywords00 
.00 
OR00 
,00 
TSQLKeywords11 
.11 
BETWEEN11 
,11 
TSQLKeywords22 
.22 
EXISTS22 
,22 
TSQLKeywords33 
.33 
END33 
,33 
TSQLKeywords44 
.44 
IN44 
,44 
TSQLKeywords55 
.55 
IS55 
,55 
TSQLKeywords66 
.66 
NOT66 
,66 
TSQLKeywords77 
.77 
LIKE77 
)88 
)99 
):: 
):: 
{;; 
where<< 	
.<<	 

Tokens<<
 
.<< 
Add<< 
(<< 
	tokenizer<< 
.<< 
Current<< &
)<<& '
;<<' (
if>> 
(>> 
	tokenizer>> 
.>> 
Current>> 
.>> 
Type>> 
==>> !
TSQLTokenType>>" /
.>>/ 0
	Character>>0 9
)>>9 :
{?? 
TSQLCharacters@@ 
	character@@ 
=@@ 
	tokenizer@@  )
.@@) *
Current@@* 1
.@@1 2
AsCharacter@@2 =
.@@= >
	Character@@> G
;@@G H
ifBB 
(BB 	
	characterBB	 
==BB 
TSQLCharactersBB $
.BB$ %
OpenParenthesesBB% 4
)BB4 5
{CC 
nestedLevelEE 
++EE 
;EE 
ifGG 
(GG	 

	tokenizerGG
 
.GG 
MoveNextGG 
(GG 
)GG 
)GG 
{HH 
ifII 	
(II
 
	tokenizerII 
.II 
CurrentII 
.II 
	IsKeywordII &
(II& '
TSQLKeywordsII' 3
.II3 4
SELECTII4 :
)II: ;
)II; <
{JJ 
TSQLSelectStatementKK 
selectStatementKK +
=KK, -
newKK. 1%
TSQLSelectStatementParserKK2 K
(KKK L
)KKL M
.KKM N
ParseKKN S
(KKS T
	tokenizerKKT ]
)KK] ^
;KK^ _
whereMM 
.MM 
TokensMM 
.MM 
AddRangeMM 
(MM 
selectStatementMM -
.MM- .
TokensMM. 4
)MM4 5
;MM5 6
ifOO 

(OO 
	tokenizerOO 
.OO 
CurrentOO 
.OO 
IsCharacterOO )
(OO) *
TSQLCharactersOO* 8
.OO8 9
CloseParenthesesOO9 I
)OOI J
)OOJ K
{PP 	
nestedLevelQQ	 
--QQ 
;QQ 
whereRR	 
.RR 
TokensRR 
.RR 
AddRR 
(RR 
	tokenizerRR #
.RR# $
CurrentRR$ +
)RR+ ,
;RR, -
}SS 	
}TT 
elseUU 
ifUU 
(UU 
	tokenizerUU 
.UU 
CurrentUU !
.UU! "
IsCharacterUU" -
(UU- .
TSQLCharactersVV 
.VV 
CloseParenthesesVV '
)VV' (
)VV( )
{WW 
nestedLevelXX 
--XX 
;XX 
whereYY 
.YY 
TokensYY 
.YY 
AddYY 
(YY 
	tokenizerYY "
.YY" #
CurrentYY# *
)YY* +
;YY+ ,
}ZZ 
else[[ 
{\\ 
where]] 
.]] 
Tokens]] 
.]] 
Add]] 
(]] 
	tokenizer]] "
.]]" #
Current]]# *
)]]* +
;]]+ ,
}^^ 
}__ 
}`` 
elseaa 	
ifaa
 
(aa 
	characteraa 
==aa 
TSQLCharactersaa )
.aa) *
CloseParenthesesaa* :
)aa: ;
{bb 
nestedLevelcc 
--cc 
;cc 
}dd 
}ee 
}ff 
returnhh 	
wherehh
 
;hh 
}ii 

TSQLClausekk 
ITSQLClauseParserkk 
.kk 
Parsekk $
(kk$ %
ITSQLTokenizerkk% 3
	tokenizerkk4 =
)kk= >
{ll 
returnmm 	
Parsemm
 
(mm 
	tokenizermm 
)mm 
;mm 
}nn 
}oo 
}pp ¨
UC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{		 
public

 
abstract

 
class

 

TSQLClause

 !
{ 
private 	
List
 
< 
	TSQLToken 
> 
_tokens !
=" #
new$ '
List( ,
<, -
	TSQLToken- 6
>6 7
(7 8
)8 9
;9 :
public 
List	 
< 
	TSQLToken 
> 
Tokens 
{ 
get 
{ 
return 

_tokens 
; 
} 
} 
} 
} ¶
YC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLFromClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLFromClause 
: 

TSQLClause )
{		 
internal

 

TSQLFromClause

 
(

 
)

 
{ 
} 
} 
} Ώ
\C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLGroupByClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLGroupByClause 
:  !

TSQLClause" ,
{		 
internal

 

TSQLGroupByClause

 
(

 
)

 
{ 
} 
} 
} Ό
[C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLHavingClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLHavingClause 
:  

TSQLClause! +
{		 
internal

 

TSQLHavingClause

 
(

 
)

 
{ 
} 
} 
} ¶
YC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLIntoClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLIntoClause 
: 

TSQLClause )
{		 
internal

 

TSQLIntoClause

 
(

 
)

 
{ 
} 
} 
} Ό
[C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLOptionClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLOptionClause 
:  

TSQLClause! +
{		 
internal

 

TSQLOptionClause

 
(

 
)

 
{ 
} 
} 
} Ώ
\C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLOrderByClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLOrderByClause 
:  !

TSQLClause" ,
{		 
internal

 

TSQLOrderByClause

 
(

 
)

 
{ 
} 
} 
} Ό
[C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLSelectClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLSelectClause 
:  

TSQLClause! +
{		 
internal

 

TSQLSelectClause

 
(

 
)

 
{ 
} 
} 
} Ή
ZC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLWhereClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLWhereClause 
: 

TSQLClause  *
{		 
internal

 

TSQLWhereClause

 
(

 
)

 
{ 
} 
} 
} Θ
YC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Clauses\TSQLWithClause.cs
	namespace 	
TSQL
 
. 
Clauses 
{ 
public 
class 
TSQLWithClause 
: 

TSQLClause )
{		 
}

 
} Υ*
XC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\IO\BufferedTextReader.cs
	namespace 	
TSQL
 
. 
IO 
{ 
internal 	
class
 
BufferedTextReader "
:# $
ICharacterReader% 5
{		 
private

 	

TextReader


 
_inputStream

 !
=

" #
null

$ (
;

( )
private 	
char
 
[ 
] 
_buffer 
= 
new 
char #
[# $
$num$ (
]( )
;) *
private 	
int
 
	_position 
= 
$num 
; 
private 	
int
 
_read 
= 
$num 
; 
private 	
bool
 
_hasMore 
= 
true 
; 
public 
BufferedTextReader	 
( 

TextReader &
inputStream' 2
)2 3
{ 
_inputStream 
= 
inputStream 
; 
} 
char 
IEnumerator 
< 
char 
> 
. 
Current  
{ 
get 
{ 
if 
( 
_hasMore 
) 
{ 
return 
_buffer 
[ 
	_position 
] 
; 
} 
else 
{ 
return 
char 
. 
MinValue 
; 
}   
}!! 
}"" 
bool$$ 
IEnumerator$$ 
.$$ 
MoveNext$$ 
($$ 
)$$ 
{%% 
if&& 
(&& 
_hasMore&& 
)&& 
{'' 
if(( 
((( 
	_position(( 
>=(( 
_read(( 
-(( 
$num(( 
)(( 
{)) 
_read** 

=** 
_inputStream** 
.** 
Read** 
(** 
_buffer** &
,**& '
$num**( )
,**) *
_buffer**+ 2
.**2 3
Length**3 9
)**9 :
;**: ;
	_position++ 
=++ 
$num++ 
;++ 
_hasMore,, 
=,, 
_read,, 
>,, 
$num,, 
;,, 
return-- 
_hasMore-- 
;-- 
}.. 
	_position00 
++00 
;00 
return11 

true11 
;11 
}22 
else33 
{44 
return55 

false55 
;55 
}66 
}77 
object99 
IEnumerator99	 
.99 
Current99 
{:: 
get;; 
{<< 
return== 

(== 
this== 
as== 
IEnumerator== 
<==  
char==  $
>==$ %
)==% &
.==& '
Current==' .
;==. /
}>> 
}?? 
voidAA 
IEnumeratorAA 
.AA 
ResetAA 
(AA 
)AA 
{BB 
throwCC 
newCC	 !
NotSupportedExceptionCC "
(CC" #
$strCC# t
+CCu v
GetTypeCCw ~
(CC~ 
)	CC €
.
CC€ 
FullName
CC ‰
+
CC ‹
$str
CC 
)
CC 
;
CC ‘
}DD 
IEnumeratorFF 
<FF 
charFF 
>FF 
IEnumerableFF 
<FF  
charFF  $
>FF$ %
.FF% &
GetEnumeratorFF& 3
(FF3 4
)FF4 5
{GG 
returnHH 	
thisHH
 
;HH 
}II 
IEnumeratorKK 
IEnumerableKK 
.KK 
GetEnumeratorKK '
(KK' (
)KK( )
{LL 
returnMM 	
thisMM
 
;MM 
}NN 
privateRR 	
boolRR
 
disposedRR 
=RR 
falseRR 
;RR  
voidTT 
IDisposableTT 
.TT 
DisposeTT 
(TT 
)TT 
{UU 
ifVV 
(VV 
!VV 
disposedVV 
)VV 
{WW 
DisposeXX 
(XX 
trueXX 
)XX 
;XX 
}YY 
}ZZ 
privatecc 	
voidcc
 
Disposecc 
(cc 
boolcc 
	disposingcc %
)cc% &
{dd 
ifee 
(ee 
!ee 
disposedee 
)ee 
{ff 
ifhh 
(hh 
	disposinghh 
)hh 
{ii 
}kk 
trynn 
{oo 
(pp 
_inputStreampp 
aspp 
IDisposablepp !
)pp! "
.pp" #
Disposepp# *
(pp* +
)pp+ ,
;pp, -
}qq 
catchrr 	
(rr
 
	Exceptionrr 
)rr 
{ss 
}uu 
_inputStreamvv 
=vv 
nullvv 
;vv 
disposedxx 
=xx 
truexx 
;xx 
}yy 
}zz 
}}} 
}~~ Ι
VC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\IO\ICharacterReader.cs
	namespace 	
TSQL
 
. 
IO 
{ 
internal		 	
	interface		
 
ICharacterReader		 $
:		% &
IDisposable		' 2
,		2 3
IEnumerator		4 ?
,		? @
IEnumerable		A L
,		L M
IEnumerator		N Y
<		Y Z
char		Z ^
>		^ _
,		_ `
IEnumerable		a l
<		l m
char		m q
>		q r
{

 
} 
} ¬
YC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\IO\TSQLCharacterReader.cs
	namespace		 	
TSQL		
 
.		 
IO		 
{

 
internal 	
partial
 
class 
TSQLCharacterReader +
{ 
private 	
ICharacterReader
 
_inputStream '
=( )
null* .
;. /
private 	
bool
 
_hasMore 
= 
true 
; 
private 	
bool
 
	_hasExtra 
= 
false  
;  !
private 	
char
 

_extraChar 
; 
public 
TSQLCharacterReader	 
( 

TextReader '
inputStream( 3
)3 4
{ 
_inputStream 
= 
new 
BufferedTextReader (
(( )
inputStream) 4
)4 5
;5 6
Position 
= 
- 
$num 
; 
} 
public 
bool	 
Read 
( 
) 
{ 
if 
( 
_hasMore 
) 
{ 
if 
( 
	_hasExtra 
) 
{ 
Current   
=   

_extraChar   
;   
	_hasExtra!! 
=!! 
false!! 
;!! 
}"" 
else## 
{$$ 
_hasMore%% 
=%% 
_inputStream%% 
.%% 
MoveNext%% %
(%%% &
)%%& '
;%%' (
if&& 
(&& 	
_hasMore&&	 
)&& 
{'' 
Current(( 
=(( 
_inputStream(( 
.(( 
Current(( $
;(($ %
Position)) 
++)) 
;)) 
}** 
else++ 	
{,, 
Current-- 
=-- 
char-- 
.-- 
MinValue-- 
;-- 
}.. 
}// 
}00 
return22 	
_hasMore22
 
;22 
}33 
public55 
bool55	 !
ReadNextNonWhitespace55 #
(55# $
)55$ %
{66 
bool77 
hasNext77 
;77 
do99 
{:: 
hasNext;; 
=;; 
Read;; 
(;; 
);; 
;;; 
}<< 
while<< 

(<< 
hasNext<< 
&&<< 
char== 
.== 	
IsWhiteSpace==	 
(== 
Current== 
)== 
)== 
;==  
return?? 	
hasNext??
 
;?? 
}@@ 
publicBB 
voidBB	 
PutbackBB 
(BB 
)BB 
{CC 
	_hasExtraDD 
=DD 
trueDD 
;DD 

_extraCharEE 
=EE 
CurrentEE 
;EE 
_hasMoreFF 
=FF 
trueFF 
;FF 
}GG 
publicII 
charII	 
CurrentII 
{JJ 
getKK 
;KK 
privateMM 

setMM 
;MM 
}NN 
publicPP 
intPP	 
PositionPP 
{QQ 
getRR 
;RR 
privateTT 

setTT 
;TT 
}UU 
publicWW 
boolWW	 
EOFWW 
{XX 
getYY 
{ZZ 
return[[ 

![[ 
_hasMore[[ 
;[[ 
}\\ 
}]] 
}^^ 
}__ δ
eC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\IO\TSQLCharacterReader.IDisposable.cs
	namespace 	
TSQL
 
. 
IO 
{ 
partial 
class	 
TSQLCharacterReader "
:# $
IDisposable% 0
{ 
private		 	
bool		
 
	_disposed		 
=		 
false		  
;		  !
void 
IDisposable 
. 
Dispose 
( 
) 
{ 
if 
( 
! 
	_disposed 
) 
{ 
Dispose 
( 
true 
) 
; 
} 
} 
private 	
void
 
Dispose 
( 
bool 
	disposing %
)% &
{ 
if 
( 
! 
	_disposed 
) 
{ 
if 
( 
	disposing 
) 
{   
}"" 
try%% 
{&& 
('' 
_inputStream'' 
as'' 
IDisposable'' !
)''! "
.''" #
Dispose''# *
(''* +
)''+ ,
;'', -
}(( 
catch)) 	
())
 
	Exception)) 
))) 
{** 
},, 
_inputStream-- 
=-- 
null-- 
;-- 
	_disposed// 
=// 
true// 
;// 
}00 
}11 
}44 
}55 Ω
QC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\ITSQLTokenizer.cs
	namespace		 	
TSQL		
 
{

 
internal 	
	interface
 
ITSQLTokenizer "
:# $
IEnumerator% 0
,0 1
IEnumerable2 =
,= >
IEnumerator? J
<J K
	TSQLTokenK T
>T U
,U V
IEnumerableW b
<b c
	TSQLTokenc l
>l m
{ 
void 
Putback 
( 
) 
; 
} 
} –
ZC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str &
)& '
]' (
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 L
)		L M
]		M N
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str (
)( )
]) *
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str /
)/ 0
]0 1
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[## 
assembly## 	
:##	 

AssemblyVersion## 
(## 
$str## $
)##$ %
]##% &
[$$ 
assembly$$ 	
:$$	 

AssemblyFileVersion$$ 
($$ 
$str$$ (
)$$( )
]$$) *
[&& 
assembly&& 	
:&&	 

InternalsVisibleTo&& 
(&& 
$str&& %
)&&% &
]&&& 'σ
jC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\Parsers\ITSQLStatementParser.cs
	namespace

 	
TSQL


 
.

 

Statements

 
.

 
Parsers

 !
{ 
internal 	
	interface
  
ITSQLStatementParser (
{ 
TSQLStatement 
Parse 
( 
ITSQLTokenizer $
	tokenizer% .
). /
;/ 0
} 
} ω=
oC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\Parsers\TSQLSelectStatementParser.cs
	namespace

 	
TSQL


 
.

 

Statements

 
.

 
Parsers

 !
{ 
internal 	
class
 %
TSQLSelectStatementParser )
:* + 
ITSQLStatementParser, @
{ 
public 
TSQLSelectStatement	 
Parse "
(" #
ITSQLTokenizer# 1
	tokenizer2 ;
); <
{ 
TSQLSelectStatement 
select 
= 
new  #
TSQLSelectStatement$ 7
(7 8
)8 9
;9 :
TSQLSelectClause 
selectClause  
=! "
new# &"
TSQLSelectClauseParser' =
(= >
)> ?
.? @
Parse@ E
(E F
	tokenizerF O
)O P
;P Q
select 	
.	 

Select
 
= 
selectClause 
;  
select 	
.	 

Tokens
 
. 
AddRange 
( 
selectClause &
.& '
Tokens' -
)- .
;. /
if 
( 
	tokenizer 
. 
Current 
. 
	IsKeyword "
(" #
TSQLKeywords# /
./ 0
INTO0 4
)4 5
)5 6
{ 
TSQLIntoClause 

intoClause 
= 
new  # 
TSQLIntoClauseParser$ 8
(8 9
)9 :
.: ;
Parse; @
(@ A
	tokenizerA J
)J K
;K L
select 

.
 
Into 
= 

intoClause 
; 
select 

.
 
Tokens 
. 
AddRange 
( 

intoClause %
.% &
Tokens& ,
), -
;- .
} 
if!! 
(!! 
	tokenizer!! 
.!! 
Current!! 
.!! 
	IsKeyword!! "
(!!" #
TSQLKeywords!!# /
.!!/ 0
FROM!!0 4
)!!4 5
)!!5 6
{"" 
TSQLFromClause## 

fromClause## 
=## 
new##  # 
TSQLFromClauseParser##$ 8
(##8 9
)##9 :
.##: ;
Parse##; @
(##@ A
	tokenizer##A J
)##J K
;##K L
select%% 

.%%
 
From%% 
=%% 

fromClause%% 
;%% 
select'' 

.''
 
Tokens'' 
.'' 
AddRange'' 
('' 

fromClause'' %
.''% &
Tokens''& ,
)'', -
;''- .
}(( 
if** 
(** 
	tokenizer** 
.** 
Current** 
.** 
	IsKeyword** "
(**" #
TSQLKeywords**# /
.**/ 0
WHERE**0 5
)**5 6
)**6 7
{++ 
TSQLWhereClause,, 
whereClause,, 
=,,  !
new,," %!
TSQLWhereClauseParser,,& ;
(,,; <
),,< =
.,,= >
Parse,,> C
(,,C D
	tokenizer,,D M
),,M N
;,,N O
select.. 

...
 
Where.. 
=.. 
whereClause.. 
;.. 
select00 

.00
 
Tokens00 
.00 
AddRange00 
(00 
whereClause00 &
.00& '
Tokens00' -
)00- .
;00. /
}11 
if33 
(33 
	tokenizer33 
.33 
Current33 
.33 
	IsKeyword33 "
(33" #
TSQLKeywords33# /
.33/ 0
GROUP330 5
)335 6
)336 7
{44 
TSQLGroupByClause55 
groupByClause55 #
=55$ %
new55& )#
TSQLGroupByClauseParser55* A
(55A B
)55B C
.55C D
Parse55D I
(55I J
	tokenizer55J S
)55S T
;55T U
select77 

.77
 
GroupBy77 
=77 
groupByClause77 "
;77" #
select99 

.99
 
Tokens99 
.99 
AddRange99 
(99 
groupByClause99 (
.99( )
Tokens99) /
)99/ 0
;990 1
}:: 
if<< 
(<< 
	tokenizer<< 
.<< 
Current<< 
.<< 
	IsKeyword<< "
(<<" #
TSQLKeywords<<# /
.<</ 0
HAVING<<0 6
)<<6 7
)<<7 8
{== 
TSQLHavingClause>> 
havingClause>> !
=>>" #
new>>$ '"
TSQLHavingClauseParser>>( >
(>>> ?
)>>? @
.>>@ A
Parse>>A F
(>>F G
	tokenizer>>G P
)>>P Q
;>>Q R
select@@ 

.@@
 
Having@@ 
=@@ 
havingClause@@  
;@@  !
selectBB 

.BB
 
TokensBB 
.BB 
AddRangeBB 
(BB 
havingClauseBB '
.BB' (
TokensBB( .
)BB. /
;BB/ 0
}CC 
ifEE 
(EE 
	tokenizerEE 
.EE 
CurrentEE 
.EE 
	IsKeywordEE "
(EE" #
TSQLKeywordsEE# /
.EE/ 0
ORDEREE0 5
)EE5 6
)EE6 7
{FF 
TSQLOrderByClauseGG 
orderByClauseGG #
=GG$ %
newGG& )#
TSQLOrderByClauseParserGG* A
(GGA B
)GGB C
.GGC D
ParseGGD I
(GGI J
	tokenizerGGJ S
)GGS T
;GGT U
selectII 

.II
 
OrderByII 
=II 
orderByClauseII "
;II" #
selectKK 

.KK
 
TokensKK 
.KK 
AddRangeKK 
(KK 
orderByClauseKK (
.KK( )
TokensKK) /
)KK/ 0
;KK0 1
}LL 
ifNN 
(NN 
	tokenizerNN 
.NN 
CurrentNN 
.NN 
	IsKeywordNN "
(NN" #
TSQLKeywordsNN# /
.NN/ 0
OPTIONNN0 6
)NN6 7
)NN7 8
{OO 
TSQLOptionClausePP 
optionClausePP !
=PP" #
newPP$ '"
TSQLOptionClauseParserPP( >
(PP> ?
)PP? @
.PP@ A
ParsePPA F
(PPF G
	tokenizerPPG P
)PPP Q
;PPQ R
selectRR 

.RR
 
OptionRR 
=RR 
optionClauseRR  
;RR  !
selectTT 

.TT
 
TokensTT 
.TT 
AddRangeTT 
(TT 
optionClauseTT '
.TT' (
TokensTT( .
)TT. /
;TT/ 0
}UU 
ifWW 
(WW 
	tokenizerXX 
.XX 
CurrentXX 
!=XX 
nullXX 
&&XX  
	tokenizerYY 
.YY 
CurrentYY 
.YY 
TypeYY 
==YY 
TSQLTokenTypeYY +
.YY+ ,
KeywordYY, 3
)YY3 4
{ZZ 
	tokenizer[[ 
.[[ 
Putback[[ 
([[ 
)[[ 
;[[ 
}\\ 
return^^ 	
select^^
 
;^^ 
}__ 
TSQLStatementaa  
ITSQLStatementParseraa $
.aa$ %
Parseaa% *
(aa* +
ITSQLTokenizeraa+ 9
	tokenizeraa: C
)aaC D
{bb 
returncc 	
Parsecc
 
(cc 
	tokenizercc 
)cc 
;cc 
}dd 
}ee 
}ff ‘
pC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\Parsers\TSQLStatementParserFactory.cs
	namespace 	
TSQL
 
. 

Statements 
. 
Parsers !
{		 
internal

 	
class


 &
TSQLStatementParserFactory

 *
{ 
public  
ITSQLStatementParser	 
Create $
($ %
	TSQLToken% .
token/ 4
)4 5
{ 
if 
( 
token 
. 
Type 
== 
TSQLTokenType "
." #
Keyword# *
)* +
{ 
TSQLKeywords 
keyword 
= 
token  
.  !
	AsKeyword! *
.* +
Keyword+ 2
;2 3
if 
( 
keyword 
== 
TSQLKeywords 
.  
SELECT  &
)& '
{ 
return 
new %
TSQLSelectStatementParser )
() *
)* +
;+ ,
} 
else++ 
{,, 
return-- 
new-- &
TSQLUnknownStatementParser-- *
(--* +
)--+ ,
;--, -
}.. 
}// 
else00 
{11 
return22 

new22 &
TSQLUnknownStatementParser22 )
(22) *
)22* +
;22+ ,
}66 
}77 
}88 
}99 ψ
pC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\Parsers\TSQLUnknownStatementParser.cs
	namespace 	
TSQL
 
. 

Statements 
. 
Parsers !
{		 
internal

 	
class


 &
TSQLUnknownStatementParser

 *
:

+ , 
ITSQLStatementParser

- A
{ 
public  
TSQLUnknownStatement	 
Parse #
(# $
ITSQLTokenizer$ 2
	tokenizer3 <
)< =
{  
TSQLUnknownStatement 
	statement !
=" #
new$ ' 
TSQLUnknownStatement( <
(< =
)= >
;> ?
	statement 
. 
Tokens 
. 
Add 
( 
	tokenizer !
.! "
Current" )
)) *
;* +
while 
(	 

	tokenizer 
. 
MoveNext 
( 
) 
&& 
! 
( 
	tokenizer 
. 
Current 
is 
TSQLCharacter '
&&( *
	tokenizer 
. 
Current 
. 
AsCharacter "
." #
	Character# ,
==- /
TSQLCharacters0 >
.> ?
	Semicolon? H
) 
) 
{ 
	statement 
. 
Tokens 
. 
Add 
( 
	tokenizer "
." #
Current# *
)* +
;+ ,
} 
if 
( 
	tokenizer 
. 
Current 
!= 
null 
&&  
	tokenizer 
. 
Current 
is 
TSQLCharacter &
&&' )
	tokenizer 
. 
Current 
. 
AsCharacter !
.! "
	Character" +
==, .
TSQLCharacters/ =
.= >
	Semicolon> G
)G H
{   
	statement!! 
.!! 
Tokens!! 
.!! 
Add!! 
(!! 
	tokenizer!! "
.!!" #
Current!!# *
)!!* +
;!!+ ,
}"" 
return$$ 	
	statement$$
 
;$$ 
}%% 
TSQLStatement''  
ITSQLStatementParser'' $
.''$ %
Parse''% *
(''* +
ITSQLTokenizer''+ 9
	tokenizer'': C
)''C D
{(( 
return)) 	
Parse))
 
()) 
	tokenizer)) 
))) 
;)) 
}** 
}++ 
},, υ
aC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\TSQLSelectStatement.cs
	namespace 	
TSQL
 
. 

Statements 
{		 
public

 
class

 
TSQLSelectStatement

 !
:

" #
TSQLStatement

$ 1
{ 
internal 

TSQLSelectStatement 
( 
)  
{ 
} 
public 
override	 
TSQLStatementType #
Type$ (
{ 
get 
{ 
return 

TSQLStatementType 
. 
Select #
;# $
} 
} 
public 
TSQLSelectClause	 
Select  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
TSQLIntoClause	 
Into 
{ 
get "
;" #
set$ '
;' (
}) *
public!! 
TSQLFromClause!!	 
From!! 
{!! 
get!! "
;!!" #
set!!$ '
;!!' (
}!!) *
public## 
TSQLWhereClause##	 
Where## 
{##  
get##! $
;##$ %
set##& )
;##) *
}##+ ,
public%% 
TSQLGroupByClause%%	 
GroupBy%% "
{%%# $
get%%% (
;%%( )
set%%* -
;%%- .
}%%/ 0
public'' 
TSQLHavingClause''	 
Having''  
{''! "
get''# &
;''& '
set''( +
;''+ ,
}''- .
public)) 
TSQLOrderByClause))	 
OrderBy)) "
{))# $
get))% (
;))( )
set))* -
;))- .
}))/ 0
public++ 
TSQLOptionClause++	 
Option++  
{++! "
get++# &
;++& '
set++( +
;+++ ,
}++- .
},, 
}-- Ν
[C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\TSQLStatement.cs
	namespace 	
TSQL
 
. 

Statements 
{		 
public

 
abstract

 
class

 
TSQLStatement

 $
{ 
private 	
List
 
< 
	TSQLToken 
> 
_tokens !
=" #
new$ '
List( ,
<, -
	TSQLToken- 6
>6 7
(7 8
)8 9
;9 :
public 
abstract	 
TSQLStatementType #
Type$ (
{ 
get 
; 
} 
public 
List	 
< 
	TSQLToken 
> 
Tokens 
{ 
get 
{ 
return 

_tokens 
; 
} 
} 
} 
} ο
^C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLSystemIdentifier.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class  
TSQLSystemIdentifier "
:# $
TSQLIdentifier% 3
{		 
internal

 
 
TSQLSystemIdentifier

 
(

  
int 
beginPostion 
, 
string 	
text
 
) 
: 
base 
( 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
SystemIdentifier )
;) *
} 
} 
public   
TSQLIdentifiers  	 

Identifier   #
{!! 
get"" 
;"" 
private## 

set## 
;## 
}$$ 
}%% 
}&& α
\C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLSystemVariable.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLSystemVariable  
:! "
TSQLVariable# /
{		 
internal

 

TSQLSystemVariable

 
(

 
int 
beginPostion 
, 
string 	
text
 
) 
: 
base 
( 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
SystemVariable '
;' (
} 
} 
public   
TSQLVariables  	 
Variable   
{!! 
get"" 
;"" 
private## 

set## 
;## 
}$$ 
}%% 
}&& ¤
[C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLBinaryLiteral.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLBinaryLiteral 
:  !
TSQLLiteral" -
{		 
internal

 

TSQLBinaryLiteral

 
(

 
int 
beginPostion 
, 
string 	
text
 
) 
: 
base 
( 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
BinaryLiteral &
;& '
} 
} 
} 
}    
ZC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLMoneyLiteral.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLMoneyLiteral 
:  
TSQLLiteral! ,
{		 
internal

 

TSQLMoneyLiteral

 
(

 
int 
beginPostion 
, 
string 	
text
 
) 
: 
base 
( 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
MoneyLiteral %
;% &
} 
} 
} 
}   „]
RC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLIdentifiers.cs
	namespace 	
TSQL
 
{ 
public 
class 
TSQLIdentifiers 
{		 
private

 	
static


 

Dictionary

 
<

 
string

 "
,

" #
TSQLIdentifiers

$ 3
>

3 4
identifierLookup

5 E
=

F G
new 

Dictionary 
< 
string 
, 
TSQLIdentifiers )
>) *
(* +
StringComparer+ 9
.9 :&
InvariantCultureIgnoreCase: T
)T U
;U V
public 
static	 
readonly 
TSQLIdentifiers (
None) -
=. /
new0 3
TSQLIdentifiers4 C
(C D
$strD F
)F G
;G H
public 
static	 
readonly 
TSQLIdentifiers (
COALESCE) 1
=2 3
new4 7
TSQLIdentifiers8 G
(G H
$strH R
)R S
;S T
public 
static	 
readonly 
TSQLIdentifiers (
CONTAINS) 1
=2 3
new4 7
TSQLIdentifiers8 G
(G H
$strH R
)R S
;S T
public 
static	 
readonly 
TSQLIdentifiers (
CONTAINSTABLE) 6
=7 8
new9 <
TSQLIdentifiers= L
(L M
$strM \
)\ ]
;] ^
public 
static	 
readonly 
TSQLIdentifiers (
CONVERT) 0
=1 2
new3 6
TSQLIdentifiers7 F
(F G
$strG P
)P Q
;Q R
public 
static	 
readonly 
TSQLIdentifiers (
CURRENT_DATE) 5
=6 7
new8 ;
TSQLIdentifiers< K
(K L
$strL Z
)Z [
;[ \
public 
static	 
readonly 
TSQLIdentifiers (
CURRENT_TIME) 5
=6 7
new8 ;
TSQLIdentifiers< K
(K L
$strL Z
)Z [
;[ \
public 
static	 
readonly 
TSQLIdentifiers (
CURRENT_TIMESTAMP) :
=; <
new= @
TSQLIdentifiersA P
(P Q
$strQ d
)d e
;e f
public 
static	 
readonly 
TSQLIdentifiers (
CURRENT_USER) 5
=6 7
new8 ;
TSQLIdentifiers< K
(K L
$strL Z
)Z [
;[ \
public 
static	 
readonly 
TSQLIdentifiers (
FREETEXT) 1
=2 3
new4 7
TSQLIdentifiers8 G
(G H
$strH R
)R S
;S T
public 
static	 
readonly 
TSQLIdentifiers (
FREETEXTTABLE) 6
=7 8
new9 <
TSQLIdentifiers= L
(L M
$strM \
)\ ]
;] ^
public 
static	 
readonly 
TSQLIdentifiers (
NULLIF) /
=0 1
new2 5
TSQLIdentifiers6 E
(E F
$strF N
)N O
;O P
public 
static	 
readonly 
TSQLIdentifiers (
OPENDATASOURCE) 7
=8 9
new: =
TSQLIdentifiers> M
(M N
$strN ^
)^ _
;_ `
public 
static	 
readonly 
TSQLIdentifiers (
	OPENQUERY) 2
=3 4
new5 8
TSQLIdentifiers9 H
(H I
$strI T
)T U
;U V
public 
static	 
readonly 
TSQLIdentifiers (

OPENROWSET) 3
=4 5
new6 9
TSQLIdentifiers: I
(I J
$strJ V
)V W
;W X
public 
static	 
readonly 
TSQLIdentifiers (
OPENXML) 0
=1 2
new3 6
TSQLIdentifiers7 F
(F G
$strG P
)P Q
;Q R
public   
static  	 
readonly   
TSQLIdentifiers   (
	RAISERROR  ) 2
=  3 4
new  5 8
TSQLIdentifiers  9 H
(  H I
$str  I T
)  T U
;  U V
public!! 
static!!	 
readonly!! 
TSQLIdentifiers!! ("
SEMANTICKEYPHRASETABLE!!) ?
=!!@ A
new!!B E
TSQLIdentifiers!!F U
(!!U V
$str!!V n
)!!n o
;!!o p
public"" 
static""	 
readonly"" 
TSQLIdentifiers"" (*
SEMANTICSIMILARITYDETAILSTABLE"") G
=""H I
new""J M
TSQLIdentifiers""N ]
(""] ^
$str""^ ~
)""~ 
;	"" €
public## 
static##	 
readonly## 
TSQLIdentifiers## (#
SEMANTICSIMILARITYTABLE##) @
=##A B
new##C F
TSQLIdentifiers##G V
(##V W
$str##W p
)##p q
;##q r
public$$ 
static$$	 
readonly$$ 
TSQLIdentifiers$$ (
SESSION_USER$$) 5
=$$6 7
new$$8 ;
TSQLIdentifiers$$< K
($$K L
$str$$L Z
)$$Z [
;$$[ \
public%% 
static%%	 
readonly%% 
TSQLIdentifiers%% (
SYSTEM_USER%%) 4
=%%5 6
new%%7 :
TSQLIdentifiers%%; J
(%%J K
$str%%K X
)%%X Y
;%%Y Z
public&& 
static&&	 
readonly&& 
TSQLIdentifiers&& (
TRY_CONVERT&&) 4
=&&5 6
new&&7 :
TSQLIdentifiers&&; J
(&&J K
$str&&K X
)&&X Y
;&&Y Z
private** 	
string**
 

Identifier** 
;** 
private,, 	
TSQLIdentifiers,,
 
(,, 
string-- 	

identifier--
 
)-- 
{.. 

Identifier// 
=// 

identifier// 
;// 
if00 
(00 

identifier00 
.00 
Length00 
>00 
$num00 
)00 
{11 
identifierLookup22 
[22 

identifier22 
]22  
=22! "
this22# '
;22' (
}33 
}44 
public66 
static66	 
TSQLIdentifiers66 
Parse66  %
(66% &
string77 	
token77
 
)77 
{88 
if99 
(99 
!:: 
string:: 
.:: 
IsNullOrEmpty:: 
(:: 
token:: 
)::  
&&::! #
identifierLookup;; 
.;; 
ContainsKey;;  
(;;  !
token;;! &
);;& '
);;' (
{<< 
return== 

identifierLookup== 
[== 
token== !
]==! "
;==" #
}>> 
else?? 
{@@ 
returnAA 

TSQLIdentifiersAA 
.AA 
NoneAA 
;AA  
}BB 
}CC 
publicEE 
staticEE	 
boolEE 
IsIdentifierEE !
(EE! "
stringFF 	
tokenFF
 
)FF 
{GG 
ifHH 
(HH 
!HH 
stringHH 
.HH 
IsNullOrWhiteSpaceHH !
(HH! "
tokenHH" '
)HH' (
)HH( )
{II 
returnJJ 

identifierLookupJJ 
.JJ 
ContainsKeyJJ '
(JJ' (
tokenJJ( -
)JJ- .
;JJ. /
}KK 
elseLL 
{MM 
returnNN 

falseNN 
;NN 
}OO 
}PP 
publicRR 
boolRR	 
InRR 
(RR 
paramsRR 
TSQLIdentifiersRR '
[RR' (
]RR( )
identifiersRR* 5
)RR5 6
{SS 
returnTT 	
identifiersUU 
!=UU 
nullUU 
&&UU 
identifiersVV 
.VV 
ContainsVV 
(VV 
thisVV 
)VV 
;VV 
}WW 
public[[ 
static[[	 
bool[[ 
operator[[ 
==[[  
([[  !
TSQLIdentifiers\\ 
a\\ 
,\\ 
TSQLIdentifiers]] 
b]] 
)]] 
{^^ 
if__ 
(__ 
Object__ 
.__ 
ReferenceEquals__ 
(__ 
a__ 
,__  
null__! %
)__% &
)__& '
{`` 
ifaa 
(aa 
Objectaa 
.aa 
ReferenceEqualsaa 
(aa 
baa  
,aa  !
nullaa" &
)aa& '
)aa' (
{bb 
returndd 
truedd 
;dd 
}ee 
returnhh 

falsehh 
;hh 
}ii 
returnll 	
all
 
.ll 
Equalsll 
(ll 
bll 
)ll 
;ll 
}mm 
publicoo 
staticoo	 
booloo 
operatoroo 
!=oo  
(oo  !
TSQLIdentifierspp 
app 
,pp 
TSQLIdentifiersqq 
bqq 
)qq 
{rr 
returnss 	
!ss
 
(ss 
ass 
==ss 
bss 
)ss 
;ss 
}tt 
publicvv 
boolvv	 
Equalsvv 
(vv 
TSQLIdentifiersvv $
objvv% (
)vv( )
{ww 
ifyy 
(yy 
Objectyy 
.yy 
ReferenceEqualsyy 
(yy 
objyy !
,yy! "
nullyy# '
)yy' (
)yy( )
{zz 
return{{ 

false{{ 
;{{ 
}|| 
if 
( 
Object 
. 
ReferenceEquals 
( 
this "
," #
obj$ '
)' (
)( )
{
€€ 
return
 

true
 
;
 
}
‚‚ 
if
…… 
(
…… 
this
…… 
.
…… 
GetType
…… 
(
…… 
)
…… 
!=
…… 
obj
…… 
.
…… 
GetType
…… $
(
……$ %
)
……% &
)
……& '
return
†† 

false
†† 
;
†† 
return
‹‹ 	

Identifier
‹‹
 
==
‹‹ 
obj
‹‹ 
.
‹‹ 

Identifier
‹‹ &
;
‹‹& '
}
 
public
 
override
	 
bool
 
Equals
 
(
 
object
 $
obj
% (
)
( )
{
 
return
 	
Equals

 
(
 
obj
 
as
 
TSQLIdentifiers
 '
)
' (
;
( )
}
‘‘ 
public
““ 
override
““	 
int
““ 
GetHashCode
““ !
(
““! "
)
““" #
{
”” 
return
•• 	

Identifier
••
 
.
•• 
GetHashCode
••  
(
••  !
)
••! "
;
••" #
}
–– 
}
™™ 
} Ή,
VC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLStatementReader.cs
	namespace 	
TSQL
 
{ 
public 
partial 
class 
TSQLStatementReader )
{ 
private 	
TSQLTokenizer
 

_tokenizer "
=# $
null% )
;) *
private 	
bool
 
_hasMore 
= 
true 
; 
private 	
TSQLStatement
 
_current  
=! "
null# '
;' (
public 
TSQLStatementReader	 
( 
string 	
tsqlText
 
) 
{ 

_tokenizer 
= 
new 
TSQLTokenizer !
(! "
tsqlText" *
)* +
;+ ,
} 
public 
TSQLStatementReader	 
( 

TextReader 

tsqlStream 
) 
{ 

_tokenizer 
= 
new 
TSQLTokenizer !
(! "

tsqlStream" ,
), -
;- .
} 
public 
TSQLStatementReader	 
( 
TSQLTokenizer *
	tokenizer+ 4
)4 5
{   

_tokenizer!! 
=!! 
	tokenizer!! 
;!! 
}"" 
public$$ 
bool$$	  
UseQuotedIdentifiers$$ "
{%% 
get&& 
{'' 
return(( 


_tokenizer(( 
.((  
UseQuotedIdentifiers(( *
;((* +
})) 
set++ 
{,, 

_tokenizer-- 
.--  
UseQuotedIdentifiers-- #
=--$ %
value--& +
;--+ ,
}.. 
}// 
public11 
bool11	 
IncludeWhitespace11 
{22 
get33 
{44 
return55 


_tokenizer55 
.55 
IncludeWhitespace55 '
;55' (
}66 
set88 
{99 

_tokenizer:: 
.:: 
IncludeWhitespace::  
=::! "
value::# (
;::( )
};; 
}<< 
public>> 
bool>>	 
MoveNext>> 
(>> 
)>> 
{?? 
CheckDisposed@@ 
(@@ 
)@@ 
;@@ 
ifBB 
(BB 
_hasMoreBB 
)BB 
{CC 
whileDD 	
(DD
 

_tokenizerEE 
.EE 
MoveNextEE 
(EE 
)EE 
&&EE 
(FF 

_tokenizerGG 
.GG 
CurrentGG 
.GG 
TypeGG 
==GG  
TSQLTokenTypeGG! .
.GG. /
SingleLineCommentGG/ @
||GGA C

_tokenizerHH 
.HH 
CurrentHH 
.HH 
TypeHH 
==HH  
TSQLTokenTypeHH! .
.HH. /
MultilineCommentHH/ ?
||HH@ B

_tokenizerII 
.II 
CurrentII 
.II 
TypeII 
==II  
TSQLTokenTypeII! .
.II. /

WhitespaceII/ 9
||II: <
(JJ 

_tokenizerKK 
.KK 
CurrentKK 
.KK 
TypeKK 
==KK !
TSQLTokenTypeKK" /
.KK/ 0
	CharacterKK0 9
&&KK: <

_tokenizerLL 
.LL 
CurrentLL 
.LL 
AsCharacterLL %
.LL% &
	CharacterLL& /
==LL0 2
TSQLCharactersLL3 A
.LLA B
	SemicolonLLB K
)MM 
)NN 
)NN 
{PP 
}RR 
ifTT 
(TT 

_tokenizerTT 
.TT 
CurrentTT 
==TT 
nullTT "
)TT" #
{UU 
_hasMoreVV 
=VV 
falseVV 
;VV 
returnXX 
_hasMoreXX 
;XX 
}YY 
_current[[ 
=[[ 
new[[ &
TSQLStatementParserFactory[[ -
([[- .
)[[. /
.[[/ 0
Create[[0 6
([[6 7

_tokenizer[[7 A
.[[A B
Current[[B I
)[[I J
.[[J K
Parse[[K P
([[P Q

_tokenizer[[Q [
)[[[ \
;[[\ ]
}\\ 
return^^ 	
_hasMore^^
 
;^^ 
}__ 
publicaa 
TSQLStatementaa	 
Currentaa 
{bb 
getcc 
{dd 
CheckDisposedee 
(ee 
)ee 
;ee 
returngg 

_currentgg 
;gg 
}hh 
}ii 
publickk 
statickk	 
Listkk 
<kk 
TSQLStatementkk "
>kk" #
ParseStatementskk$ 3
(kk3 4
stringll 	
tsqlTextll
 
,ll 
boolmm  
useQuotedIdentifiersmm 
=mm 
falsemm $
,mm$ %
boolnn 
includeWhitespacenn 
=nn 
falsenn !
)nn! "
{oo 
returnpp 	
newpp
 
TSQLStatementReaderpp !
(pp! "
tsqlTextqq 
)qq 
{rr 
IncludeWhitespacess 
=ss 
includeWhitespacess *
,ss* + 
UseQuotedIdentifierstt 
=tt  
useQuotedIdentifierstt 0
}uu 
.uu 
ToListuu 
(uu 
)uu 
;uu 
}vv 
}ww 
}xx Π
bC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLStatementReader.IDisposable.cs
	namespace 	
TSQL
 
{ 
partial 
class	 
TSQLStatementReader "
:# $
IDisposable% 0
{ 
private		 	
bool		
 
	_disposed		 
=		 
false		  
;		  !
void 
IDisposable 
. 
Dispose 
( 
) 
{ 
if 
( 
! 
	_disposed 
) 
{ 
Dispose 
( 
true 
) 
; 
} 
} 
private 	
void
 
Dispose 
( 
bool 
	disposing %
)% &
{ 
if 
( 
! 
	_disposed 
) 
{ 
if 
( 
	disposing 
) 
{   
}"" 
try%% 
{&& 
('' 

_tokenizer'' 
as'' 
IDisposable'' 
)''  
.''  !
Dispose''! (
(''( )
)'') *
;''* +
}(( 
catch)) 	
())
 
	Exception)) 
))) 
{** 
},, 

_tokenizer-- 
=-- 
null-- 
;-- 
	_disposed// 
=// 
true// 
;// 
}00 
}11 
private:: 	
void::
 
CheckDisposed:: 
(:: 
):: 
{;; 
if<< 
(<< 
	_disposed<< 
)<< 
{== 
throw>> 	
new>>
 #
ObjectDisposedException>> %
(>>% &
GetType>>& -
(>>- .
)>>. /
.>>/ 0
FullName>>0 8
,>>8 9
$str>>: e
+>>f g
$str?? ,
+??- .
$str@@ 
)@@ 
;@@ 
}AA 
}BB 
}EE 
}FF  
bC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLStatementReader.IEnumerable.cs
	namespace		 	
TSQL		
 
{

 
partial 
class	 
TSQLStatementReader "
:# $
IEnumerator% 0
,0 1
IEnumerable2 =
,= >
IEnumerator? J
<J K
TSQLStatementK X
>X Y
,Y Z
IEnumerable[ f
<f g
TSQLStatementg t
>t u
{ 
IEnumerator 
< 
TSQLStatement 
> 
IEnumerable (
<( )
TSQLStatement) 6
>6 7
.7 8
GetEnumerator8 E
(E F
)F G
{ 
return 	
this
 
; 
} 
IEnumerator 
IEnumerable 
. 
GetEnumerator '
(' (
)( )
{ 
return 	
this
 
; 
} 
object 
IEnumerator	 
. 
Current 
{ 
get 
{ 
return 

( 
this 
as 
IEnumerator 
<  
TSQLStatement  -
>- .
). /
./ 0
Current0 7
;7 8
} 
} 
void 
IEnumerator 
. 
Reset 
( 
) 
{   
throw!! 
new!!	 #
NotImplementedException!! $
(!!$ %
)!!% &
;!!& '
}"" 
}## 
}$$ δ
_C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\TSQLStatementType.cs
	namespace 	
TSQL
 
. 

Statements 
{ 
public 
enum 
TSQLStatementType 
{		 
Select 
, 	
Unknown 	
} 
} ί
bC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Statements\TSQLUnknownStatement.cs
	namespace 	
TSQL
 
. 

Statements 
{ 
public 
class  
TSQLUnknownStatement "
:# $
TSQLStatement% 2
{		 
internal

 
 
TSQLUnknownStatement

 
(

  
)

  !
{ 
} 
public 
override	 
TSQLStatementType #
Type$ (
{ 
get 
{ 
return 

TSQLStatementType 
. 
Unknown $
;$ %
} 
} 
} 
} Κ
WC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLTokenType.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
enum 
TSQLTokenType 
{ 

Whitespace 
, 
	Character 
, 

Identifier 
, 
SystemIdentifier 
, 
Keyword## 	
,##	 

SingleLineComment)) 
,)) 
MultilineComment// 
,// 
Operator55 

,55
 
Variable;; 

,;;
 
SystemVariableAA 
,AA 
NumericLiteralGG 
,GG 
StringLiteralMM 
,MM 
MoneyLiteralSS 
,SS 
BinaryLiteralYY 
}ZZ 
}[[ ο	
WC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLCharacter.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLCharacter 
: 
	TSQLToken '
{ 
internal 

TSQLCharacter 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
	Character 
= 
TSQLCharacters 
. 
Parse #
(# $
text$ (
)( )
;) *
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
	Character "
;" #
} 
} 
public 
TSQLCharacters	 
	Character !
{ 
get 
; 
private   

set   
;   
}!! 
}"" 
}## Μ
UC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLComment.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
abstract 
class 
TSQLComment "
:# $
	TSQLToken% .
{ 
internal 

TSQLComment 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
} 
public 
string	 
Comment 
{ 
get 
; 
	protected 
set 
; 
} 
} 
} Ξ
XC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLIdentifier.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLIdentifier 
: 
	TSQLToken (
{ 
internal 

TSQLIdentifier 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
if 
( 
Text 
. 

StartsWith 
( 
$str 
) 
) 
{ 
Name 
=	 

Text 
. 
	Substring 
( 
$num 
, 
Text 
. 
Length 
-  
$num! "
)" #
. 
Replace 
( 
$str 
, 
$str 
) 
; 
} 
else 
if 

( 
Text 
. 

StartsWith 
( 
$str  
)  !
)! "
{ 
Name 
=	 

Text 
. 
	Substring 
( 
$num 
, 
Text 
. 
Length 
-  
$num! "
)" #
. 
Replace 
( 
$str 
, 
$str 
) 
; 
} 
else 
if 

( 
Text 
. 

StartsWith 
( 
$str !
)! "
)" #
{ 
Name 
=	 

Text 
. 
	Substring 
( 
$num 
, 
Text 
. 
Length 
-  
$num! "
)" #
. 
Replace 
( 
$str 
, 
$str 
) 
; 
}   
else!! 
{"" 
Name## 
=##	 

Text## 
;## 
}$$ 
}%% 
public)) 
override))	 
TSQLTokenType)) 
Type))  $
{** 
get++ 
{,, 
return-- 

TSQLTokenType-- 
.-- 

Identifier-- #
;--# $
}.. 
}// 
public66 
string66	 
Name66 
{77 
get88 
;88 
private99 

set99 
;99 
}:: 
};; 
}<< ί	
UC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLKeyword.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLKeyword 
: 
	TSQLToken %
{ 
internal 

TSQLKeyword 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
Keyword 

= 
TSQLKeywords 
. 
Parse 
(  
text  $
)$ %
;% &
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
Keyword  
;  !
} 
} 
public 
TSQLKeywords	 
Keyword 
{ 
get 
; 
private   

set   
;   
}!! 
}"" 
}## ±
UC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLLiteral.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
abstract 
class 
TSQLLiteral "
:# $
	TSQLToken% .
{ 
internal 

	protected 
TSQLLiteral  
(  !
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
} 
} 
} °	
^C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLMultilineComment.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class  
TSQLMultilineComment "
:# $
TSQLComment% 0
{ 
internal 
 
TSQLMultilineComment 
(  
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
Comment 

= 
Text 
. 
	Substring 
( 
$num 
, 
Text #
.# $
Length$ *
-+ ,
$num- .
). /
;/ 0
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
MultilineComment )
;) *
} 
} 
} 
} ¨
\C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLNumericLiteral.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLNumericLiteral  
:! "
TSQLLiteral# .
{ 
internal 

TSQLNumericLiteral 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
NumericLiteral '
;' (
} 
} 
} 
} 
VC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLOperator.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLOperator 
: 
	TSQLToken &
{ 
internal 

TSQLOperator 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
Operator !
;! "
} 
} 
} 
} Ο
_C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLSingleLineComment.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class !
TSQLSingleLineComment #
:$ %
TSQLComment& 1
{ 
internal 
!
TSQLSingleLineComment  
(  !
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
Comment 

= 
Text 
. 
	Substring 
( 
$num 
) 
; 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
SingleLineComment *
;* +
} 
} 
} 
} ¤
[C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLStringLiteral.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLStringLiteral 
:  !
TSQLLiteral" -
{ 
internal 

TSQLStringLiteral 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
int 
quotePosition 
= 
$num 
; 
int 
length 
= 
text 
. 
Length 
- 
$num 
;  
if 
( 
text 
[ 
$num 
] 
== 
$char 
) 
{ 
quotePosition 
++ 
; 
length 

--
 
; 
	IsUnicode 
= 
true 
; 
} 
else 
{ 
	IsUnicode 
= 
false 
; 
} 
QuoteCharacter!! 
=!! 
text!! 
[!! 
quotePosition!! &
]!!& '
;!!' (
Value$$ 
=$$	 

text$$ 
.$$ 
	Substring$$ 
($$ 
quotePosition$$ '
+$$( )
$num$$* +
,$$+ ,
length$$- 3
)$$3 4
.%% 
Replace%% 
(%% 
new%% 
string%% 
(%% 
QuoteCharacter%% &
,%%& '
$num%%( )
)%%) *
,%%* +
QuoteCharacter%%, :
.%%: ;
ToString%%; C
(%%C D
)%%D E
)%%E F
;%%F G
}&& 
public** 
override**	 
TSQLTokenType** 
Type**  $
{++ 
get,, 
{-- 
return.. 

TSQLTokenType.. 
... 
StringLiteral.. &
;..& '
}// 
}00 
public77 
string77	 
Value77 
{88 
get99 
;99 
private;; 

set;; 
;;; 
}<< 
public>> 
char>>	 
QuoteCharacter>> 
{?? 
get@@ 
;@@ 
privateBB 

setBB 
;BB 
}CC 
publicEE 
boolEE	 
	IsUnicodeEE 
{FF 
getGG 
;GG 
privateII 

setII 
;II 
}JJ 
}KK 
}LL ί^
SC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLToken.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
abstract 
class 
	TSQLToken  
{ 
internal 

	protected 
	TSQLToken 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
{

 
BeginPostion 
= 
beginPostion 
; 
if 
( 
text 
== 
null 
) 
{ 
throw 	
new
 !
ArgumentNullException #
(# $
$str$ *
)* +
;+ ,
} 
Text 
= 	
text
 
; 
} 
public 
int	 
BeginPostion 
{ 
get 
; 
private 

set 
; 
} 
public 
int	 
EndPosition 
{ 
get 
{ 
return 

BeginPostion 
+ 
Length  
-! "
$num# $
;$ %
} 
} 
public!! 
int!!	 
Length!! 
{"" 
get## 
{$$ 
return%% 

Text%% 
.%% 
Length%% 
;%% 
}&& 
}'' 
public)) 
string))	 
Text)) 
{** 
get++ 
;++ 
private,, 

set,, 
;,, 
}-- 
public// 
abstract//	 
TSQLTokenType// 
Type//  $
{00 
get11 
;11 
}22 
public66 
static66	 
bool66 
operator66 
==66  
(66  !
	TSQLToken77 
a77 
,77 
	TSQLToken88 
b88 
)88 
{99 
if:: 
(:: 
Object:: 
.:: 
ReferenceEquals:: 
(:: 
a:: 
,::  
null::! %
)::% &
)::& '
{;; 
if<< 
(<< 
Object<< 
.<< 
ReferenceEquals<< 
(<< 
b<<  
,<<  !
null<<" &
)<<& '
)<<' (
{== 
return?? 
true?? 
;?? 
}@@ 
returnCC 

falseCC 
;CC 
}DD 
returnGG 	
aGG
 
.GG 
EqualsGG 
(GG 
bGG 
)GG 
;GG 
}HH 
publicJJ 
staticJJ	 
boolJJ 
operatorJJ 
!=JJ  
(JJ  !
	TSQLTokenKK 
aKK 
,KK 
	TSQLTokenLL 
bLL 
)LL 
{MM 
returnNN 	
!NN
 
(NN 
aNN 
==NN 
bNN 
)NN 
;NN 
}OO 
privateQQ 	
boolQQ
 
EqualsQQ 
(QQ 
	TSQLTokenQQ 
objQQ  #
)QQ# $
{RR 
ifTT 
(TT 
ObjectTT 
.TT 
ReferenceEqualsTT 
(TT 
objTT !
,TT! "
nullTT# '
)TT' (
)TT( )
{UU 
returnVV 

falseVV 
;VV 
}WW 
ifZZ 
(ZZ 
ObjectZZ 
.ZZ 
ReferenceEqualsZZ 
(ZZ 
thisZZ "
,ZZ" #
objZZ$ '
)ZZ' (
)ZZ( )
{[[ 
return\\ 

true\\ 
;\\ 
}]] 
if`` 
(`` 
this`` 
.`` 
GetType`` 
(`` 
)`` 
!=`` 
obj`` 
.`` 
GetType`` $
(``$ %
)``% &
)``& '
returnaa 

falseaa 
;aa 
returnff 	
BeginPostiongg 
==gg 
objgg 
.gg 
BeginPostiongg $
&&gg% '
EndPositionhh 
==hh 
objhh 
.hh 
EndPositionhh "
&&hh# %
Textii 
==ii	 
objii 
.ii 
Textii 
;ii 
}jj 
publicll 
overridell	 
boolll 
Equalsll 
(ll 
objectll $
objll% (
)ll( )
{mm 
returnnn 	
Equalsnn
 
(nn 
objnn 
asnn 
	TSQLTokennn !
)nn! "
;nn" #
}oo 
publicqq 
overrideqq	 
intqq 
GetHashCodeqq !
(qq! "
)qq" #
{rr 
	uncheckedtt 
{uu 
intvv 
hashvv 
=vv 
$numvv 
;vv 
hashww 
=ww	 

hashww 
*ww 
$numww 
+ww 
BeginPostionww *
.ww* +
GetHashCodeww+ 6
(ww6 7
)ww7 8
;ww8 9
hashxx 
=xx	 

hashxx 
*xx 
$numxx 
+xx 
Textxx "
.xx" #
GetHashCodexx# .
(xx. /
)xx/ 0
;xx0 1
hashyy 
=yy	 

hashyy 
*yy 
$numyy 
+yy 
Typeyy "
.yy" #
GetHashCodeyy# .
(yy. /
)yy/ 0
;yy0 1
returnzz 

hashzz 
;zz 
}{{ 
}|| 
public
„„ 
TSQLCharacter
„„	 
AsCharacter
„„ "
{
…… 
get
†† 
{
‡‡ 
return
 

this
 
as
 
TSQLCharacter
  
;
  !
}
‰‰ 
}
 
public
 
TSQLComment
	 
	AsComment
 
{
‘‘ 
get
’’ 
{
““ 
return
”” 

this
”” 
as
”” 
TSQLComment
”” 
;
”” 
}
•• 
}
–– 
public
 
TSQLIdentifier
	 
AsIdentifier
 $
{
 
get
 
{
 
return
   

this
   
as
   
TSQLIdentifier
   !
;
  ! "
}
΅΅ 
}
ΆΆ 
public
¨¨ "
TSQLSystemIdentifier
¨¨	  
AsSystemIdentifier
¨¨ 0
{
©© 
get
ªª 
{
«« 
return
¬¬ 

this
¬¬ 
as
¬¬ "
TSQLSystemIdentifier
¬¬ '
;
¬¬' (
}
­­ 
}
®® 
public
΄΄ 
TSQLKeyword
΄΄	 
	AsKeyword
΄΄ 
{
µµ 
get
¶¶ 
{
·· 
return
ΈΈ 

this
ΈΈ 
as
ΈΈ 
TSQLKeyword
ΈΈ 
;
ΈΈ 
}
ΉΉ 
}
ΊΊ 
public
ΐΐ 
TSQLLiteral
ΐΐ	 
	AsLiteral
ΐΐ 
{
ΑΑ 
get
ΒΒ 
{
ΓΓ 
return
ΔΔ 

this
ΔΔ 
as
ΔΔ 
TSQLLiteral
ΔΔ 
;
ΔΔ 
}
ΕΕ 
}
ΖΖ 
public
ΜΜ "
TSQLMultilineComment
ΜΜ	  
AsMultilineComment
ΜΜ 0
{
ΝΝ 
get
ΞΞ 
{
ΟΟ 
return
ΠΠ 

this
ΠΠ 
as
ΠΠ "
TSQLMultilineComment
ΠΠ '
;
ΠΠ' (
}
ΡΡ 
}
ÒÒ 
public
ΨΨ  
TSQLNumericLiteral
ΨΨ	 
AsNumericLiteral
ΨΨ ,
{
ΩΩ 
get
ΪΪ 
{
ΫΫ 
return
άά 

this
άά 
as
άά  
TSQLNumericLiteral
άά %
;
άά% &
}
έέ 
}
ήή 
public
δδ 
TSQLOperator
δδ	 

AsOperator
δδ  
{
εε 
get
ζζ 
{
ηη 
return
θθ 

this
θθ 
as
θθ 
TSQLOperator
θθ 
;
θθ  
}
ιι 
}
κκ 
public
ππ #
TSQLSingleLineComment
ππ	 !
AsSingleLineComment
ππ 2
{
ρρ 
get
ςς 
{
σσ 
return
ττ 

this
ττ 
as
ττ #
TSQLSingleLineComment
ττ (
;
ττ( )
}
υυ 
}
φφ 
public
όό 
TSQLStringLiteral
όό	 
AsStringLiteral
όό *
{
ύύ 
get
ώώ 
{
ÿÿ 
return
€€ 

this
€€ 
as
€€ 
TSQLStringLiteral
€€ $
;
€€$ %
}
 
}
‚‚ 
public
 
TSQLVariable
	 

AsVariable
  
{
‰‰ 
get
 
{
‹‹ 
return
 

this
 
as
 
TSQLVariable
 
;
  
}
 
}
 
public
””  
TSQLSystemVariable
””	 
AsSystemVariable
”” ,
{
•• 
get
–– 
{
—— 
return
 

this
 
as
  
TSQLSystemVariable
 %
;
% &
}
™™ 
}
 
public
   
TSQLWhitespace
  	 
AsWhitespace
   $
{
΅΅ 
get
ΆΆ 
{
££ 
return
¤¤ 

this
¤¤ 
as
¤¤ 
TSQLWhitespace
¤¤ !
;
¤¤! "
}
¥¥ 
}
¦¦ 
}
§§ 
public
©© 
static
©© 
class
©© !
TSQLTokenExtensions
©© (
{
ªª 
public
«« 
static
««	 
bool
«« 
	IsKeyword
«« 
(
«« 
this
«« #
	TSQLToken
««$ -
token
««. 3
,
««3 4
TSQLKeywords
««5 A
keyword
««B I
)
««I J
{
¬¬ 
if
­­ 
(
­­ 
token
­­ 
==
­­ 
null
­­ 
)
­­ 
{
®® 
return
―― 

false
―― 
;
―― 
}
°° 
if
²² 
(
²² 
token
²² 
.
²² 
Type
²² 
!=
²² 
TSQLTokenType
²² "
.
²²" #
Keyword
²²# *
)
²²* +
{
³³ 
return
΄΄ 

false
΄΄ 
;
΄΄ 
}
µµ 
if
·· 
(
·· 
token
·· 
.
·· 
	AsKeyword
·· 
.
·· 
Keyword
·· 
!=
·· !
keyword
··" )
)
··) *
{
ΈΈ 
return
ΉΉ 

false
ΉΉ 
;
ΉΉ 
}
ΊΊ 
return
ΌΌ 	
true
ΌΌ
 
;
ΌΌ 
}
½½ 
public
ΏΏ 
static
ΏΏ	 
bool
ΏΏ 
IsCharacter
ΏΏ  
(
ΏΏ  !
this
ΏΏ! %
	TSQLToken
ΏΏ& /
token
ΏΏ0 5
,
ΏΏ5 6
TSQLCharacters
ΏΏ7 E
	character
ΏΏF O
)
ΏΏO P
{
ΐΐ 
if
ΑΑ 
(
ΑΑ 
token
ΑΑ 
==
ΑΑ 
null
ΑΑ 
)
ΑΑ 
{
ΒΒ 
return
ΓΓ 

false
ΓΓ 
;
ΓΓ 
}
ΔΔ 
if
ΖΖ 
(
ΖΖ 
token
ΖΖ 
.
ΖΖ 
Type
ΖΖ 
!=
ΖΖ 
TSQLTokenType
ΖΖ "
.
ΖΖ" #
	Character
ΖΖ# ,
)
ΖΖ, -
{
ΗΗ 
return
ΘΘ 

false
ΘΘ 
;
ΘΘ 
}
ΙΙ 
if
ΛΛ 
(
ΛΛ 
token
ΛΛ 
.
ΛΛ 
AsCharacter
ΛΛ 
.
ΛΛ 
	Character
ΛΛ "
!=
ΛΛ# %
	character
ΛΛ& /
)
ΛΛ/ 0
{
ΜΜ 
return
ΝΝ 

false
ΝΝ 
;
ΝΝ 
}
ΞΞ 
return
ΠΠ 	
true
ΠΠ
 
;
ΠΠ 
}
ΡΡ 
}
ÒÒ 
}ΣΣ 
VC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLVariable.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLVariable 
: 
	TSQLToken &
{ 
internal 

TSQLVariable 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 
Variable !
;! "
} 
} 
} 
} –
XC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\Tokens\TSQLWhitespace.cs
	namespace 	
TSQL
 
. 
Tokens 
{ 
public 
class 
TSQLWhitespace 
: 
	TSQLToken (
{ 
internal 

TSQLWhitespace 
( 
int 
beginPostion 
, 
string		 	
text		
 
)		 
:		 
base

 
(

 
beginPostion 
, 
text 
) 	
{ 
} 
public 
override	 
TSQLTokenType 
Type  $
{ 
get 
{ 
return 

TSQLTokenType 
. 

Whitespace #
;# $
} 
} 
} 
} ±C
QC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLCharacters.cs
	namespace 	
TSQL
 
{ 
public 
class 
TSQLCharacters 
{ 
private		 	
static		
 

Dictionary		 
<		 
string		 "
,		" #
TSQLCharacters		$ 2
>		2 3
characterLookup		4 C
=		D E
new

 

Dictionary

 
<

 
string

 
,

 
TSQLCharacters

 (
>

( )
(

) *
StringComparer

* 8
.

8 9&
InvariantCultureIgnoreCase

9 S
)

S T
;

T U
public 
static	 
readonly 
TSQLCharacters '
None( ,
=- .
new/ 2
TSQLCharacters3 A
(A B
stringB H
.H I
EmptyI N
)N O
;O P
public 
static	 
readonly 
TSQLCharacters '
Comma( -
=. /
new0 3
TSQLCharacters4 B
(B C
$strC F
)F G
;G H
public 
static	 
readonly 
TSQLCharacters '
	Semicolon( 1
=2 3
new4 7
TSQLCharacters8 F
(F G
$strG J
)J K
;K L
public 
static	 
readonly 
TSQLCharacters '
OpenParentheses( 7
=8 9
new: =
TSQLCharacters> L
(L M
$strM P
)P Q
;Q R
public 
static	 
readonly 
TSQLCharacters '
CloseParentheses( 8
=9 :
new; >
TSQLCharacters? M
(M N
$strN Q
)Q R
;R S
public 
static	 
readonly 
TSQLCharacters '
Space( -
=. /
new0 3
TSQLCharacters4 B
(B C
$strC F
)F G
;G H
public 
static	 
readonly 
TSQLCharacters '
Tab( +
=, -
new. 1
TSQLCharacters2 @
(@ A
$strA E
)E F
;F G
public 
static	 
readonly 
TSQLCharacters '
CarriageReturn( 6
=7 8
new9 <
TSQLCharacters= K
(K L
$strL P
)P Q
;Q R
public 
static	 
readonly 
TSQLCharacters '
LineFeed( 0
=1 2
new3 6
TSQLCharacters7 E
(E F
$strF J
)J K
;K L
public 
static	 
readonly 
TSQLCharacters '
Period( .
=/ 0
new1 4
TSQLCharacters5 C
(C D
$strD G
)G H
;H I
private 	
string
 
Token 
; 
private 	
TSQLCharacters
 
( 
string 	
token
 
) 
{   
Token!! 
=!!	 

token!! 
;!! 
if"" 
("" 
token"" 
."" 
Length"" 
>"" 
$num"" 
)"" 
{## 
characterLookup$$ 
[$$ 
token$$ 
]$$ 
=$$ 
this$$ !
;$$! "
}%% 
}&& 
public(( 
static((	 
TSQLCharacters(( 
Parse(( $
((($ %
string)) 	
token))
 
))) 
{** 
if++ 
(++ 
!,, 
string,, 
.,, 
IsNullOrEmpty,, 
(,, 
token,, 
),,  
&&,,! #
characterLookup-- 
.-- 
ContainsKey-- 
(--  
token--  %
)--% &
)--& '
{.. 
return// 

characterLookup// 
[// 
token//  
]//  !
;//! "
}00 
else11 
{22 
return33 

TSQLCharacters33 
.33 
None33 
;33 
}44 
}55 
public77 
static77	 
bool77 
IsCharacter77  
(77  !
string88 	
token88
 
)88 
{99 
if:: 
(:: 
!:: 
string:: 
.:: 
IsNullOrWhiteSpace:: !
(::! "
token::" '
)::' (
)::( )
{;; 
return<< 

characterLookup<< 
.<< 
ContainsKey<< &
(<<& '
token<<' ,
)<<, -
;<<- .
}== 
else>> 
{?? 
return@@ 

false@@ 
;@@ 
}AA 
}BB 
publicDD 
boolDD	 
InDD 
(DD 
paramsDD 
TSQLCharactersDD &
[DD& '
]DD' (

charactersDD) 3
)DD3 4
{EE 
returnFF 	

charactersGG 
!=GG 
nullGG 
&&GG 

charactersHH 
.HH 
ContainsHH 
(HH 
thisHH 
)HH 
;HH 
}II 
publicMM 
staticMM	 
boolMM 
operatorMM 
==MM  
(MM  !
TSQLCharactersNN 
aNN 
,NN 
TSQLCharactersOO 
bOO 
)OO 
{PP 
ifQQ 
(QQ 
ObjectQQ 
.QQ 
ReferenceEqualsQQ 
(QQ 
aQQ 
,QQ  
nullQQ! %
)QQ% &
)QQ& '
{RR 
ifSS 
(SS 
ObjectSS 
.SS 
ReferenceEqualsSS 
(SS 
bSS  
,SS  !
nullSS" &
)SS& '
)SS' (
{TT 
returnVV 
trueVV 
;VV 
}WW 
returnZZ 

falseZZ 
;ZZ 
}[[ 
return^^ 	
a^^
 
.^^ 
Equals^^ 
(^^ 
b^^ 
)^^ 
;^^ 
}__ 
publicaa 
staticaa	 
boolaa 
operatoraa 
!=aa  
(aa  !
TSQLCharactersbb 
abb 
,bb 
TSQLCharacterscc 
bcc 
)cc 
{dd 
returnee 	
!ee
 
(ee 
aee 
==ee 
bee 
)ee 
;ee 
}ff 
publichh 
boolhh	 
Equalshh 
(hh 
TSQLCharactershh #
objhh$ '
)hh' (
{ii 
ifkk 
(kk 
Objectkk 
.kk 
ReferenceEqualskk 
(kk 
objkk !
,kk! "
nullkk# '
)kk' (
)kk( )
{ll 
returnmm 

falsemm 
;mm 
}nn 
ifqq 
(qq 
Objectqq 
.qq 
ReferenceEqualsqq 
(qq 
thisqq "
,qq" #
objqq$ '
)qq' (
)qq( )
{rr 
returnss 

truess 
;ss 
}tt 
ifww 
(ww 
thisww 
.ww 
GetTypeww 
(ww 
)ww 
!=ww 
objww 
.ww 
GetTypeww $
(ww$ %
)ww% &
)ww& '
returnxx 

falsexx 
;xx 
return}} 	
Token}}
 
==}} 
obj}} 
.}} 
Token}} 
;}} 
}~~ 
public
€€ 
override
€€	 
bool
€€ 
Equals
€€ 
(
€€ 
object
€€ $
obj
€€% (
)
€€( )
{
 
return
‚‚ 	
Equals
‚‚
 
(
‚‚ 
obj
‚‚ 
as
‚‚ 
TSQLCharacters
‚‚ &
)
‚‚& '
;
‚‚' (
}
ƒƒ 
public
…… 
override
……	 
int
…… 
GetHashCode
…… !
(
……! "
)
……" #
{
†† 
return
‡‡ 	
Token
‡‡
 
.
‡‡ 
GetHashCode
‡‡ 
(
‡‡ 
)
‡‡ 
;
‡‡ 
}
 
}
‹‹ 
} ρμ
OC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLKeywords.cs
	namespace 	
TSQL
 
{ 
public 
class 
TSQLKeywords 
{ 
private		 	
static		
 

Dictionary		 
<		 
string		 "
,		" #
TSQLKeywords		$ 0
>		0 1
keywordLookup		2 ?
=		@ A
new

 

Dictionary

 
<

 
string

 
,

 
TSQLKeywords

 &
>

& '
(

' (
StringComparer

( 6
.

6 7&
InvariantCultureIgnoreCase

7 Q
)

Q R
;

R S
public 
static	 
readonly 
TSQLKeywords %
None& *
=+ ,
new- 0
TSQLKeywords1 =
(= >
$str> @
)@ A
;A B
public 
static	 
readonly 
TSQLKeywords %
ALTER& +
=, -
new. 1
TSQLKeywords2 >
(> ?
$str? F
)F G
;G H
public 
static	 
readonly 
TSQLKeywords %
CREATE& ,
=- .
new/ 2
TSQLKeywords3 ?
(? @
$str@ H
)H I
;I J
public 
static	 
readonly 
TSQLKeywords %
DROP& *
=+ ,
new- 0
TSQLKeywords1 =
(= >
$str> D
)D E
;E F
public 
static	 
readonly 
TSQLKeywords %
SELECT& ,
=- .
new/ 2
TSQLKeywords3 ?
(? @
$str@ H
)H I
;I J
public 
static	 
readonly 
TSQLKeywords %
INSERT& ,
=- .
new/ 2
TSQLKeywords3 ?
(? @
$str@ H
)H I
;I J
public 
static	 
readonly 
TSQLKeywords %
UPDATE& ,
=- .
new/ 2
TSQLKeywords3 ?
(? @
$str@ H
)H I
;I J
public 
static	 
readonly 
TSQLKeywords %
DELETE& ,
=- .
new/ 2
TSQLKeywords3 ?
(? @
$str@ H
)H I
;I J
public 
static	 
readonly 
TSQLKeywords %
MERGE& +
=, -
new. 1
TSQLKeywords2 >
(> ?
$str? F
)F G
;G H
public 
static	 
readonly 
TSQLKeywords %
TRUNCATE& .
=/ 0
new1 4
TSQLKeywords5 A
(A B
$strB L
)L M
;M N
public 
static	 
readonly 
TSQLKeywords %
DISABLE& -
=. /
new0 3
TSQLKeywords4 @
(@ A
$strA J
)J K
;K L
public 
static	 
readonly 
TSQLKeywords %
ENABLE& ,
=- .
new/ 2
TSQLKeywords3 ?
(? @
$str@ H
)H I
;I J
public 
static	 
readonly 
TSQLKeywords %
EXECUTE& -
=. /
new0 3
TSQLKeywords4 @
(@ A
$strA J
)J K
;K L
public 
static	 
readonly 
TSQLKeywords %
BULK& *
=+ ,
new- 0
TSQLKeywords1 =
(= >
$str> D
)D E
;E F
public   
static  	 
readonly   
TSQLKeywords   %
GRANT  & +
=  , -
new  . 1
TSQLKeywords  2 >
(  > ?
$str  ? F
)  F G
;  G H
public!! 
static!!	 
readonly!! 
TSQLKeywords!! %
DENY!!& *
=!!+ ,
new!!- 0
TSQLKeywords!!1 =
(!!= >
$str!!> D
)!!D E
;!!E F
public"" 
static""	 
readonly"" 
TSQLKeywords"" %
REVOKE""& ,
=""- .
new""/ 2
TSQLKeywords""3 ?
(""? @
$str""@ H
)""H I
;""I J
public$$ 
static$$	 
readonly$$ 
TSQLKeywords$$ %
GO$$& (
=$$) *
new$$+ .
TSQLKeywords$$/ ;
($$; <
$str$$< @
)$$@ A
;$$A B
public&& 
static&&	 
readonly&& 
TSQLKeywords&& %
ADD&&& )
=&&* +
new&&, /
TSQLKeywords&&0 <
(&&< =
$str&&= B
)&&B C
;&&C D
public(( 
static((	 
readonly(( 
TSQLKeywords(( %
BEGIN((& +
=((, -
new((. 1
TSQLKeywords((2 >
(((> ?
$str((? F
)((F G
;((G H
public)) 
static))	 
readonly)) 
TSQLKeywords)) %
COMMIT))& ,
=))- .
new))/ 2
TSQLKeywords))3 ?
())? @
$str))@ H
)))H I
;))I J
public** 
static**	 
readonly** 
TSQLKeywords** %
ROLLBACK**& .
=**/ 0
new**1 4
TSQLKeywords**5 A
(**A B
$str**B L
)**L M
;**M N
public++ 
static++	 
readonly++ 
TSQLKeywords++ %
DUMP++& *
=+++ ,
new++- 0
TSQLKeywords++1 =
(++= >
$str++> D
)++D E
;++E F
public,, 
static,,	 
readonly,, 
TSQLKeywords,, %
BACKUP,,& ,
=,,- .
new,,/ 2
TSQLKeywords,,3 ?
(,,? @
$str,,@ H
),,H I
;,,I J
public-- 
static--	 
readonly-- 
TSQLKeywords-- %
RESTORE--& -
=--. /
new--0 3
TSQLKeywords--4 @
(--@ A
$str--A J
)--J K
;--K L
public.. 
static..	 
readonly.. 
TSQLKeywords.. %
LOAD..& *
=..+ ,
new..- 0
TSQLKeywords..1 =
(..= >
$str..> D
)..D E
;..E F
public// 
static//	 
readonly// 
TSQLKeywords// %

CHECKPOINT//& 0
=//1 2
new//3 6
TSQLKeywords//7 C
(//C D
$str//D P
)//P Q
;//Q R
public00 
static00	 
readonly00 
TSQLKeywords00 %
WHILE00& +
=00, -
new00. 1
TSQLKeywords002 >
(00> ?
$str00? F
)00F G
;00G H
public11 
static11	 
readonly11 
TSQLKeywords11 %
IF11& (
=11) *
new11+ .
TSQLKeywords11/ ;
(11; <
$str11< @
)11@ A
;11A B
public22 
static22	 
readonly22 
TSQLKeywords22 %
BREAK22& +
=22, -
new22. 1
TSQLKeywords222 >
(22> ?
$str22? F
)22F G
;22G H
public33 
static33	 
readonly33 
TSQLKeywords33 %
CONTINUE33& .
=33/ 0
new331 4
TSQLKeywords335 A
(33A B
$str33B L
)33L M
;33M N
public44 
static44	 
readonly44 
TSQLKeywords44 %
GOTO44& *
=44+ ,
new44- 0
TSQLKeywords441 =
(44= >
$str44> D
)44D E
;44E F
public55 
static55	 
readonly55 
TSQLKeywords55 %
SET55& )
=55* +
new55, /
TSQLKeywords550 <
(55< =
$str55= B
)55B C
;55C D
public66 
static66	 
readonly66 
TSQLKeywords66 %
DECLARE66& -
=66. /
new660 3
TSQLKeywords664 @
(66@ A
$str66A J
)66J K
;66K L
public77 
static77	 
readonly77 
TSQLKeywords77 %
PRINT77& +
=77, -
new77. 1
TSQLKeywords772 >
(77> ?
$str77? F
)77F G
;77G H
public88 
static88	 
readonly88 
TSQLKeywords88 %
FETCH88& +
=88, -
new88. 1
TSQLKeywords882 >
(88> ?
$str88? F
)88F G
;88G H
public99 
static99	 
readonly99 
TSQLKeywords99 %
OPEN99& *
=99+ ,
new99- 0
TSQLKeywords991 =
(99= >
$str99> D
)99D E
;99E F
public:: 
static::	 
readonly:: 
TSQLKeywords:: %
CLOSE::& +
=::, -
new::. 1
TSQLKeywords::2 >
(::> ?
$str::? F
)::F G
;::G H
public;; 
static;;	 
readonly;; 
TSQLKeywords;; %

DEALLOCATE;;& 0
=;;1 2
new;;3 6
TSQLKeywords;;7 C
(;;C D
$str;;D P
);;P Q
;;;Q R
public== 
static==	 
readonly== 
TSQLKeywords== %
WITH==& *
===+ ,
new==- 0
TSQLKeywords==1 =
(=== >
$str==> D
)==D E
;==E F
public>> 
static>>	 
readonly>> 
TSQLKeywords>> %
DBCC>>& *
=>>+ ,
new>>- 0
TSQLKeywords>>1 =
(>>= >
$str>>> D
)>>D E
;>>E F
public?? 
static??	 
readonly?? 
TSQLKeywords?? %
KILL??& *
=??+ ,
new??- 0
TSQLKeywords??1 =
(??= >
$str??> D
)??D E
;??E F
publicAA 
staticAA	 
readonlyAA 
TSQLKeywordsAA %
MOVEAA& *
=AA+ ,
newAA- 0
TSQLKeywordsAA1 =
(AA= >
$strAA> D
)AAD E
;AAE F
publicCC 
staticCC	 
readonlyCC 
TSQLKeywordsCC %
GETCC& )
=CC* +
newCC, /
TSQLKeywordsCC0 <
(CC< =
$strCC= B
)CCB C
;CCC D
publicDD 
staticDD	 
readonlyDD 
TSQLKeywordsDD %
RECEIVEDD& -
=DD. /
newDD0 3
TSQLKeywordsDD4 @
(DD@ A
$strDDA J
)DDJ K
;DDK L
publicEE 
staticEE	 
readonlyEE 
TSQLKeywordsEE %
SENDEE& *
=EE+ ,
newEE- 0
TSQLKeywordsEE1 =
(EE= >
$strEE> D
)EED E
;EEE F
publicFF 
staticFF	 
readonlyFF 
TSQLKeywordsFF %
WAITFORFF& -
=FF. /
newFF0 3
TSQLKeywordsFF4 @
(FF@ A
$strFFA J
)FFJ K
;FFK L
publicGG 
staticGG	 
readonlyGG 
TSQLKeywordsGG %
READTEXTGG& .
=GG/ 0
newGG1 4
TSQLKeywordsGG5 A
(GGA B
$strGGB L
)GGL M
;GGM N
publicHH 
staticHH	 
readonlyHH 
TSQLKeywordsHH %

UPDATETEXTHH& 0
=HH1 2
newHH3 6
TSQLKeywordsHH7 C
(HHC D
$strHHD P
)HHP Q
;HHQ R
publicII 
staticII	 
readonlyII 
TSQLKeywordsII %
	WRITETEXTII& /
=II0 1
newII2 5
TSQLKeywordsII6 B
(IIB C
$strIIC N
)IIN O
;IIO P
publicJJ 
staticJJ	 
readonlyJJ 
TSQLKeywordsJJ %
USEJJ& )
=JJ* +
newJJ, /
TSQLKeywordsJJ0 <
(JJ< =
$strJJ= B
)JJB C
;JJC D
publicKK 
staticKK	 
readonlyKK 
TSQLKeywordsKK %
SHUTDOWNKK& .
=KK/ 0
newKK1 4
TSQLKeywordsKK5 A
(KKA B
$strKKB L
)KKL M
;KKM N
publicLL 
staticLL	 
readonlyLL 
TSQLKeywordsLL %
RETURNLL& ,
=LL- .
newLL/ 2
TSQLKeywordsLL3 ?
(LL? @
$strLL@ H
)LLH I
;LLI J
publicMM 
staticMM	 
readonlyMM 
TSQLKeywordsMM %
REVERTMM& ,
=MM- .
newMM/ 2
TSQLKeywordsMM3 ?
(MM? @
$strMM@ H
)MMH I
;MMI J
publicSS 
staticSS	 
readonlySS 
TSQLKeywordsSS %
ALLSS& )
=SS* +
newSS, /
TSQLKeywordsSS0 <
(SS< =
$strSS= B
)SSB C
;SSC D
publicTT 
staticTT	 
readonlyTT 
TSQLKeywordsTT %
ANDTT& )
=TT* +
newTT, /
TSQLKeywordsTT0 <
(TT< =
$strTT= B
)TTB C
;TTC D
publicUU 
staticUU	 
readonlyUU 
TSQLKeywordsUU %
ANYUU& )
=UU* +
newUU, /
TSQLKeywordsUU0 <
(UU< =
$strUU= B
)UUB C
;UUC D
publicVV 
staticVV	 
readonlyVV 
TSQLKeywordsVV %
ASVV& (
=VV) *
newVV+ .
TSQLKeywordsVV/ ;
(VV; <
$strVV< @
)VV@ A
;VVA B
publicWW 
staticWW	 
readonlyWW 
TSQLKeywordsWW %
ASCWW& )
=WW* +
newWW, /
TSQLKeywordsWW0 <
(WW< =
$strWW= B
)WWB C
;WWC D
publicXX 
staticXX	 
readonlyXX 
TSQLKeywordsXX %
AUTHORIZATIONXX& 3
=XX4 5
newXX6 9
TSQLKeywordsXX: F
(XXF G
$strXXG V
)XXV W
;XXW X
publicYY 
staticYY	 
readonlyYY 
TSQLKeywordsYY %
BETWEENYY& -
=YY. /
newYY0 3
TSQLKeywordsYY4 @
(YY@ A
$strYYA J
)YYJ K
;YYK L
publicZZ 
staticZZ	 
readonlyZZ 
TSQLKeywordsZZ %
BROWSEZZ& ,
=ZZ- .
newZZ/ 2
TSQLKeywordsZZ3 ?
(ZZ? @
$strZZ@ H
)ZZH I
;ZZI J
public[[ 
static[[	 
readonly[[ 
TSQLKeywords[[ %
BY[[& (
=[[) *
new[[+ .
TSQLKeywords[[/ ;
([[; <
$str[[< @
)[[@ A
;[[A B
public\\ 
static\\	 
readonly\\ 
TSQLKeywords\\ %
CASCADE\\& -
=\\. /
new\\0 3
TSQLKeywords\\4 @
(\\@ A
$str\\A J
)\\J K
;\\K L
public]] 
static]]	 
readonly]] 
TSQLKeywords]] %
CASE]]& *
=]]+ ,
new]]- 0
TSQLKeywords]]1 =
(]]= >
$str]]> D
)]]D E
;]]E F
public^^ 
static^^	 
readonly^^ 
TSQLKeywords^^ %
CHECK^^& +
=^^, -
new^^. 1
TSQLKeywords^^2 >
(^^> ?
$str^^? F
)^^F G
;^^G H
public__ 
static__	 
readonly__ 
TSQLKeywords__ %
	CLUSTERED__& /
=__0 1
new__2 5
TSQLKeywords__6 B
(__B C
$str__C N
)__N O
;__O P
public`` 
static``	 
readonly`` 
TSQLKeywords`` %
COLLATE``& -
=``. /
new``0 3
TSQLKeywords``4 @
(``@ A
$str``A J
)``J K
;``K L
publicaa 
staticaa	 
readonlyaa 
TSQLKeywordsaa %
COLUMNaa& ,
=aa- .
newaa/ 2
TSQLKeywordsaa3 ?
(aa? @
$straa@ H
)aaH I
;aaI J
publicbb 
staticbb	 
readonlybb 
TSQLKeywordsbb %
	COMMITTEDbb& /
=bb0 1
newbb2 5
TSQLKeywordsbb6 B
(bbB C
$strbbC N
)bbN O
;bbO P
publiccc 
staticcc	 
readonlycc 
TSQLKeywordscc %
COMPUTEcc& -
=cc. /
newcc0 3
TSQLKeywordscc4 @
(cc@ A
$strccA J
)ccJ K
;ccK L
publicdd 
staticdd	 
readonlydd 
TSQLKeywordsdd %

CONSTRAINTdd& 0
=dd1 2
newdd3 6
TSQLKeywordsdd7 C
(ddC D
$strddD P
)ddP Q
;ddQ R
publicee 
staticee	 
readonlyee 
TSQLKeywordsee %
CROSSee& +
=ee, -
newee. 1
TSQLKeywordsee2 >
(ee> ?
$stree? F
)eeF G
;eeG H
publicff 
staticff	 
readonlyff 
TSQLKeywordsff %
CURRENTff& -
=ff. /
newff0 3
TSQLKeywordsff4 @
(ff@ A
$strffA J
)ffJ K
;ffK L
publicgg 
staticgg	 
readonlygg 
TSQLKeywordsgg %
CURSORgg& ,
=gg- .
newgg/ 2
TSQLKeywordsgg3 ?
(gg? @
$strgg@ H
)ggH I
;ggI J
publichh 
statichh	 
readonlyhh 
TSQLKeywordshh %
DATABASEhh& .
=hh/ 0
newhh1 4
TSQLKeywordshh5 A
(hhA B
$strhhB L
)hhL M
;hhM N
publicii 
staticii	 
readonlyii 
TSQLKeywordsii %
DEFAULTii& -
=ii. /
newii0 3
TSQLKeywordsii4 @
(ii@ A
$striiA J
)iiJ K
;iiK L
publicjj 
staticjj	 
readonlyjj 
TSQLKeywordsjj %
DESCjj& *
=jj+ ,
newjj- 0
TSQLKeywordsjj1 =
(jj= >
$strjj> D
)jjD E
;jjE F
publickk 
statickk	 
readonlykk 
TSQLKeywordskk %
DISKkk& *
=kk+ ,
newkk- 0
TSQLKeywordskk1 =
(kk= >
$strkk> D
)kkD E
;kkE F
publicll 
staticll	 
readonlyll 
TSQLKeywordsll %
DISTINCTll& .
=ll/ 0
newll1 4
TSQLKeywordsll5 A
(llA B
$strllB L
)llL M
;llM N
publicmm 
staticmm	 
readonlymm 
TSQLKeywordsmm %
DISTRIBUTEDmm& 1
=mm2 3
newmm4 7
TSQLKeywordsmm8 D
(mmD E
$strmmE R
)mmR S
;mmS T
publicoo 
staticoo	 
readonlyoo 
TSQLKeywordsoo %
DOUBLEoo& ,
=oo- .
newoo/ 2
TSQLKeywordsoo3 ?
(oo? @
$stroo@ H
)ooH I
;ooI J
publicpp 
staticpp	 
readonlypp 
TSQLKeywordspp %
ELSEpp& *
=pp+ ,
newpp- 0
TSQLKeywordspp1 =
(pp= >
$strpp> D
)ppD E
;ppE F
publicqq 
staticqq	 
readonlyqq 
TSQLKeywordsqq %
ENDqq& )
=qq* +
newqq, /
TSQLKeywordsqq0 <
(qq< =
$strqq= B
)qqB C
;qqC D
publicss 
staticss	 
readonlyss 
TSQLKeywordsss %
ESCAPEss& ,
=ss- .
newss/ 2
TSQLKeywordsss3 ?
(ss? @
$strss@ H
)ssH I
;ssI J
publictt 
statictt	 
readonlytt 
TSQLKeywordstt %
EXCEPTtt& ,
=tt- .
newtt/ 2
TSQLKeywordstt3 ?
(tt? @
$strtt@ H
)ttH I
;ttI J
publicuu 
staticuu	 
readonlyuu 
TSQLKeywordsuu %
EXISTSuu& ,
=uu- .
newuu/ 2
TSQLKeywordsuu3 ?
(uu? @
$struu@ H
)uuH I
;uuI J
publicww 
staticww	 
readonlyww 
TSQLKeywordsww %
EXTERNALww& .
=ww/ 0
newww1 4
TSQLKeywordsww5 A
(wwA B
$strwwB L
)wwL M
;wwM N
publicxx 
staticxx	 
readonlyxx 
TSQLKeywordsxx %
FILExx& *
=xx+ ,
newxx- 0
TSQLKeywordsxx1 =
(xx= >
$strxx> D
)xxD E
;xxE F
publicyy 
staticyy	 
readonlyyy 
TSQLKeywordsyy %

FILLFACTORyy& 0
=yy1 2
newyy3 6
TSQLKeywordsyy7 C
(yyC D
$stryyD P
)yyP Q
;yyQ R
publiczz 
staticzz	 
readonlyzz 
TSQLKeywordszz %
FORzz& )
=zz* +
newzz, /
TSQLKeywordszz0 <
(zz< =
$strzz= B
)zzB C
;zzC D
public{{ 
static{{	 
readonly{{ 
TSQLKeywords{{ %
FOREIGN{{& -
={{. /
new{{0 3
TSQLKeywords{{4 @
({{@ A
$str{{A J
){{J K
;{{K L
public|| 
static||	 
readonly|| 
TSQLKeywords|| %
FROM||& *
=||+ ,
new||- 0
TSQLKeywords||1 =
(||= >
$str||> D
)||D E
;||E F
public}} 
static}}	 
readonly}} 
TSQLKeywords}} %
FULL}}& *
=}}+ ,
new}}- 0
TSQLKeywords}}1 =
(}}= >
$str}}> D
)}}D E
;}}E F
public~~ 
static~~	 
readonly~~ 
TSQLKeywords~~ %
FUNCTION~~& .
=~~/ 0
new~~1 4
TSQLKeywords~~5 A
(~~A B
$str~~B L
)~~L M
;~~M N
public 
static	 
readonly 
TSQLKeywords %
GROUP& +
=, -
new. 1
TSQLKeywords2 >
(> ?
$str? F
)F G
;G H
public
€€ 
static
€€	 
readonly
€€ 
TSQLKeywords
€€ %
HAVING
€€& ,
=
€€- .
new
€€/ 2
TSQLKeywords
€€3 ?
(
€€? @
$str
€€@ H
)
€€H I
;
€€I J
public
 
static
	 
readonly
 
TSQLKeywords
 %
HOLDLOCK
& .
=
/ 0
new
1 4
TSQLKeywords
5 A
(
A B
$str
B L
)
L M
;
M N
public
‚‚ 
static
‚‚	 
readonly
‚‚ 
TSQLKeywords
‚‚ %
IDENTITY
‚‚& .
=
‚‚/ 0
new
‚‚1 4
TSQLKeywords
‚‚5 A
(
‚‚A B
$str
‚‚B L
)
‚‚L M
;
‚‚M N
public
ƒƒ 
static
ƒƒ	 
readonly
ƒƒ 
TSQLKeywords
ƒƒ %
IDENTITY_INSERT
ƒƒ& 5
=
ƒƒ6 7
new
ƒƒ8 ;
TSQLKeywords
ƒƒ< H
(
ƒƒH I
$str
ƒƒI Z
)
ƒƒZ [
;
ƒƒ[ \
public
„„ 
static
„„	 
readonly
„„ 
TSQLKeywords
„„ %
IDENTITYCOL
„„& 1
=
„„2 3
new
„„4 7
TSQLKeywords
„„8 D
(
„„D E
$str
„„E R
)
„„R S
;
„„S T
public
…… 
static
……	 
readonly
…… 
TSQLKeywords
…… %
IN
……& (
=
……) *
new
……+ .
TSQLKeywords
……/ ;
(
……; <
$str
……< @
)
……@ A
;
……A B
public
†† 
static
††	 
readonly
†† 
TSQLKeywords
†† %
INDEX
††& +
=
††, -
new
††. 1
TSQLKeywords
††2 >
(
††> ?
$str
††? F
)
††F G
;
††G H
public
‡‡ 
static
‡‡	 
readonly
‡‡ 
TSQLKeywords
‡‡ %
INNER
‡‡& +
=
‡‡, -
new
‡‡. 1
TSQLKeywords
‡‡2 >
(
‡‡> ?
$str
‡‡? F
)
‡‡F G
;
‡‡G H
public
 
static
	 
readonly
 
TSQLKeywords
 %
	INTERSECT
& /
=
0 1
new
2 5
TSQLKeywords
6 B
(
B C
$str
C N
)
N O
;
O P
public
‰‰ 
static
‰‰	 
readonly
‰‰ 
TSQLKeywords
‰‰ %
INTO
‰‰& *
=
‰‰+ ,
new
‰‰- 0
TSQLKeywords
‰‰1 =
(
‰‰= >
$str
‰‰> D
)
‰‰D E
;
‰‰E F
public
 
static
	 
readonly
 
TSQLKeywords
 %
IS
& (
=
) *
new
+ .
TSQLKeywords
/ ;
(
; <
$str
< @
)
@ A
;
A B
public
‹‹ 
static
‹‹	 
readonly
‹‹ 
TSQLKeywords
‹‹ %
JOIN
‹‹& *
=
‹‹+ ,
new
‹‹- 0
TSQLKeywords
‹‹1 =
(
‹‹= >
$str
‹‹> D
)
‹‹D E
;
‹‹E F
public
 
static
	 
readonly
 
TSQLKeywords
 %
KEY
& )
=
* +
new
, /
TSQLKeywords
0 <
(
< =
$str
= B
)
B C
;
C D
public
 
static
	 
readonly
 
TSQLKeywords
 %
LEFT
& *
=
+ ,
new
- 0
TSQLKeywords
1 =
(
= >
$str
> D
)
D E
;
E F
public
 
static
	 
readonly
 
TSQLKeywords
 %
LIKE
& *
=
+ ,
new
- 0
TSQLKeywords
1 =
(
= >
$str
> D
)
D E
;
E F
public
 
static
	 
readonly
 
TSQLKeywords
 %
LINENO
& ,
=
- .
new
/ 2
TSQLKeywords
3 ?
(
? @
$str
@ H
)
H I
;
I J
public
‘‘ 
static
‘‘	 
readonly
‘‘ 
TSQLKeywords
‘‘ %
NOCHECK
‘‘& -
=
‘‘. /
new
‘‘0 3
TSQLKeywords
‘‘4 @
(
‘‘@ A
$str
‘‘A J
)
‘‘J K
;
‘‘K L
public
’’ 
static
’’	 
readonly
’’ 
TSQLKeywords
’’ %
NONCLUSTERED
’’& 2
=
’’3 4
new
’’5 8
TSQLKeywords
’’9 E
(
’’E F
$str
’’F T
)
’’T U
;
’’U V
public
““ 
static
““	 
readonly
““ 
TSQLKeywords
““ %
NOT
““& )
=
““* +
new
““, /
TSQLKeywords
““0 <
(
““< =
$str
““= B
)
““B C
;
““C D
public
”” 
static
””	 
readonly
”” 
TSQLKeywords
”” %
NULL
””& *
=
””+ ,
new
””- 0
TSQLKeywords
””1 =
(
””= >
$str
””> D
)
””D E
;
””E F
public
–– 
static
––	 
readonly
–– 
TSQLKeywords
–– %
OFF
––& )
=
––* +
new
––, /
TSQLKeywords
––0 <
(
––< =
$str
––= B
)
––B C
;
––C D
public
—— 
static
——	 
readonly
—— 
TSQLKeywords
—— %
OFFSETS
——& -
=
——. /
new
——0 3
TSQLKeywords
——4 @
(
——@ A
$str
——A J
)
——J K
;
——K L
public
 
static
	 
readonly
 
TSQLKeywords
 %
ON
& (
=
) *
new
+ .
TSQLKeywords
/ ;
(
; <
$str
< @
)
@ A
;
A B
public
™™ 
static
™™	 
readonly
™™ 
TSQLKeywords
™™ %
OPTION
™™& ,
=
™™- .
new
™™/ 2
TSQLKeywords
™™3 ?
(
™™? @
$str
™™@ H
)
™™H I
;
™™I J
public
 
static
	 
readonly
 
TSQLKeywords
 %
OR
& (
=
) *
new
+ .
TSQLKeywords
/ ;
(
; <
$str
< @
)
@ A
;
A B
public
›› 
static
››	 
readonly
›› 
TSQLKeywords
›› %
ORDER
››& +
=
››, -
new
››. 1
TSQLKeywords
››2 >
(
››> ?
$str
››? F
)
››F G
;
››G H
public
 
static
	 
readonly
 
TSQLKeywords
 %
OUTER
& +
=
, -
new
. 1
TSQLKeywords
2 >
(
> ?
$str
? F
)
F G
;
G H
public
 
static
	 
readonly
 
TSQLKeywords
 %
OVER
& *
=
+ ,
new
- 0
TSQLKeywords
1 =
(
= >
$str
> D
)
D E
;
E F
public
 
static
	 
readonly
 
TSQLKeywords
 %
PERCENT
& -
=
. /
new
0 3
TSQLKeywords
4 @
(
@ A
$str
A J
)
J K
;
K L
public
 
static
	 
readonly
 
TSQLKeywords
 %
PIVOT
& +
=
, -
new
. 1
TSQLKeywords
2 >
(
> ?
$str
? F
)
F G
;
G H
public
   
static
  	 
readonly
   
TSQLKeywords
   %
PLAN
  & *
=
  + ,
new
  - 0
TSQLKeywords
  1 =
(
  = >
$str
  > D
)
  D E
;
  E F
public
ΆΆ 
static
ΆΆ	 
readonly
ΆΆ 
TSQLKeywords
ΆΆ %
	PRECISION
ΆΆ& /
=
ΆΆ0 1
new
ΆΆ2 5
TSQLKeywords
ΆΆ6 B
(
ΆΆB C
$str
ΆΆC N
)
ΆΆN O
;
ΆΆO P
public
££ 
static
££	 
readonly
££ 
TSQLKeywords
££ %
PRIMARY
££& -
=
££. /
new
££0 3
TSQLKeywords
££4 @
(
££@ A
$str
££A J
)
££J K
;
££K L
public
¤¤ 
static
¤¤	 
readonly
¤¤ 
TSQLKeywords
¤¤ %
	PROCEDURE
¤¤& /
=
¤¤0 1
new
¤¤2 5
TSQLKeywords
¤¤6 B
(
¤¤B C
$str
¤¤C N
)
¤¤N O
;
¤¤O P
public
¥¥ 
static
¥¥	 
readonly
¥¥ 
TSQLKeywords
¥¥ %
PUBLIC
¥¥& ,
=
¥¥- .
new
¥¥/ 2
TSQLKeywords
¥¥3 ?
(
¥¥? @
$str
¥¥@ H
)
¥¥H I
;
¥¥I J
public
¦¦ 
static
¦¦	 
readonly
¦¦ 
TSQLKeywords
¦¦ %
READ
¦¦& *
=
¦¦+ ,
new
¦¦- 0
TSQLKeywords
¦¦1 =
(
¦¦= >
$str
¦¦> D
)
¦¦D E
;
¦¦E F
public
§§ 
static
§§	 
readonly
§§ 
TSQLKeywords
§§ %

REPEATABLE
§§& 0
=
§§1 2
new
§§3 6
TSQLKeywords
§§7 C
(
§§C D
$str
§§D P
)
§§P Q
;
§§Q R
public
¨¨ 
static
¨¨	 
readonly
¨¨ 
TSQLKeywords
¨¨ %
RECONFIGURE
¨¨& 1
=
¨¨2 3
new
¨¨4 7
TSQLKeywords
¨¨8 D
(
¨¨D E
$str
¨¨E R
)
¨¨R S
;
¨¨S T
public
©© 
static
©©	 
readonly
©© 
TSQLKeywords
©© %

REFERENCES
©©& 0
=
©©1 2
new
©©3 6
TSQLKeywords
©©7 C
(
©©C D
$str
©©D P
)
©©P Q
;
©©Q R
public
ªª 
static
ªª	 
readonly
ªª 
TSQLKeywords
ªª %
REPLICATION
ªª& 1
=
ªª2 3
new
ªª4 7
TSQLKeywords
ªª8 D
(
ªªD E
$str
ªªE R
)
ªªR S
;
ªªS T
public
¬¬ 
static
¬¬	 
readonly
¬¬ 
TSQLKeywords
¬¬ %
RETURNS
¬¬& -
=
¬¬. /
new
¬¬0 3
TSQLKeywords
¬¬4 @
(
¬¬@ A
$str
¬¬A J
)
¬¬J K
;
¬¬K L
public
­­ 
static
­­	 
readonly
­­ 
TSQLKeywords
­­ %
RIGHT
­­& +
=
­­, -
new
­­. 1
TSQLKeywords
­­2 >
(
­­> ?
$str
­­? F
)
­­F G
;
­­G H
public
®® 
static
®®	 
readonly
®® 
TSQLKeywords
®® %
ROWCOUNT
®®& .
=
®®/ 0
new
®®1 4
TSQLKeywords
®®5 A
(
®®A B
$str
®®B L
)
®®L M
;
®®M N
public
―― 
static
――	 
readonly
―― 
TSQLKeywords
―― %

ROWGUIDCOL
――& 0
=
――1 2
new
――3 6
TSQLKeywords
――7 C
(
――C D
$str
――D P
)
――P Q
;
――Q R
public
°° 
static
°°	 
readonly
°° 
TSQLKeywords
°° %
RULE
°°& *
=
°°+ ,
new
°°- 0
TSQLKeywords
°°1 =
(
°°= >
$str
°°> D
)
°°D E
;
°°E F
public
²² 
static
²²	 
readonly
²² 
TSQLKeywords
²² %
SAVE
²²& *
=
²²+ ,
new
²²- 0
TSQLKeywords
²²1 =
(
²²= >
$str
²²> D
)
²²D E
;
²²E F
public
³³ 
static
³³	 
readonly
³³ 
TSQLKeywords
³³ %
SCHEMA
³³& ,
=
³³- .
new
³³/ 2
TSQLKeywords
³³3 ?
(
³³? @
$str
³³@ H
)
³³H I
;
³³I J
public
µµ 
static
µµ	 
readonly
µµ 
TSQLKeywords
µµ %
SETUSER
µµ& -
=
µµ. /
new
µµ0 3
TSQLKeywords
µµ4 @
(
µµ@ A
$str
µµA J
)
µµJ K
;
µµK L
public
¶¶ 
static
¶¶	 
readonly
¶¶ 
TSQLKeywords
¶¶ %
SOME
¶¶& *
=
¶¶+ ,
new
¶¶- 0
TSQLKeywords
¶¶1 =
(
¶¶= >
$str
¶¶> D
)
¶¶D E
;
¶¶E F
public
·· 
static
··	 
readonly
·· 
TSQLKeywords
·· %

STATISTICS
··& 0
=
··1 2
new
··3 6
TSQLKeywords
··7 C
(
··C D
$str
··D P
)
··P Q
;
··Q R
public
ΈΈ 
static
ΈΈ	 
readonly
ΈΈ 
TSQLKeywords
ΈΈ %
TABLE
ΈΈ& +
=
ΈΈ, -
new
ΈΈ. 1
TSQLKeywords
ΈΈ2 >
(
ΈΈ> ?
$str
ΈΈ? F
)
ΈΈF G
;
ΈΈG H
public
ΉΉ 
static
ΉΉ	 
readonly
ΉΉ 
TSQLKeywords
ΉΉ %
TABLESAMPLE
ΉΉ& 1
=
ΉΉ2 3
new
ΉΉ4 7
TSQLKeywords
ΉΉ8 D
(
ΉΉD E
$str
ΉΉE R
)
ΉΉR S
;
ΉΉS T
public
ΊΊ 
static
ΊΊ	 
readonly
ΊΊ 
TSQLKeywords
ΊΊ %
TEXTSIZE
ΊΊ& .
=
ΊΊ/ 0
new
ΊΊ1 4
TSQLKeywords
ΊΊ5 A
(
ΊΊA B
$str
ΊΊB L
)
ΊΊL M
;
ΊΊM N
public
»» 
static
»»	 
readonly
»» 
TSQLKeywords
»» %
THEN
»»& *
=
»»+ ,
new
»»- 0
TSQLKeywords
»»1 =
(
»»= >
$str
»»> D
)
»»D E
;
»»E F
public
½½ 
static
½½	 
readonly
½½ 
TSQLKeywords
½½ %
TOP
½½& )
=
½½* +
new
½½, /
TSQLKeywords
½½0 <
(
½½< =
$str
½½= B
)
½½B C
;
½½C D
public
ΎΎ 
static
ΎΎ	 
readonly
ΎΎ 
TSQLKeywords
ΎΎ %
TRANSACTION
ΎΎ& 1
=
ΎΎ2 3
new
ΎΎ4 7
TSQLKeywords
ΎΎ8 D
(
ΎΎD E
$str
ΎΎE R
)
ΎΎR S
;
ΎΎS T
public
ΏΏ 
static
ΏΏ	 
readonly
ΏΏ 
TSQLKeywords
ΏΏ %
TRIGGER
ΏΏ& -
=
ΏΏ. /
new
ΏΏ0 3
TSQLKeywords
ΏΏ4 @
(
ΏΏ@ A
$str
ΏΏA J
)
ΏΏJ K
;
ΏΏK L
public
ΑΑ 
static
ΑΑ	 
readonly
ΑΑ 
TSQLKeywords
ΑΑ %
UNION
ΑΑ& +
=
ΑΑ, -
new
ΑΑ. 1
TSQLKeywords
ΑΑ2 >
(
ΑΑ> ?
$str
ΑΑ? F
)
ΑΑF G
;
ΑΑG H
public
ΒΒ 
static
ΒΒ	 
readonly
ΒΒ 
TSQLKeywords
ΒΒ %
UNIQUE
ΒΒ& ,
=
ΒΒ- .
new
ΒΒ/ 2
TSQLKeywords
ΒΒ3 ?
(
ΒΒ? @
$str
ΒΒ@ H
)
ΒΒH I
;
ΒΒI J
public
ΓΓ 
static
ΓΓ	 
readonly
ΓΓ 
TSQLKeywords
ΓΓ %
UNPIVOT
ΓΓ& -
=
ΓΓ. /
new
ΓΓ0 3
TSQLKeywords
ΓΓ4 @
(
ΓΓ@ A
$str
ΓΓA J
)
ΓΓJ K
;
ΓΓK L
public
ΕΕ 
static
ΕΕ	 
readonly
ΕΕ 
TSQLKeywords
ΕΕ %
VALUES
ΕΕ& ,
=
ΕΕ- .
new
ΕΕ/ 2
TSQLKeywords
ΕΕ3 ?
(
ΕΕ? @
$str
ΕΕ@ H
)
ΕΕH I
;
ΕΕI J
public
ΖΖ 
static
ΖΖ	 
readonly
ΖΖ 
TSQLKeywords
ΖΖ %
VARYING
ΖΖ& -
=
ΖΖ. /
new
ΖΖ0 3
TSQLKeywords
ΖΖ4 @
(
ΖΖ@ A
$str
ΖΖA J
)
ΖΖJ K
;
ΖΖK L
public
ΗΗ 
static
ΗΗ	 
readonly
ΗΗ 
TSQLKeywords
ΗΗ %
VIEW
ΗΗ& *
=
ΗΗ+ ,
new
ΗΗ- 0
TSQLKeywords
ΗΗ1 =
(
ΗΗ= >
$str
ΗΗ> D
)
ΗΗD E
;
ΗΗE F
public
ΘΘ 
static
ΘΘ	 
readonly
ΘΘ 
TSQLKeywords
ΘΘ %
WHEN
ΘΘ& *
=
ΘΘ+ ,
new
ΘΘ- 0
TSQLKeywords
ΘΘ1 =
(
ΘΘ= >
$str
ΘΘ> D
)
ΘΘD E
;
ΘΘE F
public
ΙΙ 
static
ΙΙ	 
readonly
ΙΙ 
TSQLKeywords
ΙΙ %
WHERE
ΙΙ& +
=
ΙΙ, -
new
ΙΙ. 1
TSQLKeywords
ΙΙ2 >
(
ΙΙ> ?
$str
ΙΙ? F
)
ΙΙF G
;
ΙΙG H
public
ΛΛ 
static
ΛΛ	 
readonly
ΛΛ 
TSQLKeywords
ΛΛ %
WITHIN
ΛΛ& ,
=
ΛΛ- .
new
ΛΛ/ 2
TSQLKeywords
ΛΛ3 ?
(
ΛΛ? @
$str
ΛΛ@ H
)
ΛΛH I
;
ΛΛI J
private
ΡΡ 	
string
ΡΡ
 
Keyword
ΡΡ 
;
ΡΡ 
private
ΣΣ 	
TSQLKeywords
ΣΣ
 
(
ΣΣ 
string
ΤΤ 	
keyword
ΤΤ
 
)
ΤΤ 
{
ΥΥ 
Keyword
ΦΦ 

=
ΦΦ 
keyword
ΦΦ 
;
ΦΦ 
if
ΧΧ 
(
ΧΧ 
!
ΧΧ 
string
ΧΧ 
.
ΧΧ  
IsNullOrWhiteSpace
ΧΧ !
(
ΧΧ! "
keyword
ΧΧ" )
)
ΧΧ) *
)
ΧΧ* +
{
ΨΨ 
keywordLookup
ΩΩ 
[
ΩΩ 
keyword
ΩΩ 
]
ΩΩ 
=
ΩΩ 
this
ΩΩ !
;
ΩΩ! "
if
ΫΫ 
(
ΫΫ 
keyword
ΫΫ 
.
ΫΫ 
Equals
ΫΫ 
(
ΫΫ 
$str
ΫΫ  
,
ΫΫ  !
StringComparison
ΫΫ" 2
.
ΫΫ2 3(
InvariantCultureIgnoreCase
ΫΫ3 M
)
ΫΫM N
)
ΫΫN O
{
άά 
keywordLookup
έέ 
[
έέ 
$str
έέ 
]
έέ 
=
έέ 
this
έέ !
;
έέ! "
}
ήή 
else
ίί 
if
ίί	 
(
ίί 
keyword
ίί 
.
ίί 
Equals
ίί 
(
ίί 
$str
ίί )
,
ίί) *
StringComparison
ίί+ ;
.
ίί; <(
InvariantCultureIgnoreCase
ίί< V
)
ίίV W
)
ίίW X
{
ΰΰ 
keywordLookup
αα 
[
αα 
$str
αα 
]
αα 
=
αα 
this
αα !
;
αα! "
}
ββ 
else
γγ 
if
γγ	 
(
γγ 
keyword
γγ 
.
γγ 
Equals
γγ 
(
γγ 
$str
γγ '
,
γγ' (
StringComparison
γγ) 9
.
γγ9 :(
InvariantCultureIgnoreCase
γγ: T
)
γγT U
)
γγU V
{
δδ 
keywordLookup
εε 
[
εε 
$str
εε 
]
εε 
=
εε 
this
εε !
;
εε! "
}
ζζ 
}
ηη 
}
θθ 
public
κκ 
static
κκ	 
TSQLKeywords
κκ 
Parse
κκ "
(
κκ" #
string
λλ 	
token
λλ
 
)
λλ 
{
μμ 
if
νν 
(
νν 
keywordLookup
νν 
.
νν 
ContainsKey
νν  
(
νν  !
token
νν! &
)
νν& '
)
νν' (
{
ξξ 
return
οο 

keywordLookup
οο 
[
οο 
token
οο 
]
οο 
;
οο  
}
ππ 
else
ρρ 
{
ςς 
return
σσ 

TSQLKeywords
σσ 
.
σσ 
None
σσ 
;
σσ 
}
ττ 
}
υυ 
public
χχ 
static
χχ	 
bool
χχ 
	IsKeyword
χχ 
(
χχ 
string
ψψ 	
token
ψψ
 
)
ψψ 
{
ωω 
if
ϊϊ 
(
ϊϊ 
!
ϊϊ 
string
ϊϊ 
.
ϊϊ  
IsNullOrWhiteSpace
ϊϊ !
(
ϊϊ! "
token
ϊϊ" '
)
ϊϊ' (
)
ϊϊ( )
{
ϋϋ 
return
όό 

keywordLookup
όό 
.
όό 
ContainsKey
όό $
(
όό$ %
token
όό% *
)
όό* +
;
όό+ ,
}
ύύ 
else
ώώ 
{
ÿÿ 
return
€€ 

false
€€ 
;
€€ 
}
 
}
‚‚ 
public
„„ 
bool
„„	 
In
„„ 
(
„„ 
params
„„ 
TSQLKeywords
„„ $
[
„„$ %
]
„„% &
keywords
„„' /
)
„„/ 0
{
…… 
return
†† 	
keywords
‡‡ 
!=
‡‡ 
null
‡‡ 
&&
‡‡ 
keywords
 
.
 
Contains
 
(
 
this
 
)
 
;
 
}
‰‰ 
public
 
static
	 
bool
 
operator
 
==
  
(
  !
TSQLKeywords
 
a
 
,
 
TSQLKeywords
 
b
 
)
 
{
 
if
‘‘ 
(
‘‘ 
Object
‘‘ 
.
‘‘ 
ReferenceEquals
‘‘ 
(
‘‘ 
a
‘‘ 
,
‘‘  
null
‘‘! %
)
‘‘% &
)
‘‘& '
{
’’ 
if
““ 
(
““ 
Object
““ 
.
““ 
ReferenceEquals
““ 
(
““ 
b
““  
,
““  !
null
““" &
)
““& '
)
““' (
{
”” 
return
–– 
true
–– 
;
–– 
}
—— 
return
 

false
 
;
 
}
›› 
return
 	
a

 
.
 
Equals
 
(
 
b
 
)
 
;
 
}
 
public
΅΅ 
static
΅΅	 
bool
΅΅ 
operator
΅΅ 
!=
΅΅  
(
΅΅  !
TSQLKeywords
ΆΆ 
a
ΆΆ 
,
ΆΆ 
TSQLKeywords
££ 
b
££ 
)
££ 
{
¤¤ 
return
¥¥ 	
!
¥¥
 
(
¥¥ 
a
¥¥ 
==
¥¥ 
b
¥¥ 
)
¥¥ 
;
¥¥ 
}
¦¦ 
private
¨¨ 	
bool
¨¨
 
Equals
¨¨ 
(
¨¨ 
TSQLKeywords
¨¨ "
obj
¨¨# &
)
¨¨& '
{
©© 
if
«« 
(
«« 
Object
«« 
.
«« 
ReferenceEquals
«« 
(
«« 
obj
«« !
,
««! "
null
««# '
)
««' (
)
««( )
{
¬¬ 
return
­­ 

false
­­ 
;
­­ 
}
®® 
if
±± 
(
±± 
Object
±± 
.
±± 
ReferenceEquals
±± 
(
±± 
this
±± "
,
±±" #
obj
±±$ '
)
±±' (
)
±±( )
{
²² 
return
³³ 

true
³³ 
;
³³ 
}
΄΄ 
if
·· 
(
·· 
this
·· 
.
·· 
GetType
·· 
(
·· 
)
·· 
!=
·· 
obj
·· 
.
·· 
GetType
·· $
(
··$ %
)
··% &
)
··& '
return
ΈΈ 

false
ΈΈ 
;
ΈΈ 
return
½½ 	
Keyword
½½
 
==
½½ 
obj
½½ 
.
½½ 
Keyword
½½  
;
½½  !
}
ΎΎ 
public
ΐΐ 
override
ΐΐ	 
bool
ΐΐ 
Equals
ΐΐ 
(
ΐΐ 
object
ΐΐ $
obj
ΐΐ% (
)
ΐΐ( )
{
ΑΑ 
return
ΒΒ 	
Equals
ΒΒ
 
(
ΒΒ 
obj
ΒΒ 
as
ΒΒ 
TSQLKeywords
ΒΒ $
)
ΒΒ$ %
;
ΒΒ% &
}
ΓΓ 
public
ΕΕ 
override
ΕΕ	 
int
ΕΕ 
GetHashCode
ΕΕ !
(
ΕΕ! "
)
ΕΕ" #
{
ΖΖ 
return
ΗΗ 	
Keyword
ΗΗ
 
.
ΗΗ 
GetHashCode
ΗΗ 
(
ΗΗ 
)
ΗΗ 
;
ΗΗ  
}
ΘΘ 
}
ΛΛ 
}ΜΜ ½­
PC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLTokenizer.cs
	namespace 	
TSQL
 
{ 
public 
partial 
class 
TSQLTokenizer #
{ 
private 	
TSQLCharacterReader
 
_charReader )
=* +
null, 0
;0 1
private 	
	TSQLToken
 
_current 
= 
null #
;# $
private 	
bool
 
_hasMore 
= 
true 
; 
private 	
bool
 
	_hasExtra 
= 
false  
;  !
private 	
	TSQLToken
 
_extraToken 
;  
public 
TSQLTokenizer	 
( 
string 	
tsqlText
 
) 
: 
this 
( 	
new	 
StringReader 
( 
tsqlText "
)" #
)# $
{ 
} 
public 
TSQLTokenizer	 
( 

TextReader 

tsqlStream 
) 
{ 
_charReader 
= 
new 
TSQLCharacterReader (
(( )

tsqlStream) 3
)3 4
;4 5
}   
public"" 
bool""	  
UseQuotedIdentifiers"" "
{""# $
get""% (
;""( )
set""* -
;""- .
}""/ 0
public$$ 
bool$$	 
IncludeWhitespace$$ 
{$$  !
get$$" %
;$$% &
set$$' *
;$$* +
}$$, -
public&& 
bool&&	 
MoveNext&& 
(&& 
)&& 
{'' 
CheckDisposed(( 
((( 
)(( 
;(( 
_current** 
=** 
null** 
;** 
if,, 
(,, 
_hasMore,, 
),, 
{-- 
if.. 
(.. 
	_hasExtra.. 
).. 
{// 
_current00 
=00 
_extraToken00 
;00 
	_hasExtra11 
=11 
false11 
;11 
}22 
else33 
{44 
if55 
(55 	
IncludeWhitespace55	 
)55 
{66 
_hasMore77 
=77 
_charReader77 
.77 
Read77 !
(77! "
)77" #
;77# $
}88 
else99 	
{:: 
_hasMore;; 
=;; 
_charReader;; 
.;; !
ReadNextNonWhitespace;; 2
(;;2 3
);;3 4
;;;4 5
}<< 
if>> 
(>> 	
_hasMore>>	 
)>> 
{?? 

SetCurrent@@ 
(@@ 
)@@ 
;@@ 
}AA 
}BB 
}CC 
returnEE 	
_hasMoreEE
 
;EE 
}FF 
privateHH 	
StringBuilderHH
 
characterHolderHH '
=HH( )
newHH* -
StringBuilderHH. ;
(HH; <
)HH< =
;HH= >
privateJJ 	
voidJJ
 

SetCurrentJJ 
(JJ 
)JJ 
{KK 
characterHolderLL 
.LL 
LengthLL 
=LL 
$numLL 
;LL 
intMM 
startPositionMM 
=MM 
_charReaderMM "
.MM" #
PositionMM# +
;MM+ ,
ifOO 
(OO 
IncludeWhitespacePP 
&&PP 
charQQ 
.QQ 	
IsWhiteSpaceQQ	 
(QQ 
_charReaderQQ !
.QQ! "
CurrentQQ" )
)QQ) *
)QQ* +
{RR 
doSS 
{TT 
characterHolderUU 
.UU 
AppendUU 
(UU 
_charReaderUU '
.UU' (
CurrentUU( /
)UU/ 0
;UU0 1
}VV 
whileVV 
(VV 
_charReaderWW 
.WW 
ReadWW 
(WW 
)WW 
&&WW 
charXX 	
.XX	 

IsWhiteSpaceXX
 
(XX 
_charReaderXX "
.XX" #
CurrentXX# *
)XX* +
)XX+ ,
;XX, -
ifZZ 
(ZZ 
!ZZ 	
_charReaderZZ	 
.ZZ 
EOFZZ 
)ZZ 
{[[ 
_charReader\\ 
.\\ 
Putback\\ 
(\\ 
)\\ 
;\\ 
}]] 
}^^ 
else__ 
{`` 
characterHolderaa 
.aa 
Appendaa 
(aa 
_charReaderaa &
.aa& '
Currentaa' .
)aa. /
;aa/ 0
switchcc 

(cc 
_charReadercc 
.cc 
Currentcc 
)cc  
{dd 
caseff 	
$charff
 
:ff 
{gg 
ifhh 	
(hh
 
_charReaderhh 
.hh 
Readhh 
(hh 
)hh 
)hh 
{ii 
ifjj 

(jj 
_charReaderkk	 
.kk 
Currentkk 
==kk 
$charkk  #
||kk$ &
_charReaderll	 
.ll 
Currentll 
==ll 
$charll  #
||ll$ &
_charReadermm	 
.mm 
Currentmm 
==mm 
$charmm  #
||mm$ &
_charReadernn	 
.nn 
Currentnn 
==nn 
$charnn  #
||nn$ &
_charReaderoo	 
.oo 
Currentoo 
==oo 
$charoo  #
||oo$ &
_charReaderpp	 
.pp 
Currentpp 
==pp 
$charpp  #
||pp$ &
_charReaderqq	 
.qq 
Currentqq 
==qq 
$charqq  #
||qq$ &
_charReaderrr	 
.rr 
Currentrr 
==rr 
$charrr  #
||rr$ &
_charReaderss	 
.ss 
Currentss 
==ss 
$charss  #
||ss$ &
_charReadertt	 
.tt 
Currenttt 
==tt 
$chartt  #
)uu 	
{vv 	
characterHolderww	 
.ww 
Appendww 
(ww  
_charReaderww  +
.ww+ ,
Currentww, 3
)ww3 4
;ww4 5
gotoyy	 
caseyy 
$charyy 
;yy 
}zz 	
else{{ 
{|| 	
_charReader}}	 
.}} 
Putback}} 
(}} 
)}} 
;}} 
}~~ 	
} 
break
 
;
 
}
‚‚ 
case
„„ 	
$char
„„
 
:
„„ 
case
…… 	
$char
……
 
:
…… 
case
†† 	
$char
††
 
:
†† 
case
‡‡ 	
$char
‡‡
 
:
‡‡ 
case
 	
$char

 
:
 
{
‰‰ 
break
 
;
 
}
‹‹ 
case
 	
$char

 
:
 
{
 
if
‘‘ 	
(
‘‘
 
_charReader
‘‘ 
.
‘‘ 
Read
‘‘ 
(
‘‘ 
)
‘‘ 
)
‘‘ 
{
’’ 
if
““ 

(
““ 
_charReader
““ 
.
““ 
Current
““ 
==
““  "
$char
““# &
)
““& '
{
”” 	
do
••	 
{
––	 

characterHolder
——
 
.
—— 
Append
——  
(
——  !
_charReader
——! ,
.
——, -
Current
——- 4
)
——4 5
;
——5 6
}
	 

while
 
(
 
_charReader
™™
 
.
™™ 
Read
™™ 
(
™™ 
)
™™ 
&&
™™ 
_charReader

 
.
 
Current
 
!=
  
$char
! %
&&
& (
_charReader
››
 
.
›› 
Current
›› 
!=
››  
$char
››! %
)
››% &
;
››& '
if
	 
(
 
!
 
_charReader
 
.
 
EOF
 
)
 
{
	 

_charReader

 
.
 
Putback
 
(
 
)
 
;
  
}
  	 

}
΅΅ 	
else
ΆΆ 
if
ΆΆ 
(
ΆΆ 
_charReader
ΆΆ 
.
ΆΆ 
Current
ΆΆ $
==
ΆΆ% '
$char
ΆΆ( +
)
ΆΆ+ ,
{
££ 	
characterHolder
¤¤	 
.
¤¤ 
Append
¤¤ 
(
¤¤  
_charReader
¤¤  +
.
¤¤+ ,
Current
¤¤, 3
)
¤¤3 4
;
¤¤4 5
}
¥¥ 	
else
¦¦ 
{
§§ 	
_charReader
¨¨	 
.
¨¨ 
Putback
¨¨ 
(
¨¨ 
)
¨¨ 
;
¨¨ 
}
©© 	
}
ªª 
break
¬¬ 
;
¬¬ 
}
­­ 
case
±± 	
$char
±±
 
:
±± 
{
²² 
if
³³ 	
(
³³
 
_charReader
³³ 
.
³³ 
Read
³³ 
(
³³ 
)
³³ 
)
³³ 
{
΄΄ 
if
µµ 

(
µµ 
_charReader
µµ 
.
µµ 
Current
µµ 
==
µµ  "
$char
µµ# &
)
µµ& '
{
¶¶ 	
characterHolder
··	 
.
·· 
Append
·· 
(
··  
_charReader
··  +
.
··+ ,
Current
··, 3
)
··3 4
;
··4 5
int
ΊΊ	 
currentLevel
ΊΊ 
=
ΊΊ 
$num
ΊΊ 
;
ΊΊ 
bool
ΌΌ	 
lastWasStar
ΌΌ 
=
ΌΌ 
false
ΌΌ !
;
ΌΌ! "
bool
½½	 
lastWasSlash
½½ 
=
½½ 
false
½½ "
;
½½" #
while
ΏΏ	 
(
ΏΏ 
_charReader
ΐΐ
 
.
ΐΐ 
Read
ΐΐ 
(
ΐΐ 
)
ΐΐ 
&&
ΐΐ 
(
ΑΑ
 
currentLevel
ΒΒ 
>
ΒΒ 
$num
ΒΒ 
||
ΒΒ 
!
ΔΔ 
(
ΔΔ 
lastWasStar
ΕΕ 
&&
ΕΕ 
_charReader
ΖΖ 
.
ΖΖ 
Current
ΖΖ 
==
ΖΖ  "
$char
ΖΖ# &
)
ΗΗ 
)
ΘΘ
 
)
ΘΘ 
{
ΙΙ	 

if
ΛΛ
 
(
ΛΛ 
lastWasSlash
ΜΜ 
&&
ΜΜ 
_charReader
ΝΝ 
.
ΝΝ 
Current
ΝΝ 
==
ΝΝ !
$char
ΝΝ" %
)
ΝΝ% &
{
ΞΞ
 
currentLevel
ΟΟ 
++
ΟΟ 
;
ΟΟ 
lastWasSlash
ΠΠ 
=
ΠΠ 
false
ΠΠ 
;
ΠΠ  
lastWasStar
ΡΡ 
=
ΡΡ 
false
ΡΡ 
;
ΡΡ 
}
ÒÒ
 
else
ΤΤ
 
if
ΤΤ 
(
ΤΤ 
lastWasStar
ΥΥ 
&&
ΥΥ 
_charReader
ΦΦ 
.
ΦΦ 
Current
ΦΦ 
==
ΦΦ !
$char
ΦΦ" %
)
ΦΦ% &
{
ΧΧ
 
currentLevel
ΨΨ 
--
ΨΨ 
;
ΨΨ 
lastWasSlash
ΩΩ 
=
ΩΩ 
false
ΩΩ 
;
ΩΩ  
lastWasStar
ΪΪ 
=
ΪΪ 
false
ΪΪ 
;
ΪΪ 
}
ΫΫ
 
else
άά
 
{
έέ
 
lastWasSlash
ήή 
=
ήή 
_charReader
ήή %
.
ήή% &
Current
ήή& -
==
ήή. 0
$char
ήή1 4
;
ήή4 5
lastWasStar
ίί 
=
ίί 
_charReader
ίί $
.
ίί$ %
Current
ίί% ,
==
ίί- /
$char
ίί0 3
;
ίί3 4
}
ΰΰ
 
characterHolder
ββ
 
.
ββ 
Append
ββ  
(
ββ  !
_charReader
ββ! ,
.
ββ, -
Current
ββ- 4
)
ββ4 5
;
ββ5 6
}
γγ	 

if
εε	 
(
εε 
!
εε 
_charReader
εε 
.
εε 
EOF
εε 
)
εε 
{
ζζ	 

characterHolder
ηη
 
.
ηη 
Append
ηη  
(
ηη  !
_charReader
ηη! ,
.
ηη, -
Current
ηη- 4
)
ηη4 5
;
ηη5 6
}
θθ	 

}
ιι 	
else
κκ 
if
κκ 
(
κκ 
_charReader
κκ 
.
κκ 
Current
κκ $
==
κκ% '
$char
κκ( +
)
κκ+ ,
{
λλ 	
characterHolder
μμ	 
.
μμ 
Append
μμ 
(
μμ  
_charReader
μμ  +
.
μμ+ ,
Current
μμ, 3
)
μμ3 4
;
μμ4 5
}
νν 	
else
ξξ 
{
οο 	
_charReader
ππ	 
.
ππ 
Putback
ππ 
(
ππ 
)
ππ 
;
ππ 
}
ρρ 	
}
ςς 
break
ττ 
;
ττ 
}
υυ 
case
ωω 	
$char
ωω
 
:
ωω 
{
ϊϊ 
if
ϋϋ 	
(
ϋϋ
 
_charReader
ϋϋ 
.
ϋϋ 
Read
ϋϋ 
(
ϋϋ 
)
ϋϋ 
)
ϋϋ 
{
όό 
if
ύύ 

(
ύύ 
_charReader
ώώ	 
.
ώώ 
Current
ώώ 
==
ώώ 
$char
ώώ  #
||
ώώ$ &
_charReader
ÿÿ	 
.
ÿÿ 
Current
ÿÿ 
==
ÿÿ 
$char
ÿÿ  #
)
€€ 	
{
 	
characterHolder
‚‚	 
.
‚‚ 
Append
‚‚ 
(
‚‚  
_charReader
‚‚  +
.
‚‚+ ,
Current
‚‚, 3
)
‚‚3 4
;
‚‚4 5
}
ƒƒ 	
else
„„ 
{
…… 	
_charReader
††	 
.
†† 
Putback
†† 
(
†† 
)
†† 
;
†† 
}
‡‡ 	
}
 
break
 
;
 
}
‹‹ 
case
 	
$char

 
:
 
{
 
if
‘‘ 	
(
‘‘
 
_charReader
‘‘ 
.
‘‘ 
Read
‘‘ 
(
‘‘ 
)
‘‘ 
)
‘‘ 
{
’’ 
if
““ 

(
““ 
_charReader
””	 
.
”” 
Current
”” 
==
”” 
$char
””  #
||
””$ &
_charReader
••	 
.
•• 
Current
•• 
==
•• 
$char
••  #
||
••$ &
_charReader
––	 
.
–– 
Current
–– 
==
–– 
$char
––  #
)
—— 	
{
 	
characterHolder
™™	 
.
™™ 
Append
™™ 
(
™™  
_charReader
™™  +
.
™™+ ,
Current
™™, 3
)
™™3 4
;
™™4 5
}
 	
else
›› 
{
 	
_charReader
	 
.
 
Putback
 
(
 
)
 
;
 
}
 	
}
 
break
΅΅ 
;
΅΅ 
}
ΆΆ 
case
¥¥ 	
$char
¥¥
 
:
¥¥ 
{
¦¦ 
if
§§ 	
(
§§
 
_charReader
§§ 
.
§§ 
Read
§§ 
(
§§ 
)
§§ 
)
§§ 
{
¨¨ 
if
©© 

(
©© 
_charReader
ªª	 
.
ªª 
Current
ªª 
==
ªª 
$char
ªª  #
)
«« 	
{
¬¬ 	
characterHolder
­­	 
.
­­ 
Append
­­ 
(
­­  
_charReader
­­  +
.
­­+ ,
Current
­­, 3
)
­­3 4
;
­­4 5
}
®® 	
else
―― 
{
°° 	
_charReader
±±	 
.
±± 
Putback
±± 
(
±± 
)
±± 
;
±± 
}
²² 	
}
³³ 
break
µµ 
;
µµ 
}
¶¶ 
case
ΈΈ 	
$char
ΈΈ
 
:
ΈΈ 
case
ΊΊ 	
$char
ΊΊ
 
:
ΊΊ 
case
ΌΌ 	
$char
ΌΌ
 
:
ΌΌ 
case
ΎΎ 	
$char
ΎΎ
 
:
ΎΎ 
case
ΐΐ 	
$char
ΐΐ
 
:
ΐΐ 
case
ΒΒ 	
$char
ΒΒ
 
:
ΒΒ 
case
ΔΔ 	
$char
ΔΔ
 
:
ΔΔ 
{
ΕΕ 
if
ΖΖ 	
(
ΖΖ
 
_charReader
ΖΖ 
.
ΖΖ 
Read
ΖΖ 
(
ΖΖ 
)
ΖΖ 
)
ΖΖ 
{
ΗΗ 
if
ΘΘ 

(
ΘΘ 
_charReader
ΘΘ 
.
ΘΘ 
Current
ΘΘ 
==
ΘΘ  "
$char
ΘΘ# &
)
ΘΘ& '
{
ΙΙ 	
characterHolder
ΚΚ	 
.
ΚΚ 
Append
ΚΚ 
(
ΚΚ  
_charReader
ΚΚ  +
.
ΚΚ+ ,
Current
ΚΚ, 3
)
ΚΚ3 4
;
ΚΚ4 5
}
ΛΛ 	
else
ΜΜ 
{
ΝΝ 	
_charReader
ΞΞ	 
.
ΞΞ 
Putback
ΞΞ 
(
ΞΞ 
)
ΞΞ 
;
ΞΞ 
}
ΟΟ 	
}
ΠΠ 
break
ÒÒ 
;
ÒÒ 
}
ΣΣ 
case
ΥΥ 	
$char
ΥΥ
 
:
ΥΥ 
{
ΦΦ 
if
ΧΧ 	
(
ΧΧ
 
_charReader
ΧΧ 
.
ΧΧ 
Read
ΧΧ 
(
ΧΧ 
)
ΧΧ 
)
ΧΧ 
{
ΨΨ 
if
ΩΩ 

(
ΩΩ 
_charReader
ΩΩ 
.
ΩΩ 
Current
ΩΩ 
==
ΩΩ  "
$char
ΩΩ# '
)
ΩΩ' (
{
ΪΪ 	
characterHolder
ΫΫ	 
.
ΫΫ 
Append
ΫΫ 
(
ΫΫ  
_charReader
ΫΫ  +
.
ΫΫ+ ,
Current
ΫΫ, 3
)
ΫΫ3 4
;
ΫΫ4 5
goto
έέ	 
case
έέ 
$char
έέ 
;
έέ 
}
ήή 	
else
ίί 
if
ίί 
(
ίί 
_charReader
ίί 
.
ίί 
Current
ίί $
==
ίί% '
$char
ίί( ,
)
ίί, -
{
ΰΰ 	
characterHolder
αα	 
.
αα 
Append
αα 
(
αα  
_charReader
αα  +
.
αα+ ,
Current
αα, 3
)
αα3 4
;
αα4 5
goto
γγ	 
case
γγ 
$char
γγ 
;
γγ 
}
δδ 	
else
εε 
{
ζζ 	
_charReader
ηη	 
.
ηη 
Putback
ηη 
(
ηη 
)
ηη 
;
ηη 
goto
ιι	 
default
ιι 
;
ιι 
}
κκ 	
}
λλ 
break
νν 
;
νν 
}
ξξ 
case
ππ 	
$char
ππ
 
:
ππ 
{
ρρ 
if
ςς 	
(
ςς
 
_charReader
ςς 
.
ςς 
Read
ςς 
(
ςς 
)
ςς 
)
ςς 
{
σσ 
if
ττ 

(
ττ 
_charReader
ττ 
.
ττ 
Current
ττ 
==
ττ  "
$char
ττ# &
)
ττ& '
{
υυ 	
characterHolder
φφ	 
.
φφ 
Append
φφ 
(
φφ  
_charReader
φφ  +
.
φφ+ ,
Current
φφ, 3
)
φφ3 4
;
φφ4 5
}
χχ 	
else
ψψ 
{
ωω 	
_charReader
ϊϊ	 
.
ϊϊ 
Putback
ϊϊ 
(
ϊϊ 
)
ϊϊ 
;
ϊϊ 
}
ϋϋ 	
}
όό 
break
ώώ 
;
ώώ 
}
ÿÿ 
case
 	
$char

 
:
 
case
ƒƒ 	
$char
ƒƒ
 
:
ƒƒ 
case
…… 	
$char
……
 
:
…… 
{
†† 
char
‡‡ 

escapeChar
‡‡ 
;
‡‡ 
if
‰‰ 	
(
‰‰
 
_charReader
‰‰ 
.
‰‰ 
Current
‰‰ 
==
‰‰ !
$char
‰‰" %
)
‰‰% &
{
 

escapeChar
‹‹ 
=
‹‹ 
$char
‹‹ 
;
‹‹ 
}
 
else
 
{
 

escapeChar
 
=
 
_charReader
  
.
  !
Current
! (
;
( )
}
 
bool
’’ 
stillEscaped
’’ 
;
’’ 
do
–– 	
{
—— 
while
 
(
 
_charReader
™™	 
.
™™ 
Read
™™ 
(
™™ 
)
™™ 
&&
™™ 
_charReader
	 
.
 
Current
 
!=
 

escapeChar
  *
)
* +
{
›› 	
characterHolder
	 
.
 
Append
 
(
  
_charReader
  +
.
+ ,
Current
, 3
)
3 4
;
4 5
}
 	
;
	 

if
 

(
 
!
 
_charReader
 
.
 
EOF
 
)
 
{
   	
characterHolder
΅΅	 
.
΅΅ 
Append
΅΅ 
(
΅΅  
_charReader
΅΅  +
.
΅΅+ ,
Current
΅΅, 3
)
΅΅3 4
;
΅΅4 5
}
ΆΆ 	
stillEscaped
¤¤ 
=
¤¤ 
!
¥¥	 

_charReader
¥¥
 
.
¥¥ 
EOF
¥¥ 
&&
¥¥ 
_charReader
¦¦	 
.
¦¦ 
Read
¦¦ 
(
¦¦ 
)
¦¦ 
&&
¦¦ 
_charReader
§§	 
.
§§ 
Current
§§ 
==
§§ 

escapeChar
§§  *
;
§§* +
if
©© 

(
©© 
stillEscaped
©© 
)
©© 
{
ªª 	
characterHolder
««	 
.
«« 
Append
«« 
(
««  
_charReader
««  +
.
««+ ,
Current
««, 3
)
««3 4
;
««4 5
}
¬¬ 	
}
­­ 
while
­­	 
(
­­ 
stillEscaped
­­ 
)
­­ 
;
­­ 
if
―― 	
(
――
 
!
―― 
_charReader
―― 
.
―― 
EOF
―― 
)
―― 
{
°° 
_charReader
±± 
.
±± 
Putback
±± 
(
±± 
)
±± 
;
±± 
}
²² 
break
΄΄ 
;
΄΄ 
}
µµ 
case
ΉΉ 	
$char
ΉΉ
 
:
ΉΉ 
{
ΊΊ 
if
»» 	
(
»»
 
_charReader
»» 
.
»» 
Read
»» 
(
»» 
)
»» 
)
»» 
{
ΌΌ 
if
½½ 

(
½½ 
_charReader
ΎΎ	 
.
ΎΎ 
Current
ΎΎ 
==
ΎΎ 
$char
ΎΎ  #
||
ΎΎ$ &
_charReader
ΏΏ	 
.
ΏΏ 
Current
ΏΏ 
==
ΏΏ 
$char
ΏΏ  #
)
ΏΏ# $
{
ΐΐ 	
characterHolder
ΑΑ	 
.
ΑΑ 
Append
ΑΑ 
(
ΑΑ  
_charReader
ΑΑ  +
.
ΑΑ+ ,
Current
ΑΑ, 3
)
ΑΑ3 4
;
ΑΑ4 5
bool
ΓΓ	 
foundEnd
ΓΓ 
=
ΓΓ 
false
ΓΓ 
;
ΓΓ 
while
ΕΕ	 
(
ΕΕ 
!
ΖΖ
 
foundEnd
ΖΖ 
&&
ΖΖ 
_charReader
ΗΗ
 
.
ΗΗ 
Read
ΗΗ 
(
ΗΗ 
)
ΗΗ 
)
ΗΗ 
{
ΘΘ	 

switch
ΙΙ
 
(
ΙΙ 
_charReader
ΙΙ 
.
ΙΙ 
Current
ΙΙ %
)
ΙΙ% &
{
ΚΚ
 
case
ΛΛ 
$char
ΛΛ 
:
ΛΛ 
case
ΜΜ 
$char
ΜΜ 
:
ΜΜ 
case
ΝΝ 
$char
ΝΝ 
:
ΝΝ 
case
ΞΞ 
$char
ΞΞ 
:
ΞΞ 
case
ΟΟ 
$char
ΟΟ 
:
ΟΟ 
case
ΠΠ 
$char
ΠΠ 
:
ΠΠ 
case
ΡΡ 
$char
ΡΡ 
:
ΡΡ 
case
ÒÒ 
$char
ÒÒ 
:
ÒÒ 
case
ΣΣ 
$char
ΣΣ 
:
ΣΣ 
case
ΤΤ 
$char
ΤΤ 
:
ΤΤ 
case
ΥΥ 
$char
ΥΥ 
:
ΥΥ 
case
ΦΦ 
$char
ΦΦ 
:
ΦΦ 
case
ΧΧ 
$char
ΧΧ 
:
ΧΧ 
case
ΨΨ 
$char
ΨΨ 
:
ΨΨ 
case
ΩΩ 
$char
ΩΩ 
:
ΩΩ 
case
ΪΪ 
$char
ΪΪ 
:
ΪΪ 
case
ΫΫ 
$char
ΫΫ 
:
ΫΫ 
case
άά 
$char
άά 
:
άά 
case
έέ 
$char
έέ 
:
έέ 
case
ήή 
$char
ήή 
:
ήή 
case
ίί 
$char
ίί 
:
ίί 
case
ΰΰ 
$char
ΰΰ 
:
ΰΰ 
{
αα 
characterHolder
ββ 
.
ββ 
Append
ββ #
(
ββ# $
_charReader
ββ$ /
.
ββ/ 0
Current
ββ0 7
)
ββ7 8
;
ββ8 9
break
δδ 
;
δδ 
}
εε 
default
ζζ 
:
ζζ 
{
ηη 
foundEnd
θθ 
=
θθ 
true
θθ 
;
θθ 
break
κκ 
;
κκ 
}
λλ 
}
μμ
 
}
νν	 

if
οο	 
(
οο 
foundEnd
οο 
)
οο 
{
ππ	 

_charReader
ρρ
 
.
ρρ 
Putback
ρρ 
(
ρρ 
)
ρρ 
;
ρρ  
}
ςς	 

}
σσ 	
else
ττ 
{
υυ 	
_charReader
φφ	 
.
φφ 
Putback
φφ 
(
φφ 
)
φφ 
;
φφ 
goto
ψψ	 
case
ψψ 
$char
ψψ 
;
ψψ 
}
ωω 	
}
ϊϊ 
break
όό 
;
όό 
}
ύύ 
case
‚‚ 	
$char
‚‚
 
:
‚‚ 
case
ƒƒ 	
$char
ƒƒ
 
:
ƒƒ 
case
„„ 	
$char
„„
 
:
„„ 
case
…… 	
$char
……
 
:
…… 
case
†† 	
$char
††
 
:
†† 
case
‡‡ 	
$char
‡‡
 
:
‡‡ 
case
 	
$char

 
:
 
case
‰‰ 	
$char
‰‰
 
:
‰‰ 
case
 	
$char

 
:
 
{
‹‹ 
bool
 
foundEnd
 
=
 
false
 
;
 
bool
 
foundPeriod
 
=
 
false
 
;
  
while
 
(
 
!
 	
foundEnd
	 
&&
 
_charReader
‘‘ 
.
‘‘ 
Read
‘‘ 
(
‘‘ 
)
‘‘ 
)
‘‘ 
{
’’ 
switch
““ 
(
““ 
_charReader
““ 
.
““ 
Current
““ #
)
““# $
{
”” 	
case
••	 
$char
•• 
:
•• 
case
––	 
$char
–– 
:
–– 
{
——
 
characterHolder
 
.
 
Append
 !
(
! "
_charReader
" -
.
- .
Current
. 5
)
5 6
;
6 7
if
 
(
 
_charReader
 
.
 
Read
 
(
  
)
  !
)
! "
{
›› 
switch
 
(
 
_charReader
 
.
  
Current
  '
)
' (
{
 
case
 
$char
 
:
 
case
 
$char
 
:
 
{
   
characterHolder
΅΅ 
.
΅΅ 
Append
΅΅ %
(
΅΅% &
_charReader
΅΅& 1
.
΅΅1 2
Current
΅΅2 9
)
΅΅9 :
;
΅΅: ;
break
££ 
;
££ 
}
¤¤ 
default
¥¥ 
:
¥¥ 
{
¦¦ 
_charReader
§§ 
.
§§ 
Putback
§§ "
(
§§" #
)
§§# $
;
§§$ %
break
©© 
;
©© 
}
ªª 
}
«« 
}
¬¬ 
while
®® 
(
®® 
!
―― 
foundEnd
―― 
&&
―― 
_charReader
°° 
.
°° 
Read
°° 
(
°° 
)
°° 
)
°° 
{
±± 
switch
²² 
(
²² 
_charReader
²² 
.
²²  
Current
²²  '
)
²²' (
{
³³ 
case
΄΄ 
$char
΄΄ 
:
΄΄ 
case
µµ 
$char
µµ 
:
µµ 
case
¶¶ 
$char
¶¶ 
:
¶¶ 
case
·· 
$char
·· 
:
·· 
case
ΈΈ 
$char
ΈΈ 
:
ΈΈ 
case
ΉΉ 
$char
ΉΉ 
:
ΉΉ 
case
ΊΊ 
$char
ΊΊ 
:
ΊΊ 
case
»» 
$char
»» 
:
»» 
case
ΌΌ 
$char
ΌΌ 
:
ΌΌ 
case
½½ 
$char
½½ 
:
½½ 
{
ΎΎ 
characterHolder
ΏΏ 
.
ΏΏ 
Append
ΏΏ %
(
ΏΏ% &
_charReader
ΏΏ& 1
.
ΏΏ1 2
Current
ΏΏ2 9
)
ΏΏ9 :
;
ΏΏ: ;
break
ΑΑ 
;
ΑΑ 
}
ΒΒ 
default
ΓΓ 
:
ΓΓ 
{
ΔΔ 
foundEnd
ΕΕ 
=
ΕΕ 
true
ΕΕ 
;
ΕΕ 
break
ΗΗ 
;
ΗΗ 
}
ΘΘ 
}
ΙΙ 
}
ΚΚ 
break
ΜΜ 
;
ΜΜ 
}
ΝΝ
 
case
ΞΞ	 
$char
ΞΞ 
:
ΞΞ 
{
ΟΟ
 
if
ΠΠ 
(
ΠΠ 
foundPeriod
ΠΠ 
)
ΠΠ 
{
ΡΡ 
foundEnd
ÒÒ 
=
ÒÒ 
true
ÒÒ 
;
ÒÒ 
}
ΣΣ 
else
ΤΤ 
{
ΥΥ 
characterHolder
ΦΦ 
.
ΦΦ 
Append
ΦΦ "
(
ΦΦ" #
_charReader
ΦΦ# .
.
ΦΦ. /
Current
ΦΦ/ 6
)
ΦΦ6 7
;
ΦΦ7 8
foundPeriod
ΨΨ 
=
ΨΨ 
true
ΨΨ 
;
ΨΨ 
}
ΩΩ 
break
ΫΫ 
;
ΫΫ 
}
άά
 
case
έέ	 
$char
έέ 
:
έέ 
case
ήή	 
$char
ήή 
:
ήή 
case
ίί	 
$char
ίί 
:
ίί 
case
ΰΰ	 
$char
ΰΰ 
:
ΰΰ 
case
αα	 
$char
αα 
:
αα 
case
ββ	 
$char
ββ 
:
ββ 
case
γγ	 
$char
γγ 
:
γγ 
case
δδ	 
$char
δδ 
:
δδ 
case
εε	 
$char
εε 
:
εε 
case
ζζ	 
$char
ζζ 
:
ζζ 
{
ηη
 
characterHolder
θθ 
.
θθ 
Append
θθ !
(
θθ! "
_charReader
θθ" -
.
θθ- .
Current
θθ. 5
)
θθ5 6
;
θθ6 7
break
κκ 
;
κκ 
}
λλ
 
default
νν	 
:
νν 
{
ξξ
 
foundEnd
οο 
=
οο 
true
οο 
;
οο 
break
ρρ 
;
ρρ 
}
ςς
 
}
σσ 	
}
ττ 
if
φφ 	
(
φφ
 
foundEnd
φφ 
)
φφ 
{
χχ 
_charReader
ψψ 
.
ψψ 
Putback
ψψ 
(
ψψ 
)
ψψ 
;
ψψ 
}
ωω 
break
ϋϋ 
;
ϋϋ 
}
όό 
case
ÿÿ 	
$char
ÿÿ
 
:
ÿÿ 
{
€€ 
if
 	
(

 
_charReader
 
.
 
Read
 
(
 
)
 
)
 
{
‚‚ 
if
ƒƒ 

(
ƒƒ 
_charReader
„„	 
.
„„ 
Current
„„ 
==
„„ 
$char
„„  #
||
„„$ &
_charReader
……	 
.
…… 
Current
…… 
==
…… 
$char
……  #
||
……$ &
_charReader
††	 
.
†† 
Current
†† 
==
†† 
$char
††  #
||
††$ &
_charReader
‡‡	 
.
‡‡ 
Current
‡‡ 
==
‡‡ 
$char
‡‡  #
||
‡‡$ &
_charReader
	 
.
 
Current
 
==
 
$char
  #
||
$ &
_charReader
‰‰	 
.
‰‰ 
Current
‰‰ 
==
‰‰ 
$char
‰‰  #
||
‰‰$ &
_charReader
	 
.
 
Current
 
==
 
$char
  #
||
$ &
_charReader
‹‹	 
.
‹‹ 
Current
‹‹ 
==
‹‹ 
$char
‹‹  #
||
‹‹$ &
_charReader
	 
.
 
Current
 
==
 
$char
  #
||
$ &
_charReader
	 
.
 
Current
 
==
 
$char
  #
||
$ &
_charReader
	 
.
 
Current
 
==
 
$char
  #
||
$ &
_charReader
	 
.
 
Current
 
==
 
$char
  #
||
$ &
_charReader
	 
.
 
Current
 
==
 
$char
  #
)
‘‘	 

{
’’ 	
_charReader
““	 
.
““ 
Putback
““ 
(
““ 
)
““ 
;
““ 
goto
••	 
case
•• 
$char
•• 
;
•• 
}
–– 	
else
—— 
{
 	
_charReader
™™	 
.
™™ 
Putback
™™ 
(
™™ 
)
™™ 
;
™™ 
goto
››	 
default
›› 
;
›› 
}
 	
}
 
break
 
;
 
}
   
case
ΆΆ 	
$char
ΆΆ
 
:
ΆΆ 
case
££ 	
$char
££
 
:
££ 
case
¤¤ 	
$char
¤¤
 
:
¤¤ 
case
¥¥ 	
$char
¥¥
 
:
¥¥ 
case
¦¦ 	
$char
¦¦
 
:
¦¦ 
case
§§ 	
$char
§§
 
:
§§ 
case
¨¨ 	
$char
¨¨
 
:
¨¨ 
case
©© 	
$char
©©
 
:
©© 
case
ªª 	
$char
ªª
 
:
ªª 
case
«« 	
$char
««
 
:
«« 
case
¬¬ 	
$char
¬¬
 
:
¬¬ 
case
­­ 	
$char
­­
 
:
­­ 
case
®® 	
$char
®®
 
:
®® 
case
―― 	
$char
――
 
:
―― 
case
°° 	
$char
°°
 
:
°° 
case
±± 	
$char
±±
 
:
±± 
case
²² 	
$char
²²
 
:
²² 
case
³³ 	
$char
³³
 
:
³³ 
case
΄΄ 	
$char
΄΄
 
:
΄΄ 
case
µµ 	
$char
µµ
 
:
µµ 
case
¶¶ 	
$char
¶¶
 
:
¶¶ 
case
·· 	
$char
··
 
:
·· 
case
ΈΈ 	
$char
ΈΈ
 
:
ΈΈ 
case
ΉΉ 	
$char
ΉΉ
 
:
ΉΉ 
case
ΊΊ 	
$char
ΊΊ
 
:
ΊΊ 
case
»» 	
$char
»»
 
:
»» 
case
ΌΌ 	
$char
ΌΌ
 
:
ΌΌ 
case
½½ 	
$char
½½
 
:
½½ 
case
ΎΎ 	
$char
ΎΎ
 
:
ΎΎ 
case
ΏΏ 	
$char
ΏΏ
 
:
ΏΏ 
case
ΐΐ 	
$char
ΐΐ
 
:
ΐΐ 
case
ΑΑ 	
$char
ΑΑ
 
:
ΑΑ 
case
ΒΒ 	
$char
ΒΒ
 
:
ΒΒ 
{
ΓΓ 
bool
ΔΔ 
foundEnd
ΔΔ 
=
ΔΔ 
false
ΔΔ 
;
ΔΔ 
bool
ΕΕ 
foundPeriod
ΕΕ 
=
ΕΕ 
false
ΕΕ 
;
ΕΕ  
if
ΗΗ 	
(
ΗΗ
 
_charReader
ΗΗ 
.
ΗΗ 
Read
ΗΗ 
(
ΗΗ 
)
ΗΗ 
)
ΗΗ 
{
ΘΘ 
switch
ΙΙ 
(
ΙΙ 
_charReader
ΙΙ 
.
ΙΙ 
Current
ΙΙ #
)
ΙΙ# $
{
ΚΚ 	
case
ΛΛ	 
$char
ΛΛ 
:
ΛΛ 
case
ΜΜ	 
$char
ΜΜ 
:
ΜΜ 
{
ΝΝ
 
characterHolder
ΞΞ 
.
ΞΞ 
Append
ΞΞ !
(
ΞΞ! "
_charReader
ΞΞ" -
.
ΞΞ- .
Current
ΞΞ. 5
)
ΞΞ5 6
;
ΞΞ6 7
break
ΠΠ 
;
ΠΠ 
}
ΡΡ
 
default
ÒÒ	 
:
ÒÒ 
{
ΣΣ
 
_charReader
ΤΤ 
.
ΤΤ 
Putback
ΤΤ 
(
ΤΤ 
)
ΤΤ  
;
ΤΤ  !
break
ΦΦ 
;
ΦΦ 
}
ΧΧ
 
}
ΨΨ 	
}
ΩΩ 
while
ΫΫ 
(
ΫΫ 
!
άά 	
foundEnd
άά	 
&&
άά 
_charReader
έέ 
.
έέ 
Read
έέ 
(
έέ 
)
έέ 
)
έέ 
{
ήή 
switch
ίί 
(
ίί 
_charReader
ίί 
.
ίί 
Current
ίί #
)
ίί# $
{
ΰΰ 	
case
αα	 
$char
αα 
:
αα 
{
ββ
 
if
γγ 
(
γγ 
foundPeriod
γγ 
)
γγ 
{
δδ 
foundEnd
εε 
=
εε 
true
εε 
;
εε 
}
ζζ 
else
ηη 
{
θθ 
characterHolder
ιι 
.
ιι 
Append
ιι "
(
ιι" #
_charReader
ιι# .
.
ιι. /
Current
ιι/ 6
)
ιι6 7
;
ιι7 8
foundPeriod
λλ 
=
λλ 
true
λλ 
;
λλ 
}
μμ 
break
ξξ 
;
ξξ 
}
οο
 
case
ππ	 
$char
ππ 
:
ππ 
case
ρρ	 
$char
ρρ 
:
ρρ 
case
ςς	 
$char
ςς 
:
ςς 
case
σσ	 
$char
σσ 
:
σσ 
case
ττ	 
$char
ττ 
:
ττ 
case
υυ	 
$char
υυ 
:
υυ 
case
φφ	 
$char
φφ 
:
φφ 
case
χχ	 
$char
χχ 
:
χχ 
case
ψψ	 
$char
ψψ 
:
ψψ 
case
ωω	 
$char
ωω 
:
ωω 
{
ϊϊ
 
characterHolder
ϋϋ 
.
ϋϋ 
Append
ϋϋ !
(
ϋϋ! "
_charReader
ϋϋ" -
.
ϋϋ- .
Current
ϋϋ. 5
)
ϋϋ5 6
;
ϋϋ6 7
break
ύύ 
;
ύύ 
}
ώώ
 
default
ÿÿ	 
:
ÿÿ 
{
€€
 
foundEnd
 
=
 
true
 
;
 
break
ƒƒ 
;
ƒƒ 
}
„„
 
}
…… 	
}
†† 
if
 	
(

 
foundEnd
 
)
 
{
‰‰ 
_charReader
 
.
 
Putback
 
(
 
)
 
;
 
}
‹‹ 
break
 
;
 
}
 
default
 
:
 
{
 
bool
‘‘ 
foundEnd
‘‘ 
=
‘‘ 
false
‘‘ 
;
‘‘ 
while
““ 
(
““ 
!
”” 	
foundEnd
””	 
&&
”” 
_charReader
•• 
.
•• 
Read
•• 
(
•• 
)
•• 
)
•• 
{
–– 
switch
—— 
(
—— 
_charReader
—— 
.
—— 
Current
—— #
)
——# $
{
 	
case
	 
$char
 
:
 
case
››	 
$char
›› 
:
›› 
case
	 
$char
 
:
 
case
	 
$char
 
:
 
case
	 
$char
 
:
 
case
	 
$char
 
:
 
case
  	 
$char
   
:
   
case
΅΅	 
$char
΅΅ 
:
΅΅ 
case
ΆΆ	 
$char
ΆΆ 
:
ΆΆ 
case
££	 
$char
££ 
:
££ 
case
¤¤	 
$char
¤¤ 
:
¤¤ 
case
¥¥	 
$char
¥¥ 
:
¥¥ 
case
¦¦	 
$char
¦¦ 
:
¦¦ 
case
§§	 
$char
§§ 
:
§§ 
case
¨¨	 
$char
¨¨ 
:
¨¨ 
case
©©	 
$char
©© 
:
©© 
case
ªª	 
$char
ªª 
:
ªª 
case
««	 
$char
«« 
:
«« 
case
¬¬	 
$char
¬¬ 
:
¬¬ 
case
­­	 
$char
­­ 
:
­­ 
case
®®	 
$char
®® 
:
®® 
case
――	 
$char
―― 
:
―― 
case
°°	 
$char
°° 
:
°° 
{
±±
 
foundEnd
²² 
=
²² 
true
²² 
;
²² 
break
΄΄ 
;
΄΄ 
}
µµ
 
default
¶¶	 
:
¶¶ 
{
··
 
characterHolder
ΈΈ 
.
ΈΈ 
Append
ΈΈ !
(
ΈΈ! "
_charReader
ΈΈ" -
.
ΈΈ- .
Current
ΈΈ. 5
)
ΈΈ5 6
;
ΈΈ6 7
break
ΊΊ 
;
ΊΊ 
}
»»
 
}
ΌΌ 	
}
½½ 
if
ΏΏ 	
(
ΏΏ
 
foundEnd
ΏΏ 
)
ΏΏ 
{
ΐΐ 
_charReader
ΑΑ 
.
ΑΑ 
Putback
ΑΑ 
(
ΑΑ 
)
ΑΑ 
;
ΑΑ 
}
ΒΒ 
break
ΔΔ 
;
ΔΔ 
}
ΕΕ 
}
ΖΖ 
}
ΗΗ 
_current
ΙΙ 
=
ΙΙ  
DetermineTokenType
ΙΙ  
(
ΙΙ  !
characterHolder
ΚΚ 
.
ΚΚ 
ToString
ΚΚ 
(
ΚΚ 
)
ΚΚ 
,
ΚΚ 
startPosition
ΛΛ 
,
ΛΛ 
startPosition
ΜΜ 
+
ΜΜ 
characterHolder
ΜΜ #
.
ΜΜ# $
Length
ΜΜ$ *
-
ΜΜ+ ,
$num
ΜΜ- .
)
ΜΜ. /
;
ΜΜ/ 0
}
ΝΝ 
private
ΟΟ 	
	TSQLToken
ΟΟ
  
DetermineTokenType
ΟΟ &
(
ΟΟ& '
string
ΠΠ 	

tokenValue
ΠΠ
 
,
ΠΠ 
int
ΡΡ 
startPosition
ΡΡ 
,
ΡΡ 
int
ÒÒ 
endPosition
ÒÒ 
)
ÒÒ 
{
ΣΣ 
if
ΤΤ 
(
ΤΤ 
char
ΥΥ 
.
ΥΥ 	
IsWhiteSpace
ΥΥ	 
(
ΥΥ 

tokenValue
ΥΥ  
[
ΥΥ  !
$num
ΥΥ! "
]
ΥΥ" #
)
ΥΥ# $
)
ΥΥ$ %
{
ΦΦ 
return
ΧΧ 

new
ΨΨ 
TSQLWhitespace
ΨΨ	 
(
ΨΨ 
startPosition
ΩΩ 
,
ΩΩ 

tokenValue
ΪΪ 
)
ΪΪ 
;
ΪΪ 
}
ΫΫ 
else
άά 
if
άά 

(
άά 

tokenValue
έέ 
[
έέ 
$num
έέ 
]
έέ 
==
έέ 
$char
έέ 
)
έέ 
{
ήή 
if
ίί 
(
ίί 
TSQLVariables
ίί 
.
ίί 

IsVariable
ίί  
(
ίί  !

tokenValue
ίί! +
)
ίί+ ,
)
ίί, -
{
ΰΰ 
return
αα 
new
ββ 	 
TSQLSystemVariable
ββ
 
(
ββ 
startPosition
γγ 
,
γγ 

tokenValue
δδ 
)
δδ 
;
δδ 
}
εε 
else
ζζ 
{
ηη 
return
θθ 
new
ιι 	
TSQLVariable
ιι
 
(
ιι 
startPosition
κκ 
,
κκ 

tokenValue
λλ 
)
λλ 
;
λλ 
}
μμ 
}
νν 
else
ξξ 
if
ξξ 

(
ξξ 

tokenValue
ξξ 
.
ξξ 

StartsWith
ξξ !
(
ξξ! "
$str
ξξ" &
)
ξξ& '
)
ξξ' (
{
οο 
return
ππ 

new
ρρ #
TSQLSingleLineComment
ρρ	 
(
ρρ 
startPosition
ςς 
,
ςς 

tokenValue
σσ 
)
σσ 
;
σσ 
}
ττ 
else
υυ 
if
υυ 

(
υυ 

tokenValue
υυ 
.
υυ 

StartsWith
υυ !
(
υυ! "
$str
υυ" &
)
υυ& '
)
υυ' (
{
φφ 
return
χχ 

new
ψψ "
TSQLMultilineComment
ψψ	 
(
ψψ 
startPosition
ωω 
,
ωω 

tokenValue
ϊϊ 
)
ϊϊ 
;
ϊϊ 
}
ϋϋ 
else
όό 
if
όό 

(
όό 

tokenValue
ύύ 
.
ύύ 

StartsWith
ύύ 
(
ύύ 
$str
ύύ 
)
ύύ 
||
ύύ  "

tokenValue
ώώ 
.
ώώ 

StartsWith
ώώ 
(
ώώ 
$str
ώώ 
)
ώώ  
||
ώώ! #
(
ÿÿ 
!
€€ "
UseQuotedIdentifiers
€€ 
&&
€€ 
(
 

tokenValue
‚‚ 
.
‚‚ 

StartsWith
‚‚ 
(
‚‚ 
$str
‚‚  
)
‚‚  !
||
‚‚" $

tokenValue
ƒƒ 
.
ƒƒ 

StartsWith
ƒƒ 
(
ƒƒ 
$str
ƒƒ !
)
ƒƒ! "
)
„„ 
)
…… 
)
…… 
{
†† 
return
‡‡ 

new
 
TSQLStringLiteral
	 
(
 
startPosition
‰‰ 
,
‰‰ 

tokenValue
 
)
 
;
 
}
‹‹ 
else
 
if
 

(
 

tokenValue
 
[
 
$num
 
]
 
==
 
$char
 
)
 
{
 
if
 
(
 

tokenValue
‘‘ 
.
‘‘ 
Length
‘‘ 
>
‘‘ 
$num
‘‘ 
&&
‘‘ 
char
’’ 	
.
’’	 

IsLetter
’’
 
(
’’ 

tokenValue
’’ 
[
’’ 
$num
’’ 
]
’’  
)
’’  !
)
’’! "
{
““ 
return
”” 
new
•• 	
TSQLIdentifier
••
 
(
•• 
startPosition
–– 
,
–– 

tokenValue
—— 
)
—— 
;
—— 
}
 
else
 
{
›› 
return
 
new
 	
TSQLMoneyLiteral

 
(
 
startPosition
 
,
 

tokenValue
 
)
 
;
 
}
   
}
΅΅ 
else
ΆΆ 
if
ΆΆ 

(
ΆΆ 
CharUnicodeInfo
ΆΆ 
.
ΆΆ  
GetUnicodeCategory
ΆΆ .
(
ΆΆ. /

tokenValue
ΆΆ/ 9
[
ΆΆ9 :
$num
ΆΆ: ;
]
ΆΆ; <
)
ΆΆ< =
==
ΆΆ> @
UnicodeCategory
ΆΆA P
.
ΆΆP Q
CurrencySymbol
ΆΆQ _
)
ΆΆ_ `
{
££ 
return
¤¤ 

new
¥¥ 
TSQLMoneyLiteral
¥¥	 
(
¥¥ 
startPosition
¦¦ 
,
¦¦ 

tokenValue
§§ 
)
§§ 
;
§§ 
}
¨¨ 
else
©© 
if
©© 

(
©© 

tokenValue
©© 
.
©© 

StartsWith
©© !
(
©©! "
$str
©©" &
,
©©& '
StringComparison
©©( 8
.
©©8 9(
InvariantCultureIgnoreCase
©©9 S
)
©©S T
)
©©T U
{
ªª 
return
«« 

new
¬¬ 
TSQLBinaryLiteral
¬¬	 
(
¬¬ 
startPosition
­­ 
,
­­ 

tokenValue
®® 
)
®® 
;
®® 
}
―― 
else
°° 
if
°° 

(
°° 
char
±± 
.
±± 	
IsDigit
±±	 
(
±± 

tokenValue
±± 
[
±± 
$num
±± 
]
±± 
)
±± 
||
±±  "
(
²² 

tokenValue
³³ 
[
³³ 
$num
³³ 
]
³³ 
==
³³ 
$char
³³ 
&&
³³ 

tokenValue
΄΄ 
.
΄΄ 
Length
΄΄ 
>
΄΄ 
$num
΄΄ 
&&
΄΄ 
char
µµ 	
.
µµ	 

IsDigit
µµ
 
(
µµ 

tokenValue
µµ 
[
µµ 
$num
µµ 
]
µµ 
)
µµ  
)
¶¶ 
)
¶¶ 
{
·· 
return
ΈΈ 

new
ΉΉ  
TSQLNumericLiteral
ΉΉ	 
(
ΉΉ 
startPosition
ΊΊ 
,
ΊΊ 

tokenValue
»» 
)
»» 
;
»» 
}
ΌΌ 
else
½½ 
if
½½ 

(
½½ 

tokenValue
ΎΎ 
[
ΎΎ 
$num
ΎΎ 
]
ΎΎ 
==
ΎΎ 
$char
ΎΎ 
||
ΎΎ 

tokenValue
ΏΏ 
[
ΏΏ 
$num
ΏΏ 
]
ΏΏ 
==
ΏΏ 
$char
ΏΏ 
||
ΏΏ 

tokenValue
ΐΐ 
[
ΐΐ 
$num
ΐΐ 
]
ΐΐ 
==
ΐΐ 
$char
ΐΐ 
||
ΐΐ 

tokenValue
ΑΑ 
[
ΑΑ 
$num
ΑΑ 
]
ΑΑ 
==
ΑΑ 
$char
ΑΑ 
||
ΑΑ 

tokenValue
ΒΒ 
[
ΒΒ 
$num
ΒΒ 
]
ΒΒ 
==
ΒΒ 
$char
ΒΒ 
||
ΒΒ 

tokenValue
ΓΓ 
[
ΓΓ 
$num
ΓΓ 
]
ΓΓ 
==
ΓΓ 
$char
ΓΓ 
||
ΓΓ 

tokenValue
ΔΔ 
[
ΔΔ 
$num
ΔΔ 
]
ΔΔ 
==
ΔΔ 
$char
ΔΔ 
||
ΔΔ 

tokenValue
ΕΕ 
[
ΕΕ 
$num
ΕΕ 
]
ΕΕ 
==
ΕΕ 
$char
ΕΕ 
||
ΕΕ 

tokenValue
ΖΖ 
[
ΖΖ 
$num
ΖΖ 
]
ΖΖ 
==
ΖΖ 
$char
ΖΖ 
||
ΖΖ 

tokenValue
ΗΗ 
[
ΗΗ 
$num
ΗΗ 
]
ΗΗ 
==
ΗΗ 
$char
ΗΗ 
||
ΗΗ 

tokenValue
ΘΘ 
[
ΘΘ 
$num
ΘΘ 
]
ΘΘ 
==
ΘΘ 
$char
ΘΘ 
||
ΘΘ 

tokenValue
ΙΙ 
[
ΙΙ 
$num
ΙΙ 
]
ΙΙ 
==
ΙΙ 
$char
ΙΙ 
||
ΙΙ 

tokenValue
ΚΚ 
[
ΚΚ 
$num
ΚΚ 
]
ΚΚ 
==
ΚΚ 
$char
ΚΚ 
||
ΚΚ 

tokenValue
ΛΛ 
[
ΛΛ 
$num
ΛΛ 
]
ΛΛ 
==
ΛΛ 
$char
ΛΛ 
)
ΛΛ 
{
ΜΜ 
return
ΝΝ 

new
ΞΞ 
TSQLOperator
ΞΞ	 
(
ΞΞ 
startPosition
ΟΟ 
,
ΟΟ 

tokenValue
ΠΠ 
)
ΠΠ 
;
ΠΠ 
}
ΡΡ 
else
ÒÒ 
if
ÒÒ 

(
ÒÒ 
TSQLCharacters
ÒÒ 
.
ÒÒ 
IsCharacter
ÒÒ &
(
ÒÒ& '

tokenValue
ÒÒ' 1
)
ÒÒ1 2
)
ÒÒ2 3
{
ΣΣ 
return
ΤΤ 

new
ΥΥ 
TSQLCharacter
ΥΥ	 
(
ΥΥ 
startPosition
ΦΦ 
,
ΦΦ 

tokenValue
ΧΧ 
)
ΧΧ 
;
ΧΧ 
}
ΨΨ 
else
ΩΩ 
if
ΩΩ 

(
ΩΩ 
TSQLKeywords
ΩΩ 
.
ΩΩ 
	IsKeyword
ΩΩ "
(
ΩΩ" #

tokenValue
ΩΩ# -
)
ΩΩ- .
)
ΩΩ. /
{
ΪΪ 
return
ΫΫ 

new
άά 
TSQLKeyword
άά	 
(
άά 
startPosition
έέ 
,
έέ 

tokenValue
ήή 
)
ήή 
;
ήή 
}
ίί 
else
ΰΰ 
if
ΰΰ 

(
ΰΰ 
TSQLIdentifiers
ΰΰ 
.
ΰΰ 
IsIdentifier
ΰΰ (
(
ΰΰ( )

tokenValue
ΰΰ) 3
)
ΰΰ3 4
)
ΰΰ4 5
{
αα 
return
ββ 

new
γγ "
TSQLSystemIdentifier
γγ	 
(
γγ 
startPosition
δδ 
,
δδ 

tokenValue
εε 
)
εε 
;
εε 
}
ζζ 
else
ηη 
{
θθ 
return
ιι 

new
κκ 
TSQLIdentifier
κκ	 
(
κκ 
startPosition
λλ 
,
λλ 

tokenValue
μμ 
)
μμ 
;
μμ 
}
νν 
}
ξξ 
public
ππ 
void
ππ	 
Putback
ππ 
(
ππ 
)
ππ 
{
ρρ 
	_hasExtra
ςς 
=
ςς 
true
ςς 
;
ςς 
_extraToken
σσ 
=
σσ 
_current
σσ 
;
σσ 
_hasMore
ττ 
=
ττ 
true
ττ 
;
ττ 
}
υυ 
public
χχ 
	TSQLToken
χχ	 
Current
χχ 
{
ψψ 
get
ωω 
{
ϊϊ 
CheckDisposed
ϋϋ 
(
ϋϋ 
)
ϋϋ 
;
ϋϋ 
return
όό 

_current
όό 
;
όό 
}
ύύ 
}
ώώ 
public
€€ 
static
€€	 
List
€€ 
<
€€ 
	TSQLToken
€€ 
>
€€ 
ParseTokens
€€  +
(
€€+ ,
string
 	
tsqlText

 
,
 
bool
‚‚ "
useQuotedIdentifiers
‚‚ 
=
‚‚ 
false
‚‚ $
,
‚‚$ %
bool
ƒƒ 
includeWhitespace
ƒƒ 
=
ƒƒ 
false
ƒƒ !
)
ƒƒ! "
{
„„ 
return
…… 	
new
……
 
TSQLTokenizer
…… 
(
…… 
tsqlText
…… $
)
……$ %
{
†† "
UseQuotedIdentifiers
‡‡ 
=
‡‡ "
useQuotedIdentifiers
‡‡ /
,
‡‡/ 0
IncludeWhitespace
 
=
 
includeWhitespace
 )
}
‰‰ 
.
‰‰ 
ToList
‰‰ 
(
‰‰ 
)
‰‰ 
;
‰‰ 
}
 
}
‹‹ 
} Ζ
\C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLTokenizer.IDisposable.cs
	namespace 	
TSQL
 
{ 
partial 
class	 
TSQLTokenizer 
: 
IDisposable *
{		 
private 	
bool
 
	_disposed 
= 
false  
;  !
void 
IDisposable 
. 
Dispose 
( 
) 
{ 
if 
( 
! 
	_disposed 
) 
{ 
Dispose 
( 
true 
) 
; 
} 
} 
private 	
void
 
Dispose 
( 
bool 
	disposing %
)% &
{ 
if 
( 
! 
	_disposed 
) 
{   
if"" 
("" 
	disposing"" 
)"" 
{## 
}%% 
try(( 
{)) 
(** 
_charReader** 
as** 
IDisposable**  
)**  !
.**! "
Dispose**" )
(**) *
)*** +
;**+ ,
}++ 
catch,, 	
(,,
 
	Exception,, 
),, 
{-- 
}// 
_charReader00 
=00 
null00 
;00 
	_disposed22 
=22 
true22 
;22 
}33 
}44 
private== 	
void==
 
CheckDisposed== 
(== 
)== 
{>> 
if?? 
(?? 
	_disposed?? 
)?? 
{@@ 
throwAA 	
newAA
 #
ObjectDisposedExceptionAA %
(AA% &
GetTypeAA& -
(AA- .
)AA. /
.AA/ 0
FullNameAA0 8
,AA8 9
$strAA: e
+AAf g
$strBB ,
+BB- .
$strCC 
)CC 
;CC 
}DD 
}EE 
}HH 
}II Ζ
\C:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLTokenizer.IEnumerable.cs
	namespace 	
TSQL
 
{ 
partial		 
class			 
TSQLTokenizer		 
:		 
ITSQLTokenizer		 -
{

 
IEnumerator 
IEnumerable 
. 
GetEnumerator '
(' (
)( )
{ 
return 	
this
 
; 
} 
IEnumerator 
< 
	TSQLToken 
> 
IEnumerable $
<$ %
	TSQLToken% .
>. /
./ 0
GetEnumerator0 =
(= >
)> ?
{ 
return 	
this
 
; 
} 
object 
IEnumerator	 
. 
Current 
{ 
get 
{ 
return 

( 
this 
as 
IEnumerator 
<  
	TSQLToken  )
>) *
)* +
.+ ,
Current, 3
;3 4
} 
} 
void 
IEnumerator 
. 
Reset 
( 
) 
{   
throw!! 
new!!	 !
NotSupportedException!! "
(!!" #
$str!!# t
+!!u v
GetType!!w ~
(!!~ 
)	!! €
.
!!€ 
FullName
!! ‰
+
!! ‹
$str
!! 
)
!! 
;
!! ‘
}"" 
}%% 
}&& ‰Y
PC:\Users\shrio\Source\Repos\tsql-parser\TSQL_Parser\TSQL_Parser\TSQLVariables.cs
	namespace 	
TSQL
 
{ 
public 
class 
TSQLVariables 
{		 
private

 	
static


 

Dictionary

 
<

 
string

 "
,

" #
TSQLVariables

$ 1
>

1 2
variableLookup

3 A
=

B C
new 

Dictionary 
< 
string 
, 
TSQLVariables '
>' (
(( )
StringComparer) 7
.7 8&
InvariantCultureIgnoreCase8 R
)R S
;S T
public 
static	 
TSQLVariables 
None "
=# $
new% (
TSQLVariables) 6
(6 7
$str7 9
)9 :
;: ;
public 
static	 
TSQLVariables 
CONNECTIONS )
=* +
new, /
TSQLVariables0 =
(= >
$str> M
)M N
;N O
public 
static	 
TSQLVariables 
MAX_CONNECTIONS -
=. /
new0 3
TSQLVariables4 A
(A B
$strB U
)U V
;V W
public 
static	 
TSQLVariables 
CPU_BUSY &
=' (
new) ,
TSQLVariables- :
(: ;
$str; G
)G H
;H I
public 
static	 
TSQLVariables 
ERROR #
=$ %
new& )
TSQLVariables* 7
(7 8
$str8 A
)A B
;B C
public 
static	 
TSQLVariables 
IDENTITY &
=' (
new) ,
TSQLVariables- :
(: ;
$str; G
)G H
;H I
public 
static	 
TSQLVariables 
IDLE "
=# $
new% (
TSQLVariables) 6
(6 7
$str7 ?
)? @
;@ A
public 
static	 
TSQLVariables 
IO_BUSY %
=& '
new( +
TSQLVariables, 9
(9 :
$str: E
)E F
;F G
public 
static	 
TSQLVariables 
LANGID $
=% &
new' *
TSQLVariables+ 8
(8 9
$str9 C
)C D
;D E
public 
static	 
TSQLVariables 
LANGUAGE &
=' (
new) ,
TSQLVariables- :
(: ;
$str; G
)G H
;H I
public 
static	 
TSQLVariables 

MAXCHARLEN (
=) *
new+ .
TSQLVariables/ <
(< =
$str= K
)K L
;L M
public 
static	 
TSQLVariables 
PACK_RECEIVED +
=, -
new. 1
TSQLVariables2 ?
(? @
$str@ Q
)Q R
;R S
public 
static	 
TSQLVariables 
	PACK_SENT '
=( )
new* -
TSQLVariables. ;
(; <
$str< I
)I J
;J K
public 
static	 
TSQLVariables 
PACKET_ERRORS +
=, -
new. 1
TSQLVariables2 ?
(? @
$str@ Q
)Q R
;R S
public 
static	 
TSQLVariables 
ROWCOUNT &
=' (
new) ,
TSQLVariables- :
(: ;
$str; G
)G H
;H I
public 
static	 
TSQLVariables 

SERVERNAME (
=) *
new+ .
TSQLVariables/ <
(< =
$str= K
)K L
;L M
public   
static  	 
TSQLVariables   
SPID   "
=  # $
new  % (
TSQLVariables  ) 6
(  6 7
$str  7 ?
)  ? @
;  @ A
public!! 
static!!	 
TSQLVariables!! 
TEXTSIZE!! &
=!!' (
new!!) ,
TSQLVariables!!- :
(!!: ;
$str!!; G
)!!G H
;!!H I
public"" 
static""	 
TSQLVariables"" 
	TIMETICKS"" '
=""( )
new""* -
TSQLVariables"". ;
(""; <
$str""< I
)""I J
;""J K
public## 
static##	 
TSQLVariables## 
TOTAL_ERRORS## *
=##+ ,
new##- 0
TSQLVariables##1 >
(##> ?
$str##? O
)##O P
;##P Q
public$$ 
static$$	 
TSQLVariables$$ 

TOTAL_READ$$ (
=$$) *
new$$+ .
TSQLVariables$$/ <
($$< =
$str$$= K
)$$K L
;$$L M
public%% 
static%%	 
TSQLVariables%% 
TOTAL_WRITE%% )
=%%* +
new%%, /
TSQLVariables%%0 =
(%%= >
$str%%> M
)%%M N
;%%N O
public&& 
static&&	 
TSQLVariables&& 
	TRANCOUNT&& '
=&&( )
new&&* -
TSQLVariables&&. ;
(&&; <
$str&&< I
)&&I J
;&&J K
public'' 
static''	 
TSQLVariables'' 
VERSION'' %
=''& '
new''( +
TSQLVariables'', 9
(''9 :
$str'': E
)''E F
;''F G
private++ 	
string++
 
Variable++ 
;++ 
private-- 	
TSQLVariables--
 
(-- 
string.. 	
variable..
 
).. 
{// 
Variable00 
=00 
variable00 
;00 
if11 
(11 
variable11 
.11 
Length11 
>11 
$num11 
)11 
{22 
variableLookup33 
[33 
variable33 
]33 
=33 
this33 #
;33# $
}44 
}55 
public77 
static77	 
TSQLVariables77 
Parse77 #
(77# $
string88 	
token88
 
)88 
{99 
if:: 
(:: 
!;; 
string;; 
.;; 
IsNullOrEmpty;; 
(;; 
token;; 
);;  
&&;;! #
variableLookup<< 
.<< 
ContainsKey<< 
(<< 
token<< $
)<<$ %
)<<% &
{== 
return>> 

variableLookup>> 
[>> 
token>> 
]>>  
;>>  !
}?? 
else@@ 
{AA 
returnBB 

TSQLVariablesBB 
.BB 
NoneBB 
;BB 
}CC 
}DD 
publicFF 
staticFF	 
boolFF 

IsVariableFF 
(FF  
stringGG 	
tokenGG
 
)GG 
{HH 
ifII 
(II 
!II 
stringII 
.II 
IsNullOrWhiteSpaceII !
(II! "
tokenII" '
)II' (
)II( )
{JJ 
returnKK 

variableLookupKK 
.KK 
ContainsKeyKK %
(KK% &
tokenKK& +
)KK+ ,
;KK, -
}LL 
elseMM 
{NN 
returnOO 

falseOO 
;OO 
}PP 
}QQ 
publicSS 
boolSS	 
InSS 
(SS 
paramsSS 
TSQLVariablesSS %
[SS% &
]SS& '
	variablesSS( 1
)SS1 2
{TT 
returnUU 	
	variablesVV 
!=VV 
nullVV 
&&VV 
	variablesWW 
.WW 
ContainsWW 
(WW 
thisWW 
)WW 
;WW 
}XX 
public\\ 
static\\	 
bool\\ 
operator\\ 
==\\  
(\\  !
TSQLVariables]] 
a]] 
,]] 
TSQLVariables^^ 
b^^ 
)^^ 
{__ 
if`` 
(`` 
Object`` 
.`` 
ReferenceEquals`` 
(`` 
a`` 
,``  
null``! %
)``% &
)``& '
{aa 
ifbb 
(bb 
Objectbb 
.bb 
ReferenceEqualsbb 
(bb 
bbb  
,bb  !
nullbb" &
)bb& '
)bb' (
{cc 
returnee 
trueee 
;ee 
}ff 
returnii 

falseii 
;ii 
}jj 
returnmm 	
amm
 
.mm 
Equalsmm 
(mm 
bmm 
)mm 
;mm 
}nn 
publicpp 
staticpp	 
boolpp 
operatorpp 
!=pp  
(pp  !
TSQLVariablesqq 
aqq 
,qq 
TSQLVariablesrr 
brr 
)rr 
{ss 
returntt 	
!tt
 
(tt 
att 
==tt 
btt 
)tt 
;tt 
}uu 
publicww 
boolww	 
Equalsww 
(ww 
TSQLVariablesww "
objww# &
)ww& '
{xx 
ifzz 
(zz 
Objectzz 
.zz 
ReferenceEqualszz 
(zz 
objzz !
,zz! "
nullzz# '
)zz' (
)zz( )
{{{ 
return|| 

false|| 
;|| 
}}} 
if
€€ 
(
€€ 
Object
€€ 
.
€€ 
ReferenceEquals
€€ 
(
€€ 
this
€€ "
,
€€" #
obj
€€$ '
)
€€' (
)
€€( )
{
 
return
‚‚ 

true
‚‚ 
;
‚‚ 
}
ƒƒ 
if
†† 
(
†† 
this
†† 
.
†† 
GetType
†† 
(
†† 
)
†† 
!=
†† 
obj
†† 
.
†† 
GetType
†† $
(
††$ %
)
††% &
)
††& '
return
‡‡ 

false
‡‡ 
;
‡‡ 
return
 	
Variable

 
==
 
obj
 
.
 
Variable
 "
;
" #
}
 
public
 
override
	 
bool
 
Equals
 
(
 
object
 $
obj
% (
)
( )
{
 
return
‘‘ 	
Equals
‘‘
 
(
‘‘ 
obj
‘‘ 
as
‘‘ 
TSQLVariables
‘‘ %
)
‘‘% &
;
‘‘& '
}
’’ 
public
”” 
override
””	 
int
”” 
GetHashCode
”” !
(
””! "
)
””" #
{
•• 
return
–– 	
Variable
––
 
.
–– 
GetHashCode
–– 
(
–– 
)
––  
;
––  !
}
—— 
}
 
}›› 