using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Singleton<PlayerManager> {



  
    public Transform enemyTransform;



    //Defence trigger 
    private bool playerDef = false;

    public bool PlayerDef
    {
        get
        {
            return playerDef;
        }

        set
        {
            playerDef = value;
            
        }
    }

    private void Start()
    {
        enemyTransform = GameObject.Find("Enemies").transform;
        
    }

    public void PlayerAttack(bool supercharge = false)
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<PlayerController>().UnitAttack(supercharge);
        }
    }

   

    







}
