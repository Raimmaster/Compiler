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
            var inputString = new InputString(@"read a; read b; c = a + b; 
                d = (c / c) + a - (b * c) + (c - b); 
                print (a+b*c); print(c); print(d);");
            var lexer = new Lexer(inputString);
            
            var parser = new Parser(lexer);
            var code = parser.Parse();
            Console.Out.WriteLine("EXIT!!");
            System.Console.ReadKey();
        }
    }
}
