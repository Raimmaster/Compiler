using System;

namespace Compiler
{
    public class FieldNode : AttributeNode
    {
        public string id;

        public FieldNode(string id)
        {
            this.id = id;
        }

        public override Types EvaluateType(Types type)
        {
            if(!(type is StructType))
            {
                throw new SemanticException("Type must be an struct!");
            }
            var struc = (StructType)type;
            if(!struc.attributes.ContainsKey(id))
            {
                throw new SemanticException("Field does not exist!");
            }

            return struc.attributes[id];
        }
    }
}