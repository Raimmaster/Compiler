using System;

namespace Compiler
{
    public class ReadNode : StatementNode
    {
        private IDNode iDNode;
        private NumNode numNode;

        public ReadNode(IDNode iDNode, NumNode numNode)
        {
            this.iDNode = iDNode;
            this.numNode = numNode;
        }

        public override void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}