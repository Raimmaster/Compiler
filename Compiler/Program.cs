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
            //var input = @"read a; read b; c = a + b; 
            //    d = (c / c) + a - (b * c) + (c - b); 
             //   print (a+b*c); print(c); print(d); print(a^a^a);";
            
            var input = @"print( (6*2)^2 + 3 - (5^3) + 10);";

            var inputString = new InputString(input);
            var lexer = new Lexer(inputString);
            
            var parser = new Parser(lexer);
            var code = parser.Parse();
            foreach(var production in code)
            {
                production.Evaluate();
            }
            Console.Out.WriteLine("EXIT!!");
            System.Console.ReadKey();
        }
    }
}
