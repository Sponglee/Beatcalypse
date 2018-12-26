using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {

        if (BPM.beatFull)
        {
            StartCoroutine(StopBeat());
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

    //toggle beat off after calibration delay
    public IEnumerator StopBeat()
    {
        beatStop = true;
        //wait for 2x calibration time for calibration delay
        yield return new WaitForSeconds(BPM.calibrationRef*2);
        beatStop = false;
    }

    //Toggle 1st button press after calibration delay
    public IEnumerator StopButton0Press()
    {
        //wait for 2x calibration time for input fault
        yield return new WaitForSeconds(BPM.calibrationRef*2);
        button0Pressed = false;
    }

    public bool button0Pressed = false;
    public void Move(int button)
    {

        if (beatStop && button == 0)
        {
            button0Pressed = true;
            Debug.Log("pressed 0");
        }
        else if (beatStop && button == 1 && button0Pressed)
        {
            rb.velocity = new Vector3(0, 5f, 0);
            Debug.Log("pressed 1");
            button0Pressed = false;
        }
        else
        {
            Debug.Log("=====");
            //rb.velocity = new Vector3(0, 0, 0);
            StartCoroutine(StopButton0Press());
        }

    }
}
