using UnityEngine;
using System.Collections;

public class PersistenceScript : MonoBehaviour
{
	public static PersistenceScript test;	//Can be accessed as a singleton
	
	void Awake()
	{
		//If there is no persistence yet...
		if(test == null)
		{
			//Set this script/gameObject as the persisting values
			DontDestroyOnLoad(gameObject);
			test = this;
		}
		//else if there is already a persisting object like this...
		else if(test != this)
		{
			//destroy this immediatly
			Destroy(gameObject);
		}
	}
}