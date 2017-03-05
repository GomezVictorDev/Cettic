using UnityEngine;
using System.Collections;

public class InputController : PersistentHumbleSingleton<InputController> {

	// Use this for initialization
	[SerializeField]
	CharacterBehaviour character;
	bool enableCharacterInputs=true;


	public bool EnableCharacterInputs
	{
		set{  enableCharacterInputs= value; }
		get{return  enableCharacterInputs; }
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontalAxis = Input.GetAxisRaw ("Horizontal");
		if ( EnableCharacterInputs) 
		{
			
			if (Input.GetButtonDown ("Horizontal") && horizontalAxis > 0) {


				character.MoveRight ();
				//	transform.position = newPosition;


			}
			if (Input.GetButtonDown ("Horizontal") && horizontalAxis < 0) {


				character.MoveLeft ();
				//	transform.position = newPosition;


			}
		}

	
	}
}
