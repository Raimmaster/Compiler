namespace Compiler
{
    public abstract class StatementNode
    {
        public abstract void Interpret();
        public abstract void ValidateSemantic();
    }
}