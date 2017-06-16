using System;

namespace Compiler
{
    public class IndexArrayNode : AttributeNode
    {
        public ExpressionNode value;

        public IndexArrayNode(ExpressionNode value)
        {
            this.value = value;
        }

        public override Types EvaluateType(Types type)
        {
            if(!(type is ArrayType))
            {
                throw new SemanticException("Type must be an array!");
            }
            var arrType = (ArrayType)type;
            var exprType = value.EvaluateType();
            if(!(exprType is IntType))
            {
                throw new SemanticException("Type must be an int!");
            }
            return arrType.type;
        }

        public override ExpressionCode GenerateCode()
        {
            return value.GenerateCode();
        }
    }
}