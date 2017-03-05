using UnityEngine;
using System.Collections;

public class CharacterFollow : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	Transform target;
	[SerializeField]
	Vector3 distance;

	public bool Follow {
		get;
		 set;
	}
	void Start () {
		Follow = true;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{

		//transform.LookAt (target,Vector3.up);
		if(Follow)
		transform.position = target.position -  distance;
	
	}
}
