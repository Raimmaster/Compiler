namespace Compiler
{
    public abstract class AttributeNode
    {
        public abstract Types EvaluateType(Types type);
        public abstract ExpressionCode GenerateCode();
    }
}