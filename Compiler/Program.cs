using System;
using System.Text;
using CompilerLibrary;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputString = new InputString(@"print IDTEST = 5595 + ( TRE ) - QUATTRO * UNO//Hola


IDDUE/ALGO;");
            var lexer = new Lexer(inputString);

            Token token = lexer.GetNextToken();

            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = lexer.GetNextToken();
            }

            System.Console.Out.WriteLine(token);
            
            System.Console.ReadKey();
        }
    }
}
