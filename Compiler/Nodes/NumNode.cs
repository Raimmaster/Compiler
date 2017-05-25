using System;

namespace Compiler
{
    public class NumNode : ExpressionNode
    {
        private float v;

        public NumNode(float v)
        {
            this.v = v;
        }

        public override dynamic evaluate()
        {
            return v;
        }
    }
}