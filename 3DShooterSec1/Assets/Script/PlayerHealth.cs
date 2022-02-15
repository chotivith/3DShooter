using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSilder;
    [SerializeField] private Text healthText;

    public float hpPlayer = 100f;


    [SerializeField] private Image damageScreen;
    public bool checkDamageScreen = false;

    public static PlayerHealth instance;

    private void Awake()
    {
        instance = this;
        damageScreen.gameObject.SetActive(false);
    }

   

    // Update is called once per frame
    void Update()
    {
        healthSilder.value = hpPlayer;
        healthText.text = "HEALTH " + hpPlayer + "/100";
    }

    public void DamagePlayer(float damageAmount)
    {
        hpPlayer -= damageAmount;
    }


    public void HealPlayer(float healAmount)
    {
        hpPlayer += healAmount;

        if(hpPlayer > 100f)
        {
            hpPlayer = 100f;
        }
    }

    public void dalayDamageScreen()
    {
        damageScreen.gameObject.SetActive(true);
        damageScreen.gameObject.GetComponent<Animator>().Play("DamageScreen",0,0);
        
        
    }
}
