using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour
{
	public MainGameScript mgs;
	public FieldScript field;
	public DebugScript debug;
	public TargetAndNotificationSystemScript tans;
	RaycastHit hit;
	
	public bool debugMode = false;

	public void Attack()
	{
		//Temp variables
		//Vector3 originOfRay = new Vector3(0, 0, 0);
		//Vector3 direction = Vector3.down;
		mgs.firstAttackingMonster = mgs.selectedCard;
		
		mgs.attackP1Directly = true;
		mgs.attackP2Directly = true;
		field.CheckFieldValidity();
		
		//Check if a card effect allows for a direct attack... (show GUI here)
		
		if(mgs.directAttackByCardEffect && mgs.selectedCard.GetComponent<ExtraCardProperties>().canAttackDirectly)
		{
			//Check all other effects if there is a chain or other effect
			
			//Conduct the attack
			if(mgs.player1Turn)
			{
				mgs.player2.DecreaseLifePoints(mgs.firstAttackingMonster.GetComponent<Card>().attack);
				
				if(debugMode)
					debug.DrawAttackCheck(mgs.firstAttackingMonster.transform.position, mgs.player2.gameObject.transform.position);
			}
			else if(mgs.player2Turn)
			{
				mgs.player1.DecreaseLifePoints(mgs.firstAttackingMonster.GetComponent<Card>().attack);
				
				if(debugMode)
					debug.DrawAttackCheck(mgs.firstAttackingMonster.transform.position, mgs.player1.gameObject.transform.position);
			}
			
			//Check for a winnner
			mgs.CheckForWinCondition();
		}
		
		//If there are no monster on the other side of the field...
		if(mgs.attackP1Directly || mgs.attackP2Directly)
		{
			if(mgs.player1Turn && mgs.attackP2Directly)
			{
				mgs.player2.DecreaseLifePoints(mgs.firstAttackingMonster.GetComponent<Card>().attack);
				
				if(debugMode)
					debug.DrawAttackCheck(mgs.firstAttackingMonster.transform.position, mgs.player2.gameObject.transform.position);
			}
			else if(mgs.player2Turn && mgs.attackP1Directly)
			{
				mgs.player1.DecreaseLifePoints(mgs.firstAttackingMonster.GetComponent<Card>().attack);
				
				if(debugMode)
					debug.DrawAttackCheck(mgs.firstAttackingMonster.transform.position, mgs.player1.gameObject.transform.position);
			}
			
			//Check for a winnner
			mgs.CheckForWinCondition();
			
			mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttack = false;
			mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canChangeBattlePositions = false;
		}
		//Else, there is monsters on the other side of the field...
		else
		{
			//Targeting mode
			mgs.targetingMode = true;
			mgs.attackTargeting = true;
			
			tans.NotifyUserOfTargetableCardsForAttacking();
		}
		
		tans.NotifyUserOfAttackingCards();
		
		//Better to be safe than sorry, check if there are slots open just in case of special summons
		field.CheckFieldValidity();
	}
	
	public void Battle()
	{
		//Turn off the targeting notifications
		tans.DestroyNotifications();
		
		mgs.attackTargeting = false;
		
		//Check card properties for position
		if((mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttack) &&
		   (mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canBeTargetedByBattle))
		{
			//Calculate the damage dealt (Attack position card VS Attack position card)
			if(mgs.firstAttackingMonster.GetComponent<Card>().attack > mgs.secondTargetedMonster.GetComponent<Card>().attack)
			{
				//Destroy the second monster and inflict the difference as battle damage to the targeted player (given effects permitting)
				mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner.DecreaseLifePoints(mgs.firstAttackingMonster.GetComponent<Card>().attack - mgs.secondTargetedMonster.GetComponent<Card>().attack);
				mgs.moveDestroyedCard = true;
				mgs.secondMonsterDestroyed = true;
				mgs.startDestroyPoint = mgs.secondTargetedMonster.transform.position;
				
				//Get the end point
				if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
				
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack < mgs.secondTargetedMonster.GetComponent<Card>().attack)
			{
				//Destroy the first monster and inflict the difference as battle damage to the attacking player (given effects permitting)
				mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner.DecreaseLifePoints(mgs.secondTargetedMonster.GetComponent<Card>().attack - mgs.firstAttackingMonster.GetComponent<Card>().attack);
				mgs.moveDestroyedCard = true;
				mgs.firstMonsterDestroyed = true;
				mgs.startDestroyPoint = mgs.firstAttackingMonster.transform.position;
				
				//Get the end point
				if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack == mgs.secondTargetedMonster.GetComponent<Card>().attack)
			{
				//Destroy both monsters with no damage to either player (given effects permitting)
				mgs.moveDestroyedCard = true;
				mgs.bothMonstersDestroyed = true;
				mgs.startDestroyPoint = mgs.firstAttackingMonster.transform.position;
				mgs.startDestroyPoint2 = mgs.secondTargetedMonster.transform.position;
				
				//Get the end point
				if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
				
				if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint2 = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint2 = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation2 = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation2 = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint2 = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint2 = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation2 = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation2 = Quaternion.Euler(90, 180, 0);
				}
			}
		}
		else if((mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttack) &&
		        (mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canBeTargetedByBattle))
		{
			//Calculate the damage dealt (Attack position card VS Defense position card)
			if(mgs.firstAttackingMonster.GetComponent<Card>().attack > mgs.secondTargetedMonster.GetComponent<Card>().defense)
			{
				//Destroy the second monster (given effects permitting)
				if(mgs.isPiercing)
				{
					mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner.DecreaseLifePoints(mgs.firstAttackingMonster.GetComponent<Card>().attack - mgs.secondTargetedMonster.GetComponent<Card>().attack);
				}
				mgs.moveDestroyedCard = true;
				mgs.secondMonsterDestroyed = true;
				mgs.startDestroyPoint = mgs.secondTargetedMonster.transform.position;
				
				//Get the end point
				if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack < mgs.secondTargetedMonster.GetComponent<Card>().defense)
			{
				//No monsters are destroyed and inflict the difference as battle damage to the attacking player (given effects permitting)
				mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner.DecreaseLifePoints(mgs.secondTargetedMonster.GetComponent<Card>().defense - mgs.firstAttackingMonster.GetComponent<Card>().attack);
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack == mgs.secondTargetedMonster.GetComponent<Card>().defense)
			{
				//Nothing happens in this battle (given effects permitting)
			}
		}
		else if((mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttack) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttackInDefenseMode) &&
		        (mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canBeTargetedByBattle))
		{
			//Calculate the damage dealt (Defense position card VS Attack position card)
			if(mgs.firstAttackingMonster.GetComponent<Card>().attack > mgs.secondTargetedMonster.GetComponent<Card>().attack)
			{
				//Destroy the second monster and inflict the difference as battle damage to the targeted player (given effects permitting)
				mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner.DecreaseLifePoints(mgs.firstAttackingMonster.GetComponent<Card>().attack - mgs.secondTargetedMonster.GetComponent<Card>().attack);
				mgs.moveDestroyedCard = true;
				mgs.secondMonsterDestroyed = true;
				mgs.startDestroyPoint = mgs.secondTargetedMonster.transform.position;
				
				//Get the end point
				if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack < mgs.secondTargetedMonster.GetComponent<Card>().attack)
			{
				//Destroy the first monster and inflict the difference as battle damage to the attacking player (given effects permitting)
				mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner.DecreaseLifePoints(mgs.secondTargetedMonster.GetComponent<Card>().attack - mgs.firstAttackingMonster.GetComponent<Card>().attack);
				mgs.moveDestroyedCard = true;
				mgs.firstMonsterDestroyed = true;
				mgs.startDestroyPoint = mgs.firstAttackingMonster.transform.position;
				
				//Get the end point
				if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack == mgs.secondTargetedMonster.GetComponent<Card>().attack)
			{
				//Destroy both monsters with no damage to either player (given effects permitting)
				mgs.moveDestroyedCard = true;
				mgs.bothMonstersDestroyed = true;
				mgs.startDestroyPoint = mgs.firstAttackingMonster.transform.position;
				mgs.startDestroyPoint2 = mgs.secondTargetedMonster.transform.position;
				
				//Get the end point
				if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.firstAttackingMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
				
				if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint2 = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint2 = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation2 = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation2 = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint2 = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint2 = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation2 = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation2 = Quaternion.Euler(90, 180, 0);
				}
			}
		}
		else if((mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttack) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttackInDefenseMode) &&
		        (mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && (mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canBeTargetedByBattle))
		{
			//Calculate the damage dealt (Defense position card VS Defense position card)
			if(mgs.firstAttackingMonster.GetComponent<Card>().attack > mgs.secondTargetedMonster.GetComponent<Card>().defense)
			{
				//Destroy the second monster (given effects permitting)
				mgs.moveDestroyedCard = true;
				mgs.secondMonsterDestroyed = true;
				mgs.startDestroyPoint = mgs.secondTargetedMonster.transform.position;
				
				//Get the end point
				if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player1)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP1.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP1.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 0, 0);
				}
				else if(mgs.secondTargetedMonster.GetComponent<ExtraCardProperties>().owner == mgs.player2)
				{
					if(mgs.isBanishing)
					{
						mgs.endDestroyPoint = mgs.banishAreaP2.transform.position;
					}
					else
					{
						mgs.endDestroyPoint = mgs.graveyardP2.transform.position;
					}
					
					mgs.startDestroyRotation = mgs.secondTargetedMonster.transform.rotation;
					mgs.endDestroyRotation = Quaternion.Euler(90, 180, 0);
				}
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack < mgs.secondTargetedMonster.GetComponent<Card>().defense)
			{
				//No monsters are destroyed and inflict the difference as battle damage to the attacking player (given effects permitting)
				mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().owner.DecreaseLifePoints(mgs.secondTargetedMonster.GetComponent<Card>().defense - mgs.firstAttackingMonster.GetComponent<Card>().attack);
			}
			else if(mgs.firstAttackingMonster.GetComponent<Card>().attack == mgs.secondTargetedMonster.GetComponent<Card>().defense)
			{
				//Nothing happens in this battle (given effects permitting)
			}
		}
		
		//Check for a winnner
		mgs.CheckForWinCondition();
		
		mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canAttack = false;
		mgs.firstAttackingMonster.GetComponent<ExtraCardProperties>().canChangeBattlePositions = false;
		
		//Better to be safe than sorry, check if there are slots open just in case of special summons
		field.CheckFieldValidity();
		
		if(debugMode)
			debug.DrawAttackCheck(mgs.firstAttackingMonster.transform.position, mgs.secondTargetedMonster.transform.position);
	}
	
	public void TurnOnBattleChanges()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		//Loop through all the rays (monster card zones only)...
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone1RayP1" || mgs.positionRays[i].name == "MonsterCardZone2RayP1" || mgs.positionRays[i].name == "MonsterCardZone3RayP1" || mgs.positionRays[i].name == "MonsterCardZone4RayP1" || mgs.positionRays[i].name == "MonsterCardZone5RayP1" ||
				mgs.positionRays[i].name == "MonsterCardZone1RayP2" || mgs.positionRays[i].name == "MonsterCardZone2RayP2" || mgs.positionRays[i].name == "MonsterCardZone3RayP2" || mgs.positionRays[i].name == "MonsterCardZone4RayP2" || mgs.positionRays[i].name == "MonsterCardZone5RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					hit.transform.gameObject.GetComponent<ExtraCardProperties>().canChangeBattlePositions = true;
				}
			}
		}
	}
	
	public void TurnOnAttacking()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		//Loop through all the rays (monster card zones only)...
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone1RayP1" || mgs.positionRays[i].name == "MonsterCardZone2RayP1" || mgs.positionRays[i].name == "MonsterCardZone3RayP1" || mgs.positionRays[i].name == "MonsterCardZone4RayP1" || mgs.positionRays[i].name == "MonsterCardZone5RayP1" ||
				mgs.positionRays[i].name == "MonsterCardZone1RayP2" || mgs.positionRays[i].name == "MonsterCardZone2RayP2" || mgs.positionRays[i].name == "MonsterCardZone3RayP2" || mgs.positionRays[i].name == "MonsterCardZone4RayP2" || mgs.positionRays[i].name == "MonsterCardZone5RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					//If it is in defense mode
					if(hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense)
					{
						//It cannot attack
						hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack = false;
					}
					else
					{
						//It can attack
						hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack = true;
					}
				}
			}
		}
	}
}