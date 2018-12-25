/*
 * Copyright (c) 2015 Allan Pichardo
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System;

public class Example : MonoBehaviour
{
    private AudioProcessor processor;

    void Start ()
	{
		//Select the instance of AudioProcessor and pass a reference
		//to this object
		processor = FindObjectOfType<AudioProcessor> ();
		processor.onBeat.AddListener (onOnbeatDetected);
		processor.onSpectrum.AddListener (onSpectrum);
	}

	//this event will be called every time a beat is detected.
	//Change the threshold parameter in the inspector
	//to adjust the sensitivity
	void onOnbeatDetected ()
	{
        //Debug.Log ("Beat!!!");
        processor.changeCameraColor();
    }

    public Transform[] spectr;
	//This event will be called every frame while music is playing
	public void onSpectrum (float[] spectrum)
	{
        Debug.Log("SPECTRUM!! "+ spectrum.Length);
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i) {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i] * 10f, 0);
            Debug.DrawLine (start, end);

            //if(i<=4)
            //{
            //    spectr[i].GetChild(0).position = end;
            //    Debug.Log(spectrum[3]);
            //}
            
		}
	}
}
