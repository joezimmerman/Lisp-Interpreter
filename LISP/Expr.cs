namespace LISP {
    public abstract class Expr {
        public interface IVisitor<R> {
            R visitBinaryExpr(Binary expr);
        }
        public class Binary : Expr {
            public Binary(Token oper, Expr left, Expr right) {
                this.oper = oper;
                this.left = left;
                this.right = right;
            }
            public override R accept<R>(IVisitor<R> visitor) {
                return visitor.visitBinaryExpr(this);
            }
            public readonly Token oper;
            public readonly Expr left;
            public readonly Expr right;
        }
        public class Unary : Expr {
            public Unary(Token oper, Expr right) {
                this.oper = oper;
                this.right = right;
            }

            public override R accept<R>(IVisitor<R> visitor) {
                return visitor.visitUnaryExpr(this);
            }

            public readonly Token oper;
            public readonly Expr right;
        }
        public abstract R accept<R>(IVisitor<R> visitor);
    }
}
