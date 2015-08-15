using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	#region  Move Settings
	public float forwardVel = 12;
	public float rotateVel = 100;
	public float jumpVel = 100;
	public float distToGrounded = 0.3f;
	public float midair;
	public LayerMask ground;
	#endregion

	#region Physics Settings
	public float downAcceleration = 2f;
	#endregion

	#region Input Settings
	float forwardInput, turnInput, jumpInput;
	public float inputDelay = 0.3f;
	private string FORWARD_AXIS = "Vertical";
	private string TURN_AXIS = "Horizontal";
	private string JUMP_AXIS = "Jump";

	#endregion

	#region Variables
	private Quaternion targetRotation;
	private Rigidbody rbody;
	private Animator anim;
	private Vector3 velocity = Vector3.zero;
	public float jumpAnimationOffset = 0.3f;

	#endregion
	


	#region Properties
	public Quaternion TargetRotation{
		get { return targetRotation;}
	}	
	#endregion

	#region Unity Functions
	void Start(){
		targetRotation = transform.rotation;
		forwardInput = turnInput = jumpInput = 0;

		if (GetComponent<Rigidbody> ()) 
			rbody = GetComponent<Rigidbody> ();
		else 
			Debug.LogError("No hay un RigidBody en en Personaje");

		if (GetComponent<Animator> ())
			anim = GetComponent<Animator> ();
		else
			Debug.LogError ("El Objeto no tiene animador");
	}

	void Update(){
		GetInput ();
		Turn ();
		updateAnimation ();
	}

	void FixedUpdate(){
		Run ();
		Jump ();
		rbody.velocity = transform.TransformDirection (velocity);
	}
	#endregion

	#region Controller Functions
	private void GetInput(){
		forwardInput = Input.GetAxis (FORWARD_AXIS);
		turnInput = Input.GetAxis (TURN_AXIS);
		jumpInput = Input.GetAxisRaw (JUMP_AXIS);
	}

	private void updateAnimation(){
		anim.SetFloat ("speed", forwardInput);
		anim.SetBool ("grounded", Grounded());
		anim.SetFloat ("airVelocity", midair);
	}

	private void Run(){
		if (Mathf.Abs (forwardInput) > inputDelay) {
			//Mueve el personaje hacia adelante con respecto a su propio transform *
			velocity.z = forwardVel * forwardInput;
		} else {
			//Hace la velocidad del Rigidbody 0
			velocity.z = 0;
		}
	}

	private void Turn(){
		if (Mathf.Abs (turnInput) > inputDelay) {
			//Define nuestro objetivo de rotacion. La Rotacion se hace sobre el Eje Y global
			targetRotation *= Quaternion.AngleAxis (rotateVel*turnInput*Time.deltaTime, Vector3.up);
			transform.rotation = targetRotation;
		}		
	}

	private bool Grounded(){
		Debug.DrawRay (transform.position, Vector3.down, Color.red);
		return Physics.Raycast (transform.position, Vector3.down, distToGrounded, ground);

	}

	private void Jump(){
		//print ("Jump axis: " + jumpInput+ ", Grounded: "+Grounded());
		if (jumpInput > 0 && Grounded ()) {
			//print ("Saltar");		
			velocity.y = jumpVel;
		} else if (jumpInput == 0 && Grounded ()) {
			//print("Reducir la vel de salto a 0, y quitar el trigger de jump");
			midair = velocity.y = 0;
		} else {
			//print("Reducir la velocidad de salto");
			velocity.y -= downAcceleration;
			midair = velocity.y;
		}
	}


	#endregion

}
