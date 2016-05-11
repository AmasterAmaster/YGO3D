using UnityEngine;
using System.Collections;

public class SideDeck : MonoBehaviour
{
	//Deck variables
	public Card[] sideDeck = new Card[15];
	
	public int SideDeckSize()
	{
		return sideDeck.Length;
	}
	
	public void IncreaseSideDeckSize()
	{
		//Increase the size of the deck
		Card[] tempCards = new Card[sideDeck.Length + 1];
		
		//Put all the cards from the deck into the temp deck with more room
		for(int i = 0; i < this.sideDeck.Length; i++)
		{
			tempCards[i] = this.sideDeck[i];
		}
		
		//Put all the cards from the temp array into the deck
		sideDeck = tempCards;
	}
	
	//Shows the statistics of what is in this deck (how many monsters, spells, traps, etc)
	public string SideDeckStats()
	{
		//Temp variables
		int spellCards = 0;
		int trapCards = 0;
		int normalMonsters = 0;
		int effectMonsters = 0;
		int ritualMonsters = 0;
		int pendulumMonsters = 0;
		int fusionMonsters = 0;
		int synchroMonsters = 0;
		int xyzMonsters = 0;
		
		//Loop and count all the cards depending on the different types...
		for(int i = 0; i < sideDeck.Length - 1; i++)
		{
			//If the card is a spell card...
			if(sideDeck[i].cardType == Card.CardType.spell)
			{
				spellCards++;
			}
			
			//If the card is a trap card...
			if(sideDeck[i].cardType == Card.CardType.trap)
			{
				trapCards++;
			}
			
			//If the card is a normal monster card...
			if(sideDeck[i].cardType == Card.CardType.normalMonster)
			{
				normalMonsters++;
			}
			
			//If the card is a effect monster card...
			if(sideDeck[i].cardType == Card.CardType.effectMonster)
			{
				effectMonsters++;
			}
			
			//If the card is a ritual monster card...
			if(sideDeck[i].cardType == Card.CardType.ritualMonster)
			{
				ritualMonsters++;
			}
			
			//If the card is a pendulum monster card...
			if(sideDeck[i].isPendulum)
			{
				pendulumMonsters++;
			}
			
			//If the card is a fusion monster card...
			if(sideDeck[i].cardType == Card.CardType.fusionMonster)
			{
				fusionMonsters++;
			}
			
			//If the card is a synchro monster card...
			if(sideDeck[i].cardType == Card.CardType.synchroMonster)
			{
				synchroMonsters++;
			}
			
			//If the card is a xyz monster card...
			if(sideDeck[i].cardType == Card.CardType.xyzMonster)
			{
				xyzMonsters++;
			}
		}
		
		//Return the statistics
		return "Deck Stats; Spells: " + spellCards + " | Traps: " + trapCards + " | Normal Monsters: " + normalMonsters + " | Effect Monsters: " + effectMonsters + " | Pendulum Monsters: " + pendulumMonsters + "Fusion Monsters: " + fusionMonsters + " | Synchro Monsters: " + synchroMonsters + " | XYZ Monsters: " + xyzMonsters;
	}
}