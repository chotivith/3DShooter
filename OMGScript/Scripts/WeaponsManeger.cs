using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManeger : MonoBehaviour
{

    [SerializeField] private Weapons[] weapon;

    private int current_Weapon_Index;
    // Start is called before the first frame update
    void Start()
    {
        current_Weapon_Index = 0;
        weapon[current_Weapon_Index].gameObject.SetActive(true);

      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if(current_Weapon_Index == weaponIndex)
            return;
        weapon[current_Weapon_Index].gameObject.SetActive(false); 
        weapon[weaponIndex].gameObject.SetActive(true);
        current_Weapon_Index = weaponIndex;
    }
    public Weapons GetCurrentSelectedWeapon()
    {
        return weapon[current_Weapon_Index];
    }
}
