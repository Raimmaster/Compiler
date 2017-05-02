using CompilerLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompilerTests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void CharTest()
        {
            var inputString = new InputString("\'a\'");
            var lexer = new Lexer(inputString);
            Token token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LIT_CHAR, token.type);
        }

        [TestMethod]
        public void HexIntLiteralTest()
        {
            var inputString = new InputString("0xaa0123456789FD");
            var lexer = new Lexer(inputString);

            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.LIT_HEX_INT, token.type);
        }

        [TestMethod]
        public void BinIntLiteralTest()
        {
            var inputString = new InputString("0b00101");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LIT_BIN_INT, token.type);
        }

        [TestMethod]
        public void FloatLiteralTest()
        {
            var inputString = new InputString("0.055f");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LIT_FLOAT, token.type);
        }

        [TestMethod]
        public void TrueBoolLiteralTest()
        {
            var inputString = new InputString("true");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.TRUE_KEYWORD, token.type);
        }

        [TestMethod]
        public void FalseBoolLiteralTest()
        {
            var inputString = new InputString("false");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.FALSE_KEYWORD, token.type);
        }

        [TestMethod]
        public void DivisionAndAssignOperatorTest()
        {
            var inputString = new InputString("/=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_DIVISION_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void MultiplicationAndAssignOperatorTest()
        {
            var inputString = new InputString("*=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_MULTIPLICATION_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void SumAndAssignOperatorTest()
        {
            var inputString = new InputString("+=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_SUM_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void SubstractAndAssignOperatorTest()
        {
            var inputString = new InputString("-=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_SUBSTRACT_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void ModuloAndAssignOperatorTest()
        {
            var inputString = new InputString("%=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_MODULO_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void BitOrAndAssignOperatorTest()
        {
            var inputString = new InputString("|=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_BITWISE_OR_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void BitAndAndAssignOperatorTest()
        {
            var inputString = new InputString("&=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_BITWISE_AND_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void BitXorAndAssignOperatorTest()
        {
            var inputString = new InputString("^=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_XOR_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void NegationEqualityOperatorTest()
        {
            var inputString = new InputString("!=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_NEGATION_EQUALITY, token.type);
        }

        [TestMethod]
        public void EqualityOperatorTest()
        {
            var inputString = new InputString("==");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_EQUALITY, token.type);
        }

        [TestMethod]
        public void BitwiseUnaryAndAssignTest()
        {
            var inputString = new InputString("~=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_BITWISE_UNARY_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void LessThanOrEqualsTest()
        {
            var inputString = new InputString("<=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LESS_THAN_OR_EQUALS, token.type);
        }

        [TestMethod]
        public void GreaterThanOrEqualsTest()
        {
            var inputString = new InputString(">=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.GREATER_THAN_OR_EQUALS, token.type);
        }

        [TestMethod]
        public void BitLeftShiftTest()
        {
            var inputString = new InputString("<<");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_BIT_LEFT_SHIFT, token.type);
        }

        [TestMethod]
        public void BitLeftShiftAndAssignTest()
        {
            var inputString = new InputString("<<=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_BIT_LEFT_SHIFT_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void BitRightShiftTest()
        {
            var inputString = new InputString(">>");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_BIT_RIGHT_SHIFT, token.type);
        }

        [TestMethod]
        public void BitRightShiftAndAssignTest()
        {
            var inputString = new InputString(">>=");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_BIT_RIGHT_SHIFT_AND_ASSIGN, token.type);
        }

        [TestMethod]
        public void TernaryOperatorTest()
        {
            var inputString = new InputString("?");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_TERNARY, token.type);
        }

        [TestMethod]
        public void NullCoalescingTest()
        {
            var inputString = new InputString("??");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_NULL_COALESCING, token.type);
        }

        [TestMethod]
        public void IncrementOperatorTest()
        {
            var inputString = new InputString("++");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_INCREMENT, token.type);
        }

        [TestMethod]
        public void DecrementOperatorTest()
        {
            var inputString = new InputString("--");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.OP_DECREMENT, token.type);
        }

        [TestMethod]
        public void BracketOpenTest()
        {
            var inputString = new InputString("[");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.BRACKET_OPEN, token.type);
        }

        [TestMethod]
        public void BracketCloseTest()
        {
            var inputString = new InputString("]");
            var lexer = new Lexer(inputString);
            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.BRACKET_CLOSE, token.type);
        }
    }
}
