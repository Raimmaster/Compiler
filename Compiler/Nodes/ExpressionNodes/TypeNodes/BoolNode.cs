using System;

namespace Compiler
{
    public class BoolNode : ExpressionNode
    {
        public bool v;

        public BoolNode(bool v)
        {
            this.v = v;
        }

        public override dynamic Evaluate()
        {
            return v;
        }

        public override Types EvaluateType()
        {
            return new BoolType();
        }

        public override ExpressionCode GenerateCode()
        {
            return new ExpressionCode(v.ToString().ToLower());
        }
    }
}