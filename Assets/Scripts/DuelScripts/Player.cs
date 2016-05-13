using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
	//Player variables
	public int lifePoints = 8000;
	public int handSize = 0;
	public int handSizeLimit = 6;
	public Card[] cardsInHand = new Card[60];
	
	public TextMesh LP01;
	public TextMesh LP02;

	public MainGameScript game;

	public bool doOnce = true;

	//RockPaperScissors variables
	public RockPaperScissorsScript rps;

	//Deck variables
	public Deck deck;
	public SideDeck sideDeck;
	public ExtraDeck extraDeck;

	void Start()
	{
		//Find the rock-paper-scissor manager
		if(GameObject.Find("RockPaperScissorsManager") != null)
			rps = GameObject.Find("RockPaperScissorsManager").GetComponent<RockPaperScissorsScript>();

		//Find THIS player's deck information (to grab later during the game)
		if(GameObject.Find("CurrentDeck") != null)
		{
			deck = GameObject.Find("CurrentDeck").GetComponent<Deck>();
			sideDeck = GameObject.Find("CurrentDeck").GetComponent<SideDeck>();
			extraDeck = GameObject.Find("CurrentDeck").GetComponent<ExtraDeck>();
		}
	}
	
	void Update()
	{
		if(doOnce)
		{
			FindObjectsAndSetup();
			doOnce = false;
		}

		LP01.text = "LP: " + lifePoints;
		LP02.text = "LP: " + lifePoints;
	}

	//Find all objects needed
	public void FindObjectsAndSetup()
	{
		//If we are in the Duel Field...
		if(SceneManager.GetActiveScene().buildIndex == 2)
		{
			game = GameObject.Find("GameManager").GetComponent<MainGameScript>();

			//Place the prefab clones in the proper spot
			if(gameObject.name == "Player1(Clone)")
			{
				transform.position = game.player1.gameObject.transform.position;
				transform.rotation = game.player1.gameObject.transform.rotation;
				GetComponent<Player>().LP01 = GameObject.Find("P1LP01").GetComponent<TextMesh>();
				GetComponent<Player>().LP02 = GameObject.Find("P1LP02").GetComponent<TextMesh>();
				//GetComponent<MouseLookScript>().enabled = true;

				//Connect player 1 to all references
				game.player1 = this;

				//Remove old player 1
				if(GameObject.Find("Player1") != null)
				{
					Destroy(GameObject.Find("Player1"));
				}
			}

			if(gameObject.name == "Player2(Clone)")
			{
				transform.position = game.player2.gameObject.transform.position;
				transform.rotation = game.player2.gameObject.transform.rotation;
				GetComponent<Player>().LP01 = GameObject.Find("P2LP01").GetComponent<TextMesh>();
				GetComponent<Player>().LP02 = GameObject.Find("P2LP02").GetComponent<TextMesh>();
				//GetComponent<MouseLookScript>().enabled = true;

				//Connect player 2 to all references
				game.player2 = this;

				//Remove old player 2
				if(GameObject.Find("Player2") != null)
				{
					Destroy(GameObject.Find("Player2"));
				}
			}
		}
	}
	
	//----------LIFE POINTS---------------
	public int GetCurrentLifePoints()
	{
		return lifePoints;
	}
	
	public void SetCurrentLifePoints(int amount)
	{
		lifePoints = amount;
	}
	
	public void IncreaseLifePoints(int amount)
	{
		lifePoints = lifePoints + amount;
	}
	
	public void DecreaseLifePoints(int amount)
	{
		lifePoints = lifePoints - amount;
	}
	
	//----------HAND SIZE---------------
	public int GetHandSize()
	{
		return handSize;
	}
	
	public void SetHandSize(int amount)
	{
		handSize = amount;
	}
	
	public void IncreaseHandSize(int amount)
	{
		handSize = handSize + amount;
	}
	
	public void DecreaseHandSize(int amount)
	{
		handSize = handSize - amount;
	}
	
	//----------HAND SIZE LIMIT---------------
	public int GetHandSizeLimit()
	{
		return handSizeLimit;
	}
	
	public void SetHandSizeLimit(int amount)
	{
		handSizeLimit = amount;
	}
	
	public void IncreaseHandSizeLimit(int amount)
	{
		handSizeLimit = handSizeLimit + amount;
	}
	
	public void DecreaseHandSizeLimit(int amount)
	{
		handSizeLimit = handSizeLimit - amount;
	}
	
	//----------HAND SIZE ARRAY FUNCTIONS---------------
	public void DecreaseCardsInHand()
	{
		//Temp variables
		Card[] tempCards = new Card[cardsInHand.Length];
		
		//Move all the cards to the temporary card array
		for(int i = 0; i < this.cardsInHand.Length - 1; i++)
		{
			tempCards[i] = this.cardsInHand[i];
		}
		
		//Move the cards down the slots...
		for(int i = 0; i < tempCards.Length - 1; i++)
		{
			if(tempCards[i] == null)
			{
				tempCards[i] = tempCards[i + 1];
				tempCards[i + 1] = null;
			}
		}
		
		//Then pass the new length to the graveyard length
		cardsInHand = tempCards;
	}

	//----------Networking Functions only------------------
	//Rock-Paper-Scissors functions below
	[Command]
	public void CmdUpdateRPS(string p2Selection, bool p2Done)
	{
		rps.player2SelectionMulti = p2Selection;
		rps.p2DoneSelecting = p2Done;
	}

	[ClientRpc]
	public void RpcUpdateRPS(string p1Selection, bool p1Done)
	{
		//Update the values of all varaibles needed
		rps.player1SelectionMulti = p1Selection;
		rps.p1DoneSelecting = p1Done;
	}

	[Command]
	public void CmdResetRPS()
	{
		//Reset the values of all varaibles needed
		rps.player1SelectionMulti = "";
		rps.p1DoneSelecting = false;
		rps.player2SelectionMulti = "";
		rps.p2DoneSelecting = false;
		rps.playerTurn = true;
	}

	[ClientRpc]
	public void RpcResetRPS()
	{
		//Reset the values of all varaibles needed
		rps.player1SelectionMulti = "";
		rps.p1DoneSelecting = false;
		rps.player2SelectionMulti = "";
		rps.p2DoneSelecting = false;
		rps.playerTurn = true;
	}

	//Deck information transfer functions below
	[Command]
	public void CmdGetHostDeck()
	{
		//Transfer all information in these big types (becuase we cant send components themselves)
		game.currentDeckAI = deck;
		game.currentExtraDeckAI = extraDeck;
		game.currentSideDeckAI = sideDeck;
	}

	[ClientRpc]
	public void RpcGetClientDeck()
	{
		//Transfer all information in these big types (becuase we cant send components themselves)
		game.currentDeckAI = deck;
		game.currentExtraDeckAI = extraDeck;
		game.currentSideDeckAI = sideDeck;
	}
}