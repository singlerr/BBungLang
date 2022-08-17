using System.Collections.ObjectModel;
using Handler.Segments;
using ScriptEngine.Elements;
using ScriptEngine.Elements.Nodes;

namespace ScriptEngine.ScriptParser;

public class NodeParser
{
    private static readonly Dictionary<NodeType, Func<LineNode, Collection<object>>> NodeAnalyzers = new();

    private static readonly Dictionary<NodeType, Func<Func<LineNode, Collection<object>>, LineNode, Segment>>
        SegmentInitializers = new();


    static NodeParser()
    {
        NodeAnalyzers[NodeType.UpdateState] = LineNodeParserUnion.UpdateState;
        SegmentInitializers[NodeType.UpdateState] = SegmentUnion.UpdateState;
        NodeAnalyzers[NodeType.SetVariable] = LineNodeParserUnion.SetVariable;
        SegmentInitializers[NodeType.SetVariable] = SegmentUnion.SetVariable;
        NodeAnalyzers[NodeType.CreateActionSelector] = LineNodeParserUnion.CreateActionSelector;
        SegmentInitializers[NodeType.CreateActionSelector] = SegmentUnion.CreateActionSelector;
        NodeAnalyzers[NodeType.ShowActionSelector] = LineNodeParserUnion.ShowActionSelector;
        SegmentInitializers[NodeType.ShowActionSelector] = SegmentUnion.ShowActionSelector;
        NodeAnalyzers[NodeType.Print] = LineNodeParserUnion.Print;
        SegmentInitializers[NodeType.Print] = SegmentUnion.Print;
        NodeAnalyzers[NodeType.If] = LineNodeParserUnion.If;
        SegmentInitializers[NodeType.If] = SegmentUnion.If;
        NodeAnalyzers[NodeType.GoTo] = LineNodeParserUnion.GoTo;
        SegmentInitializers[NodeType.GoTo] = SegmentUnion.GoTo;
    }

    public static Segment ParseSegment(LineNode lineNode)
    {
        if (NodeAnalyzers.ContainsKey(lineNode.GetHeadNodeType()) &&
            SegmentInitializers.ContainsKey(lineNode.GetHeadNodeType()))
            return SegmentInitializers[lineNode.GetHeadNodeType()]
                .Invoke(NodeAnalyzers[lineNode.GetHeadNodeType()], lineNode);

        return null;
    }

    public static Collection<object> Execute(LineNode lineNode)
    {
        if (NodeAnalyzers.ContainsKey(lineNode.GetHeadNodeType()))
            return NodeAnalyzers[lineNode.GetHeadNodeType()].Invoke(lineNode);
        return new Collection<object>();
    }
}