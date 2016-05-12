using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	//Player variables
	public int lifePoints = 8000;
	public int handSize = 0;
	public int handSizeLimit = 6;
	public Card[] cardsInHand = new Card[60];
	
	public TextMesh LP01;
	public TextMesh LP02;

	public MainGameScript game;

	void Start()
	{
		game = GameObject.Find("GameManager").GetComponent<MainGameScript>();

		//Place the prefab clones in the proper spot
		if(gameObject.name == "Player1(Clone)")
		{
			transform.position = game.player1.gameObject.transform.position;
			transform.rotation = game.player1.gameObject.transform.rotation;
			GetComponent<Player>().LP01 = GameObject.Find("P1LP01").GetComponent<TextMesh>();
			GetComponent<Player>().LP02 = GameObject.Find("P1LP02").GetComponent<TextMesh>();
		}

		if(gameObject.name == "Player2(Clone)")
		{
			transform.position = game.player2.gameObject.transform.position;
			transform.rotation = game.player2.gameObject.transform.rotation;
			GetComponent<Player>().LP01 = GameObject.Find("P2LP01").GetComponent<TextMesh>();
			GetComponent<Player>().LP02 = GameObject.Find("P2LP02").GetComponent<TextMesh>();
		}
	}
	
	void Update()
	{
		LP01.text = "LP: " + lifePoints;
		LP02.text = "LP: " + lifePoints;
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
}