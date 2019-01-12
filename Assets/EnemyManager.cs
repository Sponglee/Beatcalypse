using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public Transform playerTransform;


    private void Start()
    {
        playerTransform = GameObject.Find("Players").transform;

    }

}
