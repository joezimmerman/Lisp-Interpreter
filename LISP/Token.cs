using System;

namespace LISP{

    public enum TokenType{
          // Single-character tokens.
        LEFT_PAREN, RIGHT_PAREN,
        MINUS, PLUS, SLASH, STAR,

        // One or two character tokens.
        EQUAL, 
        GREATER, 
        LESS,

        IDENTIFIER,

        // Keywords.
        DEFINE, SET, CONS, COND, CAR, CDR, AND, OR, NOT, NUMBER, SYMBOL, LIST, NIL, EQ, IF,

        EOF

    }
    public class Token{
        public TokenType type;
        public readonly String lexeme;
        public readonly Object literal;
        public readonly int line;

        public Token(TokenType type, String lexeme, Object literal, int line){
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal; 
            this.line = line;
        }
        
        public string toString(){
            return type + " " + lexeme + " " + literal;
        }
    }
}