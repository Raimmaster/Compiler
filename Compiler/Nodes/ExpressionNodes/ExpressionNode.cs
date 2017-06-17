namespace Compiler
{
    public abstract class ExpressionNode
    {
        public abstract Types EvaluateType();
        public abstract dynamic Evaluate();
        public abstract ExpressionCode GenerateCode();
    }
}