using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class Obstacles : MonoBehaviour {

	// Use this for initialization
	Rigidbody rigidBody;
	Collider coll;
	void Awake () 
	{
		rigidBody = GetComponent<Rigidbody> ();
		coll = GetComponent<Collider> ();
		rigidBody.isKinematic = true;
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	public void SetKinematics(bool kinematics)
	{
		rigidBody.isKinematic = kinematics;
	}
	public void SetTriggerCollider(bool trigguer)
	{
		coll.isTrigger = trigguer;
	}

}
