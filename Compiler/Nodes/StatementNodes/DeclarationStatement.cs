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
            var type = varType.GetVarType();
            if(SymbolsTable.types.ContainsKey(varID.idLexema))
            {
                throw new SemanticException("id cannot be named as an existing type!");
            }
            int rankCount = rankSpecifier.Count;
            if(rankCount > 0)
            {
                var arrType = new ArrayType(rankSpecifier[0]);
                ArrayType currType = arrType;
                for(int i = 1; i < rankCount; ++i)
                {
                    currType.type = new ArrayType(rankSpecifier[i]);
                    currType = (ArrayType)currType.type;
                }
                currType.type = type;

                SymbolsTable.vars[varID.idLexema] = arrType;
            }else
            {
                SymbolsTable.vars[varID.idLexema] = type;
            }
        }
    }
}