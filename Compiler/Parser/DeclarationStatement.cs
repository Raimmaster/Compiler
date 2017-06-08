using System;
using System.Collections.Generic;

namespace Compiler
{
    public class DeclarationStatement : StatementNode
    {
        public VarTypeNode varType;
        public IDNode varID;
        public List<int> rankSpecifier;

        public DeclarationStatement(VarTypeNode varType, IDNode varID, List<int> rankSpecifier)
        {
            this.varType = varType;
            this.varID = varID;
            this.rankSpecifier = rankSpecifier;
        }

        public override void Interpret()
        {
            //throw new NotImplementedException();
        }

        public override void ValidateSemantic()
        {
            //throw new NotImplementedException();
        }
    }
}