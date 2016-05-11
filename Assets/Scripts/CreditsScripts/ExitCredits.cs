using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitCredits : MonoBehaviour
{
	//Timer variables
	public bool startEndCreditsTimer = false;
	public float endCreditsTimer = 100f;
	public float fasterSpeed = 10f;
	public float reverseSpeed = -10f;
	public float resetSpeed = 1f;
	public bool ignoreNormalSpeed = false;
	public GameObject moveableObject1;

	void Start()
	{
		resetSpeed = moveableObject1.GetComponent<MoveCustom>().speed;
	}

	//Update is called once per frame
	void Update()
	{
		//If any of the escape keys are pressed...
		if(Input.GetKeyDown("escape") || Input.GetKeyDown("backspace") || Input.GetKeyDown("return") || Input.GetKeyDown("p") || Input.GetKeyDown("q"))
		{
			//Load the main menu
			//Application.LoadLevel("MainMenu"); //Outdated since unity version 5.3
			SceneManager.LoadScene("MainMenu");
		}

		if(Input.GetKey("down"))
		{
			moveableObject1.GetComponent<MoveCustom>().speed = fasterSpeed;
			endCreditsTimer = endCreditsTimer - Time.deltaTime * fasterSpeed;
			ignoreNormalSpeed = true;
		}
		else if(Input.GetKey("up"))
		{
			moveableObject1.GetComponent<MoveCustom>().speed = reverseSpeed;
			endCreditsTimer = endCreditsTimer - Time.deltaTime * reverseSpeed;
			ignoreNormalSpeed = true;
		}
		else
		{
			moveableObject1.GetComponent<MoveCustom>().speed = resetSpeed;
			ignoreNormalSpeed = false;
		}

		if(startEndCreditsTimer)
		{
			if(!ignoreNormalSpeed)
			{
				endCreditsTimer = endCreditsTimer - Time.deltaTime;
			}

			if(endCreditsTimer <= 0)
			{
				//Application.LoadLevel("MainMenu"); //Outdated since unity version 5.3
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, Screen.height - 50, 100, 50), "Main Menu"))
		{
			//Application.LoadLevel("MainMenu"); //Outdated since unity version 5.3
			SceneManager.LoadScene("MainMenu");
		}
	}
}