using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {


    public bool EnemiesInRange = false;
    public List<GameObject> enemies;

    private void Start()
    {
        enemies = new List<GameObject>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
    
            EnemiesInRange = true;
            enemies.Add(other.gameObject);
        }
     
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("EXIT " + other.gameObject.name);
            EnemiesInRange = false;
            enemies.Remove(other.gameObject);
        }
    }


    public void PlayerAttack()
    {
        foreach (GameObject enemObj in enemies)
        {
            Destroy(enemObj);
        }
    }
}
