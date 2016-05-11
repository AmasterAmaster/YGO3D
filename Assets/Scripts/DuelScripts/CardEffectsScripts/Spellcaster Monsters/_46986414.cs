using UnityEngine;
using System.Collections;

//Card: Dark Magician
public class _46986414 : CardEffectBaseClass
{
	//-----------------------Reference to GameAPI----------------------------------------------------------
	public GameAPI gapi;
	
	//Sets the Game API
	override public void SetGameAPI()
	{
		if(GameObject.Find("GameManager").GetComponent<GameAPI>() != null)
			gapi = GameObject.Find("GameManager").GetComponent<GameAPI>();
	}
	
	//-----------------------Virtual Functions (can be implemented)----------------------------------------
}