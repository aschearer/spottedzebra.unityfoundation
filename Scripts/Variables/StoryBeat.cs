using System;

namespace SpottedZebra.UnityFoundation.Variables
{
    /// <summary>
    /// A moment in the game which can be unlocked and seen by the player.
    /// </summary>
    [Serializable]
    public struct StoryBeat
    {
        /// <summary>
        /// Has the player unlocked the story beat.
        /// </summary>
        public bool IsUnlocked;

        /// <summary>
        /// Has the player seen the story beat.
        /// </summary>
        public bool IsSeen;
    }
}