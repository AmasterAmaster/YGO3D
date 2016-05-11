using UnityEngine;
using System.Collections;

public class ExtraDeck : MonoBehaviour
{
	//Extra deck variables
	public Card[] extraDeck = new Card[15];
	
	public int ExtraDeckSize()
	{
		return extraDeck.Length;
	}
	
	public void IncreaseExtraDeckSize()
	{
		//Increase the size of the deck
		Card[] tempCards = new Card[extraDeck.Length + 1];
		
		//Put all the cards from the deck into the temp deck with more room
		for(int i = 0; i < this.extraDeck.Length; i++)
		{
			tempCards[i] = this.extraDeck[i];
		}
		
		//Put all the cards from the temp array into the deck
		extraDeck = tempCards;
	}
	
	//Shows the statistics of what is in this deck (how many monsters, spells, traps, etc)
	public string DeckStats()
	{
		//Temp variables
		int fusionMonsters = 0;
		int synchroMonsters = 0;
		int xyzMonsters = 0;
		
		//Loop and count all the cards depending on the different types...
		for(int i = 0; i < extraDeck.Length - 1; i++)
		{
			//If the card is a fusion monster card...
			if(extraDeck[i].cardType == Card.CardType.fusionMonster)
			{
				fusionMonsters++;
			}
			
			//If the card is a synchro monster card...
			if(extraDeck[i].cardType == Card.CardType.synchroMonster)
			{
				synchroMonsters++;
			}
			
			//If the card is a xyz monster card...
			if(extraDeck[i].cardType == Card.CardType.xyzMonster)
			{
				xyzMonsters++;
			}
		}
		
		//Return the statistics
		return "Deck Stats; Fusion Monsters: " + fusionMonsters + " | Synchro Monsters: " + synchroMonsters + " | XYZ Monsters: " + xyzMonsters;
	}
}