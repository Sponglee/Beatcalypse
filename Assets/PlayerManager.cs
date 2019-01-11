using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    [SerializeField]
    private float hp = 10f;
    public float Hp { get { return hp; } set { hp = value; } }

    public float attackPower = 5;

    //Range Check bool
    public bool EnemiesInRange = false;
    //List of enemies
    public List<Collider> enemies;

    private void Start()
    {
        enemies = new List<Collider>();
    }


    public void Update()
    {
        //Reset enemies in range when nothing is in the list
        if (EnemiesInRange && enemies.Count == 0)
        {
            EnemiesInRange = false;
        }
    }

    //Check for enemies that enter the range
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
    
            EnemiesInRange = true;
            enemies.Add(other);
        }
     
    }

    //Check if enemy exists the range
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("EXIT " + other.gameObject.name);
            enemies.Remove(other);
        }
    }

    

    public void PlayerAttack(bool supercharge=false)
    {
        if(EnemiesInRange)
        {
            foreach (Collider enemObj in enemies)
            {
                if (enemObj)
                {
                    if(supercharge)
                    {
                        enemObj.GetComponent<EnemyManager>().Hp -= attackPower*2;
                    }
                    else
                    {
                        enemObj.GetComponent<EnemyManager>().Hp -= attackPower;
                    }
                    
                }
            }
        }
    }


       
                
}
