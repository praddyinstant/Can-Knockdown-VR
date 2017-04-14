using UnityEngine;
using System.Collections;

public class holdAndThrow : MonoBehaviour {
	private SteamVR_TrackedObject trackedObj;

	private GameObject collidingObj;
	private GameObject heldObj;

	private SteamVR_Controller.Device Controller{
		get{
			return SteamVR_Controller.Input ((int)trackedObj.index);
		}
	}

	void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void grabObject(){
		// if already holding something or not colliding anything, do nothing
		if(heldObj || !collidingObj) return;
		// create a fixed joint and attach the other object to the controller
		collidingObj.transform.position = gameObject.transform.position - (gameObject.transform.up * 0.02f) + (gameObject.transform.forward * 0.03f);
		FixedJoint fix = gameObject.AddComponent<FixedJoint>();
		fix.breakForce = 20000;
		fix.breakTorque = 20000;
		fix.connectedBody = collidingObj.GetComponent<Rigidbody> ();
		// update the references
		heldObj = collidingObj;
		collidingObj = null;
	}

	void releaseObject(){
		// if not holding anything return
		if(!heldObj) return;
		// set the velocity an drotation of the other object same as that of the controller
		heldObj.GetComponent<Rigidbody>().velocity = Controller.velocity;
		heldObj.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		// Remove the joint
		GetComponent<FixedJoint>().connectedBody = null;
		Destroy (GetComponent<FixedJoint> ());
		// update the references
		heldObj = null;
	}
	void Update(){
		if (Controller.GetHairTriggerDown ()) {
			grabObject ();
		}
		if (Controller.GetHairTriggerUp ()) {
			releaseObject ();
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
}