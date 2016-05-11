using UnityEngine;
using System.Collections;

public class Graveyard : MonoBehaviour
{
	//Graveyard variables
	public Card[] graveyard = new Card[1];
	
	public int GraveyardSize()
	{
		return graveyard.Length;
	}
	
	public void IncreaseGraveyardSize()
	{
		//Temp variables
		Card[] tempCards = new Card[graveyard.Length + 1];
	
		graveyard = tempCards;
	}
	
	public void IncreaseGraveyardSize(int amount)
	{
		//Temp variables
		Card[] tempCards = new Card[graveyard.Length + amount];
		
		graveyard = tempCards;
	}
	
	public void DecreaseGraveyardSize()
	{
		//Temp variables
		Card[] tempCards = new Card[graveyard.Length];
		
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
		graveyard = tempCards;
	}
}