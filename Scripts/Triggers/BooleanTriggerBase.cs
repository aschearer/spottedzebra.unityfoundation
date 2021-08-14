﻿using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Triggers
{
    public abstract class BooleanTriggerBase : FoundationTriggerBase
    {
        [PropertyOrder(1001)] [SerializeField]
        private UnityEvent onSuccess = new UnityEvent();

        [PropertyOrder(1002)] [SerializeField]
        private UnityEvent onFailure = new UnityEvent();
        
        protected override void OnTrigger()
        {
            bool result = this.OnTriggerBool();
            if (result)
            {
                this.onSuccess.Invoke();
            }
            else
            {
                this.onFailure.Invoke();
            }
        }

        protected abstract bool OnTriggerBool();
    }
}