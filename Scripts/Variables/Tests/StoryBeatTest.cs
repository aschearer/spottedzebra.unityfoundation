using System;
using SpottedZebra.UnityFoundation.Triggers.Conditions;
using UnityEngine;

namespace SpottedZebra.UnityFoundation.Variables.Tests
{
    public sealed class StoryBeatTest : ObservableConditionTestBase
    {
        [Space] public StoryBeatVariable Value;

        [SerializeField] private StoryBeatTestType Operation;

        public BooleanReference Target = new BooleanReference(true);

        public override bool EvaluateTest()
        {
            return StoryBeatTest.EvaluteTestStatic(this.Value.Value, this.Operation, this.Target.Value);
        }

        internal static bool EvaluteTestStatic(StoryBeat storyBeat, StoryBeatTestType operation, bool target)
        {
            bool result;
            switch (operation)
            {
                case StoryBeatTestType.Unlocked:
                    result = storyBeat.IsUnlocked == target;
                    break;
                case StoryBeatTestType.Seen:
                    result = storyBeat.IsSeen == target;
                    break;
                case StoryBeatTestType.UnlockedAndUnseen:
                    result = (storyBeat.IsUnlocked && !storyBeat.IsSeen) == target;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}