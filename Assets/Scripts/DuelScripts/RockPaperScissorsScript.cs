using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RockPaperScissorsScript : NetworkBehaviour
{
	//Needed scripts and gameobjects and variables (networking and mechanics)
	public MainGameScript game;
	public OptionsScript options;
	public NetworkManager networkManager;
	public bool multiplayerGame = false;
	public bool waitingForConnection = false;
	public bool waitOnce = true;
	public GameObject player1;
	public GameObject player2;

	[SyncVar]
	public bool p1DoneSelecting = false;
	[SyncVar]
	public bool p2DoneSelecting = false;
	[SyncVar]
	public string player1SelectionMulti = "";
	[SyncVar]
	public string player2SelectionMulti = "";

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
	public bool playerTurn = true;
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
				//Wait as we get everyone connected...
				if(waitOnce)
				{
					//Wait for the other player to connect
					waitingForConnection = true;
					waitOnce = false;

					//Depending who is connected...
					if(options.hostingPlayer)
					{
						NetworkServer.Spawn(Instantiate((GameObject)Resources.Load<GameObject>("Player1")));
					}
					else if(options.joiningPlayer)
					{
						//Attempt a connection
						networkManager.client.Connect(options.serverIp, options.serverPort);

						//Retry spawning player
						if(networkManager.client.isConnected)
						{
							NetworkServer.Spawn(Instantiate((GameObject)Resources.Load<GameObject>("Player2")));
							GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
						}
					}
					else if(options.spectatingPlayer)
					{
						//Attempt a connection
						networkManager.client.Connect(options.serverIp, options.serverPort);

						//if(networkManager.client.isConnected)
							//NetworkServer.Spawn(Instantiate((GameObject)Resources.Load<GameObject>("Player3")));
					}
				}

				//Checks if everyone is connected...
				if(waitingForConnection)
				{
					//Once we detect a connection...
					if(networkManager.IsClientConnected())
					{
						waitingForConnection = false;
					}
				}

				//If both players are done selecting...
				if(p1DoneSelecting && p2DoneSelecting)
				{
					//Compare both answers and pick a player to decide who goes first (Check to see who won rock-paper-scissors)
					//Scissors VS Scissors
					if(player1SelectionMulti == scissorsString && player2SelectionMulti == scissorsString)
					{
						//Start over
						playerTurn = true;
						p1DoneSelecting = false;
						p2DoneSelecting = false;
						if(options.hostingPlayer)
							game.player1.RpcResetRPS();
						if(options.joiningPlayer)
							game.player2.CmdResetRPS();
					}
					//Scissors VS Rock
					else if(player1SelectionMulti == scissorsString && player2SelectionMulti == rockString)
					{
						//Player 2 wins
						if(options.joiningPlayer)
						{
							playerChoice = true;
						}
						playerTurn = false;
						rockPaperScissors = false;
					}
					//Scissors VS Paper
					else if(player1SelectionMulti == scissorsString && player2SelectionMulti == paperString)
					{
						//Player 1 wins
						if(options.hostingPlayer)
						{
							playerChoice = true;
						}
						playerTurn = false;
						rockPaperScissors = false;
					}
					//Rock VS Scissors
					else if(player1SelectionMulti == rockString && player2SelectionMulti == scissorsString)
					{
						//Player 1 wins
						if(options.hostingPlayer)
						{
							playerChoice = true;
						}
						playerTurn = false;
						rockPaperScissors = false;
					}
					//Rock VS Rock
					else if(player1SelectionMulti == rockString && player2SelectionMulti == rockString)
					{
						//Start over
						playerTurn = true;
						p1DoneSelecting = false;
						p2DoneSelecting = false;
						if(options.hostingPlayer)
							game.player1.RpcResetRPS();
						if(options.joiningPlayer)
							game.player2.CmdResetRPS();
					}
					//Rock VS Paper
					else if(player1SelectionMulti == rockString && player2SelectionMulti == paperString)
					{
						///Player 2 wins
						if(options.joiningPlayer)
						{
							playerChoice = true;
						}
						playerTurn = false;
						rockPaperScissors = false;
					}
					//Paper VS Scissors
					else if(player1SelectionMulti == paperString && player2SelectionMulti == scissorsString)
					{
						///Player 2 wins
						if(options.joiningPlayer)
						{
							playerChoice = true;
						}
						playerTurn = false;
						rockPaperScissors = false;
					}
					//Paper VS Rock
					else if(player1SelectionMulti == paperString && player2SelectionMulti == rockString)
					{
						//Player 1 wins
						if(options.hostingPlayer)
						{
							playerChoice = true;
						}
						playerTurn = false;
						rockPaperScissors = false;
					}
					//Paper VS Paper
					else if(player1SelectionMulti == paperString && player2SelectionMulti == paperString)
					{
						//Start over
						playerTurn = true;
						p1DoneSelecting = false;
						p2DoneSelecting = false;
						if(options.hostingPlayer)
							game.player1.RpcResetRPS();
						if(options.joiningPlayer)
							game.player2.CmdResetRPS();
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

		//If you are a spectator...
		if(options.spectatingPlayer)
		{
			//Nothing to draw!
		}
		//Else, if we are playing rock-paper-scissors (with AI)...
		else if(rockPaperScissors && playerTurn && !multiplayerGame)
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
			//If we are hosting AND the other player is connected...
			if(options.hostingPlayer)
			{
				//Show buttons to click on
				if(GUI.Button(new Rect(width / 2 - 275, height / 2 - 150, 150, 300), scissors))
				{
					//Selected Scissors
					player1SelectionMulti = scissorsString;
					p1DoneSelecting = true;
					playerTurn = false;
					game.player1.RpcUpdateRPS(player1SelectionMulti, p1DoneSelecting);
				}

				if(GUI.Button(new Rect(width / 2 - 75, height / 2 - 150, 150, 300), rock))
				{
					//Selected Rock
					player1SelectionMulti = rockString;
					p1DoneSelecting = true;
					playerTurn = false;
					game.player1.RpcUpdateRPS(player1SelectionMulti, p1DoneSelecting);
				}

				if(GUI.Button(new Rect(width / 2 + 125, height / 2 - 150, 150, 300), paper))
				{
					//Selected Paper
					player1SelectionMulti = paperString;
					p1DoneSelecting = true;
					playerTurn = false;
					game.player1.RpcUpdateRPS(player1SelectionMulti, p1DoneSelecting);
				}
			}
			if(options.joiningPlayer)
			{
				//Show buttons to click on
				if(GUI.Button(new Rect(width / 2 - 275, height / 2 - 150, 150, 300), scissors))
				{
					//Selected Scissors
					player2SelectionMulti = scissorsString;
					p2DoneSelecting = true;
					playerTurn = false;
					game.player2.CmdUpdateRPS(player2SelectionMulti, p2DoneSelecting);
				}

				if(GUI.Button(new Rect(width / 2 - 75, height / 2 - 150, 150, 300), rock))
				{
					//Selected Rock
					player2SelectionMulti = rockString;
					p2DoneSelecting = true;
					playerTurn = false;
					game.player2.CmdUpdateRPS(player2SelectionMulti, p2DoneSelecting);
				}

				if(GUI.Button(new Rect(width / 2 + 125, height / 2 - 150, 150, 300), paper))
				{
					//Selected Paper
					player2SelectionMulti = paperString;
					p2DoneSelecting = true;
					playerTurn = false;
					game.player2.CmdUpdateRPS(player2SelectionMulti, p2DoneSelecting);
				}
			}
		}
		
		//If the player gets to choose if they want to go first or not...
		if(playerChoice)
		{
			//If not a multiplayer game... do normal "who goes first"
			if(!multiplayerGame)
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
			//Else, we are in a multiplayer game...
			else
			{
				//If we are the hosting player...
				if(options.hostingPlayer)
				{
					//Show buttons to click on
					if(GUI.Button(new Rect(width / 2 - 75, height / 2 - 75, 150, 50), "Go First"))
					{
						//Player 1 goes first
						game.player1Turn = true;
						playerChoice = false;
						game.dueling = true;
					}

					if(GUI.Button(new Rect(width / 2 - 75, height / 2 + 25, 150, 50), "Go Second"))
					{
						//Player 2 goes second
						game.player2Turn = true;
						playerChoice = false;
						game.dueling = true;
					}
				}
				//Else, we are the joining player...
				else if(options.joiningPlayer)
				{
					//Show buttons to click on
					if(GUI.Button(new Rect(width / 2 - 75, height / 2 - 75, 150, 50), "Go First"))
					{
						//Player 2 goes first
						game.player2Turn = true;
						playerChoice = false;
						game.dueling = true;
					}

					if(GUI.Button(new Rect(width / 2 - 75, height / 2 + 25, 150, 50), "Go Second"))
					{
						//Player 1 goes second
						game.player1Turn = true;
						playerChoice = false;
						game.dueling = true;
					}
				}
			}
		}
	}
}