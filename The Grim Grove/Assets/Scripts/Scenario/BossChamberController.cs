using UnityEngine;
using System.Collections;

public class BossChamberController : MonoBehaviour {
	public GameObject bossGate;
	public GameObject bossLight;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			bossGate.SetActive (true);
			bossLight.SetActive (true);
		}
	}
}
