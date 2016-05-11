using UnityEngine;
using System.Collections;

public class TargetAndNotificationSystemScript : MonoBehaviour
{
	public MainGameScript mgs;
	public DebugScript debug;
	RaycastHit hit;
	
	public bool debugMode = false;
	
	public void CheckForTributableMonsters()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		Material myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		//if(Shader.Find("Unlit/Transparent Cutout").isSupported)
		//myMaterial = new Material(Shader.Find("Unlit/Transparent Cutout"));
		if(Shader.Find("Particles/Alpha Blended").isSupported)
			myMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		else
			myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		
		float offset = 0.5f;
		
		mgs.targetingMode = true;
		mgs.tributeTargeting = true;
		
		//Amount needed for this tribute summon
		if(mgs.selectedCard.GetComponent<Card>().level > 4 && mgs.selectedCard.GetComponent<Card>().level < 7)
		{
			mgs.neededAmountToTribute = 1;
		}
		else if(mgs.selectedCard.GetComponent<Card>().level > 6)
		{
			mgs.neededAmountToTribute = 2;
		}
		
		//Loop through all the rays (monster card zones only)...
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			if(mgs.player1Turn)
			{
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone1RayP1" || mgs.positionRays[i].name == "MonsterCardZone2RayP1" || mgs.positionRays[i].name == "MonsterCardZone3RayP1" || mgs.positionRays[i].name == "MonsterCardZone4RayP1" || mgs.positionRays[i].name == "MonsterCardZone5RayP1")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						//If it is in defense mode
						if(hit.transform.gameObject.GetComponent<ExtraCardProperties>().canBeTributed)
						{
							if(!hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show dashed line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetDashedTexture);
								targetNotification.layer = 14;
							}
							else if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show solid line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
								targetNotification.layer = 14;
							}
							
