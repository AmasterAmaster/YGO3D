using UnityEngine;
using System.Collections;

public class SummonScript : MonoBehaviour
{
	//Global menu variables
	public MainGameScript mgs;
	public FieldScript field;
	public DebugScript debug;
	#pragma warning disable 0414
	RaycastHit hit;
	#pragma warning restore 0414
	
	public bool debugMode = false;
	
	//Used for general Normal, Set, Tribute, and Special Summons
	public void Summon()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		field.CheckFieldValidity();
		
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
					if(mgs.positionRays[i].name == "MonsterCardZone3RayP1" && !mgs.MZ03P1taken /*&& !mgs.MZ02P1taken && !mgs.MZ04P1taken && !mgs.MZ01P1taken && !mgs.MZ05P1taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player1POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone3P1.transform.position;
							mgs.modelNameIdentifier = "MZ3P1";
							
							mgs.MZ03P1taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone3P1.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone2RayP1" && mgs.MZ03P1taken && !mgs.MZ02P1taken /*&& !mgs.MZ04P1taken && !mgs.MZ01P1taken && !mgs.MZ05P1taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player1POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone2P1.transform.position;
							mgs.modelNameIdentifier = "MZ2P1";
							
							mgs.MZ02P1taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone2P1.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone4RayP1" && mgs.MZ03P1taken && mgs.MZ02P1taken && !mgs.MZ04P1taken /*&& !mgs.MZ01P1taken && !mgs.MZ05P1taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player1POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone4P1.transform.position;
							mgs.modelNameIdentifier = "MZ4P1";
							
							mgs.MZ04P1taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone4P1.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone1RayP1" && mgs.MZ03P1taken && mgs.MZ02P1taken && mgs.MZ04P1taken && !mgs.MZ01P1taken /*&& !mgs.MZ05P1taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player1POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone1P1.transform.position;
							mgs.modelNameIdentifier = "MZ1P1";
							
							mgs.MZ01P1taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone1P1.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone5RayP1" && mgs.MZ03P1taken && mgs.MZ02P1taken && mgs.MZ04P1taken && mgs.MZ01P1taken && !mgs.MZ05P1taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player1POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone5P1.transform.position;
							mgs.modelNameIdentifier = "MZ5P1";
							
							mgs.MZ05P1taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone5P1.transform.position);
							break;
						}
					}
				}
				//Else, it is the other player's turn...
				else if(mgs.player2Turn)
				{
					//Check for a ray name...
					if(mgs.positionRays[i].name == "MonsterCardZone3RayP2" && !mgs.MZ03P2taken /*&& !mgs.MZ02P2taken && !mgs.MZ04P2taken && !mgs.MZ01P2taken && !mgs.MZ05P2taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player2POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone3P2.transform.position;
							mgs.modelNameIdentifier = "MZ3P2";
							
							mgs.MZ03P2taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone3P2.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone2RayP2" && mgs.MZ03P2taken && !mgs.MZ02P2taken /*&& !mgs.MZ04P2taken && !mgs.MZ01P2taken && !mgs.MZ05P2taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player2POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone2P2.transform.position;
							mgs.modelNameIdentifier = "MZ2P2";
							
							mgs.MZ02P2taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone2P2.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone4RayP2" && mgs.MZ03P2taken && mgs.MZ02P2taken && !mgs.MZ04P2taken /*&& !mgs.MZ01P2taken && !mgs.MZ05P2taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player2POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone4P2.transform.position;
							mgs.modelNameIdentifier = "MZ4P2";
							
							mgs.MZ04P2taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone4P2.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone1RayP2" && mgs.MZ03P2taken && mgs.MZ02P2taken && mgs.MZ04P2taken && !mgs.MZ01P2taken /*&& !mgs.MZ05P2taken*/)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player2POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone1P2.transform.position;
							mgs.modelNameIdentifier = "MZ1P2";
							
