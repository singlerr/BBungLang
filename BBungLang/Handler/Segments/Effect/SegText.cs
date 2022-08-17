using System.Collections.ObjectModel;
using Handler.FlowContext;
using ScriptEngine.Elements;

namespace Handler.Segments.Effect;

public class SegText : Segment
{
    public override SegmentResponse Execute(Context ctx, Collection<object> args)
    {
        var vars = args[0] as Dictionary<string, Node>;
        foreach (var keyValuePair in vars) Console.WriteLine($"[{keyValuePair.Key}] {keyValuePair.Value.Value}");

        return new SegmentResponse(null, SegmentResponseType.Continue);
    }

    public override SegmentResponse OnSuspend(Context ctx)
    {
        return new SegmentResponse(null, SegmentResponseType.Continue);
    }
}