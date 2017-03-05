using UnityEngine;
using System.Collections;

public class SpherePoint : MonoBehaviour {

	// Use this for initialization


	[SerializeField]
	float distance=1;
	[SerializeField]
	float points=1;
	FloatInterpolation interpolation;
	void Start ()
	{
		int randomValue = 0;
		do {
			randomValue = Random.Range (-1, 1);
		} while (randomValue == 0);
		Debug.Log (randomValue);
		distance *= randomValue;	
		interpolation = new FloatInterpolation (transform.position.y, GetTargetY());
		interpolation.SwitchLerp (FloatInterpolation.LerpMode.Smoothstep);
		//interpolation.LerpTime = 2;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		Levitation ();
	}
	void Levitation()
	{
		interpolation.UpdateLerp ();
		transform.position =new Vector3(transform.position.x, interpolation.InterpolatedValue,transform.position.z);

		if (transform.position.y == interpolation.EndPos)
		{
			distance *= -1;
			interpolation = new FloatInterpolation (transform.position.y, GetTargetY());
		}

	}
	void OnTriggerEnter(Collider coll)
	{
		Debug.Log ("ENTEEEER: "+ coll.tag);
		if (coll.gameObject.tag.Equals ("Player")) 
		{
			CatchSphere ();
		}
		
	}
	float GetTargetY()
	{
		float targetY = transform.position.y + distance;
		return targetY;
		
	}
	protected void CatchSphere()
	{
		//efecto visual posible en este metodo
		LevelManager.Instance.AddPoint (points);
		Destroy (gameObject);

	}
}
