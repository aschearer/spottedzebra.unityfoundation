using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace SpottedZebra.UnityFoundation.Extensions.Design
{
    public class TagSelectorAttributeDrawer : OdinAttributeDrawer<TagSelectorAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            IPropertyValueEntry<string> entry = this.ValueEntry;
            entry.SmartValue = label != null
                ? EditorGUILayout.TagField(label, entry.SmartValue)
                : EditorGUILayout.TagField(entry.SmartValue);
        }
    }
}
#endif