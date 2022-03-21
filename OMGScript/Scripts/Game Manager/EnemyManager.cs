using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager intance;
    [SerializeField] private GameObject cannibal_Prefab;
    public Transform[] cannibal_SpawnPoint;
    [SerializeField] private int cannibal_Enemy_Count;
    private int initial_Cannibal_Count;
    public float wait_Before_Spawn_Enemies_Time = 10f;

    private void Awake()
    {
        MakeInstance();
    }
    private void Start()
    {
        initial_Cannibal_Count = cannibal_Enemy_Count;
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }
    void MakeInstance() {
        if(intance == null)
        {
            intance = this;
        }
    }
    void SpawnEnemies()
    {
        SpawnCannibals();
    }
    void SpawnCannibals() 
    {
        int index = 0;

        for (int i = 0; i < cannibal_Enemy_Count; i++)
        {
            if(index >= cannibal_SpawnPoint.Length)
            {
                index = 0;
            }
            
            Instantiate(cannibal_Prefab, cannibal_SpawnPoint[index].position, Quaternion.identity);
            index++;
        }
        cannibal_Enemy_Count = 0;
    }
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);
        SpawnCannibals();
        StartCoroutine("CheckToSpawnEnemies");
    }
    public void EnemyDied(bool cannibal)
    {
        if (cannibal)
        {
            cannibal_Enemy_Count++;
            if(cannibal_Enemy_Count > initial_Cannibal_Count)
            {
                cannibal_Enemy_Count = initial_Cannibal_Count;
            }
        }
    }
    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }


}
