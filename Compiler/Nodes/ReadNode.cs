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
    }
}