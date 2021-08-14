/*
 * Shim so things compile when Odin Inspector is absent.
 */

#if !ODIN_INSPECTOR
using System;

namespace Sirenix.OdinInspector
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class FoldoutGroup : Attribute
    {
        public FoldoutGroup(string name)
        {
        }
    }
}
#endif