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

        public override dynamic Evaluate()
        {
            return v;
        }

        public override Types EvaluateType()
        {
            return new IntType();
        }

        public override ExpressionCode GenerateCode()
        {
            return new ExpressionCode(v.ToString());
        }
    }
}