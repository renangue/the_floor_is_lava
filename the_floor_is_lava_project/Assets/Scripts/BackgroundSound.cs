using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip music;
    public bool loop;
    public bool persistent;
   
    [Range(0, 1)] public float volumeBGS;

    public Toggle BGSToggle;
    public Toggle SFXToggle;

    private void Awake()
    {
        if (!AudioManager.IsMuteBGS())
        {
            BGSToggle.isOn = false; 
        }

        else
        {
            BGSToggle.isOn = true;
            AudioManager.MuteBGS();
        }


        if (!AudioManager.IsMuteSFX())
            SFXToggle.isOn = false;

        else
        {
            SFXToggle.isOn = true;
            AudioManager.MuteSFX();
        }
    }

    void Start()
    {
        AudioManager.BGSVolume = volumeBGS;
        AudioManager.PlayBGS(music, loop, persistent);      

    }

    public void MuteBGS()
    {
        AudioManager.ToggleMuteBGS(true);
    }

    public void MuteSFX()
    {
        AudioManager.ToggleMuteSFX(true);
    }
  
}
