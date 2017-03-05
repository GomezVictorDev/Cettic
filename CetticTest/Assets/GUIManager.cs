using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GUIManager : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	Text  textHealth,textPoints,textTime;
	string defaultTextHealth,defaultTextPoints,defaultTextTime;
	string winText,loseText;
	[SerializeField]
	GameObject EndPanel;

	void Start () 
	{
		
		defaultTextHealth = "Vida: ";
		defaultTextPoints = "Puntos: ";
		defaultTextTime = "Tiempo: ";
		winText="FELICITACIONES";
		loseText = "INTENTALO NUEVAMENTE";
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		textHealth.text = defaultTextHealth + LevelManager.Instance.getPlayerLife();
		textPoints.text = defaultTextPoints + LevelManager.Instance.getPoints();
		textTime.text = defaultTextTime + Mathf.FloorToInt( LevelManager.Instance.getCurrentTime());

		if (LevelManager.getLevelEnd()) 
		{
			textHealth.text = "";
			textPoints.fontSize=20;
			textTime.text = "";
			EndScreen ();
		}
	
	}
	public void EndScreen()
	{
		EndPanel.SetActive (true);
		if (LevelManager.WinLevel) 
		{
			EndPanel.GetComponentInChildren<Text> ().text = winText;
		}
		else 
		{
			EndPanel.GetComponentInChildren<Text> ().text = loseText;
		}


		
	}





}
