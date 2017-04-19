using UnityEngine;
using System.Collections;

public class mainMenuHandler : MonoBehaviour {
	private SteamVR_TrackedController controller;
	private SteamVR_LaserPointer laser;
	private sceneManager sceneMan;

	private string level1Scene = "level1";
	private string levelSelectScene = "levelSelect";
	private string startButtonName = "Start";
	private string levelsButtonName = "Select level";
	// Use this for initialization
	void Start () {
		controller = GetComponent<SteamVR_TrackedController>();
		laser = GetComponent<SteamVR_LaserPointer>();
		sceneMan = gameObject.AddComponent<sceneManager>();
		controller.TriggerClicked += selectButton;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Acts according to the button pressed
	void selectButton(object s, ClickedEventArgs e){
		Debug.Log (laser.reference);
		if (laser.reference != null) {
			if (laser.reference.name.Equals (startButtonName)) {
				sceneMan.gotoLevel (level1Scene);
			} else if (laser.reference.name.Equals (levelsButtonName)) {
				sceneMan.gotoLevel (levelSelectScene);
			}
		}
	}
	void printThem(object s, PointerEventArgs e){
		Debug.Log (laser.reference);
	}
}