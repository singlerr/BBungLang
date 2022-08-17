using Handler.Segments;

namespace ScriptEngine.Table;

public class MiscVariable
{
    public static Dictionary<string, object> MiscVariables = new();

    public static object GetVariable(string varName)
    {
        return VariableExists(varName) ? MiscVariables[varName] : null;
    }

    public static bool VariableExists(string varName)
    {
        return MiscVariables.ContainsKey(varName);
    }

    public static void PutVariable(string varName, object variable)
    {
        MiscVariables[varName] = variable;
    }

    public static void PutActionSelector(string selectorName, ActionSelector selector)
    {
        PutVariable(selectorName, selector);
    }

    public static ActionSelector GetActionSelector(string selectorName)
    {
        return ActionSelectorExists(selectorName)
            ? GetVariable(selectorName) as ActionSelector
            : null;
    }

    public static bool ActionSelectorExists(string selectorName)
    {
        return VariableExists(selectorName) && GetVariable(selectorName) is ActionSelector;
    }
}