﻿namespace VRTK
{
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Reflection;

    public static class VRTK_EditorUtilities
    {
        public static GUIContent BuildGUIContent<T>(string fieldName, string displayOverride = null)
        {
            string displayName = (displayOverride != null ? displayOverride : ObjectNames.NicifyVariableName(fieldName));
            FieldInfo fieldInfo = typeof(T).GetField(fieldName);
            TooltipAttribute tooltipAttribute = (TooltipAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(TooltipAttribute));
            return (tooltipAttribute == null ? new GUIContent(displayName) : new GUIContent(displayName, tooltipAttribute.tooltip));
        }

        public static void AddHeader<T>(string fieldName, string displayOverride = null)
        {
            string displayName = (displayOverride != null ? displayOverride : ObjectNames.NicifyVariableName(fieldName));
            FieldInfo fieldInfo = typeof(T).GetField(fieldName);
            HeaderAttribute headerAttribute = (HeaderAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(HeaderAttribute));
            AddHeader(headerAttribute == null ? displayName : headerAttribute.header);
        }

        public static void AddHeader(string header, bool spaceBeforeHeader = true)
        {
            if (spaceBeforeHeader)
            {
                EditorGUILayout.Space();
            }

            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
        }

        public static void DrawUsingDestructiveStyle(GUIStyle styleToCopy, Action<GUIStyle> drawAction)
        {
            Color previousBackgroundColor = GUI.backgroundColor;
            GUIStyle destructiveButtonStyle = new GUIStyle(styleToCopy)
            {
                normal =
                {
                    textColor = Color.white
                },
                active =
                {
                    textColor = Color.white
                }
            };

            GUI.backgroundColor = Color.red;
            drawAction(destructiveButtonStyle);
            GUI.backgroundColor = previousBackgroundColor;
        }
    }
}