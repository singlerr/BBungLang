using System.Collections.ObjectModel;
using Handler.FlowContext;

namespace Handler.Segments.Effect;

public class SegGoTo : Segment
{
    public override SegmentResponse Execute(Context ctx, Collection<object> args)
    {
        throw new NotImplementedException();
    }

    public override SegmentResponse OnSuspend(Context ctx)
    {
        throw new NotImplementedException();
    }
}