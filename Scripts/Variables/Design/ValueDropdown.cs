/*
 * Shim so things compile when Odin Inspector is absent.
 */

#if !ODIN_INSPECTOR
using System;

namespace Sirenix.OdinInspector
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ValueDropdown : Attribute
    {
        public ValueDropdown(string name)
        {
        }

        public bool AppendNextDrawer { get; set; }

        public int DropdownWidth { get; set; }
    }
}
#endif