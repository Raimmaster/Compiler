using System;
using System.Collections.Generic;
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
            var input = @"
            decl int y[3];
            decl int A[5][4];
            decl bool B[2][4][3];
            decl int z;
            ";
            
            SymbolsTable.InitTypes();
            var inputString = new InputString(input);
            var lexer = new Lexer(inputString);
            
            var parser = new Parser(lexer);
            var code = parser.Parse();
            foreach(var production in code)
            {
                production.ValidateSemantic();
            }
            Console.WriteLine("Validated!");
            System.Console.ReadKey();
        }
    }
}
