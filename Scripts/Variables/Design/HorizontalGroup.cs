/*
 * Shim so things compile when Odin Inspector is absent.
 */

#if !ODIN_INSPECTOR
using System;

namespace Sirenix.OdinInspector
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HorizontalGroup : Attribute
    {
        public HorizontalGroup(string name)
        {
        }

        public int Width { get; set; }
    }
}
#endif