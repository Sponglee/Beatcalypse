using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public GameObject fltText;
    public Slider hpSlider;
    [SerializeField]
    private float hp = 10f;
    public float Hp
    {
        get {return hp;}
        set
        {
            GameObject tmp = Instantiate(fltText,transform.position, Quaternion.identity);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = (hp - value).ToString();
            StartCoroutine(StopSlider(value / hp));
            hp = value;
            if (hp <= 0)
            {
                Destroy(gameObject);
                playerTransform.GetComponent<PlayerManager>().enemies.Remove(gameObject.GetComponent<Collider>());
            }
        }
    }

    public float attackPower = 1;
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
    void Start () {
        playerTransform = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
        
	}
}
