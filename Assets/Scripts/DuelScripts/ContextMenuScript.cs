using UnityEngine;
using System.Collections;

public class ContextMenuScript : MonoBehaviour
{
	//Global menu variables
	public MainGameScript mgs;
	public SummonScript summon;
	public AttackScript attack;
	public TargetAndNotificationSystemScript tans;
	public ActivateScript activate;
	
	public GameObject menuItem;
	public Material myNewMaterial = new Material(Shader.Find("Unlit/Texture"));
	public Material alphaMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
	public float placementX = 0f;
	public float placementY = 0f;
	public float placementZ = 0f;
	public float increment = 0.5f;
	public Vector3 cardPosition;
	
	public Player player1;
	public Player player2;
	public bool player1Turn;
	public bool player2Turn;

	//Menu button variables
	public Texture emptyTexture;
	public Texture normalSummonTexture;
	public Texture tributeSummonTexture;
	public Texture specialSummonTexture;
	public Texture setTexture;
	public Texture tributeTexture;
	public Texture tributeSetTexture;
	public Texture drawTexture;
	public Texture discardTexture;
	public Texture banishTexture;
	public Texture millTexture;
	public Texture activateTexture;
	public Texture attackTexture;
	public Texture toAttackTexture;
	public Texture toDefenseTexture;
	public Texture viewTexture;
	
	public GameObject positionSelection; //The panel behind the card
	public GameObject cardSelection; //The card that is selectable in front of the panel
	
	//Options variables
	public bool manualDueling = false;
	
	void Awake()
	{
		//Find the gameobject to instanciate
		menuItem = GameObject.Find("MenuItemPlaceholder");
		positionSelection = GameObject.Find("SelectorPanel");
		cardSelection = GameObject.Find("CardItemPlaceholder");
		
		//Find the main game script
		mgs = GameObject.Find("GameManager").GetComponent<MainGameScript>();
		summon = GameObject.Find("GameManager").GetComponent<SummonScript>();
		attack = GameObject.Find("GameManager").GetComponent<AttackScript>();
		tans = GameObject.Find("GameManager").GetComponent<TargetAndNotificationSystemScript>();
		activate = GameObject.Find("GameManager").GetComponent<ActivateScript>();
		
		//Find the cameras
		player1 = GameObject.Find("Player1").GetComponent<Player>();
		player2 = GameObject.Find("Player2").GetComponent<Player>();
		
		//Load the menu textures
		emptyTexture = (Texture)Resources.Load("EmptyButton", typeof(Texture));
		normalSummonTexture = (Texture)Resources.Load("NormalSummonButton", typeof(Texture));
		tributeSummonTexture = (Texture)Resources.Load("TributeSummonButton", typeof(Texture));
		specialSummonTexture = (Texture)Resources.Load("SpecialSummonButton", typeof(Texture));
		setTexture = (Texture)Resources.Load("SetButton", typeof(Texture));
		tributeTexture = (Texture)Resources.Load("TributeButton", typeof(Texture));
		tributeSetTexture = (Texture)Resources.Load("TributeSetButton", typeof(Texture));
		drawTexture = (Texture)Resources.Load("DrawButton", typeof(Texture));
		discardTexture = (Texture)Resources.Load("DiscardButton", typeof(Texture));
		banishTexture = (Texture)Resources.Load("BanishButton", typeof(Texture));
		millTexture = (Texture)Resources.Load("MillButton", typeof(Texture));
		activateTexture = (Texture)Resources.Load("ActivateButton", typeof(Texture));
		attackTexture = (Texture)Resources.Load("AttackButton", typeof(Texture));
		toAttackTexture = (Texture)Resources.Load("ToAttackButton", typeof(Texture));
		toDefenseTexture = (Texture)Resources.Load("ToDefenseButton", typeof(Texture));
		viewTexture = (Texture)Resources.Load("ViewButton", typeof(Texture));
		
		//Load up the option that has manual dueling here
	}
	
