using System;
using System.Collections.Generic;

namespace Compiler
{
    public class StructNode : StatementNode
    {
        public Token id;
        public List<DeclarationStatement> attributeList;

        public StructNode(Token id, List<DeclarationStatement> attributeList)
        {
            this.id = id;
            this.attributeList = attributeList;
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