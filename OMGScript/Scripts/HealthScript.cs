using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyController enemyController;

    public float health = 1000f;

    public bool is_Player, is_Cannibal;

    private bool is_Dead;

    private EnemyAudio enemyAudio;
    private PlayerStart player_Starts;

    private void Awake()
    {
        if (is_Cannibal)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if (is_Player) {
            player_Starts = GetComponent<PlayerStart>();
        }
    }

    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;

        health -= damage;

        if (is_Player)
        {
            player_Starts.Display_HealthStats(health);
        }
        if (is_Cannibal) 
        {
            if(enemyController.Enemy_State == EnemyState.PATROL)
                {
                enemyController.chase_Distance = 50f;
                }
        }

        if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;
        }
    }
    void PlayerDied() 
    {
        if (is_Cannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);

            //navMeshAgent.velocity = Vector3.zero;
            //navMeshAgent.isStopped = true;
            enemyController.enabled = false;

            //enemyAnimator.Dead();

            navMeshAgent.enabled = false;
            enemyAnimator.enabled = false;

            StartCoroutine(DeadSound());
            EnemyManager.intance.EnemyDied(true);
        }

        if (is_Player)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            EnemyManager.intance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponsManeger>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }
        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }
    void RestartGame() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    
    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DeadSound();
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
}
