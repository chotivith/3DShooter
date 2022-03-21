using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,SELF_AIM,AIM
}

public enum WeaponFireType
{
    SINGLE,MULTIPLE
}

public enum WeaponBulletType
{
    BULLET, SPEAR, NONE
}

public class Weapons : MonoBehaviour
{
    private Animator anim;
    public WeaponAim weapon_Aim;

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AudioSource shootSound, reload_sound;

    [SerializeField] public WeaponFireType fireType;
    [SerializeField] public WeaponBulletType bulletType;
    [SerializeField] public GameObject attack_Point;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
        //anim.SetTrigger("Shoot");
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void Turn_on_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void Turn_off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void Play_ShootSound()
    {
        shootSound.Play();
    }

    void Play_ReloadSound()
    {
        reload_sound.Play();
    }

    void Turn_on_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    void Turn_off_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
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
}
