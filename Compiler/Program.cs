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
            var input = @"read a; read b; c = a + b; 
                d = b*c; print c; print d;";
            
            //var input = @" print(1+2);";
            

            var inputString = new InputString(input);
            var lexer = new Lexer(inputString);
            
            var parser = new Parser(lexer);
            var code = parser.Parse();
            foreach(var production in code)
            {
                production.Interpret();
            }
            Console.Out.WriteLine("EXIT!!");
            System.Console.ReadKey();
        }
    }
}
