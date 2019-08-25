using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
   
    private List<AudioClip> musicClipsA;
    private List<AudioClip> musicClipsB;
    [SerializeField]
    private AudioClip startingClip;
    private int curType;
    [SerializeField]
    private AudioSource musicSource;

    void Start ()
    {
        curType = 0;
        LoadAudioClips();

        musicSource.clip = startingClip;
        musicSource.Play();
	}
	

	void Update ()
    {
        if (!musicSource.isPlaying)
        {
            SwitchAudioClip();
        }
	}

    private void LoadAudioClips()
    {
        musicClipsA = new List<AudioClip>();
        musicClipsB = new List<AudioClip>();
        musicClipsA.Add(Resources.Load<AudioClip>("Audio/Music/CalmBeforeTheStorm_O"));
        musicClipsA.Add(Resources.Load<AudioClip>("Audio/Music/CalmBeforeTheStorm_A"));
        musicClipsA.Add(Resources.Load<AudioClip>("Audio/Music/CalmBeforeTheStorm_B"));
        musicClipsB.Add(Resources.Load<AudioClip>("Audio/Music/CalmBeforeTheStorm_C"));
        musicClipsB.Add(Resources.Load<AudioClip>("Audio/Music/CalmBeforeTheStorm_D"));
        startingClip = musicClipsA[0];
    }

    private void SwitchAudioClip()
    {
        if (curType == 0)
        {
            int x = Random.Range(0, 3);

            switch (x)
            {
                case 0:
                case 1:
                    curType = 0;
                    musicSource.clip = musicClipsA[Random.Range(0, musicClipsA.Count-1)];
                    break;
                case 2:
                    curType = 1;
                    musicSource.clip = musicClipsB[Random.Range(0, musicClipsA.Count-1)];
                    break;
            }
        }
        else
        {
            int x = Random.Range(0, 3);

            switch (x)
            {
                case 0:
                    curType = 0;
                    musicSource.clip = musicClipsA[Random.Range(0, musicClipsA.Count-1)];
                    break;
                case 1:
                case 2:
                    curType = 1;
                    musicSource.clip = musicClipsB[Random.Range(0, musicClipsA.Count-1)];
                    break;
            }
        }

        musicSource.Play();
    }
}
