using UnityEngine;

public class ButtonInspectorAttribute : PropertyAttribute 
{
    public string MethodName { get; }
    public ButtonInspectorAttribute(string methodName)
    {
        MethodName = methodName;
    }
}