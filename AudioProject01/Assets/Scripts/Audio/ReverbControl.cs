using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbControl : MonoBehaviour
{
    public AudioReverbZone reverbzone;

    private void OnTriggerEnter(Collider other)
    {
        reverbzone.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        reverbzone.enabled = false;
    }
   
}
