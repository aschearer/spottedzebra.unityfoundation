using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables
{
    [CreateAssetMenu(fileName = "Story Beat Variable", menuName = "F/Variables/Story Beat")]
    public sealed class StoryBeatVariable : VariableBase<StoryBeat>
    {
        public void SetValueFrom(StoryBeatVariable variable)
        {
            this.SetValue(variable.Value);
        }

        public void SetIsUnlock(bool isUnlocked)
        {
            StoryBeat storyBeat = this.Value;
            storyBeat.IsUnlocked = isUnlocked;
            this.SetValue(storyBeat);
        }

        public void SetIsSeen(bool isSeen)
        {
            StoryBeat storyBeat = this.Value;
            storyBeat.IsSeen = isSeen;
            this.SetValue(storyBeat);
        }

        public enum StoryBeatAccessType
        {
            IsUnlocked,
            IsSeen,
        }
    }
}