using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootStep : MonoBehaviour
{
    //[SerializeField]
    //private AudioClip[] dirtClips;
    //[SerializeField]
    //private AudioClip[] woodClips;
    //[SerializeField]
    //private AudioClip[] grassClips;
    //[SerializeField]
    //private AudioClip[] waterClips;

    //instead of listing all the type of elements we create a class//
    [System.Serializable]
    public class StepSoundsPool
    {
        public string Tag;
        public List<AudioClip> audios;
    }


    public List<StepSoundsPool> soundsPools;

    public Dictionary<string, List<AudioClip>> poolDictionary;

    //[SerializeField]
    //private AudioClip[] audioClips;
    private AudioSource audioSource;
    [HideInInspector]
    public bool isPlayingFootStep;

    private TerrainDetector terrainDetector;

    private void Awake()
    {
        //initialze the soundsPool here in code instead:

        soundsPools = new List<StepSoundsPool>();
        StepSoundsPool element = null;
        for (int i = 0; i < 4; i++) {
            element = new StepSoundsPool();
            element.audios = new List<AudioClip>();
            switch (i)
            {
                case 0:
                    element.Tag = "dirt";
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Dirt/STEPSDirt,Walk01"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Dirt/STEPSDirt,Walk02"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Dirt/STEPSDirt,Walk03"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Dirt/STEPSDirt,Walk04"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Dirt/STEPSDirt,Walk05"));

                    break;
                case 1:
                    element.Tag = "wood";
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Wood/Wood,Creaky/STEPSWood,Creaky,Walk01"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Wood/Wood,Creaky/STEPSWood,Creaky,Walk02"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Wood/Wood,Creaky/STEPSWood,Creaky,Walk03"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Wood/Wood,Creaky/STEPSWood,Creaky,Walk04"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Wood/Wood,Creaky/STEPSWood,Creaky,Walk05"));
                    break;
                case 2:
                    element.Tag = "grass";
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Bush/STEPSBush,Walk01"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Bush/STEPSBush,Walk02"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Bush/STEPSBush,Walk03"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Bush/STEPSBush,Walk04"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Bush/STEPSBush,Walk05"));
                    break;
                case 3:
                    element.Tag = "water";
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Water/STEPSWater,Walk01"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Water/STEPSWater,Walk02"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Water/STEPSWater,Walk03"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Water/STEPSWater,Walk04"));
                    element.audios.Add(Resources.Load<AudioClip>("Audio/Sounds/High_Quality_Footsteps/Water/STEPSWater,Walk05"));
                    break;
                default:
                    break;
            }
            
            soundsPools.Add(element);
        }
        
        poolDictionary = new Dictionary<string, List<AudioClip>>();
        //or pls reference soundsPools in the Unity Editor
        foreach (StepSoundsPool pool in soundsPools)
        {
            //added to the dictionary
            poolDictionary.Add(pool.Tag, pool.audios);
        }
        //set tags 
        setTagtoAllChildren("dirt");
        setTagtoAllChildren("wood");
        setTagtoAllChildren("grass");
        setTagtoAllChildren("water");

        audioSource = GetComponent<AudioSource>();
        terrainDetector = FindObjectOfType<TerrainDetector>();
        //pls reference soundsPools in the Unity Editor
    }

    private void Step()
    {
        if (terrainDetector.OnGround())
        {
            AudioClip clip = null;
            if (terrainDetector.OnGroundType != null)
            {
                switch (terrainDetector.OnGroundType)
                {
                    case "dirt":
                        clip = GetRandomClipFromPool("dirt");
                        break;
                    case "wood":
                        clip = GetRandomClipFromPool("wood");
                        break;
                    case "grass":
                        clip = GetRandomClipFromPool("grass");
                        break;
                    case "water":
                        clip = GetRandomClipFromPool("water");

                        break;
                }
            }
            //adjust the water sound
            if (clip != null) { 
                if(terrainDetector.OnGroundType == "water")
                    audioSource.volume = 0.45f;
                else
                    audioSource.volume = 1.0f;

                audioSource.PlayOneShot(clip);
                //Debug.Log(terrainDetector.curCollision.gameObject.tag+"steps played!");
            }
            isPlayingFootStep = true;
        }
        else
        {
            isPlayingFootStep = false;
        }
    }

    //randomization from the audio pool
    //private AudioClip GetRandomClip()
    //{
    //    return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
    //}
    //more efficient code
    private AudioClip GetRandomClipFromPool(string Tag)
    {
        if(!poolDictionary.ContainsKey(Tag)||Tag == null)
        {
            Debug.LogWarning("pool:" + Tag + "didn't exist!");
            return null;
        }
        List<AudioClip> clips = poolDictionary[Tag];
        AudioClip randomizedOne = clips[UnityEngine.Random.Range(0, clips.Count)];
        return randomizedOne;
    }

    private void setTagtoAllChildren(string Tag)
    {
        GameObject[] parents = GameObject.FindGameObjectsWithTag(Tag);
        foreach(GameObject par in parents)
        {
            foreach (Transform t in par.transform)
            {
                foreach(Transform f in t.gameObject.transform)
                {
                    foreach (Transform i in f.gameObject.transform)
                    {
                        i.gameObject.tag = Tag;
                    }
                    f.gameObject.tag = Tag;
                }
                t.gameObject.tag = Tag;
            }
            par.tag = Tag;
        }
    }


}
