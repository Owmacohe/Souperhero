using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
   public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        int randEnemies = Random.Range(0, enemies.Length);
        GameObject instanceEnemies = (GameObject)Instantiate(enemies[randEnemies], transform.position, Quaternion.identity);
        instanceEnemies.transform.parent = transform;
        instanceEnemies.transform.position = new Vector3(instanceEnemies.transform.position.x, instanceEnemies.transform.position.y, -0.1198f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
