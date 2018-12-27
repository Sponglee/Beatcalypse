using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : Singleton<BPM> {

    public static BPM BPMInstance;

    public float bpm;
    //seconds before actual beat (calibration delay)
    public float calibration = 0f;
    public bool beatD2Stop;

    //fractions of beat for D2
    public int d2N=2;

    private float beatInterval, beatTimer, beatIntervalD2, beatTimerD2;
    public bool beatFull, beatD2;
    public int beatCountFull, beatCountD2;

   
    
    //Debug

    
    private void Awake()
    {
        //Singleton
        if(BPMInstance != null && BPMInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            BPMInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        //for BeattimerCalibration
        calibration = Mathf.Clamp(calibration, 0, 60 / bpm / 3);
        beatInterval = 60 / bpm;
        //beatInterval for calib earlier than beat

    }

    private void Update()
    {
        BeatDetection();
    }


    public Animator beatAnim;
    //Detect wherever there's a beat
    void BeatDetection()
    {
        
       
        //divided beat count
        beatD2 = false;
        
       
        beatTimerD2 += Time.unscaledDeltaTime;

       
        
        if (beatTimerD2 >= beatInterval-calibration)
        {
            Debug.Log("BEAT START " + Time.timeSinceLevelLoad);
            beatTimer = beatTimerD2;
            beatTimerD2 = 0;
            beatD2 = true;
            beatD2Stop = true;
            beatCountD2++;
            //beatAnim[1].SetTrigger("Beat");
        }

        //full beat count
        beatFull = false;
        beatTimer += Time.unscaledDeltaTime;

        if (beatTimer >= beatInterval)
        {
           
            Debug.Log("BEAT " + Time.timeSinceLevelLoad);
            StartCoroutine(StopBeat());
            beatTimer =0;
            beatFull = true;
            beatCountFull++;
            //beatAnim.SetTrigger("Beat");
        }

       
        
       
    }

   
    //toggle beat off after calibration delay
    public IEnumerator StopBeat()
    {
        beatTimerD2 = 0;
        //wait for 2x calibration time for calibration delay
        yield return new WaitForSecondsRealtime(calibration);
        Debug.Log("BEAT END " + Time.timeSinceLevelLoad);
        beatD2Stop = false;
    }

}


