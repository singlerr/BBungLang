using ScriptEngine.Elements;

namespace ScriptEngine.Utils;

public class InStackPriority : Attribute
{
    public InStackPriority(int priority)
    {
        Priority = priority;
    }

    public int Priority { get; set; }
}

public class InComingPriority : Attribute
{
    public InComingPriority(int priority)
    {
        Priority = priority;
    }

    public int Priority { get; set; }
}

public static class StackingPriorityExt
{
    public static int GetInStackPriority(this NodeType priority)
    {
        var type = priority.GetType();
        var fieldInfo = type.GetField(priority.ToString());

        var attribs = fieldInfo.GetCustomAttributes(typeof(InStackPriority), false) as InStackPriority[];
        return attribs?.Length > 0 ? attribs[0].Priority : -1;
    }

    public static int GetInComingPriority(this NodeType priority)
    {
        var type = priority.GetType();
        var fieldInfo = type.GetField(priority.ToString());

        var attribs = fieldInfo.GetCustomAttributes(typeof(InComingPriority), false) as InComingPriority[];
        return attribs?.Length > 0 ? attribs[0].Priority : -1;
    }
}