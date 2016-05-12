using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RockPaperScissorsScript : MonoBehaviour
{
	//Needed scripts and gameobjects and variables
	public MainGameScript game;
	public OptionsScript options;
	public NetworkManager networkManager;
	public bool multiplayerGame = false;
	public bool waitingForConnection = false;
	public bool waitOnce = true;

	//Rock-Paper-Scissors variables
	public bool rockPaperScissors = true;
	
	public Texture rock;
	public Texture paper;
	public Texture scissors;
	
	private string rockString = "Rock";
	private string paperString = "Paper";
	private string scissorsString = "Scissors";
	private string player1Selection = "";
	public string player2Selection = "";
	private bool playerTurn = true;
	private bool AITurn = false;
	
	//"Who goes first?" variables
	public bool playerChoice = false;
	public bool AIChoice = false;

	//Check what game mode this is
	void Start()
	{
		//Checking game mode
		options = GameObject.Find("OptionsManager").GetComponent<OptionsScript>();
		multiplayerGame = options.startedMultiplayerGame;

		//Get the network manager
		networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
	}
	
	//Update is called once per frame
	void Update()
	{
		//If we are playing rock-paper-scissors...
		if(rockPaperScissors)
		{
			//If playing the AI (or not Multiplayer)...
			if(!multiplayerGame)
			{
				//Play rock-paper-scissors to decide who goes first
				if(AITurn)
				{
					int selection = Random.Range(1, 4);
					
					switch(selection)
					{
					case 1:
						//Scissors
						player2Selection = scissorsString;
						break;
					case 2:
						//Rock
						player2Selection = rockString;
						break;
					case 3:
						//Paper
						player2Selection = paperString;
						break;
					}
					
					//Check to see who won rock-paper-scissors
					//Scissors VS Scissors
					if(player1Selection == scissorsString && player2Selection == scissorsString)
					{
						//Start over
						AITurn = false;
						playerTurn = true;
					}
					//Scissors VS Rock
					else if(player1Selection == scissorsString && player2Selection == rockString)
					{
						//AI wins
						AIChoice = true;
						playerTurn = false;
						AITurn = false;
						rockPaperScissors = false;
					}
					//Scissors VS Paper
					else if(player1Selection == scissorsString && player2Selection == paperString)
					{
						//Player wins
						playerChoice = true;
						playerTurn = false;
						AITurn = false;
						rockPaperScissors = false;
					}
					//Rock VS Scissors
					else if(player1Selection == rockString && player2Selection == scissorsString)
					{
						//Player wins
						playerChoice = true;
						playerTurn = false;
						AITurn = false;
						rockPaperScissors = false;
					}
					//Rock VS Rock
					else if(player1Selection == rockString && player2Selection == rockString)
					{
						//Start over
						AITurn = false;
						playerTurn = true;
					}
					//Rock VS Paper
					else if(player1Selection == rockString && player2Selection == paperString)
					{
						//AI wins
						AIChoice = true;
						playerTurn = false;
						AITurn = false;
						rockPaperScissors = false;
					}
					//Paper VS Scissors
					else if(player1Selection == paperString && player2Selection == scissorsString)
					{
						//AI wins
						AIChoice = true;
						playerTurn = false;
						AITurn = false;
						rockPaperScissors = false;
					}
					//Paper VS Rock
					else if(player1Selection == paperString && player2Selection == rockString)
					{
						//Player wins
						playerChoice = true;
						playerTurn = false;
						AITurn = false;
						rockPaperScissors = false;
					}
					//Paper VS Paper
					else if(player1Selection == paperString && player2Selection == paperString)
					{
						//Start over
						AITurn = false;
						playerTurn = true;
					}
				}
				
				//If the AI gets to choose who goes first...
				if(AIChoice)
				{
					int selection = Random.Range(1, 3);
					
					switch(selection)
					{
					case 1:
						//AI goes first
						game.player2Turn = true;
						AIChoice = false;
						game.dueling = true;
						break;
					case 2:
						//Player goes first
						game.player1Turn = true;
						AIChoice = false;
						game.dueling = true;
						break;
					}
				}
			}
			//Else, this is a multiplayer game...
			else
			{
				if(waitOnce)
				{
					//Wait for the other player to connect
					waitingForConnection = true;
					waitOnce = false;
					Debug.Log("Waiting for other player to connect...");
				}

				if(waitingForConnection)
				{
					//Once we detect a connection...
					if(networkManager.IsClientConnected())
					{
						waitingForConnection = false;
					}
				}
			}
		}
	}
	
	void OnGUI()
	{
		//Reference variables
		int width = Screen.width;
		int height = Screen.height;
		
		//If we are playing rock-paper-scissors (with AI)...
		if(rockPaperScissors && playerTurn && !multiplayerGame)
		{
			//Show buttons to click on
			if(GUI.Button(new Rect(width / 2 - 275, height / 2 - 150, 150, 300), scissors))
			{
				//Selected Scissors
				player1Selection = scissorsString;
				playerTurn = false;
				AITurn = true;
			}
			
			if(GUI.Button(new Rect(width / 2 - 75, height / 2 - 150, 150, 300), rock))
			{
				//Selected Rock
				player1Selection = rockString;
				playerTurn = false;
				AITurn = true;
			}
			
			if(GUI.Button(new Rect(width / 2 + 125, height / 2 - 150, 150, 300), paper))
			{
				//Selected Paper
				player1Selection = paperString;
				playerTurn = false;
				AITurn = true;
			}
		}
		//Else, we are playing rock-paper-scissors (Multiplayer)...
		else if(rockPaperScissors && playerTurn && multiplayerGame && !waitingForConnection)
		{
			//Show buttons to click on
			if(GUI.Button(new Rect(width / 2 - 275, height / 2 - 150, 150, 300), scissors))
			{
				//Selected Scissors
				player1Selection = scissorsString;
				playerTurn = false;
				AITurn = true;
			}

			if(GUI.Button(new Rect(width / 2 - 75, height / 2 - 150, 150, 300), rock))
			{
				//Selected Rock
				player1Selection = rockString;
				playerTurn = false;
				AITurn = true;
			}

			if(GUI.Button(new Rect(width / 2 + 125, height / 2 - 150, 150, 300), paper))
			{
				//Selected Paper
				player1Selection = paperString;
				playerTurn = false;
				AITurn = true;
			}
		}
		
		//If the player gets to choose if they want to go first or not...
		if(playerChoice)
		{
			//Show buttons to click on
			if(GUI.Button(new Rect(width / 2 - 75, height / 2 - 75, 150, 50), "Go First"))
			{
				//Player goes first
				game.player1Turn = true;
				playerChoice = false;
				game.dueling = true;
			}
			
			if(GUI.Button(new Rect(width / 2 - 75, height / 2 + 25, 150, 50), "Go Second"))
			{
				//Player goes second
				game.player2Turn = true;
				playerChoice = false;
				game.dueling = true;
			}
		}
	}
}