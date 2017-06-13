﻿using System;
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
            var input = @"decl int A; 
                decl bool B; 
                decl int arr[10]; 
                struct uno
                    decl int x;
                    decl bool f;
                    decl int y;
                end
                A = 10; 
                B = false;
                uno.x = 4;
                uno.y = 15;
                arr[0] = uno.x + uno.y;
                arr[uno.x][2].z = uno.y + 2;
            ";
            
            SymbolsTable.InitTypes();
            var inputString = new InputString(input);
            var lexer = new Lexer(inputString);
            
            var parser = new Parser(lexer);
            var code = parser.Parse();
            foreach(var production in code)
            {
                //production.ValidateSemantic();
            }
            Console.WriteLine("Validated!");
            System.Console.ReadKey();
        }
    }
}
