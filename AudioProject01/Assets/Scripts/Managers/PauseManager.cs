using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{
		
	Canvas canvas;
	
	void Start()
	{
		canvas = GetComponent<Canvas>();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			canvas.enabled = !canvas.enabled;
			Pause();
		}
	}
	
	public void Pause()
	{
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        if (Time.timeScale == 0) //game is being paused
        {
            //Debug.Log("0");
            GetComponent<MixLevels>().SetSfxLvl(-80);
            GetComponent<MixLevels>().SetMusicLvl(-80);
        }
        else
        {
            GetComponent<MixLevels>().SetSfxLvl(-16.41f);
            GetComponent<MixLevels>().SetMusicLvl(0);
        }
	}
		
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
