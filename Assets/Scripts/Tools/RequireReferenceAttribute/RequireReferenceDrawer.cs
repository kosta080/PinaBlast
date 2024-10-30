#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Kor.Tools
{
    [CustomPropertyDrawer(typeof(RequireReferenceAttribute))]
    public class RequireReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
        
            if (property.propertyType == SerializedPropertyType.ObjectReference 
                && property.objectReferenceValue == null)
            {
                position.x += EditorGUIUtility.labelWidth - 20;
                EditorGUI.HelpBox(position, $"", MessageType.Warning);
            }
        }
    }
}
#endif