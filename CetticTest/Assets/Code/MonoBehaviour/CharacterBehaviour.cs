using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour,CanTakeDamage {

	// Use this for initialization
	CharacterController characterController;
	float initialY;
	[SerializeField]
	float health=1;
	[SerializeField]
	int collisionDamage=1;
	[SerializeField]
	float startSpeed=10;
	const float  maxSpeed=20f, minSpeed=-10;
	Vector3 velocity= Vector3.zero;
	Vector3 oldVelocity;
	protected bool OnRutine = false;
	protected bool isOnCollision=false;
	CollisionFlags collision;
	[SerializeField]
	LayerMask collisionLayer=0;

	public float getHealth()
	{
		return health;
	}

	public float StartSpeed
	{
		get{return startSpeed; }
	}
	public bool IsOnCollision
	{
		get{ return isOnCollision; }
		protected set { isOnCollision=value; }
	}
	void Awake () 
	{
		characterController = GetComponent<CharacterController> ();
		SetVelocityToFoward (startSpeed);
		initialY = transform.position.y;

	
	}
	void Update()
	{
		

			isOnCollision = false;
		OnControllerCollisionNone ();
		

		/*if ((collision & CollisionFlags.Sides)!=0)
			print("Touching sides!");*/

		if (collision == CollisionFlags.Sides)
		{
			
				isOnCollision = true;
			

			OnCollisionSides ();
		}

		/*if ((collision & CollisionFlags.Above)!=0)
			print("Touching Ceiling!");*/

		if (collision == CollisionFlags.Above) 
		{
			

			
			OnControllerCollisionAbove ();
		}

		/*	if ((collision & CollisionFlags.Below)!=0)
			print("Touching ground!");*/

		if (collision == CollisionFlags.Below) 
		{
			

			
			OnControllerCollisionBelow ();
		}
		Debug.Log (health);
		if (transform.position.y < initialY) //este metodo es para solucionar un problema en un tipo de collision que se da al moverse horizontalmente
		{
			transform.position = new Vector3 (transform.position.x, initialY, transform.position.z);
		}
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		



		/*
		*/
		 collision= characterController.Move (velocity);



	}
	public virtual void MoveLeft()
	{
		if (!OnRutine) 
		{
			float targetPositionX = LevelManager.Instance.GetPreviusRoute (transform.position.x);
			StartCoroutine (HorizontalMove(targetPositionX,.3f));
		}

		//transform.position = targetPosition;

	}
	public virtual void MoveRight ()
	{
		if (!OnRutine) 
		{
			float targetPositionX = LevelManager.Instance.GetNextRoute (transform.position.x);
			StartCoroutine (HorizontalMove(targetPositionX,.3f));
		}


	}

	public  void AddVelocityToFoward(float speed)
	{
		if (isOnCollision) 
		{
			return;
		}
		Vector3 targetVelocity= Vector3.forward * speed * Time.deltaTime;

		if (speed >= maxSpeed) 
		{
			targetVelocity = velocity;
		}
		if (speed <= minSpeed) 
		{
			targetVelocity = velocity ;
		}
		velocity += targetVelocity;
		//Mathf.SmoothDamp (velocity.x, targetVelocity.x, ref currentVelocity, 0.18f);
	}
	public  void SetVelocityToFoward(float speed)
	{
		if (isOnCollision) 
		{
			return;
		}
		Vector3 targetVelocity= Vector3.forward * speed * Time.deltaTime;

		if (speed >= maxSpeed) 
		{
			targetVelocity = velocity;
		}
		if (speed <= minSpeed) 
		{
			targetVelocity = velocity ;
		}
		velocity = targetVelocity;
		//Mathf.SmoothDamp (velocity.x, targetVelocity.x, ref currentVelocity, 0.18f);
	}

	protected IEnumerator HorizontalMove(float targetPositionX, float time)
	{
		//este movimiento al no ser  hecho con  collision= characterController.Move (velocity) puede generar probllemas de colisiones
		//float currentVelocity=0;
		//transform.position = new Vector3( Mathf.SmoothDamp (transform.position.x, targetPositionX, ref currentVelocity,.01f),transform.position.y,transform.position.z);
		FloatInterpolation targetInterpolation= new FloatInterpolation(transform.position.x,targetPositionX);// esta es una de mis herramientas para realizar interpolaciones de valores. 
		targetInterpolation.LerpTime = .1f;//este valor debería cambiar con la velocidad del vehiculo... a mayor velocidad menor tiempo se debe demorar en moverse horizontalmente.
		targetInterpolation.SwitchLerp (FloatInterpolation.LerpMode.Smootherstep);
		OnRutine = true;
		while (transform.position.x != targetInterpolation.EndPos ) 
		{
			targetInterpolation.UpdateLerp ();
			float  newPositionX= targetInterpolation.InterpolatedValue;
			transform.position= new Vector3(newPositionX ,transform.position.y,transform.position.z);
			yield return null;
		}
	
			OnRutine=false;
	}

	public void TakeDamage(int damage,GameObject instigator)
	{
		if (LevelManager.getLevelEnd()) 
		{
			return;
		}
		health-=damage ;
	
		if (health <= 0) 
		{
			
			LevelManager.Instance.EndGame (false);
		}
		
	}

	protected void OnControllerColliderHit(ControllerColliderHit hit) 
	{
		
		if ((collisionLayer.value & (1<< hit.gameObject.layer)) != 0)//  si collisionlayer contiene el valor de la capa del objeto con el que collisiona entonces el valor obtenido será distinto de 0
		{
			
			// aqui podemos establecer una animación de choque.
			//AddVelocityToFoward(velocity.z);
			SetVelocityToFoward(startSpeed);
			TakeDamage (collisionDamage, hit.gameObject);

		}
	}
	protected virtual void OnCollisionSides()
	{
		
			
	

		
	}
	protected virtual void OnControllerCollisionAbove()
	{

	}
	protected virtual void OnControllerCollisionBelow()
	{

	}
	protected virtual void OnControllerCollisionNone()
	{

	}
}
