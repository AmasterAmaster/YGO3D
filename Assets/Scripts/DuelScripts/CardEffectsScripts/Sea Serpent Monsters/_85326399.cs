using UnityEngine;
using System.Collections;

//Card: Spike Seadra
public class _85326399 : CardEffectBaseClass
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