	//Turn on the menus
	public void CreateMenu()
	{
		//Check whos turn it is
		player1Turn = mgs.player1Turn;
		player2Turn = mgs.player2Turn;
	
		//If we are manual dueling...
		if(manualDueling)
		{
		
		}
		//Else, handle this automatically and have the appropriate actions available...
		else
		{
			//If it is a monster card...
			if(this.gameObject.GetComponent<Card>().GetCardType() == Card.CardType.effectMonster || this.gameObject.GetComponent<Card>().GetCardType() == Card.CardType.normalMonster)
			{
				//In the hand
				if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand)
				{
					//Can be normal summoned...
					if(this.gameObject.GetComponent<ExtraCardProperties>().normalSummonable && mgs.usedNormalSummons < mgs.allowedNormalSummons)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Normal Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", normalSummonTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Normal Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", normalSummonTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be tribute summoned...
					if(this.gameObject.GetComponent<ExtraCardProperties>().tributeSummonable && mgs.usedNormalSummons < mgs.allowedNormalSummons)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Tribute Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", tributeSummonTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Tribute Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", tributeSummonTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be tribute Set...
					if(this.gameObject.GetComponent<ExtraCardProperties>().tributeSummonable && this.gameObject.GetComponent<ExtraCardProperties>().setable && mgs.usedNormalSummons < mgs.allowedNormalSummons)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Tribute Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", tributeSetTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Tribute Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", tributeSetTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be special summoned...
					if(this.gameObject.GetComponent<ExtraCardProperties>().specialSummonable)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be set...
					if(this.gameObject.GetComponent<ExtraCardProperties>().setable && mgs.usedNormalSummons < mgs.allowedNormalSummons)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", setTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", setTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate && this.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					
					//Increment the next menu position
					placementY = placementY + increment;
				}
				//Else, on the field
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
				{
					//Can change battle positions...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canChangeBattlePositions && (mgs.phase == MainGameScript.Phases.main1 || mgs.phase == MainGameScript.Phases.main2))
					{
						//To attack mode
						if(this.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense || this.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || this.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
						{
							//Show the menu action (to face up attack)
							if(player1Turn)
							{
								GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
								menuClone.name = "Menu Action: To Attack";
								menuClone.GetComponent<Renderer>().material = myNewMaterial;
								menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", toAttackTexture);
							}
							else if(player2Turn)
							{
								GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
								menuClone.name = "Menu Action: To Attack";
								menuClone.GetComponent<Renderer>().material = myNewMaterial;
								menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", toAttackTexture);
							}
							
							//Increment the next menu position
							placementY = placementY + increment;
						}
						//To defense mode
						if(this.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack)
						{
							//Show the menu action (to face up defense)
							if(player1Turn)
							{
								GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
								menuClone.name = "Menu Action: To Defense";
								menuClone.GetComponent<Renderer>().material = myNewMaterial;
								menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", toDefenseTexture);
							}
							else if(player2Turn)
							{
								GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
								menuClone.name = "Menu Action: To Defense";
								menuClone.GetComponent<Renderer>().material = myNewMaterial;
								menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", toDefenseTexture);
							}
							
							//Increment the next menu position
							placementY = placementY + increment;
						}
					}
					//Can attack...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canAttack && mgs.phase == MainGameScript.Phases.battle && !(this.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack || this.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense))
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
							menuClone.name = "Menu Action: Attack";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", attackTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
							menuClone.name = "Menu Action: Attack";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", attackTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be tributed...
//					if(this.gameObject.GetComponent<ExtraCardProperties>().canbeTributed)
//					{
//						//Show the menu action
//						if(player1Turn)
//						{
//							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
//							menuClone.name = "Menu Action: Tribute";
//							menuClone.GetComponent<Renderer>().material = myNewMaterial;
//							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", tributeTexture);
//						}
//						else if(player2Turn)
//						{
//							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
//							menuClone.name = "Menu Action: Tribute";
//							menuClone.GetComponent<Renderer>().material = myNewMaterial;
//							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", tributeTexture);
//						}
//						
//						//Increment the next menu position
//						placementY = placementY + increment;
//					}
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate && this.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the graveyard
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.graveyard)
				{
					//Can be special summoned...
					if(this.gameObject.GetComponent<ExtraCardProperties>().specialSummonable)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the banish zone
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.banished)
				{
					//Can be special summoned...
					if(this.gameObject.GetComponent<ExtraCardProperties>().specialSummonable)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the deck
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.deck)
				{
					//Can be special summoned...
					if(this.gameObject.GetComponent<ExtraCardProperties>().specialSummonable)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the extra deck
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.extraDeck)
				{
					//Can be special summoned...
					if(this.gameObject.GetComponent<ExtraCardProperties>().specialSummonable)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Special Summon";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", specialSummonTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
			}
			//Else, if it is a spell card...
			else if(this.gameObject.GetComponent<Card>().GetCardType() == Card.CardType.spell)
			{
				//In the hand
				if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be set...
					if(this.gameObject.GetComponent<ExtraCardProperties>().setable)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", setTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", setTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, on the field
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the graveyard
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.graveyard)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the banish zone
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.banished)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the deck
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.deck)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the extra deck
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.extraDeck)
				{
					//Not possible!
				}
			}
			//Else, if it is a trap card...
			else if(this.gameObject.GetComponent<Card>().GetCardType() == Card.CardType.trap)
			{
				//In the hand
				if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be set...
					if(this.gameObject.GetComponent<ExtraCardProperties>().setable)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", setTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Set";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", setTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, on the field
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the graveyard
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.graveyard)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the banish zone
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.banished)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the deck
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.deck)
				{
					//Can be activated...
					if(this.gameObject.GetComponent<ExtraCardProperties>().canActivate)
					{
						//Show the menu action
						if(player1Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 180, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						else if(player2Turn)
						{
							GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 0, 0));
							menuClone.name = "Menu Action: Activate";
							menuClone.GetComponent<Renderer>().material = myNewMaterial;
							menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", activateTexture);
						}
						
						//Increment the next menu position
						placementY = placementY + increment;
					}
					//Can be viewed...
					//Show the menu action
					if(player1Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x + increment, cardPosition.y + placementY, cardPosition.z - increment), Quaternion.Euler(90, 90, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
					else if(player2Turn)
					{
						GameObject menuClone = (GameObject)Instantiate(menuItem, new Vector3(cardPosition.x - increment, cardPosition.y + placementY, cardPosition.z + increment), Quaternion.Euler(90, 270, 0));
						menuClone.name = "Menu Action: View";
						menuClone.GetComponent<Renderer>().material = myNewMaterial;
						menuClone.GetComponent<Renderer>().material.SetTexture("_MainTex", viewTexture);
					}
				}
				//Else, in the extra deck
				else if(this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.extraDeck)
				{
					//Not possible!
				}
			}
		}
	}
	
	public void SpecialSummonMenu()
	{
		float XOffset = 1.2f;
		float YOffset = 1.1f;
		float ZOffset = 4f;
		float ZOffsetCard = 3.9f;
	
		//If we want to special summon
		if(mgs.special)
		{
			if(mgs.selectedCard.GetComponent<ExtraCardProperties>().playerChoiceOnSpecialSummon)
			{
				if(mgs.allowedToBeFaceUpAttack)
				{
					if(mgs.player1Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player1.gameObject.transform.position.x - XOffset, mgs.player1.gameObject.transform.position.y + YOffset, mgs.player1.gameObject.transform.position.z + ZOffset), Quaternion.Euler(90f, 180f, 0f));
						selector.name = "Menu Action: FaceUpAttackSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 10;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player1.gameObject.transform.position.x - XOffset, mgs.player1.gameObject.transform.position.y + YOffset, mgs.player1.gameObject.transform.position.z + ZOffsetCard), Quaternion.Euler(90f, 180f, 0f));
						cardSelection2.name = "Card: FaceUpAttackSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardFrontTexture());
					}
					else if(mgs.player2Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player2.gameObject.transform.position.x + XOffset, mgs.player2.gameObject.transform.position.y + YOffset, mgs.player2.gameObject.transform.position.z - ZOffset), Quaternion.Euler(90f, 0f, 0f));
						selector.name = "Menu Action: FaceUpAttackSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 11;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player2.gameObject.transform.position.x + XOffset, mgs.player2.gameObject.transform.position.y + YOffset, mgs.player2.gameObject.transform.position.z - ZOffsetCard), Quaternion.Euler(90f, 0f, 0f));
						cardSelection2.name = "Card: FaceUpAttackSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardFrontTexture());
					}
				}
				
				if(mgs.allowedToBeFaceUpDefense)
				{
					if(mgs.player1Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player1.gameObject.transform.position.x + XOffset, mgs.player1.gameObject.transform.position.y + YOffset, mgs.player1.gameObject.transform.position.z + ZOffset), Quaternion.Euler(90f, 180f, 0f));
						selector.name = "Menu Action: FaceUpDefenseSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 10;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player1.gameObject.transform.position.x + XOffset, mgs.player1.gameObject.transform.position.y + YOffset, mgs.player1.gameObject.transform.position.z + ZOffsetCard), Quaternion.Euler(0f, 90f, 270f));
						cardSelection2.name = "Card: FaceUpDefenseSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardFrontTexture());
					}
					else if(mgs.player2Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player2.gameObject.transform.position.x - XOffset, mgs.player2.gameObject.transform.position.y + YOffset, mgs.player2.gameObject.transform.position.z - ZOffset), Quaternion.Euler(90f, 0f, 0f));
						selector.name = "Menu Action: FaceUpDefenseSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 11;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player2.gameObject.transform.position.x - XOffset, mgs.player2.gameObject.transform.position.y + YOffset, mgs.player2.gameObject.transform.position.z - ZOffsetCard), Quaternion.Euler(0f, 270f, 270f));
						cardSelection2.name = "Card: FaceUpDefenseSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardFrontTexture());
					}
				}
				
				if(mgs.allowedToBeFaceDownAttack)
				{
					if(mgs.player1Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player1.gameObject.transform.position.x - XOffset, mgs.player1.gameObject.transform.position.y - YOffset, mgs.player1.gameObject.transform.position.z + ZOffset), Quaternion.Euler(90f, 180f, 0f));
						selector.name = "Menu Action: FaceDownAttackSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 10;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player1.gameObject.transform.position.x - XOffset, mgs.player1.gameObject.transform.position.y - YOffset, mgs.player1.gameObject.transform.position.z + ZOffsetCard), Quaternion.Euler(90f, 180f, 0f));
						cardSelection2.name = "Card: FaceDownAttackSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardBackTexture());
					}
					else if(mgs.player2Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player2.gameObject.transform.position.x + XOffset, mgs.player2.gameObject.transform.position.y - YOffset, mgs.player2.gameObject.transform.position.z - ZOffset), Quaternion.Euler(90f, 0f, 0f));
						selector.name = "Menu Action: FaceDownAttackSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 11;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player2.gameObject.transform.position.x + XOffset, mgs.player2.gameObject.transform.position.y - YOffset, mgs.player2.gameObject.transform.position.z - ZOffsetCard), Quaternion.Euler(90f, 0f, 0f));
						cardSelection2.name = "Card: FaceDownAttackSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardBackTexture());
					}
				}
				
				if(mgs.allowedToBeFaceDownDefense)
				{
					if(mgs.player1Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player1.gameObject.transform.position.x + XOffset, mgs.player1.gameObject.transform.position.y - YOffset, mgs.player1.gameObject.transform.position.z + ZOffset), Quaternion.Euler(90f, 180f, 0f));
						selector.name = "Menu Action: FaceDownDefenseSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 10;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player1.gameObject.transform.position.x + XOffset, mgs.player1.gameObject.transform.position.y - YOffset, mgs.player1.gameObject.transform.position.z + ZOffsetCard), Quaternion.Euler(0f, 90f, 270f));
						cardSelection2.name = "Card: FaceDownDefenseSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardBackTexture());
					}
					else if(mgs.player2Turn)
					{
						GameObject selector = (GameObject)Instantiate(positionSelection, new Vector3(mgs.player2.gameObject.transform.position.x - XOffset, mgs.player2.gameObject.transform.position.y - YOffset, mgs.player2.gameObject.transform.position.z - ZOffset), Quaternion.Euler(90f, 0f, 0f));
						selector.name = "Menu Action: FaceDownDefenseSelection";
						selector.GetComponent<Renderer>().material = alphaMaterial;
						selector.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.targetSolidTexture);
						selector.layer = 11;
						
						GameObject cardSelection2 = (GameObject)Instantiate(cardSelection, new Vector3(mgs.player2.gameObject.transform.position.x - XOffset, mgs.player2.gameObject.transform.position.y - YOffset, mgs.player2.gameObject.transform.position.z - ZOffsetCard), Quaternion.Euler(0f, 270f, 270f));
						cardSelection2.name = "Card: FaceDownDefenseSelection";
						cardSelection2.GetComponent<Renderer>().material = myNewMaterial;
						cardSelection2.GetComponent<Renderer>().material.SetTexture("_MainTex", mgs.selectedCard.GetComponent<Card>().GetCardBackTexture());
					}
				}
			}
		}
	}
	
	//Turn off the menus
	public void DestroyMenu()
	{
		//Turn off/destroy menu
		if(GameObject.Find("Menu Action: Empty") != null)
			Destroy(GameObject.Find("Menu Action: Empty"));
		if(GameObject.Find("Menu Action: Normal Summon") != null)
			Destroy(GameObject.Find("Menu Action: Normal Summon"));
		if(GameObject.Find("Menu Action: Tribute Summon") != null)
        	Destroy(GameObject.Find("Menu Action: Tribute Summon"));
		if(GameObject.Find("Menu Action: Special Summon") != null)
			Destroy(GameObject.Find("Menu Action: Special Summon"));
		if(GameObject.Find("Menu Action: Set") != null)
        	Destroy(GameObject.Find("Menu Action: Set"));
		if(GameObject.Find("Menu Action: Tribute") != null)
			Destroy(GameObject.Find("Menu Action: Tribute"));
		if(GameObject.Find("Menu Action: Tribute Set") != null)
			Destroy(GameObject.Find("Menu Action: Tribute Set"));
		if(GameObject.Find("Menu Action: Draw") != null)
        	Destroy(GameObject.Find("Menu Action: Draw"));
		if(GameObject.Find("Menu Action: Discard") != null)
			Destroy(GameObject.Find("Menu Action: Discard"));
		if(GameObject.Find("Menu Action: Banish") != null)
        	Destroy(GameObject.Find("Menu Action: Banish"));
		if(GameObject.Find("Menu Action: Mill") != null)
			Destroy(GameObject.Find("Menu Action: Mill"));
		if(GameObject.Find("Menu Action: Activate") != null)
        	Destroy(GameObject.Find("Menu Action: Activate"));
		if(GameObject.Find("Menu Action: Attack") != null)
			Destroy(GameObject.Find("Menu Action: Attack"));
		if(GameObject.Find("Menu Action: To Attack") != null)
        	Destroy(GameObject.Find("Menu Action: To Attack"));
		if(GameObject.Find("Menu Action: To Defense") != null)
			Destroy(GameObject.Find("Menu Action: To Defense"));
		if(GameObject.Find("Menu Action: View") != null)
			Destroy(GameObject.Find("Menu Action: View"));
			
		if(GameObject.Find("Menu Action: FaceUpAttackSelection") != null)
			Destroy(GameObject.Find("Menu Action: FaceUpAttackSelection"));
		if(GameObject.Find("Menu Action: FaceUpDefenseSelection") != null)
			Destroy(GameObject.Find("Menu Action: FaceUpDefenseSelection"));
		if(GameObject.Find("Menu Action: FaceDownAttackSelection") != null)
			Destroy(GameObject.Find("Menu Action: FaceDownAttackSelection"));
		if(GameObject.Find("Menu Action: FaceDownDefenseSelection") != null)
			Destroy(GameObject.Find("Menu Action: FaceDownDefenseSelection"));
		if(GameObject.Find("Card: FaceUpAttackSelection") != null)
			Destroy(GameObject.Find("Card: FaceUpAttackSelection"));
		if(GameObject.Find("Card: FaceUpDefenseSelection") != null)
			Destroy(GameObject.Find("Card: FaceUpDefenseSelection"));
		if(GameObject.Find("Card: FaceDownAttackSelection") != null)
			Destroy(GameObject.Find("Card: FaceDownAttackSelection"));
		if(GameObject.Find("Card: FaceDownDefenseSelection") != null)
			Destroy(GameObject.Find("Card: FaceDownDefenseSelection"));
	
		//Reset the global variables
		placementX = 0;
		placementY = 0;
		placementZ = 0;
		
		//Turn off the component to save update cycles
		enabled = false;
	}
	
	void Update()
	{
		//Listen for mouse clicks for selecting the proper action
		//Get the mouse click (high enough priority to be on the top level update)
		Ray mouseRayP1 = player1.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		Ray mouseRayP2 = player2.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		
		RaycastHit mouseHit;
		
		//If the ray cast physics...
		if(player1Turn && Physics.Raycast(mouseRayP1, out mouseHit))
		{
			if(mouseHit.collider.gameObject.name == "Menu Action: Empty" && Input.GetButtonDown("Fire1"))
			{
				//Do nothing
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Normal Summon" && Input.GetButtonDown("Fire1"))
			{
				//Normal summon the card
				mgs.normal = true;
				summon.Summon();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Tribute Summon" && Input.GetButtonDown("Fire1"))
			{
				//Tribute summon the card
				mgs.tribute = true;
				tans.CheckForTributableMonsters();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Tribute Set" && Input.GetButtonDown("Fire1"))
			{
				//Tribute Set the card
				//mgs.tribute = true;
				mgs.tributeSetting = true;
				tans.CheckForTributableMonsters();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Special Summon" && Input.GetButtonDown("Fire1"))
			{
				//Remove the menu
				DestroyMenu();
				
				//Special summon the card
				mgs.special = true;
				SpecialSummonMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Set" && Input.GetButtonDown("Fire1"))
			{
				//Set the card
				mgs.set = true;
				
				//If it is a monster...
				if(this.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster || this.gameObject.GetComponent<Card>().cardType == Card.CardType.normalMonster)
				{
					summon.Summon();
				}
				//If it is a spell or trap
				else if(this.gameObject.GetComponent<Card>().cardType == Card.CardType.spell || this.gameObject.GetComponent<Card>().cardType == Card.CardType.trap)
				{
					activate.Activate();
				}
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Tribute" && Input.GetButtonDown("Fire1"))
			{
				//Tribute the card
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Draw" && Input.GetButtonDown("Fire1"))
			{
				//Draw the card
				mgs.Draw(mgs.player1, 1);
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Discard" && Input.GetButtonDown("Fire1"))
			{
				//Discard the card
				mgs.Discard(mgs.player1, 1);
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Banish" && Input.GetButtonDown("Fire1"))
			{
				//Banish the card
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Mill" && Input.GetButtonDown("Fire1"))
			{
				//Mill the card
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Activate" && Input.GetButtonDown("Fire1"))
			{
				//Activate the card
				activate.Activate();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Attack" && Input.GetButtonDown("Fire1"))
			{
				//Attak with the card
				attack.Attack();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: To Attack" && Input.GetButtonDown("Fire1"))
			{
				//Change to attack position
				mgs.goingToFaceUpAttack = true;
				mgs.BattlePositionChange();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: To Defense" && Input.GetButtonDown("Fire1"))
			{
				//Change to defense position
				mgs.goingToFaceUpDefense = true;
				mgs.BattlePositionChange();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: View" && Input.GetButtonDown("Fire1"))
			{
				//Change to defense position
				mgs.ViewCard();
				
				//Remove the menu
				DestroyMenu();
			}
		}
		//If the ray cast physics...
		if(player2Turn && Physics.Raycast(mouseRayP2, out mouseHit))
		{
			if(mouseHit.collider.gameObject.name == "Menu Action: Empty" && Input.GetButtonDown("Fire1"))
			{
				//Do nothing
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Normal Summon" && Input.GetButtonDown("Fire1"))
			{
				//Normal summon the card
				mgs.normal = true;
				summon.Summon();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Tribute Summon" && Input.GetButtonDown("Fire1"))
			{
				//Tribute summon the card
				mgs.tribute = true;
				tans.CheckForTributableMonsters();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Tribute Set" && Input.GetButtonDown("Fire1"))
			{
				//Tribute Set the card
				//mgs.tribute = true;
				mgs.tributeSetting = true;
				tans.CheckForTributableMonsters();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Special Summon" && Input.GetButtonDown("Fire1"))
			{
				//Remove the menu
				DestroyMenu();
				
				//Special summon the card
				mgs.special = true;
				SpecialSummonMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Set" && Input.GetButtonDown("Fire1"))
			{
				//Set the card
				mgs.set = true;
				
				//If it is a monster...
				if(this.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster || this.gameObject.GetComponent<Card>().cardType == Card.CardType.normalMonster)
				{
					summon.Summon();
				}
				//If it is a spell or trap
				else if(this.gameObject.GetComponent<Card>().cardType == Card.CardType.spell || this.gameObject.GetComponent<Card>().cardType == Card.CardType.trap)
				{
					activate.Activate();
				}
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Tribute" && Input.GetButtonDown("Fire1"))
			{
				//Tribute the card
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Draw" && Input.GetButtonDown("Fire1"))
			{
				//Draw the card
				mgs.Draw(mgs.player2, 1);
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Discard" && Input.GetButtonDown("Fire1"))
			{
				//Discard the card
				mgs.Discard(mgs.player2, 1);
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Banish" && Input.GetButtonDown("Fire1"))
			{
				//Banish the card
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Mill" && Input.GetButtonDown("Fire1"))
			{
				//Mill the card
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Activate" && Input.GetButtonDown("Fire1"))
			{
				//Activate the card
				activate.Activate();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: Attack" && Input.GetButtonDown("Fire1"))
			{
				//Attak with the card
				attack.Attack();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: To Attack" && Input.GetButtonDown("Fire1"))
			{
				//Change to attack position
				mgs.goingToFaceUpAttack = true;
				mgs.BattlePositionChange();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: To Defense" && Input.GetButtonDown("Fire1"))
			{
				//Change to defense position
				mgs.goingToFaceUpDefense = true;
				mgs.BattlePositionChange();
				
				//Remove the menu
				DestroyMenu();
			}
			if(mouseHit.collider.gameObject.name == "Menu Action: View" && Input.GetButtonDown("Fire1"))
			{
				//Change to defense position
				mgs.ViewCard();
				
				//Remove the menu
				DestroyMenu();
			}
		}
	}
}