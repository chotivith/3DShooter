using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesAlive = 0;
    [SerializeField] private int round = 0;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

     void Update()
    {
        if(enemiesAlive <= 0)
        {
            round++;
            NextWave(round);
        }
    }

    void NextWave(int round)
    {
        for(int i = 0; i<round; i++)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);

            enemiesAlive++;
        }
    }
}
