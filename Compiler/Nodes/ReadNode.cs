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

        public override void Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}