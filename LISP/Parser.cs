using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LISP {
        public class ParseError : Exception { public ParseError(string message = null!) : base(message) { } }    
        public class Parser{
        private readonly List<Token> tokens; 
        private int current = 0;
        public Parser(List<Token> tokens){
            this.tokens = tokens;
        }
        //I WOULD SAY THIS IS WHERE I STRUGGLED THE MOST. 
        //TRYING TO FIGURE OUT WHAT TO ADD TO CORRECTLY GET TO THE INTERPRETER
        //NOT SURE WHAT TO KEEP FROM LOX, WHAT TO ADD, AND WHAT TO CHANGE
        //I DO KNOW WHAT IT WOULD TAKE A LOT OF WORK TO COMPLETE IT
        //IF I'M BEING HONEST, THE CODE BELOW IS GARBAGE, JUST SOMETHING I TRIED 
        //TRIED TO MAINLY GET MATH OPERATIONS TO WORK AND THAT WAS A COMPLETE FAIL
        private Expr expression() {
            if (match(TokenType.LEFT_PAREN)){
                consume(TokenType.LEFT_PAREN, "Expect '(' after expression.");
                Token oper = consume(TokenType.IDENTIFIER, "Expect operator.");
                string operatorLexeme = oper.lexeme;
                Expr left = expression();
                Expr right = expression();
                consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
                return new Expr.Binary(oper, left, right);
            }
  
            else
            {
                return null!;
            }
        }

        /*
        private Expr equality(){
            Expr expr = comparison();
            while (match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL)){
                Token oper = previous(); //might need an @ before the operator, could be operator instead of oper?
                Expr right = comparison();
                expr = new Expr.Binary(expr, oper, right); //might need an @ in front of oper
            }
            return expr;
        }
        */  
        private bool match(params TokenType[] types){
            foreach (var type in types){
                if(check(type)){advance(); return true;}
            }
            return false;
        }

        private bool check(TokenType type){
            if(isAtEnd()) return false;
            return peek().type == type;
        }

        private Token advance(){
            if (!isAtEnd()) current++;
            return previous();
        }
        private bool isAtEnd(){
            return peek().type == TokenType.EOF;
        }
        private Token peek(){
            return tokens[current];
        }

        private Token previous(){
            return tokens[current-1];
        }
        
        /*

        private Expr comparison(){
            Expr expr = term();
            while(match(TokenType.GREATER, TokenType.LESS)){
                Token oper = previous();
                Expr right = term();
                expr = new Expr.Binary(oper, expr, right);
            }
            return expr;
        }

        private Expr term(){
            Expr expr = factor();
            while(match(TokenType.MINUS, TokenType.PLUS)){
                Token oper = previous();
                Expr right = factor();
                expr = new Expr.Binary(oper, expr, right);
            }
            return expr;
        }
        private Expr factor(){
            Expr expr = unary();
            while(match(TokenType.SLASH, TokenType.STAR)){
                Token oper = previous();
                Expr right = unary();
                expr = new Expr.Binary(oper, expr, right);
            }
            return expr;
        }

        private Expr unary(){
            if(match(TokenType.MINUS)){
                Token oper = previous();
                Expr right = unary();
                return new Expr.Unary(oper, right);
            }
            return null!;
        }
        */
        public Token consume(TokenType type, string message){
            if(check(type)) return advance();
            throw error(peek(), message);
        }

        public ParseError error(Token token, string message){
            Lisp.error(token, message);
            return new ParseError();
        }
        /*
        public void synchronize(){
            advance();
            while(!isAtEnd()){
                if(previous().type == TokenType.SEMICOLON) return;
                switch (peek().type){
                    case TokenType.CLASS:
                    case TokenType.FUN:
                    case TokenType.VAR:
                    case TokenType.FOR:
                    case TokenType.IF:
                    case TokenType.WHILE:
                    case TokenType.PRINT:
                    case TokenType.RETURN:
                        return;
                }
                advance();
            }
        }
        */

        public Expr parse() {
            try {
            return expression();
            } catch (ParseError error) {
            return null!;
            }
        }
    }
}