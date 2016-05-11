using UnityEngine;
using System.Collections;

public class ActivateScript : MonoBehaviour
{
	public MainGameScript mgs;
	public TargetAndNotificationSystemScript tans;
	#pragma warning disable 0414
	RaycastHit hit;
	#pragma warning restore 0414
	
	//If activating by selected card
	public void Activate()
	{
		if(mgs.selectedCard.GetComponent<Card>().cardType == Card.CardType.effectMonster)
		{
			ActivateMonsterEffect();
		}
		//Activating by hand...
		else if((mgs.selectedCard.GetComponent<Card>().cardType == Card.CardType.spell || mgs.selectedCard.GetComponent<Card>().cardType == Card.CardType.trap) && mgs.selectedCard.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand)
		{
			ActivateSpellOrTrapCardFromHand();
		}
		//Activating from the field...
		else if((mgs.selectedCard.GetComponent<Card>().cardType == Card.CardType.spell || mgs.selectedCard.GetComponent<Card>().cardType == Card.CardType.trap) && mgs.selectedCard.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
		{
			ActivateSpellOrTrapCardFromField();
		}
	}
	
	//If activating directly by monster
	public void ActivateMonsterEffect()
	{
		//mgs.selectedCard.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
	}
	
	public void ActivateSpellOrTrapCardFromField()
	{
		if(mgs.selectedCard.GetComponent<ExtraCardProperties>().isSet)
		{
			//Flip it
			mgs.movingActivateCard = true;
			mgs.startActivatePoint = mgs.selectedCard.transform.position;
			mgs.endActivatePoint = mgs.selectedCard.transform.position;
			mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
			
			if(mgs.player1Turn)
			{
				mgs.endActivateRotation = Quaternion.Euler(90, 0, 0);
				mgs.player1POV = true;
			}
			else if(mgs.player2Turn)
			{
				mgs.endActivateRotation = Quaternion.Euler(90, 180, 0);
				mgs.player2POV = true;
			}
			
			mgs.selectedCard.GetComponent<ExtraCardProperties>().isSet = false;
			
			//Then activate
			//mgs.selectedCard.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			//Activate as normal
			//mgs.selectedCard.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	//If activating by spell or trap
	public void ActivateSpellOrTrapCardFromHand()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		
		if(mgs.set)
		{
			if(mgs.player1Turn)
			{
				rotation = Quaternion.Euler(270, 180, 0);
			}
			else if(mgs.player2Turn)
			{
				rotation = Quaternion.Euler(270, 0, 0);
			}
		}
		else
		{
			if(mgs.player1Turn)
			{
				rotation = Quaternion.Euler(90, 0, 0);
			}
			else if(mgs.player2Turn)
			{
				rotation = Quaternion.Euler(90, 180, 0);
			}
		}
		
		//If predetermined card placement is selected...
		if(mgs.predeterminedCardPlacement)
		{
			//Loop through all the rays...
			for(int i = 0; i < mgs.positionRays.Length; i++)
			{
				//Check if it is player1's turn...
				if(mgs.player1Turn)
				{
					//Check for a ray name...
					if(mgs.positionRays[i].name == "S/TCardZone3RayP1" && !mgs.STZ03P1taken && !mgs.STZ02P1taken && !mgs.STZ04P1taken && !mgs.STZ01P1taken && !mgs.STZ05P1taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player1POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone3P1.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ3P1";
							
							mgs.STZ03P1taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone2RayP1" && mgs.STZ03P1taken && !mgs.STZ02P1taken && !mgs.STZ04P1taken && !mgs.STZ01P1taken && !mgs.STZ05P1taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player1POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone2P1.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ2P1";
							
							mgs.STZ02P1taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone4RayP1" && mgs.STZ03P1taken && mgs.STZ02P1taken && !mgs.STZ04P1taken && !mgs.STZ01P1taken && !mgs.STZ05P1taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player1POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone4P1.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ4P1";
							
							mgs.STZ04P1taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone1RayP1" && mgs.STZ03P1taken && mgs.STZ02P1taken && mgs.STZ04P1taken && !mgs.STZ01P1taken && !mgs.STZ05P1taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player1POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone1P1.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ1P1";
							
							mgs.STZ01P1taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone5RayP1" && mgs.STZ03P1taken && mgs.STZ02P1taken && mgs.STZ04P1taken && mgs.STZ01P1taken && !mgs.STZ05P1taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player1POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone5P1.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ5P1";
							
							mgs.STZ05P1taken = true;
							break;
						}
					}
				}
				//Else, it is the other player's turn...
				else if(mgs.player2Turn)
				{
					//Check for a ray name...
					if(mgs.positionRays[i].name == "S/TCardZone3RayP2" && !mgs.STZ03P2taken && !mgs.STZ02P2taken && !mgs.STZ04P2taken && !mgs.STZ01P2taken && !mgs.STZ05P2taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player2POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone3P2.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ3P2";
							
							mgs.STZ03P2taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone2RayP2" && mgs.STZ03P2taken && !mgs.STZ02P2taken && !mgs.STZ04P2taken && !mgs.STZ01P2taken && !mgs.STZ05P2taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player2POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone2P2.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ2P2";
							
							mgs.STZ02P2taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone4RayP2" && mgs.STZ03P2taken && mgs.STZ02P2taken && !mgs.STZ04P2taken && !mgs.STZ01P2taken && !mgs.STZ05P2taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player2POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone4P2.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ4P2";
							
							mgs.STZ04P2taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone1RayP2" && mgs.STZ03P2taken && mgs.STZ02P2taken && mgs.STZ04P2taken && !mgs.STZ01P2taken && !mgs.STZ05P2taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player2POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone1P2.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ1P2";
							
							mgs.STZ01P2taken = true;
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "S/TCardZone5RayP2" && mgs.STZ03P2taken && mgs.STZ02P2taken && mgs.STZ04P2taken && mgs.STZ01P2taken && !mgs.STZ05P2taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingActivateCard = true;
							mgs.player2POV = true;
							mgs.startActivatePoint = mgs.selectedCard.transform.position;
							mgs.endActivatePoint = mgs.STCardZone5P2.transform.position;
							mgs.startActivateRotation = mgs.selectedCard.transform.rotation;
							mgs.endActivateRotation = rotation;
							mgs.modelNameIdentifier = "STZ5P2";
							
							mgs.STZ05P2taken = true;
							break;
						}
					}
				}
			}
		}
		
		if(mgs.player1POV)
		{
			mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player1;
		}
		else if(mgs.player2POV)
		{
			mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player2;
		}
		
		//The card is no longer in the hand (take it out of the player hand array and change th state of the card)
		mgs.selectedCard.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.field;
		
		//If it is player1's turn...
		if(mgs.player1Turn)
		{
			//Loop through the player's hand...
			for(int i = 0; i < mgs.player1.cardsInHand.Length; i++)
			{
				//Search for the name of the selected card...
				if(mgs.selectedCard.name == mgs.player1.cardsInHand[i].name && mgs.player1.cardsInHand[i].name != null)
				{
					//Remove the card from the hand (cannot be selected, moved, or altered in the hand anymore) and fix the hand so there are no nulls in between
					mgs.player1.cardsInHand[i] = null;
					mgs.player1.DecreaseCardsInHand();
					mgs.player1.handSize--;
					
					break;
				}
			}
			
			for(int i = 0; i < mgs.player1.cardsInHand.Length; i++)
			{
				if(mgs.player1.cardsInHand[i] != null)
				{
					mgs.CalculateCardPositions(mgs.player1.cardsInHand[i].gameObject, mgs.player1, i, 1, true, mgs.player1.handSize);
				}
				else
				{
					break;
				}
			}
		}
		//Else, if it is player2's turn...
		else if(mgs.player2Turn)
		{
			//Loop through the player's hand...
			for(int i = 0; i < mgs.player2.cardsInHand.Length; i++)
			{
				//Search for the name of the selected card...
				if(mgs.selectedCard.name == mgs.player2.cardsInHand[i].name && mgs.player2.cardsInHand[i].name != null)
				{
					//Remove the card from the hand (cannot be selected, moved, or altered in the hand anymore) and fix the hand so there are no nulls in between
					mgs.player2.cardsInHand[i] = null;
					mgs.player2.DecreaseCardsInHand();
					mgs.player2.handSize--;
					
					break;
				}
			}
			
			for(int i = 0; i < mgs.player2.cardsInHand.Length; i++)
			{
				if(mgs.player2.cardsInHand[i] != null)
				{
					mgs.CalculateCardPositions(mgs.player2.cardsInHand[i].gameObject, mgs.player2, i, 1, true, mgs.player2.handSize);
				}
				else
				{
					break;
				}
			}
		}
	}
}