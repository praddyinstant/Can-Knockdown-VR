using UnityEngine;
using System.Collections;

public class canHitGroundSound : MonoBehaviour {

	public AudioClip canHitSound;
	//private AudioSource audios;
	// Use this for initialization
	void Start () {
		//audios = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.name.ToString ().Contains ("Floor")) {
			//audios.PlayOneShot (canHitSound, 1.0F);
			AudioSource.PlayClipAtPoint (canHitSound, transform.position);
			//Debug.Log ("Can hit ground");
			//new WaitForSeconds ((float)100000);
			Destroy(this.gameObject);
		}
	}
}