using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public GameObject fltText;
    public Slider hpSlider;
    public Animator enemyAnim;

    private float maxHp;
    [SerializeField]
    private float hp = 10f;
    public float Hp
    {
        get {return hp;}
        set
        {
            GameObject tmp = Instantiate(fltText,transform.position, Quaternion.identity);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = (hp - value).ToString();
            StartCoroutine(StopSlider(value / maxHp));
            hp = value;
            if (hp <= 0)
            {
                StopAllCoroutines();
                Destroy(gameObject);
                playerTransform.GetComponent<PlayerManager>().enemies.Remove(gameObject.GetComponent<Collider>());
            }
        }
    }

    public float attackPower = 1;
    public float attackSpeed = 2;
    public Transform playerTransform;
    public float speed = 1f;

    public IEnumerator StopSlider(float sliderValue)
    {
        while(hpSlider.value > sliderValue)
        {
            hpSlider.value -= 0.1f;
            yield return null;
        }
    }

    // Use this for initialization
    void Start ()
    {
        maxHp = hp;
        playerTransform = GameObject.Find("Player").transform;
        enemyAnim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        
	}



    //Check for enemies that enter the range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(Attack());
        }

    }

    //Check if enemy exists the range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Stop attack;
            StopAllCoroutines();
        }
    }


    public IEnumerator Attack()
    {
        PlayerManager tmp = playerTransform.GetComponent<PlayerManager>();
        while(true)
        {
            enemyAnim.SetTrigger("Attack");
            if(tmp.PlayerDef)
            {
                tmp.Hp -= 0;
            }
            else
            {
                tmp.Hp -= attackPower;
            }
            
            yield return new WaitForSeconds(attackSpeed);
        }
        
    }
}
