using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {
	private string[] scenes = {"level1","level2","level3","level4"};
	private int level = 0;

	public void gotoLevel(string levelName){
		SceneManager.LoadScene(levelName, LoadSceneMode.Single);
	}

	public void nextLevel(){
		SceneManager.LoadScene(scenes[level], LoadSceneMode.Single);
		if (level < scenes.Length - 1)
			level++;
		else
			level = 0;
	}

//	// Use this for initialization
//	void Awake()
//	{
//		trackedObj = GetComponent<SteamVR_TrackedObject>();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//		if (Controller.GetPressDown (Valve.VR.EVRButtonId.k_EButton_Grip)) {
//			Debug.Log ("pressed");
//			if (level == 3) {
//				Debug.Log ("end of levels");
//			}
//			level++;
//			SceneManager.LoadScene (scenes[level], LoadSceneMode.Single);
//			Debug.Log ("level has changed");
//		}
//	}
}