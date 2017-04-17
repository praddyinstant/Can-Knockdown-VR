using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private string[] scenes = {"level1","level2","level3","level4"};
	private int level = 0;

	private SteamVR_Controller.Device Controller
	{
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		}
	}

	// Use this for initialization
	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Controller.GetPressDown (Valve.VR.EVRButtonId.k_EButton_Grip)) {
			Debug.Log ("pressed");
			if (level == 3) {
				Debug.Log ("end of levels");
			}
			level++;
			SceneManager.LoadScene (scenes[level], LoadSceneMode.Single);
			Debug.Log ("level has changed");
		}
	}
}