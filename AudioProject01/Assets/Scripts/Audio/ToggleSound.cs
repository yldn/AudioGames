using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour
{
    public  Toggle              audioToggle;
    public  AudioMixer          mixer;
    public  AudioMixerSnapshot  disabled;
    private AudioMixerSnapshot  lastSnapshot;
    private bool                audioEnabled; 

    void Start()
    {
        lastSnapshot = mixer.FindSnapshot("Unpaused");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || !audioToggle.isOn)
        {
            disabled.TransitionTo(0.0f);
        }
        if(Input.GetKeyDown(KeyCode.L) || audioToggle.isOn)
        {
            lastSnapshot.TransitionTo(0.0f);
        }
    }
    
    
}
