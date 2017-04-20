using UnityEngine;
using System.Collections;

public class holdAndThrow : MonoBehaviour {
	private SteamVR_TrackedObject trackedObj;

	private GameObject collidingObj;
	private GameObject heldObj;
	private bool grabbed;
	private bool levelComplete;
	private int ballsRemaining = 3;
	private int curScore = 0;
	private Time ballPicked;
	private TextMesh ballsDisplay;
	private TextMesh scoreDisplay;

	private SteamVR_Controller.Device Controller{
		get{
			return SteamVR_Controller.Input ((int)trackedObj.index);
		}
	}

	void Start () {
		levelComplete = false;
		//text = gameObject.GetComponent ("TextMesh") as TextMesh;
		ballsDisplay = GameObject.Find("BallsRemValue").GetComponent<TextMesh>();
		//ballDisp.text = "100";

		scoreDisplay = GameObject.Find ("ScoreValue").GetComponent<TextMesh>();
		//scoreDisp.text = "50";
	}
	void printSome(object sender, ClickedEventArgs e){
		Debug.Log ("pad clicked");
	}
	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void calcScore(){
		//Debug.Log (Time.realtimeSinceStartup);
		new WaitForSeconds ((float)100);
		//Debug.Log (Time.realtimeSinceStartup);
		curScore = 50;
		scoreDisplay.text = curScore.ToString ();
		ballsDisplay.text = ballsRemaining.ToString ();
	}

	void grabObject(){
		// if already holding something or not colliding anything, do nothing
		if(heldObj || !collidingObj) return;
		// create a fixed joint and attach the other object to the controller
		collidingObj.transform.position = gameObject.transform.position - (gameObject.transform.up * 0.02f) + (gameObject.transform.forward * 0.03f);
		grabbed = true;
		collidingObj.transform.position = gameObject.transform.position;
		FixedJoint fix = gameObject.AddComponent<FixedJoint>();
		fix.breakForce = 20000;
		fix.breakTorque = 20000;
		fix.connectedBody = collidingObj.GetComponent<Rigidbody> ();
		Debug.Log ("in Grab object");
		//Haptic feedback on grabbing the object
		SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(3000);
		// update the references
		heldObj = collidingObj;
		collidingObj = null;
	}

	void releaseObject(){
		// if not holding anything return
		Debug.Log(heldObj);
		if(!heldObj) return;
		Debug.Log("Releasing obj");
		grabbed = false;
		// set the velocity an drotation of the other object same as that of the controller
		heldObj.GetComponent<Rigidbody>().velocity = Controller.velocity;
		heldObj.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		// Remove the joint
		GetComponent<FixedJoint>().connectedBody = null;
		Destroy (GetComponent<FixedJoint> ());
		// update the references
		heldObj = null;
		// Updating the remaining Balls
		ballsRemaining--;

		// Updating the Score
		calcScore ();
		//Debug.Log (ballsRemaining);
	}

	void Update(){
		if (GameObject.FindGameObjectsWithTag ("can").Length == 0 && !levelComplete) {
			levelComplete = true;
			Debug.Log ("Scene Over");
		}
		if (Controller.GetHairTriggerDown ()) {
			grabObject ();
		}
		if (Controller.GetHairTriggerUp ()) {
			releaseObject ();
		}

//		laser.po
//		if (laser.controller != null && controller.TriggerClicked())
//		{
//			if(bHit) {
//				Debug.Log (hit.collider.name);
//			}
//		}

	}


	void OnCollisionEnter(Collision other){
		// if colliding object is present, do nothing
		// else update colliding body
		//Debug.Log("in OnCollisionEnter");

		SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(3000);
		SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(3000);
		SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(3000);

	}
	void OnCollisionStay(Collision other){
		// if colliding object is present, do nothing
		// else update colliding body
		//Debug.Log("in OnCollision Stay");
		if(!grabbed){
			SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(3000);
		 	SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(3000);
			SteamVR_Controller.Input ((int)trackedObj.index).TriggerHapticPulse(3000);
		}
	}

	void OnTriggerEnter(Collider other){
		// if colliding object is present, do nothing
		// else update colliding body
		if (!collidingObj && other.GetComponent<Rigidbody>()) {
			collidingObj = other.gameObject;
		}
	}
	void OnTriggerStay(Collider other){
		// if colliding object is present, do nothing
		// else update colliding body
		if (!collidingObj && other.GetComponent<Rigidbody>()) {
			collidingObj = other.gameObject;
		}
	}
	void OnTriggerExit(Collider other){
		// if colliding object is present, set the colliding object to null
		if (collidingObj) {
			collidingObj = null;
		}
		// else do nothing
	}


	//length is how long the vibration should go for
	//strength is vibration strength from 0-1
	IEnumerator LongVibration(float length, float strength) {
		for(float i = 0; i < length; i += Time.deltaTime) {
			SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
			yield return null;
		}
	}


}