using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {


    public GameObject fltText;
    public Slider hpSlider;
    public Animator enemyAnim;

    private float maxHp;
    [SerializeField]
    private float hp = 10f;
    public float Hp
    {
        get { return hp; }
        set
        {
            GameObject tmp = Instantiate(fltText, transform.position, Quaternion.identity);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = (hp - value).ToString();
            StartCoroutine(StopSlider(value / maxHp));
            hp = value;
            if (hp <= 0)
            {
                StopAllCoroutines();
                foreach (Transform enem in transform.parent.GetComponent<EnemyManager>().playerTransform)
                {
                    enem.GetComponent<PlayerController>()
                    .enemies.Remove(gameObject.GetComponent<Collider>());
                }
                Destroy(gameObject);
            }
        }
    }


    //Range Check bool
    public bool PlayersInRange = false;
    //List of enemies
    public List<Collider> players;


    public float attackPower = 1;
    public float attackSpeed = 2;
    public float speed = 1f;
   

    //Target
    [SerializeField]
    PlayerController target;
    PlayerManager playerRef;

    public IEnumerator StopSlider(float sliderValue)
    {
        while (hpSlider.value > sliderValue)
        {
            hpSlider.value -= 0.1f;
            yield return null;
        }
    }

    // Use this for initialization
    void Start()
    {
        maxHp = hp;
        enemyAnim = gameObject.GetComponent<Animator>();
        //Reference for player holder
        playerRef = GameObject.Find("Players").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }



    //Check for enemies that enter the range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NOW " + other.gameObject.name);
            target = other.transform.parent.GetComponent<PlayerController>();
            StartCoroutine(Attack());
        }

    }

    //Check if enemy exists the range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            //Stop attack;
            StopAllCoroutines();
        }
    }


    public IEnumerator Attack()
    {

        while (target != null)
        {
            enemyAnim.SetTrigger("Attack");
            if (playerRef.PlayerDef)
            {
                target.Hp -= 0;
            }
            else
            {
                target.Hp -= attackPower;
            }

            yield return new WaitForSeconds(attackSpeed);
        }
      

    }
}
