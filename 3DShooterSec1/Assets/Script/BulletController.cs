using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f, lifeTime = 5f;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private GameObject impactEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * moveSpeed;
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }
}
