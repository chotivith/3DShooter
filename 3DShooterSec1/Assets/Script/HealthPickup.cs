using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealth.instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
