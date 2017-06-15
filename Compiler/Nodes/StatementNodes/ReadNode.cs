using System;

namespace Compiler
{
    public class ReadNode : StatementNode
    {
        private IDNode iDNode;
        public ReadNode(IDNode iDNode)
        {
            this.iDNode = iDNode;
        }

        public override string GenerateCode()
        {
            var code = iDNode.idLexema + " = readlineSync.question(\"Insert value: \");\n";
            return code;
        }

        public override void Interpret()
        {
            VariablesSingleton.Variables[iDNode.ToString()] = float.Parse(Console.ReadLine());
        }

        public override void ValidateSemantic()
        {
            var type = iDNode.EvaluateType();
            if((!(type is IntType) && !(type is BoolType)))
            {
                throw new SemanticException("Invalid types.");
            }
        }
    }
}