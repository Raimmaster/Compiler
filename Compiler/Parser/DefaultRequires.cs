using System;

namespace Compiler
{
    public class DefaultRequires : StatementNode
    {
        public override string GenerateCode()
        {
            var requires = @"let readlineSync = require('readline-sync');\n";
            return requires;
        }

        public override void Interpret()
        {
            throw new NotImplementedException();
        }

        public override void ValidateSemantic()
        {
            throw new NotImplementedException();
        }
    }
}