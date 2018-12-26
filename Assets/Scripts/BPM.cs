using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour {

    public static BPM BPMInstance;

    public float bpm;
    //seconds before actual beat (calibration delay)
    public float calibration = 0.2f;

    private float beatInterval, beatTimer, beatIntervalD2, beatTimerD2;

    //fractions of beat for D2
    public int d2N=2;
    public static bool beatFull, beatD2;
    public static int beatCountFull, beatCountD2;

    //Static references for accessing 
    public static float bpmRef;
    public static float calibrationRef;

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
        calibration = Mathf.Clamp(calibration, 0, 60 / bpm /3);
        bpmRef = bpm;
        calibrationRef = calibration;
        beatTimer -= calibration;
    }

    private void Update()
    {
        BeatDetection();
    }


    public Animator[] beatAnim;
    //Detect wherever there's a beat
    void BeatDetection()
    {
        
        //full beat count
        beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;
        if(beatTimer >= beatInterval)
        {
            beatTimer -=beatInterval;
            beatFull = true;
            beatCountFull++;

            beatAnim[beatCountFull%beatAnim.Length].SetTrigger("Beat");
            //Debug.Log("BEAT");
        }
        //divided beat count
        beatD2 = false;
        beatIntervalD2 = beatInterval / d2N;
        beatTimerD2 += Time.deltaTime;
        if(beatTimerD2>=beatIntervalD2)
        {
            beatTimerD2 -= beatIntervalD2;
            beatD2 = true;
            beatCountD2++;
            //beatAnim[1].SetTrigger("Beat");
        }
    }
}


