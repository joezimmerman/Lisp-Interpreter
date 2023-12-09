using System.Diagnostics;

namespace LISP{
    public class Interpreter : Expr.IVisitor<Object> {
        public Object evaluate(Expr expr){
            return expr.accept(this);
        }
        //^ Definetly would have to change this in some capactiy, I think?
        public Object visitBinaryExpr(Expr.Binary expr){
            Object left = evaluate(expr.left);
            Object right = evaluate(expr.right);
            switch(expr.oper.type){
                case TokenType.GREATER:
                    checkNumberOperands(expr.oper, left, right);
                    return (double)left > (double)right;
                case TokenType.LESS:
                    checkNumberOperands(expr.oper, left, right);
                    return (double)left < (double)right;
                case TokenType.MINUS:
                    checkNumberOperands(expr.oper, left, right);
                    return (double)left - (double)right;
                case TokenType.PLUS:
                    if(left is double && right is double) {return (double)left + (double)right;}
                    if(left is string && right is string) {return (string)left + (string)right;}
                    throw new RuntimeError(expr.oper, "Operands must be two numbers or two strings.");
                case TokenType.SLASH:
                    checkNumberOperands(expr.oper, left, right);
                    return (double)left / (double)right;
                case TokenType.STAR:
                    checkNumberOperands(expr.oper, left, right);
                    return (double)left * (double)right;
                case TokenType.EQUAL: 
                    //maybe check to see if the inputs are correct.
                    return left == right;
                case TokenType.CONS:
                    //would have to do sometihng with lists, not sure remotely how to do it.
                    return null!;
                case TokenType.AND: 
                    //if (expr1) && (expr2) return True; 
                    //else retrun False;
                    return null!;
                case TokenType.OR:
                    //if (expr1) || (expr2) return True; 
                    //else retrun False;
                    return null!;
            }
            return null!;
        }
        public Object visitUnaryExpr(Expr.Unary expr){
            Object right = evaluate(expr.right);
            switch (expr.oper.type){
                case TokenType.CAR:
                    //return the first element of a list
                    return null!;
                case TokenType.CDR:
                    //return everything except the first element of a list
                    return null!;
                case TokenType.NUMBER:
                    //return isDigit(expr) <-- would have to edit the isDigit function
                    return null!;
                case TokenType.SYMBOL:
                    //return isAlpha(expr) <-- same thing as isDigit(), I would have to edit it
                    return null!;
                case TokenType.LIST:
                    //would have to create an isList function that checks if the variable is a list
                    return null!;
                case TokenType.NIL:
                    //return expr == null;
                    return null!;
            }
            return null!;
        }

        //THIS IS WHERE I WOULD CREATE A visit.... FOR Function calls and ifs

        private void checkNumberOperands(Token oper, Object left, Object right) {
            if (left is double && right is double) return;
            throw new RuntimeError(oper, "Operands must be numbers.");
        }
        private bool isEqual(Object a, Object b){
            if (a == null && b == null) return true;
            if (a == null) return false;
            return a.Equals(b);
        }

        //Maybe keep the interept function?????
       
        private String stringify(Object obj) {
            if (obj == null) return "nil";
            if (obj is Double) {
                String text = obj.ToString();
                if (text.EndsWith(".0")) {
                    text = text.Substring(0, text.Length - 2);  //  Double check string not cutting off chars
                }
                return text;
            }
            return obj.ToString();
        }

    }

}