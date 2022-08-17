using ScriptEngine.Utils;

namespace ScriptEngine.Elements;

public enum NodeType
{
    /**
         * Basic Node Type
         */
    Line,
    String,
    Int,
    Double,


    [NodeExpression("{")] StartBracket,
    [NodeExpression("}")] EndBracket,
    [NodeExpression(",")] Comma,

    [InStackPriority(3)] [InComingPriority(3)] [NodeExpression("뿌")]
    Add,

    [InStackPriority(3)] [InComingPriority(3)] [NodeExpression("뽁")]
    Sub,

    [InStackPriority(2)] [InComingPriority(2)] [NodeExpression("뿕")]
    Mul,

    [InStackPriority(2)] [InComingPriority(2)] [NodeExpression("삑")]
    Div,

    [InComingPriority(5)] [NodeExpression("뿌욱")]
    Equal,

    [InComingPriority(4)] [NodeExpression("뿡뿡!")]
    LeftBig,

    [InComingPriority(4)] [NodeExpression("뿡뿡뿡!")]
    RightBig,

    [InComingPriority(4)] [NodeExpression("뿍!")]
    LeftBigEqual,

    [InComingPriority(4)] [NodeExpression("뿍뿍!")]
    RightBigEqual,

    [InComingPriority(5)] [NodeExpression("뿍뿍뿍!")]
    NotEqual,
    [NodeExpression("\"")] Quote,
    [NodeExpression("뿌웅")] If,
    [NodeExpression(":")] NameAndVarSeparator,
    [NodeExpression(".")] Dot,
    [NodeExpression("")] Unknown,
    [NodeExpression("@")] NewLine,
    [NodeExpression("$")] Variable,
    [NodeExpression("뿌우웅")] Else,
    [NodeExpression("뿌우우웅")] EndIf,
    [NodeExpression("뿌우우욱")] ElseIf,

    [InStackPriority(8)] [InComingPriority(0)] [NodeExpression("(")]
    StartRoundBracket,
    [NodeExpression(")")] EndRoundBracket,

    /**
         * Reserved words
         */
    [NodeExpression("삥")] UpdateState,
    [NodeExpression("빵")] SetVariable,
    [NodeExpression("뿡")] Print,

    [NodeExpression("create_action_selector")]
    CreateActionSelector,
    [NodeExpression("create_action")] CreateAction,
    [NodeExpression("bind_callback")] BindCallback,

    [NodeExpression("show_action_selector")]
    ShowActionSelector,
    [NodeExpression("go_to")] GoTo
}

public class NodeExpression : Attribute
{
    public NodeExpression(string expression)
    {
        Expression = expression;
    }

    public string Expression { get; }
}

public static class NodeTypeExt
{
    public static bool IsOperator(this NodeType nodeType)
    {
        return nodeType is NodeType.Add or NodeType.Div or NodeType.Mul or NodeType.Sub;
    }

    public static string GetExpression(this NodeType nodeType)
    {
        var type = nodeType.GetType();
        var fieldInfo = type.GetField(nodeType.ToString());

        var attribs = fieldInfo.GetCustomAttributes(typeof(NodeExpression), false) as NodeExpression[];
        return attribs?.Length > 0 ? attribs[0].Expression : null;
    }
}