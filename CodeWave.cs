
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF            =  0, // (EOF)
        SYMBOL_ERROR          =  1, // (Error)
        SYMBOL_WHITESPACE     =  2, // Whitespace
        SYMBOL_MINUS          =  3, // '-'
        SYMBOL_MINUSMINUS     =  4, // '--'
        SYMBOL_EXCLAMEQ       =  5, // '!='
        SYMBOL_EXCLAMFALSE    =  6, // '!false'
        SYMBOL_LPAREN         =  7, // '('
        SYMBOL_RPAREN         =  8, // ')'
        SYMBOL_TIMES          =  9, // '*'
        SYMBOL_TIMESEQ        = 10, // '*='
        SYMBOL_DIV            = 11, // '/'
        SYMBOL_DIVEQ          = 12, // '/='
        SYMBOL_COLON          = 13, // ':'
        SYMBOL_SEMI           = 14, // ';'
        SYMBOL_LBRACE         = 15, // '{'
        SYMBOL_RBRACE         = 16, // '}'
        SYMBOL_PLUS           = 17, // '+'
        SYMBOL_PLUSPLUS       = 18, // '++'
        SYMBOL_PLUSEQ         = 19, // '+='
        SYMBOL_LT             = 20, // '<'
        SYMBOL_LTEQ           = 21, // '<='
        SYMBOL_EQ             = 22, // '='
        SYMBOL_MINUSEQ        = 23, // '-='
        SYMBOL_GT             = 24, // '>'
        SYMBOL_GTEQ           = 25, // '>='
        SYMBOL_CASE           = 26, // case
        SYMBOL_CODE           = 27, // Code
        SYMBOL_ELSIF          = 28, // elsif
        SYMBOL_END            = 29, // end
        SYMBOL_ENDELSIF       = 30, // endelsif
        SYMBOL_ENDFOR         = 31, // endfor
        SYMBOL_ENDIF          = 32, // endif
        SYMBOL_ENDSWITCH      = 33, // endswitch
        SYMBOL_ENDWHILE       = 34, // endwhile
        SYMBOL_FLOAT          = 35, // float
        SYMBOL_FOR            = 36, // for
        SYMBOL_IDENTIFIER     = 37, // identifier
        SYMBOL_IF             = 38, // if
        SYMBOL_INT            = 39, // int
        SYMBOL_SWITCH         = 40, // switch
        SYMBOL_TRUE           = 41, // true
        SYMBOL_WAVE           = 42, // Wave
        SYMBOL_WHILE          = 43, // while
        SYMBOL_ADD            = 44, // <add>
        SYMBOL_ASSIGNMENT     = 45, // <assignment>
        SYMBOL_ASSIGNMENT_OP  = 46, // <assignment_op>
        SYMBOL_CONSTANT       = 47, // <constant>
        SYMBOL_DECLARATION    = 48, // <declaration>
        SYMBOL_DIV2           = 49, // <div>
        SYMBOL_EXPRESSION     = 50, // <expression>
        SYMBOL_FOR_ASSIGNMENT = 51, // <for_assignment>
        SYMBOL_FOR_INC_DEC    = 52, // <for_inc_dec>
        SYMBOL_MUL            = 53, // <mul>
        SYMBOL_PROGRAM        = 54, // <program>
        SYMBOL_STATEMENT_LIST = 55, // <statement_list>
        SYMBOL_SUB            = 56  // <sub>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_CODE_LBRACE_RBRACE_WAVE                                                          =  0, // <program> ::= Code '{' <constant> '}' Wave
        RULE_CONSTANT                                                                                 =  1, // <constant> ::= <assignment> <statement_list>
        RULE_CONSTANT2                                                                                =  2, // <constant> ::= <declaration> <statement_list>
        RULE_CONSTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE_ENDFOR                                =  3, // <constant> ::= for '(' <for_assignment> ';' <expression> ';' <for_inc_dec> ')' '{' <statement_list> '}' endfor
        RULE_CONSTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE_ENDWHILE                                      =  4, // <constant> ::= while '(' <expression> ')' '{' <statement_list> '}' endwhile
        RULE_CONSTANT_SWITCH_LPAREN_IDENTIFIER_RPAREN_LBRACE_CASE_INT_COLON_RBRACE_ENDSWITCH          =  5, // <constant> ::= switch '(' identifier ')' '{' case int ':' <statement_list> '}' endswitch
        RULE_CONSTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF                                            =  6, // <constant> ::= if '(' <expression> ')' '{' <statement_list> '}' endif
        RULE_CONSTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ENDELSIF =  7, // <constant> ::= if '(' <expression> ')' '{' <statement_list> '}' endif elsif '(' <expression> ')' '{' <statement_list> '}' endelsif
        RULE_ASSIGNMENT_IDENTIFIER_EQ_IDENTIFIER_INT_END                                              =  8, // <assignment> ::= identifier '=' identifier <assignment_op> int end
        RULE_ASSIGNMENT_OP_PLUSEQ                                                                     =  9, // <assignment_op> ::= '+='
        RULE_ASSIGNMENT_OP_MINUSEQ                                                                    = 10, // <assignment_op> ::= '-='
        RULE_ASSIGNMENT_OP_TIMESEQ                                                                    = 11, // <assignment_op> ::= '*='
        RULE_ASSIGNMENT_OP_DIVEQ                                                                      = 12, // <assignment_op> ::= '/='
        RULE_DECLARATION_IDENTIFIER_EQ_INT_END                                                        = 13, // <declaration> ::= identifier '=' int end
        RULE_DECLARATION_IDENTIFIER_EQ_FLOAT_END                                                      = 14, // <declaration> ::= identifier '=' float end
        RULE_FOR_ASSIGNMENT_IDENTIFIER_EQ_INT                                                         = 15, // <for_assignment> ::= identifier '=' int
        RULE_FOR_ASSIGNMENT_IDENTIFIER                                                                = 16, // <for_assignment> ::= identifier
        RULE_FOR_INC_DEC_IDENTIFIER_PLUSPLUS                                                          = 17, // <for_inc_dec> ::= identifier '++'
        RULE_FOR_INC_DEC_IDENTIFIER_MINUSMINUS                                                        = 18, // <for_inc_dec> ::= identifier '--'
        RULE_FOR_INC_DEC_PLUSPLUS_IDENTIFIER                                                          = 19, // <for_inc_dec> ::= '++' identifier
        RULE_FOR_INC_DEC_MINUSMINUS_IDENTIFIER                                                        = 20, // <for_inc_dec> ::= '--' identifier
        RULE_FOR_INC_DEC_IDENTIFIER_PLUSEQ_INT                                                        = 21, // <for_inc_dec> ::= identifier '+=' int
        RULE_FOR_INC_DEC_IDENTIFIER_MINUSEQ_INT                                                       = 22, // <for_inc_dec> ::= identifier '-=' int
        RULE_FOR_INC_DEC_IDENTIFIER_TIMESEQ_INT                                                       = 23, // <for_inc_dec> ::= identifier '*=' int
        RULE_FOR_INC_DEC_IDENTIFIER_DIVEQ_INT                                                         = 24, // <for_inc_dec> ::= identifier '/=' int
        RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_PLUS_INT                                            = 25, // <for_inc_dec> ::= identifier '=' identifier '+' int
        RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_MINUS_INT                                           = 26, // <for_inc_dec> ::= identifier '=' identifier '-' int
        RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_TIMES_INT                                           = 27, // <for_inc_dec> ::= identifier '=' identifier '*' int
        RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_DIV_INT                                             = 28, // <for_inc_dec> ::= identifier '=' identifier '/' int
        RULE_EXPRESSION_IDENTIFIER_LTEQ_INT                                                           = 29, // <expression> ::= identifier '<=' int
        RULE_EXPRESSION_IDENTIFIER_GTEQ_INT                                                           = 30, // <expression> ::= identifier '>=' int
        RULE_EXPRESSION_IDENTIFIER_GT_INT                                                             = 31, // <expression> ::= identifier '>' int
        RULE_EXPRESSION_IDENTIFIER_LT_INT                                                             = 32, // <expression> ::= identifier '<' int
        RULE_EXPRESSION_IDENTIFIER_EXCLAMEQ_INT                                                       = 33, // <expression> ::= identifier '!=' int
        RULE_EXPRESSION_TRUE                                                                          = 34, // <expression> ::= true
        RULE_EXPRESSION_EXCLAMFALSE                                                                   = 35, // <expression> ::= '!false'
        RULE_STATEMENT_LIST                                                                           = 36, // <statement_list> ::= <constant> <statement_list>
        RULE_STATEMENT_LIST2                                                                          = 37, // <statement_list> ::= <add> <statement_list>
        RULE_STATEMENT_LIST3                                                                          = 38, // <statement_list> ::= <sub> <statement_list>
        RULE_STATEMENT_LIST4                                                                          = 39, // <statement_list> ::= <div> <statement_list>
        RULE_STATEMENT_LIST5                                                                          = 40, // <statement_list> ::= <mul> <statement_list>
        RULE_STATEMENT_LIST6                                                                          = 41, // <statement_list> ::= 
        RULE_ADD_IDENTIFIER_EQ_IDENTIFIER_PLUS_INT_END                                                = 42, // <add> ::= identifier '=' identifier '+' int end
        RULE_ADD_IDENTIFIER_PLUSEQ_INT_END                                                            = 43, // <add> ::= identifier '+=' int end
        RULE_ADD_IDENTIFIER_EQ_IDENTIFIER_PLUS_IDENTIFIER_END                                         = 44, // <add> ::= identifier '=' identifier '+' identifier end
        RULE_SUB_IDENTIFIER_EQ_IDENTIFIER_MINUS_INT_END                                               = 45, // <sub> ::= identifier '=' identifier '-' int end
        RULE_SUB_IDENTIFIER_MINUSEQ_INT_END                                                           = 46, // <sub> ::= identifier '-=' int end
        RULE_SUB_IDENTIFIER_EQ_IDENTIFIER_MINUS_IDENTIFIER_END                                        = 47, // <sub> ::= identifier '=' identifier '-' identifier end
        RULE_DIV_IDENTIFIER_EQ_IDENTIFIER_DIV_INT_END                                                 = 48, // <div> ::= identifier '=' identifier '/' int end
        RULE_DIV_IDENTIFIER_DIVEQ_INT_END                                                             = 49, // <div> ::= identifier '/=' int end
        RULE_DIV_IDENTIFIER_EQ_IDENTIFIER_DIV_IDENTIFIER_END                                          = 50, // <div> ::= identifier '=' identifier '/' identifier end
        RULE_MUL_IDENTIFIER_EQ_IDENTIFIER_TIMES_INT_END                                               = 51, // <mul> ::= identifier '=' identifier '*' int end
        RULE_MUL_IDENTIFIER_TIMESEQ_INT_END                                                           = 52, // <mul> ::= identifier '*=' int end
        RULE_MUL_IDENTIFIER_EQ_IDENTIFIER_TIMES_IDENTIFIER_END                                        = 53  // <mul> ::= identifier '=' identifier '*' identifier end
    };

    public class MyParser
    {
        private LALRParser parser;

        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMFALSE :
                //'!false'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESEQ :
                //'*='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIVEQ :
                //'/='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSEQ :
                //'+='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSEQ :
                //'-='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CODE :
                //Code
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSIF :
                //elsif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //end
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDELSIF :
                //endelsif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDFOR :
                //endfor
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDIF :
                //endif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDSWITCH :
                //endswitch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ENDWHILE :
                //endwhile
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //true
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WAVE :
                //Wave
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADD :
                //<add>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT_OP :
                //<assignment_op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONSTANT :
                //<constant>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLARATION :
                //<declaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV2 :
                //<div>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_ASSIGNMENT :
                //<for_assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_INC_DEC :
                //<for_inc_dec>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MUL :
                //<mul>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT_LIST :
                //<statement_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SUB :
                //<sub>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_CODE_LBRACE_RBRACE_WAVE :
                //<program> ::= Code '{' <constant> '}' Wave
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONSTANT :
                //<constant> ::= <assignment> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONSTANT2 :
                //<constant> ::= <declaration> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONSTANT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE_ENDFOR :
                //<constant> ::= for '(' <for_assignment> ';' <expression> ';' <for_inc_dec> ')' '{' <statement_list> '}' endfor
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONSTANT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE_ENDWHILE :
                //<constant> ::= while '(' <expression> ')' '{' <statement_list> '}' endwhile
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONSTANT_SWITCH_LPAREN_IDENTIFIER_RPAREN_LBRACE_CASE_INT_COLON_RBRACE_ENDSWITCH :
                //<constant> ::= switch '(' identifier ')' '{' case int ':' <statement_list> '}' endswitch
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONSTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF :
                //<constant> ::= if '(' <expression> ')' '{' <statement_list> '}' endif
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONSTANT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ENDIF_ELSIF_LPAREN_RPAREN_LBRACE_RBRACE_ENDELSIF :
                //<constant> ::= if '(' <expression> ')' '{' <statement_list> '}' endif elsif '(' <expression> ')' '{' <statement_list> '}' endelsif
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_IDENTIFIER_EQ_IDENTIFIER_INT_END :
                //<assignment> ::= identifier '=' identifier <assignment_op> int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_OP_PLUSEQ :
                //<assignment_op> ::= '+='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_OP_MINUSEQ :
                //<assignment_op> ::= '-='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_OP_TIMESEQ :
                //<assignment_op> ::= '*='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_OP_DIVEQ :
                //<assignment_op> ::= '/='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_IDENTIFIER_EQ_INT_END :
                //<declaration> ::= identifier '=' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DECLARATION_IDENTIFIER_EQ_FLOAT_END :
                //<declaration> ::= identifier '=' float end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_ASSIGNMENT_IDENTIFIER_EQ_INT :
                //<for_assignment> ::= identifier '=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_ASSIGNMENT_IDENTIFIER :
                //<for_assignment> ::= identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_PLUSPLUS :
                //<for_inc_dec> ::= identifier '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_MINUSMINUS :
                //<for_inc_dec> ::= identifier '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_PLUSPLUS_IDENTIFIER :
                //<for_inc_dec> ::= '++' identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_MINUSMINUS_IDENTIFIER :
                //<for_inc_dec> ::= '--' identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_PLUSEQ_INT :
                //<for_inc_dec> ::= identifier '+=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_MINUSEQ_INT :
                //<for_inc_dec> ::= identifier '-=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_TIMESEQ_INT :
                //<for_inc_dec> ::= identifier '*=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_DIVEQ_INT :
                //<for_inc_dec> ::= identifier '/=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_PLUS_INT :
                //<for_inc_dec> ::= identifier '=' identifier '+' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_MINUS_INT :
                //<for_inc_dec> ::= identifier '=' identifier '-' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_TIMES_INT :
                //<for_inc_dec> ::= identifier '=' identifier '*' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_INC_DEC_IDENTIFIER_EQ_IDENTIFIER_DIV_INT :
                //<for_inc_dec> ::= identifier '=' identifier '/' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_IDENTIFIER_LTEQ_INT :
                //<expression> ::= identifier '<=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_IDENTIFIER_GTEQ_INT :
                //<expression> ::= identifier '>=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_IDENTIFIER_GT_INT :
                //<expression> ::= identifier '>' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_IDENTIFIER_LT_INT :
                //<expression> ::= identifier '<' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_IDENTIFIER_EXCLAMEQ_INT :
                //<expression> ::= identifier '!=' int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_TRUE :
                //<expression> ::= true
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_EXCLAMFALSE :
                //<expression> ::= '!false'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST :
                //<statement_list> ::= <constant> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST2 :
                //<statement_list> ::= <add> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST3 :
                //<statement_list> ::= <sub> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST4 :
                //<statement_list> ::= <div> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST5 :
                //<statement_list> ::= <mul> <statement_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_LIST6 :
                //<statement_list> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_IDENTIFIER_EQ_IDENTIFIER_PLUS_INT_END :
                //<add> ::= identifier '=' identifier '+' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_IDENTIFIER_PLUSEQ_INT_END :
                //<add> ::= identifier '+=' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADD_IDENTIFIER_EQ_IDENTIFIER_PLUS_IDENTIFIER_END :
                //<add> ::= identifier '=' identifier '+' identifier end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_IDENTIFIER_EQ_IDENTIFIER_MINUS_INT_END :
                //<sub> ::= identifier '=' identifier '-' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_IDENTIFIER_MINUSEQ_INT_END :
                //<sub> ::= identifier '-=' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SUB_IDENTIFIER_EQ_IDENTIFIER_MINUS_IDENTIFIER_END :
                //<sub> ::= identifier '=' identifier '-' identifier end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_IDENTIFIER_EQ_IDENTIFIER_DIV_INT_END :
                //<div> ::= identifier '=' identifier '/' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_IDENTIFIER_DIVEQ_INT_END :
                //<div> ::= identifier '/=' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIV_IDENTIFIER_EQ_IDENTIFIER_DIV_IDENTIFIER_END :
                //<div> ::= identifier '=' identifier '/' identifier end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_IDENTIFIER_EQ_IDENTIFIER_TIMES_INT_END :
                //<mul> ::= identifier '=' identifier '*' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_IDENTIFIER_TIMESEQ_INT_END :
                //<mul> ::= identifier '*=' int end
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MUL_IDENTIFIER_EQ_IDENTIFIER_TIMES_IDENTIFIER_END :
                //<mul> ::= identifier '=' identifier '*' identifier end
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
        }

    }
}
