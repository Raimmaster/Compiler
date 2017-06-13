namespace Compiler
{
    public class ArrayType : Types
    {
        public int size;
        public Types type;
        private int rankCount;

        public ArrayType()
        {
            size = 0;
            type = null;
        }

        public ArrayType(int v)
        {
            this.rankCount = v;
        }

        public ArrayType(int rankCount, Types arrayType)
        {
            this.rankCount = rankCount;
            this.type = arrayType;
        }
    }
}