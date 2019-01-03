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
    public int[] tempSubCombo;
    public List<int> currentCombo;
    public int comboCount;
    public int fever = 0;

    public bool buttonPress = false;

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


        //if(BPM.Instance.beatD2Stop && !buttonPress)
        //{
        //    ClearCurrentCombo();
        //}


        if (Input.GetKeyDown(KeyCode.Z))
        {
            buttonPress = true;
            Move(0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            buttonPress = true;
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
            ComboHit(button);
            fever++;
        }
        else
        {
            MissedCombo(button);
            fever = 0;
        }

        //Check for combo
        if (comboCount == 0)
        {
            //Lock thread to avoid errors
            
                currentCombo.Add(button);
            comboCount = 1;
            //button0Pressed = true;
            //Debug.Log("pressed 0");
        }
        else 
        {
          
            currentCombo.Add(button);
            comboCount++;
            if(comboCount >= 4)
            {
               
                int[] tempCombo = currentCombo.ToArray();
                Debug.Log(tempCombo.Length + " ::: " + combo);

                
                for (int i = 0; i < 4; i++)
                {
                    tempSubCombo[i] = tempCombo[tempCombo.Length - 4 + i];
                }
                //int[] temp = System.Array.ConvertAll(comboString.Split(','), int.Parse);
                if (compArr(tempSubCombo, combo))
                {
                    Instantiate(comboPref);
                    rb.velocity = new Vector3(0, -25f, 0);
                    //reset combo
                    //comboCount = 0;
                    ClearCurrentCombo();
                    for (int i = 0; i < tempSubCombo.Length; i++)
                    {
                        tempSubCombo[i] = 99;
                    }
                    return;
                }
                else
                {
                    for (int i = 0; i < tempSubCombo.Length; i++)
                    {
                        tempSubCombo[i] = 99;
                    }
                    return;
                }
                   
            }
            //rb.velocity = new Vector3(0, 5f, 0);
            //Debug.Log("pressed 1");
            
            ////button0Pressed = false;
        }
        //else
        //{
        //    comboCount = 0;
        //    fever = 0;
        //}
        
   
       
    }


   
    private bool compArr<T, S>(T[] arrayA, S[] arrayB)
    {
        if (arrayA.Length != arrayB.Length) return false;

        for (int i = 0; i < arrayA.Length; i++)
        {
            if (!arrayA[i].Equals(arrayB[i])) return false;
        }

        return true;
    }

    public  int[] SubArray(int[] data, int index, int length)
    {
        int[] result = new int[length];
        System.Array.Copy(data, index, result, 0, length);
        return result;
    }




    public void ClearCurrentCombo()
    {
        comboCount = 0;
        currentCombo.Clear();
       
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



