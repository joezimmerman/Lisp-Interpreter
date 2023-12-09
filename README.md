# Lisp-Interpreter

If you have not already looked at my project report, please do so, here is the link for it: https://docs.google.com/document/d/18iPiXVlDqWvjRcuAJGO1WV9SyCZjNP4TxutA8EMOCEE/edit
<br>
<br>

This proved to be an immense challenge for me, and I wasn’t even close to completing it. I would like for you to consider this project as an attempt and to look at my code and the methodology behind it. I tried to implement a Lisp intepreter, in C#, by editing the Lox interpreter that I already created.

Here is my approach:
1. Update tokens to Lisp tokens <br>
2. Edit the scanner file by removing tokens that aren't being used in Lisp and adding Tokens new tokens that are used in Lisp. <br>
Correct me if I am wrong, but in Lisp everything can be written as an expression, mostly binary and unary expressions. Binary expressions include: +, -, *, /, = , <, >, CONS, AND?, OR, while unary expressions are everything except if and function declarations. <br>
3. Update the Expr class to handle those expressions <br>
4. Update the Parser class to handle only the expressions <br>
5. Update the interpreter to make sure you’re only interpreting expressions <br>
6. Change the main to incorporate those changes <br>

I’m not really sure if this is the best approach, but to be completely honest I went with it anyways. What really got to me was trying to change the Expr, Parser, and Interpreter files. For me it was hard to figure out exactly what I needed, what I needed to delete, and what I needed to keep. I tried my hardest to complete this task, but I just wasn’t able to complete it. I think there is some merit to my submission, as at least I tried to get it done. Hopefully you can honor this submission for partial credit, even if it is failing. 
