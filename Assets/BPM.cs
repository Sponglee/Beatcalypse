using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour {

    public static BPM BPMInstance;

    public float bpm;
    private float beatInterval, beatTimer, beatIntervalD2, beatTimerD2;

    //fractions of beat for D2
    public int d2N=2;
    public static bool beatFull, beatD2;
    public static int beatCountFull, beatCountD2;

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
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
            beatAnim[0].SetTrigger("Beat");
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
            beatAnim[1].SetTrigger("Beat");
        }
    }
}


