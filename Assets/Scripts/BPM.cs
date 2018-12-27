﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour {

    public static BPM BPMInstance;

    public float bpm;
    //seconds before actual beat (calibration delay)
    public float calibration = 0f;

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
        bpmRef = bpm;
        //for BeattimerCalibration
        calibration = Mathf.Clamp(calibration, 0, 60 / bpm / 3);
        calibrationRef = calibration;
        //beatInterval for calib earlier than beat
        beatTimerD2 -= calibration;
    }

    private void Update()
    {
        BeatDetection();
    }


    public Animator beatAnim;
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

            beatAnim.SetTrigger("Beat");
            Debug.Log("BEAT");
        }

        //divided beat count
        beatD2 = false;
        
        beatTimerD2 += Time.deltaTime;
        if(beatTimerD2>=beatInterval)
        {
            beatTimerD2 -= beatInterval;
            beatD2 = true;
            beatCountD2++;
            Debug.Log("BEATD2");
            //beatAnim[1].SetTrigger("Beat");
        }
    }
}


