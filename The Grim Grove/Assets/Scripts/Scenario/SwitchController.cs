using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {
	public GameObject target;
	public GameObject enable;
	public GameObject nextLocation;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			//print("Player is standing in switch");
			Destroy(target);
			Destroy(this.gameObject);
			if(enable != null)enable.SetActive(true);
			if(nextLocation != null){nextLocation.SetActive(true);}
		}
	}

}
