using UnityEngine;
using System.Collections;

public class ballHitCanSound : MonoBehaviour {

	public AudioClip ballHitSound;
	private AudioSource audios;
	// Use this for initialization
	void Start () {
		audios = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.name.ToString ().Contains ("Can")) {
			audios.PlayOneShot (ballHitSound, 1.0F);
			//Debug.Log ("Sound Played");
		}
	}
}