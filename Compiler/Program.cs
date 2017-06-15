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
            decl int y;
            decl bool b;
            y = 9;
            decl int z;
            z = (y + 5) * 9 - 3 / ( y + 4);
            b = false;
            decl bool m;
            m = true;
            print m;
            print b;
            read z;

            ";
            
            SymbolsTable.InitTypes();
            var inputString = new InputString(input);
            var lexer = new Lexer(inputString);
            
            var parser = new Parser(lexer);
            var code = parser.Parse();
            foreach(var production in code)
            {
                Console.Write(production.GenerateCode());
            }
            Console.WriteLine("Validated!");
            System.Console.ReadKey();
        }
    }
}
