using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EQTriggerBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> sources;
    private AudioMixer mixer;
    

    void Start()
    {
        mixer = Resources.Load<AudioMixer>("Audio/Mixers/MasterMixer");
    }

    void Awake()
    {
        sources = new List<AudioSource>();
        sources.Add(GameObject.Find("Environment Audio Village").GetComponent<AudioSource>());
        sources.Add(GameObject.Find("Environment Audio Shore").GetComponent<AudioSource>());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("EQ_inside_Trigger"))
        {
            if (sources != null)
            {
                foreach (AudioSource s in sources)
                {
                    s.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Soundeffects/Soundeffects Dampened")[0];
                }
            }
        }
        else if (col.gameObject.tag.Equals("EQ_seminside_Trigger"))
        {
            if (sources != null)
            {
                foreach (AudioSource s in sources)
                {
                    s.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Soundeffects/Soundeffects SemiDampened")[0];
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals("EQ_inside_Trigger"))
        {
            if (sources != null)
            {
                foreach (AudioSource s in sources)
                {
                    s.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Soundeffects")[0];
                }
            }
        }
        else if (col.gameObject.tag.Equals("EQ_seminside_Trigger"))
        {
            if (sources != null)
            {
                foreach (AudioSource s in sources)
                {
                    s.outputAudioMixerGroup = mixer.FindMatchingGroups("Master/Soundeffects")[0];
                }
            }
        }
    }
}
