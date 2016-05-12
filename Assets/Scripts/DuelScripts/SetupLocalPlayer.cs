using UnityEngine;
using System.Collections;
using UnityEngine.Networking; //To establish client-only controls

public class SetupLocalPlayer : NetworkBehaviour
{
	//Start up the networking for client-only controlled players
	void Start()
	{
		//If we are the spawned player...
		if(isLocalPlayer)
		{
			GetComponent<MouseLookScript>().enabled = true;
		}
	}
}