﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputString = new InputString(@"print IDTEST    +



IDDUE   +  TRE");

            var lexer = new Lexer(inputString);

            Token token = lexer.GetNextToken();

            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = lexer.GetNextToken();
            }

            System.Console.Out.WriteLine(token);
            
            //Symbol currentSymbol = inputString.GetNextSymbol();

        
            /*while (currentSymbol.character != '\0')
            {
                Console.WriteLine("Row: " + currentSymbol.rowCount +
                    " Column: " + currentSymbol.colCount + 
                    " Char: " + currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            }*/

            System.Console.ReadKey();
        }
    }
}
