using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private WeaponsManeger weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool zoomed;
    private Camera mainCam;
    private GameObject crosshair;
    private bool is_Aiming;

    private void Awake()
    {
        weapon_Manager = GetComponent<WeaponsManeger>();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT)
            .transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair =  GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
        
    }
    void WeaponShoot()
    {
        if(weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
               
                BulletFired();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();    
                }

                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();
                }
                else
                {
                    if (is_Aiming)
                    {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (weapon_Manager.GetCurrentSelectedWeapon().bulletType
                            == WeaponBulletType.SPEAR)
                        {

                        }
                    }


                }
            }
        }


    }
    void ZoomInAndOut()
    {
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);

                crosshair.SetActive(false);
            }

            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);

                crosshair.SetActive(true);
            }
        }

        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);

                is_Aiming = (true);

            }

            if (Input.GetMouseButtonUp(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);

                is_Aiming = (false);

            }
        }
    }
    void BulletFired()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }






    public class Tags
    {

        public const string LOOK_ROOT = "Look Root";
        public const string ZOOM_CAMERA = "FP Camera";
        public const string CROSSHAIR = "Crosshair";
        public const string ARROW_TAG = "Arrow";

        public const string AXE_TAG = "Axe";

        public const string PLAYER_TAG = "Player";
        public const string ENEMY_TAG = "Enemy";

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

