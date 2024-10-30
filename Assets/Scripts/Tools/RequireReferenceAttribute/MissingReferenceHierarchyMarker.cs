#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Kor.Tools
{
    [InitializeOnLoad]
    public class MissingReferenceHierarchyMarker
    {
        private const int IconMargin = 15;
        private static readonly Dictionary<Type, FieldInfo[]> CachedFields = new Dictionary<Type, FieldInfo[]>();

        static MissingReferenceHierarchyMarker()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (!(EditorUtility.InstanceIDToObject(instanceID) is GameObject gameObject)) return;
            foreach (var component in gameObject.GetComponents<MonoBehaviour>())
            {
                if (component == null) continue; 
                var componentType = component.GetType();
                if (!CachedFields.TryGetValue(componentType, out var fields))
                {
                    fields = componentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    CachedFields[componentType] = fields;
                }
                foreach (var field in fields)
                {
                    if (field.GetCustomAttribute<RequireReferenceAttribute>() == null) continue;
                    var fieldValue = field.GetValue(component) as UnityEngine.Object;
                    if (fieldValue != null) continue;
                    Rect iconRect = new Rect(selectionRect.xMax - IconMargin, selectionRect.y, IconMargin, IconMargin);
                    EditorGUI.LabelField(iconRect, "⚠️");
                }
            }
        }
    }
}
#endif