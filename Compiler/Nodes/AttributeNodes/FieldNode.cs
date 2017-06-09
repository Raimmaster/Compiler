namespace Compiler
{
    public class FieldNode : AttributeNode
    {
        public string id;

        public FieldNode(string id)
        {
            this.id = id;
        }
    }
}