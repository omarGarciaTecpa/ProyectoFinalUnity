using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	#region  Variables
	public Transform target;
	public float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0,1.23f,-2.18f);
	public float xTilt = 10;

	Vector3 destination = Vector3.zero;
	CharacterController charController;
	float rotateVel;
	#endregion

	#region Properties
	#endregion
	
	#region Unity Functions
	void Start(){
		SetCameraTarget (target);

	}

	//Se ejecuta despues de Update y FixedUpdate
	void LateUpdate(){
		//Moviendose
		MoveToTarget ();
		//Rotando
		LookAtTarget ();
	}	
	#endregion
	
	#region Controller Functions
	public void SetCameraTarget(Transform t){
		target = t;
		if (target == null) {
			Debug.LogError ("No hay target para la camara");
		} else {
			if(target.GetComponent<CharacterController>()){
				charController = target.GetComponent<CharacterController>();
			}else{
				Debug.Log ("El target no tiene un Character controller");
			}
		}
	}

	void MoveToTarget(){
		destination = charController.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget(){
		float eulerYAngle = Mathf.SmoothDampAngle (transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler (transform.eulerAngles.x,eulerYAngle,0);
	}
	#endregion
}
