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

    public GameObject[] comboPrefs;
    public Animator playerAnim;

    [System.Serializable]
    public class Combos
    {
        public int[] combo;
    }
    public Combos[] ComboList;

    //public int[] combo;

    public int[] tempSubCombo;
    public List<int> currentCombo;
    public int comboCount;
    public int fever = 0;

    public bool buttonPress = false;
    public bool checkBeat = false;
    //for preventing spam
    public int checkBeatCount = -1;
    //for checking beat presses
    public bool beatStop = false;



    void Start () {

        
       
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        //Track calibrated beat to start earlier
        if (BPM.Instance.beatCalibStop)
        {
            beatStop = true;
            //Check for combos once (checkbeat prevents multiple checks)
            if(checkBeat)
            {

                //Check  if same combos combos
                foreach (Combos tmpCombo in ComboList)
                {
                   
                    if (compArr(tempSubCombo, tmpCombo.combo))
                    {
                        int ListIDX = System.Array.IndexOf(ComboList, tmpCombo);
                        Debug.Log(ListIDX);

                        ComboInvoke(ListIDX);
                        return;
                    }

                }
                // Reset buttonPress on beat
                if (buttonPress)
                {
                    buttonPress = false;
                }
                //If nothing is pressed - reset temp combo and curr combo
                else if (!buttonPress)
                {
                    //HERE INPUT RESET PIZZAZ
                    ClearCurrentCombo();
                    comboCount = 0;
                    currentCombo.Clear();
                    fever = 0;
                }
                checkBeat = false;
            }
        }
        else
        {
            beatStop = false;
            if(!checkBeat)
            {
                checkBeat = true;
            }

        }

       
        
        //Input
        if (Input.GetKeyDown(KeyCode.Z))
        {
            buttonPress = true;
            //StartCoroutine(ButtonPress());
            Move(0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            buttonPress = true;
            //StartCoroutine(ButtonPress());
            Move(1);
        }


        
    }

    
    //Invoke combo
    public void ComboInvoke(int index)
    {
        switch (index)
        {
            case 0:
                {
                    rb.velocity = new Vector3(0, 10f, 0);
                    //pizzaz
                    Instantiate(comboPrefs[index], gameObject.transform);
                    playerAnim.SetTrigger("Walk");
                    break;
                }
            case 1:
                {
                    rb.velocity = new Vector3(10f, 0, 0);
                    //pizzaz
                    Instantiate(comboPrefs[index], gameObject.transform);
                    playerAnim.SetTrigger("Run");
                    break;
                }
            case 2:
                {
                    rb.velocity = new Vector3(-10f, 0, 0);
                    //pizzaz
                    Instantiate(comboPrefs[index],gameObject.transform);
                    playerAnim.SetTrigger("Run");
                    break;
                }
            case 3:
                {
                    rb.velocity = new Vector3(0, -25f, 0);
                    //pizzaz
                    Instantiate(comboPrefs[index], gameObject.transform);
                    break;
                }
        }

        
        //reset combo temp
        ClearCurrentCombo();
        comboCount = 0;
        currentCombo.Clear();
        checkBeat = false;
        // Fever bonus 
        fever++;
    }
    
    //Check for press timing, fever, add indexes to tempCombo
    public void Move(int button)
    {
        if(BPM.Instance.beatCountFull != checkBeatCount)
        {
            checkBeatCount = BPM.Instance.beatCountFull;
        }
        //Reset combos and fever on spam
        else
        {
            Debug.Log("CLEAR");
            //HERE INPUT RESET PIZZAZ
            ClearCurrentCombo();
            //comboCount = 0;
            //currentCombo.Clear();
          
        }

        //check for beat clicks
        if (beatStop)
        {
            ComboHit(button);
            
        }
        else
        {
            MissedCombo(button);
            
        }

        //Remember input sequence for combo
        if (comboCount == 0)
        {
            currentCombo.Add(button);
            comboCount = 1;
        }
        else
        {
            currentCombo.Add(button);
            comboCount++;
            if (comboCount >= 4)
            {
                //Make list to array and grab last 4 elements
                int[] tempCombo = currentCombo.ToArray();
                for (int i = 0; i < 4; i++)
                {
                    tempSubCombo[i] = tempCombo[tempCombo.Length - 4 + i];
                }
            }
        }
        //else
        //{
        //    comboCount = 0;
        //    fever = 0;
        //}


    }


    //Compare arrays
    private bool compArr<T, S>(T[] arrayA, S[] arrayB)
    {
        if (arrayA.Length != arrayB.Length) return false;

        for (int i = 0; i < arrayA.Length; i++)
        {
            if (!arrayA[i].Equals(arrayB[i])) return false;
        }

        return true;
    }


    //Get array's part
    public  int[] SubArray(int[] data, int index, int length)
    {
        int[] result = new int[length];
        System.Array.Copy(data, index, result, 0, length);
        return result;
    }



    // Clear tempCombo to avoid intersections
    public void ClearCurrentCombo()
    {
      
        for (int i = 0; i < tempSubCombo.Length; i++)
        {
            tempSubCombo[i] = 99;
        }
    }


    //Input within beat range
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


    //Input outside beatrange
    public void MissedCombo(int button)
    {
        //If first button was pressed
        if (button == 0)
        {
            GameObject tmp = Instantiate(missLeftPref, leftPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(leftPanel.rect.xMin, leftPanel.rect.xMax), Random.Range(leftPanel.rect.yMin, leftPanel.rect.yMax), 2);
        }
        //If second button was pressed
        else if (button == 1)
        {
            GameObject tmp = Instantiate(missRightPref, rightPanel.transform);
            tmp.transform.localPosition = new Vector3(Random.Range(rightPanel.rect.xMin, rightPanel.rect.xMax), Random.Range(rightPanel.rect.yMin, rightPanel.rect.yMax), 2);
        }
    }
}



