using System.Collections.ObjectModel;
using Handler.FlowContext;

namespace Handler.Segments.System;

public class SegIf : Segment
{
    public override SegmentResponse Execute(Context ctx, Collection<object> args)
    {
        return new SegmentResponse(null, SegmentResponseType.Continue);
    }

    public override SegmentResponse OnSuspend(Context ctx)
    {
        return new SegmentResponse(null, SegmentResponseType.Continue);
    }
}