							if(debugMode)
								debug.DrawSelectionCheck(originOfRay, hit.transform.position);
						}
					}
				}
			}
			else if(mgs.player2Turn)
			{
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone1RayP2" || mgs.positionRays[i].name == "MonsterCardZone2RayP2" || mgs.positionRays[i].name == "MonsterCardZone3RayP2" || mgs.positionRays[i].name == "MonsterCardZone4RayP2" || mgs.positionRays[i].name == "MonsterCardZone5RayP2")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						//If it is in defense mode
						if(hit.transform.gameObject.GetComponent<ExtraCardProperties>().canBeTributed)
						{
							if(!hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show dashed line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetDashedTexture);
								targetNotification.layer = 14;
							}
							else if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show solid line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
								targetNotification.layer = 14;
							}
							
							if(debugMode)
								debug.DrawSelectionCheck(originOfRay, hit.transform.position);
						}
					}
				}
			}
		}
	}
	
	public void NotifyUserOfAttackingCards()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		Material myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		
		//if(Shader.Find("Unlit/Transparent Cutout").isSupported)
		//myMaterial = new Material(Shader.Find("Unlit/Transparent Cutout"));
		if(Shader.Find("Particles/Alpha Blended").isSupported)
			myMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		else
			myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		
		//Loop through all the rays (monster card zones only)...
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			//Check if it is player1's turn...
			if(mgs.player1Turn)
			{
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone1RayP1")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ1P1_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ1P1_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ1P1_AttackNotification.name = "MZ1P1_AttackNotification";
								mgs.MZ1P1_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ1P1_AttackNotification.transform.rotation = Quaternion.Euler(0, 180, 0);
								mgs.MZ1P1_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ1P1_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ1P1_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ1P1_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ1P1_AttackNotification.layer = 13;
								
								mgs.MZ1P1_AttackNotification_Start = mgs.MZ1P1_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ1P1_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ1P1_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ1P1_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ1P1_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone2RayP1")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ2P1_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ2P1_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ2P1_AttackNotification.name = "MZ2P1_AttackNotification";
								mgs.MZ2P1_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ2P1_AttackNotification.transform.rotation = Quaternion.Euler(0, 180, 0);
								mgs.MZ2P1_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ2P1_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ2P1_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ2P1_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ2P1_AttackNotification.layer = 13;
								
								mgs.MZ2P1_AttackNotification_Start = mgs.MZ2P1_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ2P1_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ2P1_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ2P1_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ2P1_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone3RayP1")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ3P1_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ3P1_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ3P1_AttackNotification.name = "MZ3P1_AttackNotification";
								mgs.MZ3P1_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ3P1_AttackNotification.transform.rotation = Quaternion.Euler(0, 180, 0);
								mgs.MZ3P1_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ3P1_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ3P1_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ3P1_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ3P1_AttackNotification.layer = 13;
								
								mgs.MZ3P1_AttackNotification_Start = mgs.MZ3P1_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ3P1_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ3P1_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ3P1_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ3P1_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone4RayP1")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ4P1_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ4P1_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ4P1_AttackNotification.name = "MZ4P1_AttackNotification";
								mgs.MZ4P1_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ4P1_AttackNotification.transform.rotation = Quaternion.Euler(0, 180, 0);
								mgs.MZ4P1_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ4P1_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ4P1_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ4P1_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ4P1_AttackNotification.layer = 13;
								
								mgs.MZ4P1_AttackNotification_Start = mgs.MZ4P1_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ4P1_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ4P1_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ4P1_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ4P1_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone5RayP1")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ5P1_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ5P1_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ5P1_AttackNotification.name = "MZ5P1_AttackNotification";
								mgs.MZ5P1_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ5P1_AttackNotification.transform.rotation = Quaternion.Euler(0, 180, 0);
								mgs.MZ5P1_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ5P1_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ5P1_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ5P1_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ5P1_AttackNotification.layer = 13;
								
								mgs.MZ5P1_AttackNotification_Start = mgs.MZ5P1_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ5P1_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ5P1_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ5P1_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ5P1_AttackNotification);
						}
					}
				}
			}
			else if(mgs.player2Turn)
			{
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone1RayP2")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ1P2_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ1P2_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ1P2_AttackNotification.name = "MZ1P2_AttackNotification";
								mgs.MZ1P2_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ1P2_AttackNotification.transform.rotation = Quaternion.Euler(0, 0, 0);
								mgs.MZ1P2_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ1P2_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ1P2_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ1P2_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ1P2_AttackNotification.layer = 13;
								
								mgs.MZ1P2_AttackNotification_Start = mgs.MZ1P2_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ1P2_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ1P2_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ1P2_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ1P2_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone2RayP2")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ2P2_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ2P2_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ2P2_AttackNotification.name = "MZ2P2_AttackNotification";
								mgs.MZ2P2_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ2P2_AttackNotification.transform.rotation = Quaternion.Euler(0, 0, 0);
								mgs.MZ2P2_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ2P2_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ2P2_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ2P2_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ2P2_AttackNotification.layer = 13;
								
								mgs.MZ2P2_AttackNotification_Start = mgs.MZ2P2_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ2P2_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ2P2_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ2P2_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ2P2_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone3RayP2")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ3P2_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ3P2_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ3P2_AttackNotification.name = "MZ3P2_AttackNotification";
								mgs.MZ3P2_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ3P2_AttackNotification.transform.rotation = Quaternion.Euler(0, 0, 0);
								mgs.MZ3P2_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ3P2_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ3P2_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ3P2_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ3P2_AttackNotification.layer = 13;
								
								mgs.MZ3P2_AttackNotification_Start = mgs.MZ3P2_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ3P2_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ3P2_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ3P2_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ3P2_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone4RayP2")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ4P2_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ4P2_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ4P2_AttackNotification.name = "MZ4P2_AttackNotification";
								mgs.MZ4P2_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ4P2_AttackNotification.transform.rotation = Quaternion.Euler(0, 0, 0);
								mgs.MZ4P2_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ4P2_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ4P2_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ4P2_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ4P2_AttackNotification.layer = 13;
								
								mgs.MZ4P2_AttackNotification_Start = mgs.MZ4P2_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ4P2_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ4P2_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ4P2_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ4P2_AttackNotification);
						}
					}
				}
				
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone5RayP2")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						if((hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense) && hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttack || hit.transform.gameObject.GetComponent<ExtraCardProperties>().canAttackInDefenseMode)
						{
							if(mgs.MZ5P2_AttackNotification == null)
							{
								//Create plane with a texture (with a unique name so it can be destroyed later)
								mgs.MZ5P2_AttackNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								mgs.MZ5P2_AttackNotification.name = "MZ5P2_AttackNotification";
								mgs.MZ5P2_AttackNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.5f, hit.transform.position.z);
								mgs.MZ5P2_AttackNotification.transform.rotation = Quaternion.Euler(0, 0, 0);
								mgs.MZ5P2_AttackNotification.transform.localScale = new Vector3(mgs.cardWidth / 2, hit.transform.localScale.y, mgs.cardHeight / 2);
								mgs.MZ5P2_AttackNotification.GetComponent<MeshCollider>().enabled = false;
								mgs.MZ5P2_AttackNotification.GetComponent<Renderer>().material = myMaterial;
								mgs.MZ5P2_AttackNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.attackTexture);
								mgs.MZ5P2_AttackNotification.layer = 13;
								
								mgs.MZ5P2_AttackNotification_Start = mgs.MZ5P2_AttackNotification.transform.position;
							}
						}
						else
						{
							//If the notification is still there...
							if(mgs.MZ5P2_AttackNotification != null)
							{
								//Remove it because the card is non-existent
								Destroy(mgs.MZ5P2_AttackNotification);
							}
						}
					}
					else
					{
						//If the notification is still there...
						if(mgs.MZ5P2_AttackNotification != null)
						{
							//Remove it because the card is non-existent
							Destroy(mgs.MZ5P2_AttackNotification);
						}
					}
				}
			}
		}
	}
	
	public void NotifyUserOfTargetableCardsForAttacking()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		Material myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		
		//if(Shader.Find("Unlit/Transparent Cutout").isSupported)
		//myMaterial = new Material(Shader.Find("Unlit/Transparent Cutout"));
		if(Shader.Find("Particles/Alpha Blended").isSupported)
			myMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		else
			myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		
		float offset = 0.5f;
		
		//Loop through all the rays...
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			if(mgs.player1Turn)
			{
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone1RayP2" || mgs.positionRays[i].name == "MonsterCardZone2RayP2" || mgs.positionRays[i].name == "MonsterCardZone3RayP2" || mgs.positionRays[i].name == "MonsterCardZone4RayP2" || mgs.positionRays[i].name == "MonsterCardZone5RayP2")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						//if it is within some paramiters
						if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().canBeTargetedByBattle)
						{
							if(!hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show dashed line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetDashedTexture);
								targetNotification.layer = 14;
							}
							else if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show solid line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
								targetNotification.layer = 14;
							}
							
							if(debugMode)
								debug.DrawSelectionCheck(originOfRay, hit.transform.position);
						}
					}
					else
					{
						if(GameObject.Find("targetNotification" + i) != null)
						{
							Destroy(GameObject.Find("targetNotification" + i));
						}
					}
				}
			}
			else if(mgs.player2Turn)
			{
				//Check for a ray name...
				if(mgs.positionRays[i].name == "MonsterCardZone1RayP1" || mgs.positionRays[i].name == "MonsterCardZone2RayP1" || mgs.positionRays[i].name == "MonsterCardZone3RayP1" || mgs.positionRays[i].name == "MonsterCardZone4RayP1" || mgs.positionRays[i].name == "MonsterCardZone5RayP1")
				{
					originOfRay = mgs.positionRays[i].transform.position;
					
					//Check if it is occupied...
					if(Physics.Raycast(originOfRay, direction, out hit))
					{
						//if it is within some paramiters
						if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().canBeTargetedByBattle)
						{
							if(!hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show dashed line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetDashedTexture);
								targetNotification.layer = 14;
							}
							else if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show solid line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
								targetNotification.layer = 14;
							}
							
							if(debugMode)
								debug.DrawSelectionCheck(originOfRay, hit.transform.position);
						}
					}
					else
					{
						if(GameObject.Find("targetNotification" + i) != null)
						{
							Destroy(GameObject.Find("targetNotification" + i));
						}
					}
				}
			}
		}
	}
	
	//Should be a catch all for any target from any card effect
	//public void CheckForTargetableCards(bool attackPosition = false, bool defensePosition = false, bool attackValue = false, int ATK = 0, bool defenseValue = false, int DEF = 0, bool canAttack = false, bool canActivate = false, bool checkCardType = false, bool checkCardAttrabute = false, bool hasCounters = false, bool aliasName = false, bool controllerOfCardIsEffectedOnTheField = false)
	public void CheckForTargetableCards(string command = "", bool equalToTheValue = true, bool lessThanTheValue = true)
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		bool attackPosition = false;
		bool defensePosition = false;
		bool attackValue = false;
		bool defenseValue = false;
		bool canAttack = false;
		bool canActivate = false;
		bool checkCardType = false;
		bool checkCardAttribute = false;
		bool hasCounters = false;
		bool aliasName = false;
		bool controllerEffected = false;
		
		Material myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		//if(Shader.Find("Unlit/Transparent Cutout").isSupported)
		//myMaterial = new Material(Shader.Find("Unlit/Transparent Cutout"));
		if(Shader.Find("Particles/Alpha Blended").isSupported)
			myMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		else
			myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
			
		float offset = 0.5f;
		
		//Determains the action by string command...
		switch(command)
		{
			case "": break;
			case "attackPosition": attackPosition = true; break;
			case "defensePosition": defensePosition = true; break;
			case "attackValue":	//Use ATK and compare it to the mgs.selectedCard (or some targeted value)
				attackValue = true;
				//if(equalToTheValue)
					//
				break;
			case "defenseValue": defenseValue = true; break;	//Use DEF and compare it to the mgs.selectedCard (or some targeted value)
			case "canAttack": canAttack = true; break;
			case "canActivate": canActivate = true; break;
			case "checkCardType": checkCardType = true; break;	//Use CardType and compare it to the mgs.selectedCard (or some targeted value)
			case "checkCardAttribute": checkCardAttribute = true; break;	//Use DEF and compare it to the mgs.selectedCard (or some targeted value)
			case "hasCounters": hasCounters = true; break;
			case "aliasName": aliasName = true; break;
			case "controllerEffected": controllerEffected = true; break;
			default: break;
		}
		
		//If ANY of the above cases are TRUE, then use that ray to select the card and target it (or make it selectable for the user)
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone1RayP1" || mgs.positionRays[i].name == "MonsterCardZone2RayP1" || mgs.positionRays[i].name == "MonsterCardZone3RayP1" || mgs.positionRays[i].name == "MonsterCardZone4RayP1" || mgs.positionRays[i].name == "MonsterCardZone5RayP1" ||
				mgs.positionRays[i].name == "MonsterCardZone1RayP2" || mgs.positionRays[i].name == "MonsterCardZone2RayP2" || mgs.positionRays[i].name == "MonsterCardZone3RayP2" || mgs.positionRays[i].name == "MonsterCardZone4RayP2" || mgs.positionRays[i].name == "MonsterCardZone5RayP2" ||
			   	mgs.positionRays[i].name == "S/TCardZone1RayP1" || mgs.positionRays[i].name == "S/TCardZone2RayP1" || mgs.positionRays[i].name == "S/TCardZone3RayP1" || mgs.positionRays[i].name == "S/TCardZone4RayP1" || mgs.positionRays[i].name == "S/TCardZone5RayP1" ||
			   	mgs.positionRays[i].name == "S/TCardZone1RayP2" || mgs.positionRays[i].name == "S/TCardZone2RayP2" || mgs.positionRays[i].name == "S/TCardZone3RayP2" || mgs.positionRays[i].name == "S/TCardZone4RayP2" || mgs.positionRays[i].name == "S/TCardZone5RayP2" ||
			   	mgs.positionRays[i].name == "FieldZoneRayP1" || mgs.positionRays[i].name == "FieldZoneRayP1" || mgs.positionRays[i].name == "PendulumRedRayP1" || mgs.positionRays[i].name == "PendulumRedRayP2" || mgs.positionRays[i].name == "PendulumBlueRayP1" || mgs.positionRays[i].name == "PendulumBlueRayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					//If this card can be targeted by effect
					if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().canBeTargetedByCardeffect)
					{
						//If any of the above parameters are true
						if(attackPosition || defensePosition || attackValue || defenseValue || canAttack || canActivate || checkCardType || checkCardAttribute || hasCounters || aliasName || controllerEffected)
						{
							if(!hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show dashed line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.none)
									targetNotification.transform.rotation = Quaternion.identity;
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetDashedTexture);
								targetNotification.layer = 14;
							}
							else if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//To prevent duplicates...
								if(GameObject.Find("targetNotification" + i) != null)
								{
									Destroy(GameObject.Find("targetNotification" + i));
								}
								
								//Show solid line in front of card
								GameObject targetNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
								targetNotification.name = "targetNotification" + i;
								targetNotification.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + offset, hit.transform.position.z);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									targetNotification.transform.rotation = Quaternion.identity;
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									targetNotification.transform.rotation = Quaternion.Euler(0, 90, 0);
								if(hit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.none)
									targetNotification.transform.rotation = Quaternion.identity;
								targetNotification.transform.localScale = new Vector3(mgs.cardWidth, hit.transform.localScale.y, mgs.cardHeight);
								targetNotification.GetComponent<MeshCollider>().enabled = false;
								targetNotification.GetComponent<Renderer>().material = myMaterial;
								targetNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
								targetNotification.layer = 14;
							}
							
							if(debugMode)
								debug.DrawSelectionCheck(originOfRay, hit.transform.position);
						}
					}
					else
					{
						if(GameObject.Find("targetNotification" + i) != null)
						{
							Destroy(GameObject.Find("targetNotification" + i));
						}
					}
				}
			}
		}
	}
	
	public void NotifyUserOfDiscardingCards()
	{
		Material myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		
		//if(Shader.Find("Unlit/Transparent Cutout").isSupported)
		//myMaterial = new Material(Shader.Find("Unlit/Transparent Cutout"));
		if(Shader.Find("Particles/Alpha Blended").isSupported)
			myMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		else
			myMaterial = new Material(Shader.Find("Mobile/VertexLit"));
		
		float offset = -0.5f;
		
		//Depending on the turn...
		if(mgs.player1Turn)
		{
			offset = -0.5f;
			
			//Loop for each card in the hand...
			for(int i = 0; i < mgs.player1.cardsInHand.Length; i++)
			{
				//If it is not null
				if(mgs.player1.cardsInHand[i] != null)
				{
					//If the card is not selected
					if(!mgs.player1.cardsInHand[i].GetComponent<ExtraCardProperties>().isSelected)
					{
						//To prevent duplicates...
						if(GameObject.Find("discardNotification" + i) != null)
						{
							Destroy(GameObject.Find("discardNotification" + i));
						}
						
						//Show dashed line in front of card
						GameObject discardNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
						discardNotification.name = "discardNotification" + i;
						discardNotification.transform.position = new Vector3(mgs.player1.cardsInHand[i].transform.position.x, mgs.player1.cardsInHand[i].transform.position.y, mgs.player1.cardsInHand[i].transform.position.z + offset);
						discardNotification.transform.rotation = Quaternion.Euler(90, 180, 0);
						discardNotification.transform.localScale = new Vector3(mgs.cardWidth, mgs.player1.cardsInHand[i].transform.localScale.y, mgs.cardHeight);
						discardNotification.GetComponent<MeshCollider>().enabled = false;
						discardNotification.GetComponent<Renderer>().material = myMaterial;
						discardNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetDashedTexture);
						discardNotification.layer = 14;
					}
					//Else, if the card is selected
					else if(mgs.player1.cardsInHand[i].GetComponent<ExtraCardProperties>().isSelected)
					{
						//To prevent duplicates...
						if(GameObject.Find("discardNotification" + i) != null)
						{
							Destroy(GameObject.Find("discardNotification" + i));
						}
						
						//Show solid line in front of card
						GameObject discardNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
						discardNotification.name = "discardNotification" + i;
						discardNotification.transform.position = new Vector3(mgs.player1.cardsInHand[i].transform.position.x, mgs.player1.cardsInHand[i].transform.position.y, mgs.player1.cardsInHand[i].transform.position.z + offset);
						discardNotification.transform.rotation = Quaternion.Euler(90, 180, 0);
						discardNotification.transform.localScale = new Vector3(mgs.cardWidth, mgs.player1.cardsInHand[i].transform.localScale.y, mgs.cardHeight);
						discardNotification.GetComponent<MeshCollider>().enabled = false;
						discardNotification.GetComponent<Renderer>().material = myMaterial;
						discardNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						discardNotification.layer = 14;
					}
				}
			}
		}
		else if(mgs.player2Turn)
		{
			offset = 0.5f;
			
			//Loop for each card in the hand...
			for(int i = 0; i < mgs.player2.cardsInHand.Length; i++)
			{
				//If it is not null
				if(mgs.player2.cardsInHand[i] != null)
				{
					//If the card is not selected
					if(!mgs.player2.cardsInHand[i].GetComponent<ExtraCardProperties>().isSelected)
					{
						//To prevent duplicates...
						if(GameObject.Find("discardNotification" + i) != null)
						{
							Destroy(GameObject.Find("discardNotification" + i));
						}
						
						//Show dashed line in front of card
						GameObject discardNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
						discardNotification.name = "discardNotification" + i;
						discardNotification.transform.position = new Vector3(mgs.player2.cardsInHand[i].transform.position.x, mgs.player2.cardsInHand[i].transform.position.y, mgs.player2.cardsInHand[i].transform.position.z + offset);
						discardNotification.transform.rotation = Quaternion.Euler(90, 180, 0);
						discardNotification.transform.localScale = new Vector3(mgs.cardWidth, mgs.player2.cardsInHand[i].transform.localScale.y, mgs.cardHeight);
						discardNotification.GetComponent<MeshCollider>().enabled = false;
						discardNotification.GetComponent<Renderer>().material = myMaterial;
						discardNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetDashedTexture);
						discardNotification.layer = 14;
					}
					//Else, if the card is selected
					else if(mgs.player2.cardsInHand[i].GetComponent<ExtraCardProperties>().isSelected)
					{
						//To prevent duplicates...
						if(GameObject.Find("discardNotification" + i) != null)
						{
							Destroy(GameObject.Find("discardNotification" + i));
						}
						
						//Show solid line in front of card
						GameObject discardNotification = GameObject.CreatePrimitive(PrimitiveType.Plane);
						discardNotification.name = "discardNotification" + i;
						discardNotification.transform.position = new Vector3(mgs.player2.cardsInHand[i].transform.position.x, mgs.player2.cardsInHand[i].transform.position.y, mgs.player2.cardsInHand[i].transform.position.z + offset);
						discardNotification.transform.rotation = Quaternion.Euler(90, 180, 0);
						discardNotification.transform.localScale = new Vector3(mgs.cardWidth, mgs.player2.cardsInHand[i].transform.localScale.y, mgs.cardHeight);
						discardNotification.GetComponent<MeshCollider>().enabled = false;
						discardNotification.GetComponent<Renderer>().material = myMaterial;
						discardNotification.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						discardNotification.layer = 14;
					}
				}
			}
		}
	}
	
	public void DestroyNotifications()
	{
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			if(GameObject.Find("targetNotification" + i) != null)
			{
				Destroy(GameObject.Find("targetNotification" + i));
			}
		}
	}
}