using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer VolMixer;

    public void SetLevelOfVolume(float SliderValue){

        VolMixer.SetFloat("MusicVol",Mathf.Log10(SliderValue)*20);

    }
}
