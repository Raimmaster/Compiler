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