using System;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace LISP{

    //Pretty confident that this is what a scanner would look like for a lisp interpreter
    //to be honest, it barely changes from lox to lisp.
    //You're just editing cases in the switch statements.
    public class Scanner{
        public readonly String source;
        public readonly List<Token> tokens = new List<Token>();
        private int start = 0; 
        private int current = 0; 
        private int line = 1;

        public Scanner(String source){
            this.source = source;
        }

        public List<Token> scanTokens(){
            while(!isAtEnd()){
                start = current;
                scanToken();
            }
            tokens.Add(new Token(TokenType.EOF, "", null!, line));
            return tokens;
        }
        private void scanToken(){
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case '*': addToken(TokenType.STAR); break;
                case '=': addToken(TokenType.EQUAL); break;
                case '/': 
                    if (match('/')){
                        // A comment goes until the end of the line.
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else {addToken(TokenType.SLASH);} break;
                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;
                case '\n':
                    line++;
                    break;
                //not sure if I need to keep any of the code below this comment
                //case '"': lookString(); break; //can not use string()
                case 'o': 
                    if(match('r')){addToken(TokenType.OR);} break;
                default:
                    if(isDigit(c)){ number(); }
                    else if (isAlpha(c)) {}
                    else{Lisp.error(line, "Unexpected character.");}
                    break;
            } 
        }
        private bool match(char expected){
            if(isAtEnd()) return false;
            if(source[current] != expected) return false; 
            current++;
            return true; 
        }

        private char peek(){
            if(isAtEnd()) return '\0';
            return source[current];
        }

        private char advance(){
            return source[current++];
        }
        
        private void addToken(TokenType type){
            addToken(type, null!);
        }
        private void addToken(TokenType type, object literal){
            string text = source.Substring(start, current - start);
            tokens.Add(new Token(type, text, literal, line));
        }

        private bool isAtEnd(){
            return current >= source.Length;
        }

        private bool isDigit(char c){
            return c >= '0' && c <= '9';
        }

        private void number(){
            while (isDigit(peek())) advance();
            if (peek() == '.' && isDigit(peekNext())) {
                // Consume the "."
                advance();
                while (isDigit(peek())) advance();
            }

            addToken(TokenType.NUMBER, double.Parse(source.Substring(start, current - start)));

        }

        private char peekNext(){
            if(current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        private bool isAlpha(char c){
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
        }

        private bool isAlhpaNumeric(char c){
            return isAlpha(c) || isDigit(c);
        }

        private static readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType> {
            { "and?",   TokenType.AND },
            { "define",   TokenType.DEFINE},
            { "set", TokenType.SET},
            { "cons", TokenType.CONS},
            { "car", TokenType.CAR},
            { "cdr", TokenType.CDR},
            { "not?", TokenType.NOT},
            { "number?", TokenType.NUMBER},
            { "symbol?", TokenType.SYMBOL},
            { "list?", TokenType.LIST},
            { "eq?", TokenType.EQ},
            //{ "false",  TokenType.FALSE},
            { "if",     TokenType.IF},
            { "nil?",   TokenType.NIL },
            { "or",     TokenType.OR },
            //{ "true",   TokenType.TRUE },
            // SET, CONS, COND, CAR, CDR, AND, OR, NOT, NUMBER, SYMBOL, LIST, NIL, EQ, IF,
        };
    }
}