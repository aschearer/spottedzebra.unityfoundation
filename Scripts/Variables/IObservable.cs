using UnityEngine.Events;

namespace SpottedZebra.UnityFoundation.Variables
{
    public interface IObservable
    {
        UnityEvent OnChanged { get; }
    }
}