#if FLOW_CANVAS
using FlowCanvas;
using FlowCanvas.Nodes;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using SpottedZebra.UnityFoundation.Variables;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Flows.Variables
{
    [Category("F/Data/Variables/Set")]
    [ContextDefinedInputsAttribute(typeof(Wild))]
    public abstract class SetVariableBase<TVariable, TValue> : ParameterVariableNode
        where TVariable : VariableBase<TValue>
    {
        [BlackboardOnly] public BBParameter<TVariable> targetVariable;
        public override BBParameter parameter => targetVariable;

        [HideInInspector] public OperationMethod operation = OperationMethod.Set;

        public override string name
        {
            get
            {
                return string.Format("{0}{1}{2}", targetVariable, OperationTools.GetOperationString(operation),
                    "Value");
            }
        }

        protected override void RegisterPorts()
        {
            var o = AddFlowOutput("Out");
            var v = AddValueInput<TValue>("Value");
            AddValueOutput<TValue>("Value", () => { return targetVariable.value.Value; });
            AddFlowInput("In", (f) =>
            {
                DoSet(v.value);
                o.Call(f);
            });
        }

        void DoSet(TValue value)
        {
            TValue newValue = value;
            if (operation != OperationMethod.Set)
            {
                if (typeof(TValue) == typeof(float))
                    newValue = (TValue) (object) OperationTools.Operate((float) (object) targetVariable.value.Value,
                        (float) (object) value, operation);
                else if (typeof(TValue) == typeof(int))
                    newValue = (TValue) (object) OperationTools.Operate((int) (object) targetVariable.value.Value,
                        (int) (object) value, operation);
                else if (typeof(TValue) == typeof(Vector3))
                    newValue = (TValue) (object) OperationTools.Operate((Vector3) (object) targetVariable.value.Value,
                        (Vector3) (object) value, operation);
                else newValue = value;
            }
            else
            {
                newValue = value;
            }

            this.targetVariable.value.SetValue(newValue);
        }

        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR

        protected override void OnNodeInspectorGUI()
        {
            DrawDefaultInspector();
            if (typeof(TValue) == typeof(float) || typeof(TValue) == typeof(int) || typeof(TValue) == typeof(Vector3))
            {
                operation = (OperationMethod) UnityEditor.EditorGUILayout.EnumPopup("Operation", operation);
            }

            EditorUtils.BoldSeparator();
            base.DrawValueInputsGUI();
        }

#endif
    }
}
#endif