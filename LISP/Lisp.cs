using System;
using System.Runtime.InteropServices;
//Consile.WriteLine to print
namespace LISP{
    public class Lisp {
        private static readonly Interpreter interpreter = new Interpreter();
        static bool hadError = false;
        static bool hadRuntimeError = false;
        public static void Main(String[] args) {
            Console.WriteLine("Welcome to my intepreter");
            if (args.Length > 1) {
                //System.exit(64); 
                } else if (args.Length == 1) {
                    runFile(args[0]);
                } else {
                    runPrompt();
                }
            }
        private static void runFile(String path){
            byte[] bytes = File.ReadAllBytes(Path.GetFullPath(path));
            string str = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            run(str);
        }

        private static void runPrompt(){
            //function that creates the REPL loop
            while(true){
                string line = Console.ReadLine() ?? string.Empty;
                if(line == null) break;
                run(line);
                hadError = false;
            }
        }

        //The run function is another area i struggled in 
        //couldnt figure out how the exact logic behind making it work
        //always ran into areas with the interpreter not running correctly
        private static void run(string source){
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();
            Parser parser = new Parser(tokens);
            Expr expression = parser.parse();
            if (hadError) return;
            Console.WriteLine("Getting here");
            //I know for a fact that the next line isn't even close to right. 
            interpreter.evaluate(expression);
        }

        public static void error(int line, string message){
            report(line, "", message);
        }
        private static void report(int line, string where, string message){
            Console.Error.WriteLine($"[line {line}] Error{where}: {message}");
            hadError = true;
        }
        public static void error(Token token, string message){
            if(token.type == TokenType.EOF){report(token.line, "at end", message);}
            else{report(token.line, $" at '{token.lexeme}'", message);}
        }
        public static void runtimeError(RuntimeError error){
            Console.Error.WriteLine($"{error.Message}\n[line {error.Token.line}]");
            hadRuntimeError = true;
        }

    }
}