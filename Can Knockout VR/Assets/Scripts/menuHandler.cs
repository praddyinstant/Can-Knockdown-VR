using UnityEngine;
using System.Collections;

public class menuHandler : MonoBehaviour {
	private SteamVR_TrackedController controller;
	private SteamVR_LaserPointer laser;

	private int level1Scene = 0;
	private string homeScene = "homeScene";
	private string levelSelectScene = "levelSelect";
	private string startButtonName = "Start";
	private string levelsButtonName = "Select level";
	private string nextButtonName = "Next";
	private string retryButtonName = "Retry";
	private string homeButtonName = "Home";
	// Use this for initialization
	void Start () {
		Debug.Log ("Menu handler is active");
		controller = GetComponent<SteamVR_TrackedController>();
		laser = GetComponent<SteamVR_LaserPointer>();
		controller.TriggerClicked += selectButton;
	}

	// Acts according to the button pressed
	void selectButton(object s, ClickedEventArgs e){
		Debug.Log (laser.reference);
		if (laser.reference != null) {
			string hitName = laser.reference.name;
			if (hitName.Equals (startButtonName)) {
				sceneManager.gotoLevel (level1Scene);
			} else if (hitName.Equals (levelsButtonName)) {
				sceneManager.gotoLevel (levelSelectScene);
			} else if (hitName.Equals (nextButtonName)) {
				sceneManager.nextLevel ();
			} else if (hitName.Equals (retryButtonName)) {
				sceneManager.gotoLevel (dataHolder.currentLevel);
			} else if (hitName.Equals (homeButtonName)) {
				sceneManager.gotoLevel (homeScene);
			} else if (hitName.StartsWith("level")) {
				int levelNum = int.Parse (hitName.Substring (5))-1;
				sceneManager.gotoLevel (levelNum);
			}
		}
	}
}