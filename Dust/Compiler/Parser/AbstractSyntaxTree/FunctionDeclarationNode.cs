﻿using System.Collections.Generic;

namespace Dust.Compiler.Parser.AbstractSyntaxTree
{
  public class FunctionDeclarationNode : Node
  {
    public string Name { get; }
    public List<FunctionModifier> Modifiers { get; }
    public List<FunctionParameter> Parameters { get; }
    public Node Body { get; }

    public FunctionDeclarationNode(string name, List<FunctionModifier> modifiers, List<FunctionParameter> parameters, Node body, SourceRange range)
    {
      Name = name;
      Modifiers = modifiers;
      Parameters = parameters;
      Body = body;
      Range = range;
    }

    public override void Visit(IVisitor visitor)
    {
      visitor.Accept(this);

      Body.Visit(visitor);
    }

    public override void VisitChildren(IVisitor visitor)
    {
    }
  }
}