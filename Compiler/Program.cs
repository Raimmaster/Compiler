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
            decl int z;
            struct fecha
                decl int mes;
                decl int dia;
                decl int anio;
            end

            decl fecha x;
            x.dia = z;
            y[x.dia] = 10;
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
