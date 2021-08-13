#if UNITY_EDITOR && ODIN_INSPECTOR
namespace SpottedZebra.UnityFoundation.Variables.Editor
{
    using Sirenix.OdinInspector.Editor;
    using UnityEditor;
    using UnityEngine;

    public class VariableReferenceBaseDrawer<TReference, TVariable, TValue> : OdinValueDrawer<TReference>
        where TReference : VariableReferenceBase<TVariable, TValue>
        where TVariable : VariableBase<TValue>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            IPropertyValueEntry<TReference> entry = this.ValueEntry;
            TReference value = entry.SmartValue;

            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if (value.UseConstant)
                    {
                        entry.Property.Children["ConstantValue"].Draw(label);
                    }
                    else
                    {
                        entry.Property.Children["Variable"].Draw(label);
                    }

                    GUIContent popupIcon = EditorGUIUtility.IconContent("_Menu");
                    GUILayoutOption[] options = new GUILayoutOption[]
                    {
                        GUILayout.Width(30),
                    };
                    
                    if (EditorGUILayout.DropdownButton(popupIcon, FocusType.Passive, options))
                    {
                        GenericMenu menu = new GenericMenu();
                        menu.AddItem(content: new GUIContent("Value"), @on: value.UseConstant, func: () => this.SetValues(true));
                        menu.AddItem(content: new GUIContent("Variable"), @on: !value.UseConstant, func: () => this.SetValues(false));
                        menu.ShowAsContext();
                    }

                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void SetValues(bool useConstant)
        {
            foreach (TReference value in this.ValueEntry.Values)
            {
                value.UseConstant = useConstant;
            }
        }
    }
}
#endif