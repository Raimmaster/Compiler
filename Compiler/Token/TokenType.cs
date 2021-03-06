﻿namespace Compiler
{
    public enum TokenType
    {
        ID,
        EOF,
        OP_SUM,
        PRINT_CALL,
        READ_CALL,
        OP_SUBSTRACT,
        OP_DIVISION,
        OP_MULTIPLICATION,
        OP_MODULO,
        OP_ASSIGN,
        LIT_INT,
        PAREN_OPEN,
        PAREN_CLOSE,
        END_STATEMENT,
        OP_EXPONENT,
        TRUE_KW,
        FALSE_KW,
        LIT_BOOL,
        INT_KW,
        BOOL_KW,
        STRUCT_KW,
        END_KW,
        BRACKET_OPEN,
        BRACKET_CLOSE,
        DECL_KW,
        DOT_OPERATOR
    }
}