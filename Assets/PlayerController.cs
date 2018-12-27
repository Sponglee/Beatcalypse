using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;



    public RectTransform leftPanel;
    public RectTransform rightPanel;
    public GameObject bamPref;
    public GameObject missPref;

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
    
    

    //Toggle 1st button press after calibration delay
    public IEnumerator StopButton0Press()
    {
        //wait for 2x calibration time for input fault
        yield return new WaitForSeconds(BPM.Instance.calibration * 2);
        button0Pressed = false;
    }

    public bool button0Pressed = false;
    public void Move(int button)
    {

        if (beatStop && button == 0)
        {
            button0Pressed = true;
            Debug.Log("pressed 0");
            GameObject tmp = Instantiate(bamPref,leftPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(leftPanel.rect.xMin, leftPanel.rect.xMax), Random.Range(leftPanel.rect.yMin, leftPanel.rect.yMax), 2);
        }
        else if (beatStop && button == 1 && button0Pressed)
        {
            rb.velocity = new Vector3(0, 5f, 0);
            Debug.Log("pressed 1");
            GameObject tmp = Instantiate(bamPref, rightPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(rightPanel.rect.xMin, rightPanel.rect.xMax), Random.Range(rightPanel.rect.yMin, rightPanel.rect.yMax), 2); 
            button0Pressed = false;
        }
        else
        {
            Debug.Log("=====");

            if(button == 0)
            {
                GameObject tmp = Instantiate(missPref, leftPanel.transform);
                tmp.transform.localPosition = new Vector3(Random.Range(leftPanel.rect.xMin, leftPanel.rect.xMax), Random.Range(leftPanel.rect.yMin, leftPanel.rect.yMax), 2);
                StartCoroutine(StopButton0Press());
            }
            else if (button == 1)
            {
                GameObject tmp = Instantiate(missPref, rightPanel.transform);
                tmp.transform.localPosition = new Vector3(Random.Range(rightPanel.rect.xMin, rightPanel.rect.xMax), Random.Range(rightPanel.rect.yMin, rightPanel.rect.yMax), 2);
                StartCoroutine(StopButton0Press());
            }
           
        }

    }
    #endregion
}
