/*
 * Shim so things compile when Odin Inspector is absent.
 */

#if !ODIN_INSPECTOR
using System;

namespace Sirenix.OdinInspector
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Button : Attribute
    {
    }
}
#endif