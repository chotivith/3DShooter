using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private string cpName;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerCheckPoint"))
        {
            if(PlayerPrefs.GetString("PlayerCheckPoint") == cpName)
            {
                PlayerMovement.instance.transform.position = transform.position + Vector3.up * 8;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.DeleteKey("PlayerCheckPoint");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("AAAAAA");
            PlayerPrefs.SetString("PlayerCheckPoint", cpName);
        }
    }

   
}
