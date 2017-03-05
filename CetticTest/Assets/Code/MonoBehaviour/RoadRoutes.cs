using UnityEngine;
using System.Collections;
using System;
public class RoadRoutes : MonoBehaviour {

	// el objetivo de este script es poder tener presicion en el ancho del terreno para colocar los objetos y saber hasta donde puede moverse en el eje X el personaje.
	MeshCollider roadMeshCollider;
	static float[] routs;
	[SerializeField]
	int routesForSides;
	[SerializeField]
	float routesOffset=2;
	Bounds meshBounds;

	void Awake () 
	{
		roadMeshCollider = GetComponent<MeshCollider> ();
		 meshBounds = roadMeshCollider.bounds;
		SetRoutesForSides (routesForSides);
	}
	
	public static float [] Getroutes()
	{
		if (routs.Length > 0) 
		{
			
			return routs;
		}
		return null;
	}

	public void SetRoutes()
	{
		routs = new float[3];
		routs[0]= meshBounds.min.x + routesOffset;
		routs[1]= meshBounds.center.x;
		routs[2]= meshBounds.max.x - routesOffset;

	}
	public void SetRoutesForSides(int routsPorLado)//este metodo permite crear las rutas en las cual se tiene que mover el jugador y donde deben estar los diferentes objetos en el plano X
	{
		routs = new float[routsPorLado * 2 +1];//
		float[] leftRoutes = new float[routsPorLado]; 
		float[] rightRoutes = new float[routsPorLado]; 
		float partSize = meshBounds.extents.x / routsPorLado;

		routs [routs.Length-1] = meshBounds.center.x;// el ultimo valor de las posibles posiciones en el plano X siempre será el centro
		for (int x = 1; x < routsPorLado +1; x++) 
		{
			
			float leftRoute = meshBounds.center.x - partSize * x ;
			float rightRoute = meshBounds.center.x + partSize * x ;

			leftRoutes [x - 1] = leftRoute +routesOffset ;
			rightRoutes [x - 1] = rightRoute -routesOffset ;

		}

		//Array.Sort (leftRoutes);
	//	Array.Sort (rightRoutes);

		for (int x = 0; x < routs.Length-1; x++) 
		{
			 
			if (x < leftRoutes.Length)
			{
				


					
					routs [x] = leftRoutes [x];
			//		Debug.Log ("left"+x+" : "+routs [x]);
				

			}
			else 
			{
				

			

					routs [x] = rightRoutes [x -rightRoutes.Length];
				//	Debug.Log ("right" +x+" : "+routs[x]);
				
			}







		}
		Array.Sort (routs);

		for(int x=0; x< routs.Length;x++)
		{
			//Debug.Log(routs [x]);
		}


		
	}

}
