using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPM : Singleton<BPM> {

    public static BPM BPMInstance;

    public GameManager gameManagerRef;
    public Image border;
    [SerializeField]
    private float bpm;
    public float Bpm
    {
        get
        {
            return bpm;
        }

        set
        {
            bpm = value;
            //for BeattimerCalibration
            beatInterval = 60 / bpm;
            //beatInterval for calib earlier than beat
            calibration = beatInterval / 3;

        }
    }
    //seconds before actual beat (calibration delay)
    public float calibration = 0f;
    public bool beatCalibStop;
    public bool beatEnd = false;

    //fractions of beat for Calibrated beat
    public int CalibN=2;

    private float beatInterval, beatTimer, beatIntervalCalib, beatTimerCalib;
    public bool beatFull, beatCalib;
    public int beatCountFull, beatCountCalib;

   
    
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
        beatInterval = 60 / bpm;
        //beatInterval for calib earlier than beat
        calibration = beatInterval / 3;

    }

    private void Start()
    {
        gameManagerRef = GameManager.Instance;
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
        beatCalib = false;
        
       
        beatTimerCalib += Time.unscaledDeltaTime;

       
        
        if (beatTimerCalib >= beatInterval-calibration)
        {
            //Debug.Log("BEAT START " + Time.timeSinceLevelLoad);
            beatTimer = beatTimerCalib;
            beatTimerCalib = 0;
            beatCalib = true;
            beatCalibStop = true;
            beatCountCalib++;

            //FLASH THE BORDER
            if(gameManagerRef.ActionInProgress)
            {
                border.color = Color.gray;
                StartCoroutine(StopColor(Color.gray));
            }
            else if(GameManager.Instance.Fever<4)
            {

                border.color = Color.white;
                StartCoroutine(StopColor(Color.white));
            }
            else
            {
                border.color = Color.yellow;
                StartCoroutine(StopColor(Color.yellow));
            }
            
          
            ////////////////////
        }

        //full beat count
        beatFull = false;
        beatTimer += Time.unscaledDeltaTime;

        if (beatTimer >= beatInterval)
        {

            //Debug.Log("BEAT " + Time.timeSinceLevelLoad);
            StartCoroutine(StopBeat());
            beatEnd = false;
            beatTimer =0;
            beatFull = true;
            beatCountFull++;
            //beatAnim.SetTrigger("Beat");


        }
     
        
       
    }

   
   
    public IEnumerator StopColor(Color col)
    {
        float t = 0f;
        float duration = calibration;

        while (border.color != Color.black)
        {
            border.color = Color.Lerp(col, Color.black, t);
            t += Time.deltaTime / duration;
            //Debug.Log("EEEERE");
            yield return new WaitForSecondsRealtime(0.01f);
        }
        
    }
   
    //toggle beat off after calibration delay
    public IEnumerator StopBeat()
    {
       
        beatTimerCalib = 0;
        //wait for 2x calibration time for calibration delay
        yield return new WaitForSecondsRealtime(calibration);
        //Debug.Log("BEAT END " + Time.timeSinceLevelLoad);
        beatCalibStop = false;
        beatEnd = true;
    }

}


