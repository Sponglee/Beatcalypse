using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;



    public RectTransform leftPanel;
    public RectTransform rightPanel;
    public GameObject bamLeftPref;
    public GameObject bamRightPref;
    public GameObject missLeftPref;
    public GameObject missRightPref;
    public GameObject comboPref;

    public int[] combo;
    public int comboCount;
    public int fever = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        //Track calibrated beat to start earlier
        if (BPM.Instance.beatD2Stop)
        {
            beatStop = true;
        }
        else
        {
            beatStop = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Move(0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Move(1);
        }

    }
    public bool beatStop = false;

    #region calibrated beats
    
    

    ////Toggle 1st button press after calibration delay
    //public IEnumerator StopButton0Press()
    //{
    //    //wait for 2x calibration time for input fault
    //    yield return new WaitForSeconds(BPM.Instance.calibration * 2);
    //    button0Pressed = false;
    //}




    //public bool button0Pressed = false;

    public void Move(int button)
    {
        //check for beat clicks
        if (beatStop)
        {
            fever++;
        }
        else
        {
            fever = 0;
        }
        //Check for combo
        if ( button == combo[comboCount] && comboCount == 0)
        {
            comboCount = 1;
            //button0Pressed = true;
            //Debug.Log("pressed 0");

            if(beatStop)
            {
                ComboHit(button);
            }
            else
            {
                MissedCombo(button);
            }
            
        }
        else if (button == combo[comboCount] /*&& button0Pressed*/)
        {
            if (beatStop)
            {
                ComboHit(button);
            }
            else
            {
                MissedCombo(button);
            }

            comboCount++;
            if(comboCount>= combo.Length)
            {
                //reset combo
                comboCount = 0;
                Instantiate(comboPref);
                rb.velocity = new Vector3(0, -25f, 0);
                return;
            }

           
            rb.velocity = new Vector3(0, 5f, 0);
            Debug.Log("pressed 1");
            
            //button0Pressed = false;
        }
        else
        {
            comboCount = 0;
            fever = 0;
        }
        
   
       
    }

    public void ComboHit(int button)
    {
        if (button == 0)
        {
            GameObject tmp = Instantiate(bamLeftPref, leftPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(leftPanel.rect.xMin, leftPanel.rect.xMax), Random.Range(leftPanel.rect.yMin, leftPanel.rect.yMax), 2);
        }
        else if (button == 1)
        {
            GameObject tmp = Instantiate(bamRightPref, rightPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(rightPanel.rect.xMin, rightPanel.rect.xMax), Random.Range(rightPanel.rect.yMin, rightPanel.rect.yMax), 2);
        }
    }


    public void MissedCombo(int button)
    {
        //Debug.Log("=====");
        ////reset combo
        //comboCount = 0;
        if (button == 0)
        {
            GameObject tmp = Instantiate(missLeftPref, leftPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(leftPanel.rect.xMin, leftPanel.rect.xMax), Random.Range(leftPanel.rect.yMin, leftPanel.rect.yMax), 2);
            //StartCoroutine(StopButton0Press());
        }
        else if (button == 1)
        {
            GameObject tmp = Instantiate(missRightPref, rightPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(rightPanel.rect.xMin, rightPanel.rect.xMax), Random.Range(rightPanel.rect.yMin, rightPanel.rect.yMax), 2);
            //StartCoroutine(StopButton0Press());
        }
    }
    #endregion
}
