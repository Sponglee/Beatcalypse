using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : Singleton<PlayerManager> {

    public GameObject fltText;
    public Slider hpSlider;
    
    private float maxHp;
    [SerializeField]
    private float hp;
    public float Hp
    {
        get { return hp; }
        set
        {
            GameObject tmp = Instantiate(fltText, transform.position, Quaternion.identity);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = (hp - value).ToString();
         
            //Debug.Log("HP " + hp + " ::: " + value);
            StartCoroutine(StopSlider(value / maxHp));
            hp = value;
            if (hp <= 0)
            {
                //GAMEOVER
            }
        }
    }

    public IEnumerator StopSlider(float sliderValue)
    {
        while (hpSlider.value > sliderValue)
        {
            hpSlider.value -= 0.1f;
            yield return null;
        }
    }

    public float attackPower = 5;

    //Range Check bool
    public bool EnemiesInRange = false;
    //List of enemies
    public List<Collider> enemies;

    private void Start()
    {
        maxHp = hp;
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
