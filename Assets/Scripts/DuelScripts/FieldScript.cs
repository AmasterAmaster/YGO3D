using UnityEngine;
using System.Collections;

public class FieldScript : MonoBehaviour
{
	//Global menu variables
	#pragma warning disable 0414
	public MainGameScript mgs;
	public DebugScript debug;
	RaycastHit hit;
	#pragma warning restore 0414
	
	public bool debugMode;

	public void CheckFieldValidity()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		//Loop through all the rays (monster card zones only)...
		for(int i = 0; i < mgs.positionRays.Length; i++)
		{
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone1RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ01P1taken = true;
					mgs.attackP1Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone1P1.transform.position, true);
				}
				else
				{
					mgs.MZ01P1taken = false;
					if(GameObject.Find("ModelMZ1P1") != null)
						Destroy(GameObject.Find("ModelMZ1P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone1P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone2RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ02P1taken = true;
					mgs.attackP1Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone2P1.transform.position, true);
				}
				else
				{
					mgs.MZ02P1taken = false;
					if(GameObject.Find("ModelMZ2P1") != null)
						Destroy(GameObject.Find("ModelMZ2P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone2P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone3RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ03P1taken = true;
					mgs.attackP1Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone3P1.transform.position, true);
				}
				else
				{
					mgs.MZ03P1taken = false;
					if(GameObject.Find("ModelMZ3P1") != null)
						Destroy(GameObject.Find("ModelMZ3P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone3P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone4RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ04P1taken = true;
					mgs.attackP1Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone4P1.transform.position, true);
				}
				else
				{
					mgs.MZ04P1taken = false;
					if(GameObject.Find("ModelMZ4P1") != null)
						Destroy(GameObject.Find("ModelMZ4P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone4P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone5RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ05P1taken = true;
					mgs.attackP1Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone5P1.transform.position, true);
				}
				else
				{
					mgs.MZ05P1taken = false;
					if(GameObject.Find("ModelMZ5P1") != null)
						Destroy(GameObject.Find("ModelMZ5P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone5P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone1RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ01P2taken = true;
					mgs.attackP2Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone1P2.transform.position, true);
				}
				else
				{
					mgs.MZ01P2taken = false;
					if(GameObject.Find("ModelMZ1P2") != null)
						Destroy(GameObject.Find("ModelMZ1P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone1P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone2RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ02P2taken = true;
					mgs.attackP2Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone2P2.transform.position, true);
				}
				else
				{
					mgs.MZ02P2taken = false;
					if(GameObject.Find("ModelMZ2P2") != null)
						Destroy(GameObject.Find("ModelMZ2P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone2P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone3RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ03P2taken = true;
					mgs.attackP2Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone3P2.transform.position, true);
				}
				else
				{
					mgs.MZ03P2taken = false;
					if(GameObject.Find("ModelMZ3P2") != null)
						Destroy(GameObject.Find("ModelMZ3P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone3P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone4RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ04P2taken = true;
					mgs.attackP2Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone4P2.transform.position, true);
				}
				else
				{
					mgs.MZ04P2taken = false;
					if(GameObject.Find("ModelMZ4P2") != null)
						Destroy(GameObject.Find("ModelMZ4P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone4P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "MonsterCardZone5RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.MZ05P2taken = true;
					mgs.attackP2Directly = false;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone5P2.transform.position, true);
				}
				else
				{
					mgs.MZ05P2taken = false;
					if(GameObject.Find("ModelMZ5P2") != null)
						Destroy(GameObject.Find("ModelMZ5P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.monsterCardZone5P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone1RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ01P1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone1P1.transform.position, true);
				}
				else
				{
					mgs.STZ01P1taken = false;
					if(GameObject.Find("ModelSTZ1P1") != null)
						Destroy(GameObject.Find("ModelSTZ1P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone1P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone2RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ02P1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone2P1.transform.position, true);
				}
				else
				{
					mgs.STZ02P1taken = false;
					if(GameObject.Find("ModelSTZ2P1") != null)
						Destroy(GameObject.Find("ModelSTZ2P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone2P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone3RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ03P1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone3P1.transform.position, true);
				}
				else
				{
					mgs.STZ03P1taken = false;
					if(GameObject.Find("ModelSTZ3P1") != null)
						Destroy(GameObject.Find("ModelSTZ3P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone3P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone4RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ04P1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone4P1.transform.position, true);
				}
				else
				{
					mgs.STZ04P1taken = false;
					if(GameObject.Find("ModelSTZ4P1") != null)
						Destroy(GameObject.Find("ModelSTZ4P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone4P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone5RayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ05P1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone5P1.transform.position, true);
				}
				else
				{
					mgs.STZ05P1taken = false;
					if(GameObject.Find("ModelSTZ5P1") != null)
						Destroy(GameObject.Find("ModelSTZ5P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone5P1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone1RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ01P2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone1P2.transform.position, true);
				}
				else
				{
					mgs.STZ01P2taken = false;
					if(GameObject.Find("ModelSTZ1P2") != null)
						Destroy(GameObject.Find("ModelSTZ1P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone1P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone2RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ02P2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone2P2.transform.position, true);
				}
				else
				{
					mgs.STZ02P2taken = false;
					if(GameObject.Find("ModelSTZ2P2") != null)
						Destroy(GameObject.Find("ModelSTZ2P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone2P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone3RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ03P2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone3P2.transform.position, true);
				}
				else
				{
					mgs.STZ03P2taken = false;
					if(GameObject.Find("ModelSTZ3P2") != null)
						Destroy(GameObject.Find("ModelSTZ3P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone3P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone4RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ04P2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone4P2.transform.position, true);
				}
				else
				{
					mgs.STZ04P2taken = false;
					if(GameObject.Find("ModelSTZ4P2") != null)
						Destroy(GameObject.Find("ModelSTZ4P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone4P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "S/TCardZone5RayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.STZ05P2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone5P2.transform.position, true);
				}
				else
				{
					mgs.STZ05P2taken = false;
					if(GameObject.Find("ModelSTZ5P2") != null)
						Destroy(GameObject.Find("ModelSTZ5P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.STCardZone5P2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "FieldZoneRayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.fieldP1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.fieldZoneP1.transform.position, true);
				}
				else
				{
					mgs.fieldP1taken = false;
					if(GameObject.Find("ModelFZ1P1") != null)
						Destroy(GameObject.Find("ModelFZ1P1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.fieldZoneP1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "FieldZoneRayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.fieldP2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.fieldZoneP2.transform.position, true);
				}
				else
				{
					mgs.fieldP2taken = false;
					if(GameObject.Find("ModelFZ1P2") != null)
						Destroy(GameObject.Find("ModelFZ1P2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.fieldZoneP2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "PendulumRedRayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.PendulumRedP1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumRedP1.transform.position, true);
				}
				else
				{
					mgs.PendulumRedP1taken = false;
					if(GameObject.Find("ModelPRP1") != null)
						Destroy(GameObject.Find("ModelPRP1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumRedP1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "PendulumBlueRayP1")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.PendulumBlueP1taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumBlueP1.transform.position, true);
				}
				else
				{
					mgs.PendulumBlueP1taken = false;
					if(GameObject.Find("ModelPBP1") != null)
						Destroy(GameObject.Find("ModelPBP1"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumBlueP1.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "PendulumRedRayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.PendulumRedP2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumRedP2.transform.position, true);
				}
				else
				{
					mgs.PendulumRedP2taken = false;
					if(GameObject.Find("ModelPRP2") != null)
						Destroy(GameObject.Find("ModelPRP2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumRedP2.transform.position, false);
				}
			}
			
			//Check for a ray name...
			if(mgs.positionRays[i].name == "PendulumBlueRayP2")
			{
				originOfRay = mgs.positionRays[i].transform.position;
				
				//Check if it is occupied...
				if(Physics.Raycast(originOfRay, direction, out hit))
				{
					mgs.PendulumBlueP2taken = true;
					
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumBlueP2.transform.position, true);
				}
				else
				{
					mgs.PendulumBlueP2taken = false;
					if(GameObject.Find("ModelPBP2") != null)
						Destroy(GameObject.Find("ModelPBP2"));
						
					if(debugMode)
						debug.DrawFieldCheck(originOfRay, mgs.PendulumBlueP2.transform.position, false);
				}
			}
		}
	}
}