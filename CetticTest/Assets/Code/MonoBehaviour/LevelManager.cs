using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager :PersistentHumbleSingleton<LevelManager>
{
	//level manager maneja cuando termina el juego y almacena los puntos obtenidos, en un futuro se encargara de posicionar los obstaculos y los puntos en la carreteras
	[SerializeField]
	float levelTime;
	float currentTime;
	float points;
	[SerializeField]
	CharacterBehaviour character;
	[SerializeField]
	CharacterFollow camFollow;
	GameObject CoinsPrefab;
	GameObject TowerPrefab;
	// Use this for initialization
	 List<float> xRoadPositions = new List<float>();
	// List<float>  zRoadPositions = new List<float>();
	 int xRoadIndex=0;
	static bool levelEnd= false;
	static bool winLevel= false;
	public static  bool WinLevel
	{
		get { return winLevel; }
	}
	public static bool getLevelEnd()
	{
		return levelEnd;
	}
	public float getCurrentTime()
	{
		return currentTime; 
		
	}
	public float getPoints()
	{
		return points;
	}
	public float getPlayerLife()
	{
		return character.getHealth();
	}
	void Start () 
	{
		float[] routes = RoadRoutes.Getroutes ();
		foreach (float route in routes) 
		{
			xRoadPositions.Add (route);
		
		}
		levelEnd = false;
		winLevel = false;
		currentTime = levelTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//la velocidad será aumentada en 3 partes de la partida, la última parte debe ser la más dificil se superar



		currentTime -= Time.deltaTime;

		if (!character.IsOnCollision) 
		{
			character.AddVelocityToFoward (Time.deltaTime);
		}



		if (currentTime <=0.1f) 
		{
			
			EndGame (false);
		}
			


		Debug.Log ("TIME: "+currentTime);

	}

	public float GetNextRoute(float actualPosition)
	{
		//buscamos el index actual
		xRoadIndex= xRoadPositions.IndexOf(actualPosition);

		if (xRoadIndex < xRoadPositions.Count-1) 
		{
			//si el index no es el ultimo avanzamos en uno
			xRoadIndex++;

		}

		return  xRoadPositions [xRoadIndex];
	}

	public float GetPreviusRoute(float actualPosition)
	{
		//buscamos el index actual
		xRoadIndex= xRoadPositions.IndexOf(actualPosition);

		if (xRoadIndex > 0) 
		{
			//si el index no es menor al permitimos retrocedemos en uno
			xRoadIndex--;

		}
		return  xRoadPositions [xRoadIndex];
	}
	public void EndGame(bool win)
	{
		levelEnd = true;
		winLevel = win;
		InputController.Instance.EnableCharacterInputs= false;
		camFollow.Follow = false;
	}
	public void AddPoint(float point)
	{
		points += point;
		
	}
}
