using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("Animator",
                 "PlayAnimation",
                 "")]
    public class AnimatorPlayAnimation : Command
    {
        [TextArea]
        [SerializeField]
        [Tooltip("A description of what this command does. Appears in the command summary.")]
        public string description;
        public string animationName;
        public Animator animator;
        public override void OnEnter()
        {
            animator.Play(animationName);
            Continue();
        }
    }
}
