using System.Collections.Generic;
using Dust.Compiler.Lexer;
using Xunit;

namespace Dust.Tests
{
  public class DustLexerTests
  {
    public static List<SyntaxTokenKind> ConvertToSyntaxTokenKinds(string input)
    {
      List<SyntaxTokenKind> tokenKinds = new List<SyntaxTokenKind>();
      string[] tokenKindStrArray = input.Split(",");
      foreach (string tokenKindName in tokenKindStrArray)
      {
        if (SyntaxTokenKind.TryParse(tokenKindName, out SyntaxTokenKind tokenKind))
        {
          tokenKinds.Add(tokenKind);
        }
        
      }

      return tokenKinds;
    }
    
    [Theory]
    [InlineData("2+2-2*2/2", "NumericLiteral,Plus,NumericLiteral,Minus,NumericLiteral,Asterisk,NumericLiteral,Slash,NumericLiteral,EndOfFile")]
    [InlineData("let mut variableName = \"string\"", "LetKeyword,MutKeyword,Identifier,StringLiteral,EndOfFile")]
    [InlineData("private let fn functionName { return 7*8 }", "PrivateKeyword,LetKeyword,FnKeyword,Identifier,OpenBrace,ReturnKeyword,NumericLiteral,Asterisk,NumericLiteral,CloseBrace,EndOfFile")]
    [InlineData("a += 5", "Identifier,PlusEquals,NumericLiteral,EndOfFile")]
    [InlineData("a -= 5", "Identifier,MinusEquals,NumericLiteral,EndOfFile")]
    [InlineData("a *= 5", "Identifier,AsteriskEquals,NumericLiteral,EndOfFile")]
    [InlineData("a /= 5", "Identifier,SlashEquals,NumericLiteral,EndOfFile")]
    [InlineData("a ** 5", "Identifier,AsteriskAsterisk,NumericLiteral,EndOfFile")]
    [InlineData("a // 5", "Identifier,SlashSlash,NumericLiteral,EndOfFile")]
    [InlineData("a++", "Identifier,PlusPlus,EndOfFile")]
    [InlineData("a--", "Identifier,MinusMinus,EndOfFile")]
    [InlineData("public protected internal let fn name {}", "PublicKeyword,ProtectedKeyword,InternalKeyword,LetKeyword,FnKeyword,Identifier,OpenBrace,CloseBrace,EndOfFile")]
    [InlineData("let nullVariable = null", "LetKeyword,Identifier,NullKeyword,EndOfFile")]
    [InlineData("typeof nullVariable", "TypeOfKeyword,Identifier,EndOfFile")]
    [InlineData("let trueVariable = true", "LetKeyword,Identifier,TrueKeyword,EndOfFile")]
    [InlineData("let falseVariable = false", "LetKeyword,Identifier,FalseKeyword,EndOfFile")]
    public static void Test_LexIdentifiers(string input, string expected)
    {
      List<SyntaxToken> tokens = new SyntaxLexer(input).Lex();
      List<SyntaxTokenKind> tokenKinds = new List<SyntaxTokenKind>();
      
      foreach (SyntaxToken token in tokens)
      {
        tokenKinds.Add(token.Kind);
      }
      List<SyntaxTokenKind> expectedTokenKinds = ConvertToSyntaxTokenKinds(expected);
      Assert.Equal(expectedTokenKinds, tokenKinds);
    }
  }
}