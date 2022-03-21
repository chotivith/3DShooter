using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Walk(bool walk)
    {
        anim.SetBool(AnimationTags.WALK_PARAMETER, walk);
    }

    public void Run(bool run)
    {
        anim.SetBool(AnimationTags.RUN_PARAMETER, run);
    }

    public void Attack()
    {
        anim.SetTrigger(AnimationTags.ATTACK_TRIGGER);
    }

    public void Dead()
    {
        anim.SetTrigger(AnimationTags.DEAD_TRIGGER);
    }
    public class AnimationTags
    {

        public const string ZOOM_IN_ANIM = "ZoomIn";
        public const string ZOOM_OUT_ANIM = "ZoomOut";

        public const string SHOOT_TRIGGER = "Shoot";
        public const string AIM_PARAMETER = "Aim";

        public const string WALK_PARAMETER = "Walk";
        public const string RUN_PARAMETER = "Run";
        public const string ATTACK_TRIGGER = "Attack";
        public const string DEAD_TRIGGER = "Dead";

    }
} //class