							mgs.MZ01P2taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone1P2.transform.position);
							break;
						}
					}
					//Check for a ray name...
					else if(mgs.positionRays[i].name == "MonsterCardZone5RayP2" && mgs.MZ03P2taken && mgs.MZ02P2taken && mgs.MZ04P2taken && mgs.MZ01P2taken && !mgs.MZ05P2taken)
					{
						originOfRay = mgs.positionRays[i].transform.position;
						
						//Check if it is not occupied...
						if(!Physics.Raycast(originOfRay, direction, out hit))
						{
							mgs.movingSummonCard = true;
							mgs.player2POV = true;
							mgs.endSummonPoint = mgs.monsterCardZone5P2.transform.position;
							mgs.modelNameIdentifier = "MZ5P2";
							
							mgs.MZ05P2taken = true;
							
							if(debugMode)
								debug.DrawSummonCheck(originOfRay, mgs.monsterCardZone5P2.transform.position);
							break;
						}
					}
				}
			}
		}
		//If selection card placement is selected...
		else if(mgs.selectionCardPlacement)
		{
			//Not supported...
		}
		//If random card placement is selected...
		else if(mgs.randomCardPlacement)
		{
			//Not supported...
		}
		
		mgs.startSummonPoint = mgs.selectedCard.transform.position;
		mgs.startSummonRotation = mgs.selectedCard.transform.rotation;
		
		if(mgs.player1POV)
		{
			if(mgs.normal)
			{
				mgs.endSummonRotation = Quaternion.Euler(90, 0, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player1;
				mgs.usedNormalSummons++;
			}
			else if(mgs.set)
			{
				mgs.endSummonRotation = Quaternion.Euler(270, 90, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player1;
				mgs.usedNormalSummons++;
			}
			else if(mgs.tribute)
			{
				mgs.endSummonRotation = Quaternion.Euler(90, 0, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player1;
				mgs.usedNormalSummons++;
				mgs.neededAmountToTribute = 0;
				mgs.selectedAnountToTribute = 0;
			}
			else if(mgs.tributeSetting)
			{
				mgs.endSummonRotation = Quaternion.Euler(270, 90, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player1;
				mgs.usedNormalSummons++;
				mgs.neededAmountToTribute = 0;
				mgs.selectedAnountToTribute = 0;
				mgs.tributeSetting = false;
			}
			else if(mgs.special)
			{
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player1;
				
				//Turn off the GUI
				//selectedCard.GetComponent<ExtraCardProperties>().playerChoiceOnSpecialSummon = false;
			}
		}
		else if(mgs.player2POV)
		{
			if(mgs.normal)
			{
				mgs.endSummonRotation = Quaternion.Euler(90, 180, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player2;
				mgs.usedNormalSummons++;
			}
			else if(mgs.set)
			{
				mgs.endSummonRotation = Quaternion.Euler(270, 270, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player2;
				mgs.usedNormalSummons++;
			}
			else if(mgs.tribute)
			{
				mgs.endSummonRotation = Quaternion.Euler(90, 180, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player2;
				mgs.usedNormalSummons++;
				mgs.neededAmountToTribute = 0;
				mgs.selectedAnountToTribute = 0;
			}
			else if(mgs.tributeSetting)
			{
				mgs.endSummonRotation = Quaternion.Euler(270, 270, 0);
				mgs.selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player2;
				mgs.usedNormalSummons++;
				mgs.neededAmountToTribute = 0;
				mgs.selectedAnountToTribute = 0;
				mgs.tributeSetting = false;
			}
			else if(mgs.special)
			{
				mgs.selectedCard.GetComponent<ExtraCardProperties>().controller = mgs.player2;
				
				//Turn off the GUI
				//selectedCard.GetComponent<ExtraCardProperties>().playerChoiceOnSpecialSummon = false;
			}
		}
		
		//The card is no longer in the hand (take it out of the player hand array and change th state of the card)
		mgs.selectedCard.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.field;
		
		//If it is player1's turn...
		if(mgs.player1Turn)
		{
			//Loop through the player's hand...
			for(int i = 0; i < mgs.player1.cardsInHand.Length; i++)
			{
				if(mgs.player1.cardsInHand[i] != null)
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
				if(mgs.player2.cardsInHand[i] != null)
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
		
		mgs.selectedCard.GetComponent<ExtraCardProperties>().canChangeBattlePositions = false;
		
		mgs.calculateRotationOnce = true;
		mgs.tributeTargeting = false;
		
		mgs.oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
		
		field.CheckFieldValidity();
	}
	
	public void RitualSummon()
	{
		//Do something special
		
		//Summon
		Summon();
	}
	
	public void FusionSummon()
	{
		//Do something special
		
		//Summon
		Summon();
	}
	
	public void SynchroSummon()
	{
		//Do something special
		
		//Summon
		Summon();
	}
	
	public void XYZSummon()
	{
		//Do something special
		
		//Summon
		Summon();
	}
	
	public void PendulumSummon()
	{
		//Do something special
		
		//Summon
		Summon();
	}
}