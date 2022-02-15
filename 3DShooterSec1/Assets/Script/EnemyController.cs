using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;

    [SerializeField] private float hpMummy = 10f;
    private float distance;

    [SerializeField] private float maxFollowDistance = 20f;
    [SerializeField] private float distanceToStop = 4f;

    [SerializeField] private bool isWalk = false;
    [SerializeField] private bool isDie = false;
    [SerializeField] private bool isAttack = false;

    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Material material;

    [SerializeField] private float coolDownAttack = 1f;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(hpMummy > 0)
        {
            Seeking();
        }
        else
        {
            Dead();
        }
       // print(distance);
        enemyAnimator.SetBool("isWalk", isWalk);
        enemyAnimator.SetBool("isDie", isDie);
        enemyAnimator.SetBool("isAttack", isAttack);

    }

    void Seeking()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > distanceToStop && distance < maxFollowDistance)
        {
            navMesh.SetDestination(player.transform.position);
            isWalk = true;
            isAttack = false;
            coolDownAttack = 1f;
        }
        else
        {
            navMesh.SetDestination(transform.position);
            isWalk = false;
            if(distance < distanceToStop)
            {
                isAttack = true;
                coolDownAttack -= Time.deltaTime;
                if(coolDownAttack <= 0.2f)
                {
                    PlayerHealth.instance.dalayDamageScreen();
                    PlayerHealth.instance.DamagePlayer(10);
                    coolDownAttack = 1f;
                }
            }
        }
    }

    void Dead()
    {
        if(hpMummy < 1)
        {
            navMesh.isStopped = true;
            isDie = true;
            StartCoroutine(delayDead());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hpMummy -= 1;
            print(hpMummy);

            if(hpMummy > 0)
            {
                enemyAnimator.Play("takeDamage");
                StartCoroutine(flashEnemy());
            }
        }
    }

    IEnumerator delayDead()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        GameManager.instance.enemiesAlive--;
    }

    IEnumerator flashEnemy()
    {
        for(int i = 0; i < 5; i++)
        {
            this.GetComponentInChildren<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            this.GetComponentInChildren<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

   
}
