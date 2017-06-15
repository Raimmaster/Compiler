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

        public override string GenerateCode()
        {
            var code = new System.Text.StringBuilder("let " + varID.GenerateCode().Code);
            if(rankSpecifier.Count > 0)
            {
                /*for(int i = 0; i < rankSpecifier.Count; ++i)
                {
                    code.Append("[]");
                }
                code.Append(" = new ");
                code.Append(varType.typeName);
                for(int i = 0; i < rankSpecifier.Count; ++i)
                {
                    code.Append('[');
                    code.Append(rankSpecifier[i]);
                    code.Append(']');
                    //let a = new Type[1][3][4];
                }*/
            }
            code.Append(";\n");

            return code.ToString();
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