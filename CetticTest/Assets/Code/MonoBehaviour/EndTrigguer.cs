using UnityEngine;
using System.Collections;

public class EndTrigguer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider coll)
	{
		
		LevelManager.Instance.EndGame (true);

	}
}
