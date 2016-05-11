using UnityEngine;
using System.Collections;

public class Deck : MonoBehaviour
{
	//Deck variables
	public string deckName = "";
	public Card[] deck = new Card[40];
	
	public int DeckSize()
	{
		return deck.Length;
	}
	
	public void IncreaseDeckSize()
	{
		//Increase the size of the deck
		Card[] tempCards = new Card[deck.Length + 1];
		
		//Put all the cards from the deck into the temp deck with more room
		for(int i = 0; i < this.deck.Length; i++)
		{
			tempCards[i] = this.deck[i];
		}
		
		//Put all the cards from the temp array into the deck
		deck = tempCards;
	}
	
	public void IncreaseDeckSize(int amount)
	{
		//Temp variables
		Card[] tempCards = new Card[deck.Length + amount];
		
		deck = tempCards;
	}
	
	public void DecreaseDeckSize()
	{
		//Temp variables
		Card[] tempCards = new Card[deck.Length];
		
//		//Move all the cards to the temporary card array
//		for(int i = 0; i < this.cardsInHand.Length - 1; i++)
//		{
//			tempCards[i] = this.cardsInHand[i];
//		}
		
		//Move the cards down the slots...
		for(int i = 0; i < tempCards.Length - 1; i++)
		{
			if(tempCards[i] == null)
			{
				tempCards[i] = tempCards[i + 1];
				tempCards[i + 1] = null;
			}
		}
		
		//Once another null is found the second time, cut off the array...
		for(int i = 0; i < tempCards.Length - 1; i++)
		{
			if(tempCards[i] == null)
			{
				//tempCards.Length = i;
				break;
			}
		}
		
		//Then pass the new length to the graveyard length
		deck = tempCards;
	}
	
	public void ResetDeckSize()
	{
		//Temp variables
		Card[] tempCards = new Card[40];
		
		//Cut the Deck down to a set limit (40)
		deck = tempCards;
	}
	
	//Shows the statistics of what is in this deck (how many monsters, spells, traps, etc)
	public string DeckStats()
	{
		//Temp variables
		int spellCards = 0;
		int trapCards = 0;
		int normalMonsters = 0;
		int effectMonsters = 0;
		int ritualMonsters = 0;
		int pendulumMonsters = 0;
		
		//Loop and count all the cards depending on the different types...
		for(int i = 0; i < deck.Length - 1; i++)
		{
			//If the card is a spell card...
			if(deck[i].cardType == Card.CardType.spell)
			{
				spellCards++;
			}
			
			//If the card is a trap card...
			if(deck[i].cardType == Card.CardType.trap)
			{
				trapCards++;
			}
			
			//If the card is a normal monster card...
			if(deck[i].cardType == Card.CardType.normalMonster)
			{
				normalMonsters++;
			}
			
			//If the card is a effect monster card...
			if(deck[i].cardType == Card.CardType.effectMonster)
			{
				effectMonsters++;
			}
			
			//If the card is a ritual monster card...
			if(deck[i].cardType == Card.CardType.ritualMonster)
			{
				ritualMonsters++;
			}
			
			//If the card is a pendulum monster card...
			if(deck[i].isPendulum)
			{
				pendulumMonsters++;
			}
		}
		
		//Return the statistics
		return "Deck Stats; Spells: " + spellCards + " | Traps: " + trapCards + " | Normal Monsters: " + normalMonsters + " | Effect Monsters: " + effectMonsters + " | Pendulum Monsters: " + pendulumMonsters;
	}
}