using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainGameScript : MonoBehaviour
{
	//Game mode variables
	[Tooltip("This mode means that this game is in the process of being debuged at the moment.")]
	public bool DebugGame = false;
	[Tooltip("This mode means that this game is fixed in a way to practice the game and mechanics (only Player1 gets this check).")]
	public bool Situational = false;
	[Tooltip("This mode means that this game is a normal multiplayer match (only Player1 and Player2 get this check).")]
	public bool multiplayerGame = false;
	[Tooltip("This mode means that this game is a normal AI match (only Player1 and AI (Player2) get this check).")]
	public bool AIGame = false;
	[Tooltip("This mode means that this game is being viewed by a spectator (Free roaming - Only other people get this check).")]
	public bool SpectatingGame = false;

	public OptionsScript options;

	//Persistent variables
	public Deck currentDeck;
	public ExtraDeck currentExtraDeck;
	public SideDeck currentSideDeck;
	public Deck currentDeckAI;
	public ExtraDeck currentExtraDeckAI;
	public SideDeck currentSideDeckAI;
	
	//Position variables
	public GameObject monsterCardZone1P1;
	public GameObject monsterCardZone2P1;
	public GameObject monsterCardZone3P1;
	public GameObject monsterCardZone4P1;
	public GameObject monsterCardZone5P1;
	public GameObject monsterCardZone1P2;
	public GameObject monsterCardZone2P2;
	public GameObject monsterCardZone3P2;
	public GameObject monsterCardZone4P2;
	public GameObject monsterCardZone5P2;
	
	public GameObject STCardZone1P1;
	public GameObject STCardZone2P1;
	public GameObject STCardZone3P1;
	public GameObject STCardZone4P1;
	public GameObject STCardZone5P1;
	public GameObject STCardZone1P2;
	public GameObject STCardZone2P2;
	public GameObject STCardZone3P2;
	public GameObject STCardZone4P2;
	public GameObject STCardZone5P2;
	
	public GameObject mainDeckAreaP1;
	public GameObject mainDeckAreaP2;
	public GameObject extraDeckAreaP1;
	public GameObject extraDeckAreaP2;
	public GameObject fieldZoneP1;
	public GameObject fieldZoneP2;
	public GameObject graveyardP1;
	public GameObject graveyardP2;
	public GameObject banishAreaP1;
	public GameObject banishAreaP2;
	public GameObject PendulumRedP1;
	public GameObject PendulumBlueP1;
	public GameObject PendulumRedP2;
	public GameObject PendulumBlueP2;
	
	public GameObject P1HandLeftSideLimit;
	public GameObject P1HandRightSideLimit;
	public GameObject P2HandLeftSideLimit;
	public GameObject P2HandRightSideLimit;
	
	public GameObject P1DrawRay;
	public GameObject P2DrawRay;
	public GameObject P1ExtraDeckRay;
	public GameObject P2ExtraDeckRay;
	
	public GameObject originTop;
	public GameObject originMiddle;
	public GameObject originBottom;
	
	public GameObject[] positionRays = new GameObject[26];

	//Game state variables
	public bool officiallyStarted = false;
	public bool dueling = false;
	public bool waitingForPlayer = false; //For any reason to hold the loop
	public bool waitingForEffect = false; //For any reason to hold the loop
	public bool waitingForAnimation = false; //For any reason to hold the loop
	
	public bool isBanishing = false;
	public bool isPiercing = false;
	
	public enum Phases {draw, standby, main1, battle, main2, end};
	[Tooltip("The current phase of the game.")] public Phases phase;
	public static Phases phaseStatic;
	
	public TextMesh turnCounter01;
	public TextMesh turnCounter02;
	
	//battle Phase variables
	public enum BattleSteps {startStep, battleStep, damageStep, endStep};
	[Tooltip("The current Battle Phase step of the game.")] public BattleSteps battleSteps;
	public static BattleSteps battleStepsStatic;
	
	public bool attackers = false;
	public GameObject firstAttackingMonster;
	public GameObject secondTargetedMonster;
	
	//Gameplay variables
	[Tooltip("The 1st player of the game.")]
	public Player player1;
	[Tooltip("The 2nd player of the game.")]
	public Player player2;
	
	[Tooltip("The 1st player's turn.")]
	public bool player1Turn = false;
	[Tooltip("The 2nd player's turn.")]
	public bool player2Turn = false;
	
	[Tooltip("The turn counter of the game.")]
	public int turnCounter = 0;
	
	[Tooltip("The starting Life Points of the game.")]
	public int startingLifePointCount = 8000;
	
	[Tooltip("The starting hand of the game.")]
	public int startingHand = 5;
	
	[Tooltip("The number of cards to draw per turn.")]
	public int cardsPerDraw = 1;
	
	[Tooltip("The time limit for each turn.")]
	public float turnTimeLimit = 180f;
	public float turnTimeLimitReset = 180f;
	
	[Tooltip("The obsolete ruling where the player can draw on their first turn, play only one field spell, etc.")]
	public bool obsoleteRulings = false;
	
	//Other variables
	public SummonScript summon;
	public AttackScript attack;
	public FieldScript field;
	public TargetAndNotificationSystemScript tans;
	public ActivateScript activate;
	public RockPaperScissorsScript rps;
	public Texture cardPreviewPicture;
	public GameObject card;
	public GameObject cardFront;
	public GameObject cardBack;
	
	public bool noMoreEffects = true;
	public GameObject targetableCard;
	public bool selectedBattlePhase = false;
	public bool selectedMainPhase2 = false;
	public bool selectedEndPhase = false;
	
	public RaycastHit mouseHit;
	public RaycastHit oldMouseHit;
	public GameObject selectedCard;
	
	public Camera topViewCamera;
	
	public Ray mouseRayP1;
	public Ray mouseRayP2;
	
	public float cardWidth = 0.225f;
	public float cardHeight = 0.325f;
	
	public float globalIncrement = 0.02f;
	
	public string modelNameIdentifier = "";
	
	public int allowedNormalSummons = 1;
	public int usedNormalSummons = 0;
	
	//Draw variables
	public RaycastHit hit;
	public float maxDistance = 20;
	public GameObject drawableCard;
	public bool movingDrawCard = false;
	public bool player1Drawing = false;
	public bool player2Drawing = false;
	public Vector3 startingPosition;
	public Quaternion startingRotation;
	public float drawTimer = 1f;
	public float resetDrawTimer = 1f;
	
	public float multiplyer = 1f;
	public float speedOfDrawPosition = 3f;
	public float speedOfDrawRotation = 3f;
	public float speedOfSummonPosition = 3f;
	public float speedOfSummonRotation = 3f;
	public float drawTimerPositioning = 0f;
	
	//Hand variables
	public bool calculateOnce = true;
	public float percentage = 0f;
	
	//Summon/card placement variables
	public bool normal = false;
	public bool set = false;
	public bool tribute = false;
	public bool special = false;
	
	public bool allowedToBeFaceUpAttack = true;
	public bool allowedToBeFaceUpDefense = true;
	public bool allowedToBeFaceDownAttack = true;
	public bool allowedToBeFaceDownDefense = true;
	
	public bool movingSummonCard = false;
	public bool predeterminedCardPlacement = true;
	public bool selectionCardPlacement = false;
	public bool randomCardPlacement = false;
	
	public Vector3 startSummonPoint = new Vector3(0, 0, 0);
	public Vector3 endSummonPoint = new Vector3(0, 0, 0);
	public Quaternion startSummonRotation;
	public Quaternion endSummonRotation;
	public float summonTimer = 1f;
	public float resetSummonTimer = 1f;
	public bool player1POV = false;
	public bool player2POV = false;
	public float summonTimerPositioning = 0f;
	public float speedOfTributePosition = 3f;
	public float speedOfTributeRotation = 3f;
	
	public bool tributeTargeting = false;
	public bool movingTributeCards = false;
	public int neededAmountToTribute = 0;
	public int selectedAnountToTribute = 0;
	public Vector3[] startingTributePositions = new Vector3[5];
	public Vector3 endTributePosition = new Vector3(0, 0, 0);
	public Quaternion[] startingTributeRotations = new Quaternion[5];
	public Quaternion endTributeRotation;
	public float tributeTimer = 1f;
	public float resetTributeTimer = 1f;
	public float tributeTimerPositioning = 0f;
	
	public bool tributeSetting = false;
	
	#pragma warning disable 0414
	public bool MZ01P1taken = false;
	public bool MZ02P1taken = false;
	public bool MZ03P1taken = false;
	public bool MZ04P1taken = false;
	public bool MZ05P1taken = false;
	public bool MZ01P2taken = false;
	public bool MZ02P2taken = false;
	public bool MZ03P2taken = false;
	public bool MZ04P2taken = false;
	public bool MZ05P2taken = false;
	
	public bool STZ01P1taken = false;
	public bool STZ02P1taken = false;
	public bool STZ03P1taken = false;
	public bool STZ04P1taken = false;
	public bool STZ05P1taken = false;
	public bool STZ01P2taken = false;
	public bool STZ02P2taken = false;
	public bool STZ03P2taken = false;
	public bool STZ04P2taken = false;
	public bool STZ05P2taken = false;
	
	public bool fieldP1taken = false;
	public bool fieldP2taken = false;
	
	public bool PendulumRedP1taken = false;
	public bool PendulumBlueP1taken = false;
	public bool PendulumRedP2taken = false;
	public bool PendulumBlueP2taken = false;
	#pragma warning restore 0414
	
	//Battle variables
	bool movingBattleChangeCard = false;
	public bool goingToFaceUpAttack = false;
	public bool goingToFaceDownAttack = false;
	public bool goingToFaceUpDefense = false;
	public bool goingToFaceDownDefense = false;
	public float battleChangeTimer = 1f;
	public float resetBattleChangeTimer = 1f;
	public float battleChangeTimerPositioning = 0f;
	public Quaternion startBattleChangeRotation;
	public Quaternion endBattleChangeRotation;
	public float speedOfBattleChangeRotation = 3f;
	
	public bool attackingCards = false;
	public Texture attackTexture;
	public GameObject MZ1P1_AttackNotification;
	public GameObject MZ2P1_AttackNotification;
	public GameObject MZ3P1_AttackNotification;
	public GameObject MZ4P1_AttackNotification;
	public GameObject MZ5P1_AttackNotification;
	public GameObject MZ1P2_AttackNotification;
	public GameObject MZ2P2_AttackNotification;
	public GameObject MZ3P2_AttackNotification;
	public GameObject MZ4P2_AttackNotification;
	public GameObject MZ5P2_AttackNotification;
	public Vector3 MZ1P1_AttackNotification_Start;
	public Vector3 MZ2P1_AttackNotification_Start;
	public Vector3 MZ3P1_AttackNotification_Start;
	public Vector3 MZ4P1_AttackNotification_Start;
	public Vector3 MZ5P1_AttackNotification_Start;
	public Vector3 MZ1P2_AttackNotification_Start;
	public Vector3 MZ2P2_AttackNotification_Start;
	public Vector3 MZ3P2_AttackNotification_Start;
	public Vector3 MZ4P2_AttackNotification_Start;
	public Vector3 MZ5P2_AttackNotification_Start;
	public float forwardMovement = 0f;
	public float backwardMovement = 0f;
	public bool forward = true;
	public bool backward = false;
	
	public bool attackP1Directly = true;
	public bool attackP2Directly = true;
	public bool directAttackByCardEffect = false;
	
	public bool afterFlipBattle = false;
	
	//Destroyed variables
	public bool moveDestroyedCard = false;
	public bool firstMonsterDestroyed = false;
	public bool secondMonsterDestroyed = false;
	public bool bothMonstersDestroyed = false;
	public bool targetedMonsterDestroyed = false;
	public float destroyTimer = 1f;
	public float resetDestroyTimer = 1f;
	public float destroyTimerPositioning = 0f;
	public float speedOfDestroyPosition = 3f;
	public float speedOfDestroyRotation = 3f;
	public float graveyardIncrement = 0f;
	public Vector3 startDestroyPoint = new Vector3(0, 0, 0);
	public Vector3 startDestroyPoint2 = new Vector3(0, 0, 0);
	public Vector3 endDestroyPoint = new Vector3(0, 0, 0);
	public Vector3 endDestroyPoint2 = new Vector3(0, 0, 0);
	public Quaternion startDestroyRotation;
	public Quaternion startDestroyRotation2;
	public Quaternion endDestroyRotation;
	public Quaternion endDestroyRotation2;
	
	//Tageting mode variables
	public bool targetingMode = false;
	public GameObject[] deck = new GameObject[10];
	public int numberOftargetedCards = 0;
	public bool attackTargeting = false;
	public bool cardEffectTargeting = false;
	
	//Discard variables
	public bool discardingMode = false;
	public int neededAmountToDiscard = 0;
	public int selectedAnountToDiscard = 0;
	public bool movingDiscardedCards = false;
	public float discardTimerPositioning = 0f;
	public float discardTimer = 1f;
	public float resetDiscardTimer = 1f;
	public float speedOfDiscardPosition = 3f;
	public float speedOfDiscardRotation = 3f;
	public Vector3[] startingDiscardPositions = new Vector3[50];
	public Vector3 endDiscardPosition = new Vector3(0, 0, 0);
	public Quaternion[] startingDiscardRotations = new Quaternion[50];
	public Quaternion endDiscardRotation;
	
	//Selection vriables
	public GameObject[] selectedCards = new GameObject[50];
	public Texture targetDashedTexture;
	public Texture targetSolidTexture;
	
	//Activate variables
	public bool movingActivateCard = false;
	public Vector3 startActivatePoint = new Vector3(0, 0, 0);
	public Vector3 endActivatePoint = new Vector3(0, 0, 0);
	public Quaternion startActivateRotation;
	public Quaternion endActivateRotation;
	public float activateTimerPositioning = 0f;
	public float speedOfActivatePosition = 3f;
	public float activateTimer = 1f;
	public float resetActivateTimerf = 1f;
	
	//Send variables
	public float sendTimerPositioning = 0f;
	public Vector3 startSendPoint = new Vector3(0, 0, 0);
	public Vector3 endSendPoint = new Vector3(0, 0, 0);
	public float speedOfSendPosition = 3f;
	public Quaternion startSendRotation;
	public Quaternion endSendRotation;
	public float sendTimer = 1f;
	public bool movingSendCard = false;
	public float resetSendTimerf = 1;
	
	//GUI variables
	public CrosshairScript crosshair;
	public bool calculateRotationOnce = true;
	#pragma warning disable 0414
	public Texture2D rotatedFrontTexture;
	public Texture2D rotatedBackTexture;
	#pragma warning restore 0414
	public bool askPlayer = false;
	public bool generalActivationQuestion = false;
	public bool customizedQuestion = false;
	public string confirmationOfActivation = "Do you want to activate a card?";
	public string customizableConfirmation = "Do you want to...?";
	
	//Animation variables
	public float animationTimer = 2;
	public float resetAnimationTimer = 2;
	public bool afterAnimationDestroyCard = false;
	public bool afterAnimationSendCard = false;
	
	//View variables
	public GameObject viewCardObjectP1;
	public GameObject viewContentP1; //Text, counters, attrabute, etc...
	public GameObject viewDescriptionP1;
	public GameObject viewCardObjectP2;
	public GameObject viewContentP2; //Text, counters, attrabute, etc...
	public GameObject viewDescriptionP2;
	
	//Show/Hide variables
	public GameObject showHandP1;
	public GameObject hideHandP1;
	public GameObject showInfoP1;
	public GameObject hideInfoP1;
	public GameObject showHandP2;
	public GameObject hideHandP2;
	public GameObject showInfoP2;
	public GameObject hideInfoP2;
	
	//Gameover/Replay variables
	public string gameoverReason = "";
	
	//-------------------------------------------------------------------------STARTUP-----------------------------------------------------------------------------------------------
	void Start()
	{
		//Make sure rock-paper-scissors is on!
		rps.gameObject.SetActive(true);

		//Find the OptionsManager
		options = GameObject.Find("OptionsManager").GetComponent<OptionsScript>();

		//Load options here... (from options script)

		//Check the options if this is a multiplayer game
		multiplayerGame = options.startedMultiplayerGame;

		//Make sure that both players are active
		player1.gameObject.SetActive(true);
		player2.gameObject.SetActive(true);

		//Making sure that both life points are set to 8000
		player1.SetCurrentLifePoints(startingLifePointCount);
		player2.SetCurrentLifePoints(startingLifePointCount);
		
		currentDeck = GameObject.Find("CurrentDeck").GetComponent<Deck>();
		currentExtraDeck = GameObject.Find("CurrentDeck").GetComponent<ExtraDeck>();
		currentSideDeck = GameObject.Find("CurrentDeck").GetComponent<SideDeck>();
		
		currentDeckAI = GameObject.Find("AICurrentDeck").GetComponent<Deck>();
		currentExtraDeckAI = GameObject.Find("AICurrentDeck").GetComponent<ExtraDeck>();
		currentSideDeckAI = GameObject.Find("AICurrentDeck").GetComponent<SideDeck>();

		//If it is a multiplayer game...
		if(multiplayerGame)
		{
			//Replace the AI deck with the other player's deck (by looping through all the cards from the other player)
			if(options.hostingPlayer)
			{
				player2.RpcGetClientDeck();
			}

			if(options.joiningPlayer)
			{
				player1.CmdGetHostDeck();
			}
		}
		
		//Set up the deck of cards to have the extra card properties included with the card game objects
		if(currentDeck != null && currentExtraDeck != null && currentSideDeck != null && currentDeckAI != null && currentExtraDeckAI != null && currentSideDeckAI != null)
		{
			//Loop through the deck to add the extra properties...
			for(int i = 0; i < currentDeck.deck.Length; i++)
			{
				if(currentDeck.deck[i] != null)
				{
					//Add the extra properties component
					currentDeck.deck[i].gameObject.AddComponent<ExtraCardProperties>();
					currentDeck.deck[i].gameObject.GetComponent<ExtraCardProperties>().owner = player1;
				}
			}
			
			//Loop through the extra deck to add the extra properties...
			for(int i = 0; i < currentExtraDeck.extraDeck.Length; i++)
			{
				//Add the extra properties component
				if(currentExtraDeck.extraDeck[i] != null)
				{
					currentExtraDeck.extraDeck[i].gameObject.AddComponent<ExtraCardProperties>();
					currentExtraDeck.extraDeck[i].gameObject.GetComponent<ExtraCardProperties>().owner = player1;
				}
			}
			
			//Loop through the side deck to add the extra properties... (just incase of a Match and not a Single duel)
			for(int i = 0; i < currentSideDeck.sideDeck.Length; i++)
			{
				//Add the extra properties component
				if(currentSideDeck.sideDeck[i] != null)
				{
					currentSideDeck.sideDeck[i].gameObject.AddComponent<ExtraCardProperties>();
					currentSideDeck.sideDeck[i].gameObject.GetComponent<ExtraCardProperties>().owner = player1;
				}
			}
			
			//Loop through the AI deck to add the extra properties...
			for(int i = 0; i < currentDeckAI.deck.Length; i++)
			{
				if(currentDeckAI.deck[i] != null)
				{
					//Add the extra properties component
					currentDeckAI.deck[i].gameObject.AddComponent<ExtraCardProperties>();
					currentDeckAI.deck[i].gameObject.GetComponent<ExtraCardProperties>().owner = player2;
				}
			}
			
			//Loop through the AI extra deck to add the extra properties...
			for(int i = 0; i < currentExtraDeckAI.extraDeck.Length; i++)
			{
				//Add the extra properties component
				if(currentExtraDeckAI.extraDeck[i] != null)
				{
					currentExtraDeckAI.extraDeck[i].gameObject.AddComponent<ExtraCardProperties>();
					currentExtraDeckAI.extraDeck[i].gameObject.GetComponent<ExtraCardProperties>().owner = player2;
				}
			}
			
			//Loop through the AI side deck to add the extra properties... (just incase of a Match and not a Single duel)
			for(int i = 0; i < currentSideDeckAI.sideDeck.Length; i++)
			{
				//Add the extra properties component
				if(currentSideDeckAI.sideDeck[i] != null)
				{
					currentSideDeckAI.sideDeck[i].gameObject.AddComponent<ExtraCardProperties>();
					currentSideDeckAI.sideDeck[i].gameObject.GetComponent<ExtraCardProperties>().owner = player2;
				}
			}
			
			//Shuffle the cards (both players)
			ShuffleDeck(currentDeck);
			ShuffleDeck(currentDeckAI);
			
			//Load Crosshair
			crosshair = GameObject.Find("Crosshair").GetComponent<CrosshairScript>();
		}
	}
	
	///-------------------------------------------------------------------------UPDATE (DUEL/GAMEPLAY LOGIC)-----------------------------------------------------------------------------------------------
	void Update()
	{
		//Get the mouse click (high enough priority to be on the top level update)
		mouseRayP1 = player1.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		mouseRayP2 = player2.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		
		//Update turn counters
		turnCounter01.text = "Turn: " + turnCounter;
		turnCounter02.text = "Turn: " + turnCounter;
		
		//If we are moving a card (by drawing)...
		if(movingDrawCard)
		{
			//Check which player is drawing...
			if(player1Drawing)
			{
				multiplyer = 5 + (-player1.handSize + 5);
				
				//Get the percentage
				if(calculateOnce)
				{
					percentage = CalculateCardPositions(drawableCard, player1, player1.handSize);
					
					for(int i = 0; i < player1.cardsInHand.Length; i++)
					{
						if(player1.cardsInHand[i] != null)
						{
							CalculateCardPositions(player1.cardsInHand[i].gameObject, player1, i, 1, true, player1.handSize);
						}
						else
						{
							break;
						}
					}
					
					calculateOnce = false;
				}
				
				drawTimerPositioning = drawTimerPositioning + Time.deltaTime;
				
				//Lerp the movement of the card
				drawableCard.transform.position = Vector3.Lerp(startingPosition, new Vector3(P1HandRightSideLimit.transform.position.x - (percentage * player1.handSize * multiplyer), P1HandRightSideLimit.transform.position.y, P1HandRightSideLimit.transform.position.z), drawTimerPositioning * speedOfDrawPosition);
				
				//Lerp the rotation of the card
				drawableCard.transform.rotation = Quaternion.Lerp(startingRotation, Quaternion.Euler(0, 0, 0), drawTimerPositioning * speedOfDrawRotation);
			}
			else if(player2Drawing)
			{
				multiplyer = 5 + (-player2.handSize + 5);
				
				if(calculateOnce)
				{
					percentage = CalculateCardPositions(drawableCard, player2, player2.handSize);
					
					for(int i = 0; i < player2.cardsInHand.Length; i++)
					{
						if(player2.cardsInHand[i] != null)
						{
							CalculateCardPositions(player2.cardsInHand[i].gameObject, player2, i, 1, true, player2.handSize);
						}
						else
						{
							break;
						}
					}
					
					calculateOnce = false;
				}
				
				drawTimerPositioning = drawTimerPositioning + Time.deltaTime;
				
				//Lerp the movement of the card
				drawableCard.transform.position = Vector3.Lerp(startingPosition, new Vector3(P2HandRightSideLimit.transform.position.x + (percentage * player2.handSize * multiplyer), P2HandRightSideLimit.transform.position.y, P2HandRightSideLimit.transform.position.z), drawTimerPositioning * speedOfDrawPosition);
				
				//Lerp the rotation of the card
				drawableCard.transform.rotation = Quaternion.Lerp(startingRotation, Quaternion.Euler(0, 180, 0), drawTimerPositioning * speedOfDrawRotation);
			}
			
			//Decrement the timer
			drawTimer = drawTimer - Time.deltaTime;
			
			//If the tiemr runs out...
			if(drawTimer <= 0)
			{
				//Get out of drawing a card
				movingDrawCard = false;
				player1Drawing = false;
				player2Drawing = false;
				drawTimer = resetDrawTimer;
				calculateOnce = true;
				multiplyer = 1f;
				speedOfDrawPosition = 3f;
				speedOfDrawRotation = 3f;
				drawTimerPositioning = 0;
			}
		}
		//If we are waiting for a player...
		else if(movingSummonCard)
		{
			summonTimerPositioning = summonTimerPositioning + Time.deltaTime;
			
			selectedCard.transform.position = Vector3.Lerp(startSummonPoint, endSummonPoint, summonTimerPositioning * speedOfSummonPosition);
			selectedCard.transform.rotation = Quaternion.Lerp(startSummonRotation, endSummonRotation, summonTimerPositioning * speedOfSummonRotation);
			
			//Decrement the timer
			summonTimer = summonTimer - Time.deltaTime;
			
			//If the tiemr runs out...
			if(summonTimer <= 0)
			{
				selectedCard.GetComponent<ExtraCardProperties>().slot = modelNameIdentifier;
				summonTimer = resetSummonTimer;
				summonTimerPositioning = 0;
				movingSummonCard = false;
				
				//If the model of the card is available...
				if(selectedCard.GetComponent<Card>().GetCardModel() != null && (selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense))
				{
					//Show the model and animations
					GameObject monsterModel = new GameObject();
					
					if(player1POV)
					{
						monsterModel = (GameObject)Instantiate(selectedCard.GetComponent<Card>().GetCardModel(), new Vector3(selectedCard.transform.position.x, selectedCard.transform.position.y + 1, selectedCard.transform.position.z), Quaternion.Euler(0, 180, 0));  
					}
					else if(player2POV)
					{
						monsterModel = (GameObject)Instantiate(selectedCard.GetComponent<Card>().GetCardModel(), new Vector3(selectedCard.transform.position.x, selectedCard.transform.position.y + 1, selectedCard.transform.position.z), Quaternion.Euler(0, 0, 0));  
					}
					
					monsterModel.name = "Model" + modelNameIdentifier;
					monsterModel.layer = 12;
					monsterModel.transform.GetChild(0).gameObject.layer = 12;
				}
				
				player1POV = false;
				player2POV = false;
				
				//Activate effects here...
				if(normal)
				{
					//selectedCard.SendMessage("NormalSummonedEffect", SendMessageOptions.DontRequireReceiver);
					selectedCard.GetComponent<ExtraCardProperties>().wasNormalSummoned = true;
				}
				
				if(set)
				{
					//selectedCard.SendMessage("NormalSetEffect", SendMessageOptions.DontRequireReceiver);
					//selectedCard.SendMessage("TributeSetEffect", SendMessageOptions.DontRequireReceiver);
					//selectedCard.SendMessage("SpecialSetEffect", SendMessageOptions.DontRequireReceiver);
					selectedCard.GetComponent<ExtraCardProperties>().wasNormalSummoned = true;
				}
				
				if(tribute)
				{
					//selectedCard.SendMessage("TributeSummonedEffect", SendMessageOptions.DontRequireReceiver);
					selectedCard.GetComponent<ExtraCardProperties>().wasTributeSummoned = true;
				}
				
				if(special)
				{
					//selectedCard.SendMessage("SpecialSummonedEffect", SendMessageOptions.DontRequireReceiver);
					//selectedCard.SendMessage("RitualSummonedEffect", SendMessageOptions.DontRequireReceiver);
					//selectedCard.SendMessage("FusionSummonedEffect", SendMessageOptions.DontRequireReceiver);
					//selectedCard.SendMessage("SynchroSummonedEffect", SendMessageOptions.DontRequireReceiver);
					//selectedCard.SendMessage("XYZSummonedEffect", SendMessageOptions.DontRequireReceiver);
					//selectedCard.SendMessage("PendulumSummonedEffect", SendMessageOptions.DontRequireReceiver);
					selectedCard.GetComponent<ExtraCardProperties>().wasSpecialSummoned = true;
				}
				
				normal = false;
				set = false;
				tribute = false;
				special = false;
				
				crosshair.TurnOn();
				
				field.CheckFieldValidity();
			}
		}
		else if(movingBattleChangeCard)
		{
			battleChangeTimerPositioning = battleChangeTimerPositioning + Time.deltaTime;
			
			//If flipping the targeted monster...
			if(afterFlipBattle)
			{
				secondTargetedMonster.transform.rotation = Quaternion.Lerp(startBattleChangeRotation, endBattleChangeRotation, battleChangeTimerPositioning * speedOfBattleChangeRotation);
			}
			//Else, if regular battle position change...
			else
			{
				selectedCard.transform.rotation = Quaternion.Lerp(startBattleChangeRotation, endBattleChangeRotation, battleChangeTimerPositioning * speedOfBattleChangeRotation);
			}
			
			//Decrement the timer
			battleChangeTimer = battleChangeTimer - Time.deltaTime;
			
			//If the tiemr runs out...
			if(battleChangeTimer <= 0)
			{
				goingToFaceUpAttack = false;
				goingToFaceDownAttack = false;
				goingToFaceUpDefense = false;
				goingToFaceDownDefense = false;
				player1POV = false;
				player2POV = false;
				battleChangeTimerPositioning = 0;
				battleChangeTimer = resetBattleChangeTimer;
				movingBattleChangeCard = false;
				
				
				//If the model of the card is available... Reveal Model (if any)
				if(selectedCard.GetComponent<Card>().GetCardModel() != null && (selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack || selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense))
				{
					//Show the model and animations
					GameObject monsterModel = new GameObject();
					
					if(player1POV)
					{
						monsterModel = (GameObject)Instantiate(selectedCard.GetComponent<Card>().GetCardModel(), new Vector3(selectedCard.transform.position.x, selectedCard.transform.position.y + 1, selectedCard.transform.position.z), Quaternion.Euler(0, 180, 0));  
					}
					else if(player2POV)
					{
						monsterModel = (GameObject)Instantiate(selectedCard.GetComponent<Card>().GetCardModel(), new Vector3(selectedCard.transform.position.x, selectedCard.transform.position.y + 1, selectedCard.transform.position.z), Quaternion.Euler(0, 0, 0));  
					}
					
					monsterModel.name = "Model" + selectedCard.GetComponent<ExtraCardProperties>().slot;
					monsterModel.layer = 12;
				}
				
//				if(afterFlipActivate)
//				{
//					activate.Activate();
//					afterFlipActivate = false;
//				}
				
				if(afterFlipBattle)
				{
					attack.Battle();
					afterFlipBattle = false;
				}
				
				selectedCard.GetComponent<ExtraCardProperties>().canChangeBattlePositions = false;
			}
		}
		//Else, if there is a destroyed card...
		else if(moveDestroyedCard)
		{
			//positions and rotations
			destroyTimerPositioning = destroyTimerPositioning + Time.deltaTime;
			
			//Checks for what is being destroyed...
			if(firstMonsterDestroyed)
			{
				firstAttackingMonster.transform.position = Vector3.Lerp(startDestroyPoint, new Vector3(endDestroyPoint.x, endDestroyPoint.y + graveyardIncrement, endDestroyPoint.z), destroyTimerPositioning * speedOfDestroyPosition);
				firstAttackingMonster.transform.rotation = Quaternion.Lerp(startDestroyRotation, endDestroyRotation, destroyTimerPositioning * speedOfDestroyRotation);
				
				//Check if we are banishing
				if(isBanishing)
				{
					firstAttackingMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.banished;
				}
				//Else, we are just destroying
				else
				{
					firstAttackingMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
				}
			}
			else if(secondMonsterDestroyed)
			{
				secondTargetedMonster.transform.position = Vector3.Lerp(startDestroyPoint, new Vector3(endDestroyPoint.x, endDestroyPoint.y + graveyardIncrement, endDestroyPoint.z), destroyTimerPositioning * speedOfDestroyPosition);
				secondTargetedMonster.transform.rotation = Quaternion.Lerp(startDestroyRotation, endDestroyRotation, destroyTimerPositioning * speedOfDestroyRotation);
				
				//Check if we are banishing
				if(isBanishing)
				{
					secondTargetedMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.banished;
				}
				//Else, we are just destroying
				else
				{
					secondTargetedMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
				}
			}
			else if(bothMonstersDestroyed)
			{
				firstAttackingMonster.transform.position = Vector3.Lerp(startDestroyPoint, new Vector3(endDestroyPoint.x, endDestroyPoint.y + graveyardIncrement, endDestroyPoint.z), destroyTimerPositioning * speedOfDestroyPosition);
				secondTargetedMonster.transform.position = Vector3.Lerp(startDestroyPoint2, new Vector3(endDestroyPoint2.x, endDestroyPoint2.y + graveyardIncrement, endDestroyPoint2.z), destroyTimerPositioning * speedOfDestroyPosition);
				firstAttackingMonster.transform.rotation = Quaternion.Lerp(startDestroyRotation, endDestroyRotation, destroyTimerPositioning * speedOfDestroyRotation);
				secondTargetedMonster.transform.rotation = Quaternion.Lerp(startDestroyRotation2, endDestroyRotation2, destroyTimerPositioning * speedOfDestroyRotation);
				
				//Check if we are banishing
				if(isBanishing)
				{
					firstAttackingMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.banished;
					secondTargetedMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.banished;
				}
				//Else, we are just destroying
				else
				{
					firstAttackingMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
					secondTargetedMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
				}
			}
			else if(targetedMonsterDestroyed)
			{
				//targetedMonster.transform.position = Vector3.Lerp(startDestroyPoint, new Vector3(endDestroyPoint.x, endDestroyPoint.y + graveyardIncrement, endDestroyPoint.z), destroyTimerPositioning * speedOfDestroyPosition);
				
				//Check if we are banishing
//				if(isBanishing)
//				{
//					firstAttackingMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.banished;
//				}
//				//Else, we are just destroying
//				else
//				{
//					firstAttackingMonster.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
//				}
			}

			//Decrement the timer
			destroyTimer = destroyTimer - Time.deltaTime;

			//If the tiemr runs out...
			if(destroyTimer <= 0)
			{
				moveDestroyedCard = false;
				firstMonsterDestroyed = false;
				secondMonsterDestroyed = false;
				bothMonstersDestroyed = false;
				targetedMonsterDestroyed = false;
				destroyTimer = resetDestroyTimer;
				destroyTimerPositioning = 0f;
				speedOfDestroyPosition = 3f;
				speedOfDestroyRotation = 3f;
				graveyardIncrement = graveyardIncrement + globalIncrement;
				
				//selectedCard.SendMessage("DestroyedEffect", SendMessageOptions.DontRequireReceiver);
				selectedCard.GetComponent<ExtraCardProperties>().wasNormalSummoned = false;
				selectedCard.GetComponent<ExtraCardProperties>().wasTributeSummoned = false;
				selectedCard.GetComponent<ExtraCardProperties>().wasSpecialSummoned = false;
				
				//Free up spaces and Remove Models (if any)
				field.CheckFieldValidity();
				
				//What else can attack
				tans.NotifyUserOfAttackingCards();
			}
		}
		else if(movingDiscardedCards)
		{
			//positions and rotations
			discardTimerPositioning = discardTimerPositioning + Time.deltaTime;
			
			//loop through all the selected cards and update their positions and rotations
			for(int i = 0; i < selectedCards.Length; i++)
			{
				//If the slot is not null
				if(selectedCards[i] != null)
				{
					//Update values (endDiscardPosition
					selectedCards[i].transform.position = Vector3.Lerp(startingDiscardPositions[i], new Vector3(endDiscardPosition.x, endDiscardPosition.y + graveyardIncrement + (globalIncrement * (i + 1)), endDiscardPosition.z), discardTimerPositioning * speedOfDiscardPosition);
					selectedCards[i].transform.rotation = Quaternion.Lerp(startingDiscardRotations[i], endDiscardRotation, discardTimerPositioning * speedOfDiscardRotation);
				}
			}
			
			//Decrement the timer
			discardTimer = discardTimer - Time.deltaTime;
			
			//If the tiemr runs out...
			if(discardTimer <= 0)
			{
				discardTimer = resetDiscardTimer;
				discardTimerPositioning = 0f;
				speedOfDiscardPosition = 3f;
				speedOfDiscardRotation = 3f;
				graveyardIncrement = graveyardIncrement + (globalIncrement * neededAmountToDiscard);
				movingDiscardedCards = false;
				discardingMode = false;
				selectedAnountToDiscard = 0;
				
				//Get rid of the notifications
				if(player1Turn)
				{
					for(int i = 0; i < player1.cardsInHand.Length; i++)
					{
						if(player1.cardsInHand[i] != null)
						{
							if(GameObject.Find("discardNotification" + i) != null)
							{
								Destroy(GameObject.Find("discardNotification" + i));
							}
						}
					}
				}
				else if(player2Turn)
				{
					for(int i = 0; i < player2.cardsInHand.Length; i++)
					{
						if(player2.cardsInHand[i] != null)
						{
							if(GameObject.Find("discardNotification" + i) != null)
							{
								Destroy(GameObject.Find("discardNotification" + i));
							}
						}
					}
				}
				
				//Label the cards as if they are in the graveyard and remove it from the hand
				for(int i = 0; i < selectedCards.Length; i++)
				{
					//If the slot is not null
					if(selectedCards[i] != null)
					{
						//Update values
						selectedCards[i].GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
						
						//Remove the card from the hand...
						if(player1Turn)
						{
							//Loop for how ever big the hand is...
							for(int j = 0; j < player1.cardsInHand.Length; j++)
							{
								//If it is not null...
								if(player1.cardsInHand[j] != null)
								{
									//Check if the names are the same...
									if(player1.cardsInHand[j].name == selectedCards[i].name)
									{
										//Remove the card
										player1.cardsInHand[j] = null;
									}
								}
							}
						}
						else if(player2Turn)
						{
							//Loop for how ever big the hand is...
							for(int j = 0; j < player2.cardsInHand.Length; j++)
							{
								//If it is not null...
								if(player2.cardsInHand[j] != null)
								{
									//Check if the names are the same...
									if(player2.cardsInHand[j].name == selectedCards[i].name)
									{
										//Remove the card
										player2.cardsInHand[j] = null;
									}
								}
							}
						}
						
						//We are done with the slot
						selectedCards[i] = null;
					}
				}
				
				//Calculate card positions in the hand...
				if(player1Turn)
				{
					//Get the correct cards in hand (number)
					player1.handSize = player1.handSize - neededAmountToDiscard;
					player1.DecreaseCardsInHand();
				
					for(int i = 0; i < player1.cardsInHand.Length; i++)
					{
						if(player1.cardsInHand[i] != null)
						{
							CalculateCardPositions(player1.cardsInHand[i].gameObject, player1, i, 1, true, player1.handSize);
						}
						else
						{
							break;
						}
					}
				}
				else if(player2Turn)
				{
					//Get the correct cards in hand (number)
					player2.handSize = player2.handSize - neededAmountToDiscard;
					player2.DecreaseCardsInHand();
					
					for(int i = 0; i < player2.cardsInHand.Length; i++)
					{
						if(player2.cardsInHand[i] != null)
						{
							CalculateCardPositions(player2.cardsInHand[i].gameObject, player2, i, 1, true, player2.handSize);
						}
						else
						{
							break;
						}
					}
				}
				
				//selectedCard.SendMessage("DiscardedEffect", SendMessageOptions.DontRequireReceiver);
			}
		}
		//Else, if moving activating card
		else if(movingActivateCard)
		{
			activateTimerPositioning = activateTimerPositioning + Time.deltaTime;
			
			selectedCard.transform.position = Vector3.Lerp(startActivatePoint, endActivatePoint, activateTimerPositioning * speedOfActivatePosition);
			selectedCard.transform.rotation = Quaternion.Lerp(startActivateRotation, endActivateRotation, activateTimerPositioning * speedOfActivatePosition);
			
			//Decrement the timer
			activateTimer = activateTimer - Time.deltaTime;
			
			//If the tiemr runs out...
			if(activateTimer <= 0)
			{
				movingActivateCard = false;
				resetActivateTimerf = 1;
				activateTimer = resetActivateTimerf;
				activateTimerPositioning = 0f;
				speedOfActivatePosition = 3f;
				
				if(set)
				{
					//Update values
					selectedCard.GetComponent<ExtraCardProperties>().isSet = set;
				}
				else
				{
					//selectedCard.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
					
					//Do some checks on what type of ation for this card is needed
					if(selectedCard.GetComponent<Card>().spellType == Card.SpellType.normal || selectedCard.GetComponent<Card>().spellType == Card.SpellType.quickPlay || selectedCard.GetComponent<Card>().spellType == Card.SpellType.ritual || selectedCard.GetComponent<Card>().trapType == Card.TrapType.normal || selectedCard.GetComponent<Card>().trapType == Card.TrapType.counter || (selectedCard.GetComponent<Card>().cardType == Card.CardType.spell && selectedCard.GetComponent<Card>().spellType == Card.SpellType.none) || (selectedCard.GetComponent<Card>().cardType == Card.CardType.trap && selectedCard.GetComponent<Card>().trapType == Card.TrapType.none))
					{
						afterAnimationSendCard = true;
					}
				}
				
				set = false;
				player1POV = false;
				player2POV = false;
				
				waitingForAnimation = true;
				
				selectedCard.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.field;
				
				field.CheckFieldValidity();
			}
		}
		else if(movingTributeCards)
		{
			//positions and rotations
			tributeTimerPositioning = tributeTimerPositioning + Time.deltaTime;
			
			//loop through all the selected cards and update their positions and rotations
			for(int i = 0; i < selectedCards.Length; i++)
			{
				//If the slot is not null
				if(selectedCards[i] != null)
				{
					//Update values (endDiscardPosition
					selectedCards[i].transform.position = Vector3.Lerp(startingTributePositions[i], new Vector3(endTributePosition.x, endTributePosition.y + graveyardIncrement + (globalIncrement * (i + 1)), endTributePosition.z), tributeTimerPositioning * speedOfTributePosition);
					selectedCards[i].transform.rotation = Quaternion.Lerp(startingTributeRotations[i], endTributeRotation, tributeTimerPositioning * speedOfTributeRotation);
				}
			}
			
			//Decrement the timer
			tributeTimer = tributeTimer - Time.deltaTime;
			
			//If the tiemr runs out...
			if(tributeTimer <= 0)
			{
				tributeTimer = resetTributeTimer;
				tributeTimerPositioning = 0f;
				speedOfTributePosition = 3f;
				speedOfTributeRotation = 3f;
				graveyardIncrement = graveyardIncrement + (globalIncrement * neededAmountToTribute);
				movingTributeCards = false;
				tributeTargeting = false;
				targetingMode = false;
				selectedAnountToTribute = 0;
				
				for(int i = 0; i < selectedCards.Length; i++)
				{
					//If the slot is not null
					if(selectedCards[i] != null)
					{
						selectedCards[i].GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
					}
				}
				
				field.CheckFieldValidity();
				
				tans.DestroyNotifications();
				
				summon.Summon();
			}
		}
		else if(movingSendCard)
		{
			sendTimerPositioning = sendTimerPositioning + Time.deltaTime;
			
			selectedCard.transform.position = Vector3.Lerp(startSendPoint, endSendPoint, sendTimerPositioning * speedOfSendPosition);
			selectedCard.transform.rotation = Quaternion.Lerp(startSendRotation, endSendRotation, sendTimerPositioning * speedOfSendPosition);
			
			//Decrement the timer
			sendTimer = sendTimer - Time.deltaTime;
			
			//If the tiemr runs out...
			if(sendTimer <= 0)
			{
				movingSendCard = false;
				resetSendTimerf = 1;
				sendTimer = resetSendTimerf;
				sendTimerPositioning = 0f;
				speedOfSendPosition = 3f;
				graveyardIncrement = graveyardIncrement + globalIncrement;
				
				player1POV = false;
				player2POV = false;
				
				selectedCard.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.graveyard;
				
				field.CheckFieldValidity();
			}
		}
		//If we are waiting for a card effect...
		else if(waitingForAnimation)
		{
			//Loop here for animations (and the loop when the animation is finished)
			//Animation timer MUST be reset after every use because it is unknown how long the animations really are
			animationTimer = animationTimer - Time.deltaTime;
			
			//If the animation timer has ran out...
			if(animationTimer <= 0)
			{
				//Get out of the animation loop
				waitingForAnimation = false;
				animationTimer = resetAnimationTimer;
				
				//Extra things to do after animation is finished
				if(afterAnimationDestroyCard)
				{
					moveDestroyedCard = true;
				}
				
				if(afterAnimationSendCard)
				{
					movingSendCard = true;
					startSendPoint = selectedCard.transform.position;
					startSendRotation = selectedCard.transform.rotation;
					
					if(selectedCard.GetComponent<ExtraCardProperties>().controller == player1)
					{
						player1POV = true;
						endSendPoint = new Vector3(graveyardP1.transform.position.x, graveyardP1.transform.position.y + graveyardIncrement, graveyardP1.transform.position.z);
						endSendRotation = Quaternion.Euler(90, 0, 0);
					}
					else if(selectedCard.GetComponent<ExtraCardProperties>().controller == player2)
					{
						player2POV = true;
						endSendPoint = new Vector3(graveyardP2.transform.position.x, graveyardP2.transform.position.y + graveyardIncrement, graveyardP2.transform.position.z);
						endSendRotation = Quaternion.Euler(90, 180, 0);
					}
				}
			}
		}
		//If we are waiting for a player...
		else if(waitingForPlayer)
		{
			//Loop here until someone does something (anything)
			if(phase == Phases.draw)
			{
				//If there are no more effects to activate...
				if(noMoreEffects && !askPlayer)
				{
					//Move on to the next phase
					phase = Phases.standby;
					phaseStatic = Phases.standby;
					waitingForPlayer = false;
				}
			}
			
			if(phase == Phases.standby)
			{
				//If there are no more effects to activate...
				if(noMoreEffects && !askPlayer)
				{
					//Move on to the next phase
					phase = Phases.main1;
					phaseStatic = Phases.main1;
					waitingForPlayer = false;
				}
			}
			
			if(phase == Phases.main1)
			{
				//If there are no more effects to activate... (and the player allows the continuation to the next phase)
				if(noMoreEffects && !askPlayer && selectedBattlePhase)
				{
					//Move on to the next phase
					phase = Phases.battle;
					phaseStatic = Phases.battle;
					waitingForPlayer = false;
				}
				//If there are no more effects to activate... (and the player allows the continuation to the next phase)
				else if(noMoreEffects && !askPlayer && selectedEndPhase)
				{
					//Move on to the next phase
					phase = Phases.end;
					phaseStatic = Phases.end;
					waitingForPlayer = false;
				}
			}
			
			if(phase == Phases.battle)
			{
				//If there are no more effects to activate... (and the player allows the continuation to the next phase)
				if(noMoreEffects && !askPlayer && selectedMainPhase2)
				{
					//Move on to the next phase
					phase = Phases.main2;
					phaseStatic = Phases.main2;
					waitingForPlayer = false;
				}
				//If there are no more effects to activate... (and the player allows the continuation to the next phase)
				else if(noMoreEffects && !askPlayer && selectedEndPhase)
				{
					//Move on to the next phase
					phase = Phases.end;
					phaseStatic = Phases.end;
					waitingForPlayer = false;
				}
			}
			
			if(phase == Phases.main2)
			{
				//If there are no more effects to activate... (and the player allows the continuation to the next phase)
				if(noMoreEffects && !askPlayer && selectedEndPhase)
				{
					//Move on to the next phase
					phase = Phases.end;
					phaseStatic = Phases.end;
					waitingForPlayer = false;
				}
			}
			
			if(phase == Phases.end)
			{
				//If there are no more effects to activate...
				if(noMoreEffects && !askPlayer && !discardingMode)
				{
					//Return to original values for the next turn (for temporay attack and defense buffs or nerfs, etc...)
					
					//Move on to the next turn
					if(player1Turn)
					{
						player1Turn = false;
						player2Turn = true;
					}
					else if(player2Turn)
					{
						player1Turn = true;
						player2Turn = false;
					}
					
					//Reset the phases
					phase = Phases.draw;
					phaseStatic = Phases.draw;
					selectedMainPhase2 = false;
					selectedBattlePhase = false;
					selectedEndPhase = false;
					waitingForPlayer = false;
				}
			}
		}
		//If we are waiting for a card effect...
		else if(waitingForEffect)
		{
			//Loop here until all effects are done
		}
		//Else, if we are dueling... (waiting should be checked first becuase a player may make a move)
		else if(dueling && !discardingMode)
		{
			//If it is the draw phase...
			if(phase == Phases.draw)
			{
				//Increment the turn counter
				turnCounter++;
			
				//Startup before the duel officially begins!
				if(!officiallyStarted)
				{
					//Officially start the duel by setting up the decks, field, and hands of both players
					SetupDuel();
				}
			
				//if first turn && obsolete rules, draw on first turn
				if(turnCounter == 1 && obsoleteRulings)
				{
					//Draw a card
					if(player1Turn)
					{
						Draw(player1, cardsPerDraw);
					}
					else if(player2Turn)
					{
						Draw(player2, cardsPerDraw);
					}
				}
				else if(turnCounter != 1)
				{
					//Draw a card
					if(player1Turn)
					{
						Draw(player1, cardsPerDraw);
					}
					else if(player2Turn)
					{
						Draw(player2, cardsPerDraw);
					}
				}
					
				//Do effects...
				CheckAllCardEffects();
				
				waitingForPlayer = true;
			}
			
			//If it is the standby phase...
			if(phase == Phases.standby)
			{
				//Do effects...
				CheckAllCardEffects();
				
				waitingForPlayer = true;
			}
			
			//If it is the main phase 1...
			if(phase == Phases.main1)
			{
				//Do effects...
				CheckAllCardEffects();
				
				//Wait for player input on the next action
				waitingForPlayer = true;
			}
			
			//If it is the battle phase...
			if(phase == Phases.battle)
			{
				waitingForPlayer = true;
				
				//Show which cards can attack
				attackingCards = true;
				
				//Make special planes to show the user that these cards can attack
				tans.NotifyUserOfAttackingCards();
			
				//Battle Phase begins
				if(battleSteps == BattleSteps.startStep)
				{
					//Start step has ended
					battleSteps = BattleSteps.battleStep;
					battleStepsStatic = BattleSteps.battleStep;
				}
				
				if(battleSteps == BattleSteps.battleStep)
				{
					//Check for effects
					CheckAllCardEffects();
					
					//Change to the next step
					battleSteps = BattleSteps.damageStep;
					battleStepsStatic = BattleSteps.damageStep;
				}
				
				if(battleSteps == BattleSteps.damageStep)
				{
					//Check for effects
					CheckAllCardEffects();
				}
					
				if(battleSteps == BattleSteps.endStep)
				{
					//Check for effects
					CheckAllCardEffects();
				}
			}
			
			//If it is the main phase 2...
			if(phase == Phases.main2)
			{
				//We are not in the battle phase anymore
				attackingCards = false;
				
				//Do effects...
				CheckAllCardEffects();
				
				//Wait for player input on the next action
				waitingForPlayer = true;
			}
			
			//If it is the end phase...
			if(phase == Phases.end)
			{
				//We are not in the battle phase anymore
				attackingCards = false;
				
				//Do effects...
				CheckAllCardEffects();
				
				//Wait for player input on the next action
				waitingForPlayer = true;
			}
		}
		
		//If the ray cast physics...
		if(player1Turn && Physics.Raycast(mouseRayP1, out mouseHit))
		{
			//If the surrender button was hit...
			if(mouseHit.collider.gameObject.name == "SurrenderButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//End the game and go back to the main menu (the other player wins upon doing so)
				crosshair.TurnOff();
				crosshair.SearchForCursorToLock = false;
				//Application.LoadLevel("MainMenu");
				SceneManager.LoadScene("MainMenu");
			}
			//Else, if the shuffle button was hit...
			if(mouseHit.collider.gameObject.name == "ShuffleButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				ShuffleHand(player1);
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the draw phase button was hit...
			if(mouseHit.collider.gameObject.name == "DrawPhaseButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the standby phase button was hit...
			if(mouseHit.collider.gameObject.name == "StandbyPhaseButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the main phase 1 button was hit...
			if(mouseHit.collider.gameObject.name == "MainPhase1ButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the battle phase button was hit...
			if(mouseHit.collider.gameObject.name == "BattlePhaseButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && turnCounter != 1)
			{
				//The monsters that did attack the turn before can now attack next turn (unless card effects say otherwise)
				attack.TurnOnAttacking();
				
				selectedBattlePhase = true;
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the main phase 2 button was hit...
			if(mouseHit.collider.gameObject.name == "MainPhase2ButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Get rid of the attack notifications (from Battle phase to main phase 2)
				DestroyAttackNotifications();
					
				if(phase == Phases.battle)
				{
					selectedMainPhase2 = true;
				}
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the end phase button was hit...
			if(mouseHit.collider.gameObject.name == "EndPhaseButtonP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Get rid of the attack notifications (from Battle phase to end phase)
				DestroyAttackNotifications();
					
				selectedEndPhase = true;
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
				
				//Select cards to discard to get back to the hand size limit
				if(player1.handSize - player1.handSizeLimit >= 1)
				{
					Discard(player1, player1.handSize - player1.handSizeLimit);
				}
				
				attack.TurnOnBattleChanges();
				usedNormalSummons = 0;
				allowedNormalSummons = 1;
			}
			if(mouseHit.collider.gameObject.name == "ShowHandP1" && Input.GetButtonDown("Fire1"))
			{
				//Show the "Hide hand" button
				hideHandP1.SetActive(true);
				
				//Hide the "Show hand" button
				showHandP1.SetActive(false);
				
				//Turn on collisions
				for(int i = 0; i < player1.cardsInHand.Length && player1.cardsInHand[i] != null; i++)
				{
					player1.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true;
					player1.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = true;
				}
				
				//Show the hand
				player1.gameObject.GetComponent<Camera>().cullingMask ^= 256; // = 100000000 binary
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			if(mouseHit.collider.gameObject.name == "HideHandP1" && Input.GetButtonDown("Fire1"))
			{
				//Show the "Show hand" button
				showHandP1.SetActive(true);
				
				//Hide the "Hide hand" button
				hideHandP1.SetActive(false);
				
				//Turn off collisions
				for(int i = 0; i < player1.cardsInHand.Length && player1.cardsInHand[i] != null; i++)
				{
					player1.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
					player1.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = false;
				}
				
				//Hide the hand
				player1.gameObject.GetComponent<Camera>().cullingMask ^= 256; // = 100000000 binary
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			if(mouseHit.collider.gameObject.name == "ShowInfoP1" && Input.GetButtonDown("Fire1"))
			{
				//Show the "Hide info" button
				hideInfoP1.SetActive(true);
				
				//Hide the "Show info" button
				showInfoP1.SetActive(false);
				
				//Hide the info
				viewCardObjectP1.SetActive(true);
				viewContentP1.SetActive(true);
				viewDescriptionP1.SetActive(true);
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			if(mouseHit.collider.gameObject.name == "HideInfoP1" && Input.GetButtonDown("Fire1"))
			{
				//Show the "Show info" button
				showInfoP1.SetActive(true);
				
				//Hide the "Hide info" button
				hideInfoP1.SetActive(false);
				
				//Hide the info
				viewCardObjectP1.SetActive(false);
				viewContentP1.SetActive(false);
				viewDescriptionP1.SetActive(false);
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the clicked on thing was a card...
			if(mouseHit.collider.gameObject.name == "CardFront" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			
				//Show a menu of all possable actions for the card (if it is manual dueling, show all options)
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().enabled = true;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player1Turn = true;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player2Turn = false;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().cardPosition = mouseHit.collider.gameObject.transform.position;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().CreateMenu();
				oldMouseHit = mouseHit;
				selectedCard = mouseHit.collider.gameObject.transform.parent.gameObject;
				
				//Check if it is the correct/appropreate selected card...
				if(selectedCard.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand)
				{
					//Do nothing
				}
				else
				{
					//Wrong card, get rid of the menu (prevents cheating)
					mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}

				//Reset the collisions
				ResetCollisions();
			}
			//Else, If it is the overview plane...
			else if(mouseHit.collider.gameObject.name == "OverviewP1" && Input.GetButtonDown("Fire1") && player1Turn && !movingSummonCard && !movingBattleChangeCard && !targetingMode)
			{
				Vector2 localPoint = mouseHit.textureCoord;
				//Convert the hit texture coordinates into camera coordinates
				Ray portalRay = topViewCamera.ScreenPointToRay(new Vector2(localPoint.x * topViewCamera.pixelWidth, localPoint.y * topViewCamera.pixelHeight));
				RaycastHit portalHit;
				
				//Test these camera coordinates in another raycast test
				if(Physics.Raycast(portalRay, out portalHit))
				{
					//If that point was a card (another ray determined there was a card)...
					if((portalHit.collider.gameObject.name == "CardFront" || portalHit.collider.gameObject.name == "CardBack"))
					{
						//Destroy the older menu, it is not relevent
						if(oldMouseHit.transform != null)
						{
							oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
						}
						
						//Show a menu of all possable actions for the card (if it is manual dueling, show all options)
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().enabled = true;
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player1Turn = false;
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player2Turn = true;
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().cardPosition = mouseHit.collider.gameObject.transform.position;
						oldMouseHit = portalHit;
						selectedCard = portalHit.collider.gameObject.transform.parent.gameObject;
						
						//Check if it is the correct/appropreate selected card...
						if(selectedCard.GetComponent<ExtraCardProperties>().controller == player1)
						{
							//Create menu
							portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().CreateMenu();
						}
						else
						{
							//Do nothing
						}
					}
				}
			}
			if(targetingMode && Input.GetButtonDown("Fire1"))
			{
				//Get rid of the attack notifications
				DestroyAttackNotifications();
					
				if(mouseHit.collider.gameObject.name == "OverviewP1")
				{
					Vector2 localPoint = mouseHit.textureCoord;
					//Convert the hit texture coordinates into camera coordinates
					Ray portalRay = topViewCamera.ScreenPointToRay(new Vector2(localPoint.x * topViewCamera.pixelWidth, localPoint.y * topViewCamera.pixelHeight));
					RaycastHit portalHit;
					
					//Test these camera coordinates in another raycast test
					if(Physics.Raycast(portalRay, out portalHit))
					{
						if(attackTargeting)
						{
							if(portalHit.transform.gameObject.GetComponent<ExtraCardProperties>().canBeTargetedByBattle &&
							(portalHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster || portalHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.normalMonster || portalHit.transform.gameObject.GetComponent<Card>().isTrapMonster || portalHit.transform.gameObject.GetComponent<Card>().isSpellMonster) &&
							portalHit.transform.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
							{
								if(portalHit.transform.gameObject.GetComponent<ExtraCardProperties>().controller == player2)
								{
									secondTargetedMonster = portalHit.transform.gameObject;
									
									//If it is face down defense...
									if(secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									{
										//Flip it face up
										movingBattleChangeCard = true;
										startBattleChangeRotation = secondTargetedMonster.transform.rotation;
										endBattleChangeRotation = Quaternion.Euler(90, 90, 0);
										afterFlipBattle = true;
										player2POV = true;
										targetingMode = false;
										secondTargetedMonster.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpDefense;
										
										//Activate effects (if any)
									}
									//If it is face down attack...
									else if(secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									{
										//Flip it face up
										movingBattleChangeCard = true;
										startBattleChangeRotation = secondTargetedMonster.transform.rotation;
										endBattleChangeRotation = Quaternion.Euler(90, 180, 0);
										afterFlipBattle = true;
										player2POV = true;
										targetingMode = false;
										secondTargetedMonster.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
											
										//Activate effects (if any)
									}
									else if(secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack)
									{
										targetingMode = false;
										attack.Battle();
									}
								}
							}
						}
						else if(cardEffectTargeting)
						{
							//Add later...
							targetingMode = false;
						}
						else if(tributeTargeting)
						{
							//Destroy the older menu, it is not relevent
							if(oldMouseHit.transform != null)
							{
								oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
							}
							
							//Check the field for clicks...
							if((portalHit.collider.gameObject.name == "CardFront" || portalHit.collider.gameObject.name == "CardBack") && portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field && !portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//Put the selected cards into an array until the amount is reached
								portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = true;
								selectedCards[selectedAnountToTribute] = portalHit.collider.gameObject.transform.parent.gameObject;
								startingTributePositions[selectedAnountToTribute] = portalHit.collider.gameObject.transform.parent.gameObject.transform.position;
								startingTributeRotations[selectedAnountToTribute] = portalHit.collider.gameObject.transform.parent.gameObject.transform.rotation;
								endTributePosition = graveyardP1.transform.position;
								endTributeRotation = Quaternion.Euler(90, 0, 0);
								
								selectedAnountToTribute++;
								
								tans.CheckForTributableMonsters();
								
								//if amount is reached
								if(neededAmountToTribute == selectedAnountToTribute)
								{
									//Turn on tributing animation and send the array of cards to the graveyard (or banish) area
									movingTributeCards = true;
								}
							}
							else if((portalHit.collider.gameObject.name == "CardFront" || portalHit.collider.gameObject.name == "CardBack") && portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field && portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//Already selected then clicked on again means to deselect the selected card
								portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = false;
								selectedAnountToTribute--;
								
								//Loop through the selected cards
								for(int i = 0; i < selectedCards.Length; i++)
								{
									//Check if it is not null
									if(selectedCards[i] != null)
									{
										//Check by card name...
										if(selectedCards[i].name == portalHit.collider.gameObject.transform.parent.gameObject.name)
										{
											//Remove the selected card
											selectedCards[i] = null;
											startingTributePositions[i] = new Vector3(0, 0, 0);
											startingTributeRotations[i] = Quaternion.Euler(0, 0, 0);
										}
									}
								}
								
								tans.CheckForTributableMonsters();
							}
						}
					}
				}
				else if(mouseHit.collider.gameObject.name == "CardFront" || mouseHit.collider.gameObject.name == "CardBack")
				{
					if(attackTargeting)
					{
						if(mouseHit.transform.gameObject.GetComponent<ExtraCardProperties>().canBeTargetedByBattle && (mouseHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster || mouseHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.normalMonster || mouseHit.transform.gameObject.GetComponent<Card>().isTrapMonster || mouseHit.transform.gameObject.GetComponent<Card>().isSpellMonster) && mouseHit.transform.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
						{
							secondTargetedMonster = mouseHit.transform.gameObject;
							targetingMode = false;
						}
					}
					else if(cardEffectTargeting)
					{
						//Add later...
						targetingMode = false;
					}
				}
			}
			//Check for an exit to targeting mode
			if(targetingMode && Input.GetButtonDown("Fire2"))
			{
				//Turn on the attack notifications
				TurnOnAttackNotifications();
				
				//Turn off the targeting notifications
				tans.DestroyNotifications();
				
				//Exit targeting mode
				targetingMode = false;
				
				if(tributeTargeting)
				{
					tributeTargeting = false;
				}
			}
			//Similar to targeting mode, except that you cant get out of a discard
			if(discardingMode && Input.GetButtonDown("Fire1"))
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
				
				//Check the hand for clicks...
				if((mouseHit.collider.gameObject.name == "CardFront" || mouseHit.collider.gameObject.name == "CardBack") && mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand && !mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
				{
					//Put the selected cards into an array until the amount is reached
					mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = true;
					selectedCards[selectedAnountToDiscard] = mouseHit.collider.gameObject.transform.parent.gameObject;
					startingDiscardPositions[selectedAnountToDiscard] = mouseHit.collider.gameObject.transform.parent.gameObject.transform.position;
					startingDiscardRotations[selectedAnountToDiscard] = mouseHit.collider.gameObject.transform.parent.gameObject.transform.rotation;
					endDiscardPosition = graveyardP1.transform.position;
					endDiscardRotation = Quaternion.Euler(90, 0, 0);
					selectedAnountToDiscard++;
					
					tans.NotifyUserOfDiscardingCards();
					
					//if amount is reached
					if(neededAmountToDiscard == selectedAnountToDiscard)
					{
						//Turn on discarding animation and send the array of cards to the graveyard (or banish) area
						movingDiscardedCards = true;
					}
				}
				else if((mouseHit.collider.gameObject.name == "CardFront" || mouseHit.collider.gameObject.name == "CardBack") && mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand && mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
				{
					//Already selected then clicked on again means to deselect the selected card
					mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = false;
					selectedAnountToDiscard--;
					
					//Loop through the selected cards
					for(int i = 0; i < selectedCards.Length; i++)
					{
						//Check if it is not null
						if(selectedCards[i] != null)
						{
							//Check by card name...
							if(selectedCards[i].name == mouseHit.collider.gameObject.transform.parent.gameObject.name)
							{
								//Remove the selected card
								selectedCards[i] = null;
								startingDiscardPositions[i] = new Vector3(0, 0, 0);
								startingDiscardRotations[i] = Quaternion.Euler(0, 0, 0);
							}
						}
					}
					
					tans.NotifyUserOfDiscardingCards();
				}
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceUpAttackSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(90, 0, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceUpDefenseSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(90, 270, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpDefense;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceDownAttackSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(270, 180, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownAttack;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceDownDefenseSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(270, 90, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(special && Input.GetButtonDown("Fire2"))
			{
				//Cancel the special summon
				special = false;
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		//If the ray cast physics...
		if(player2Turn && Physics.Raycast(mouseRayP2, out mouseHit))
		{
			//If the surrender button was hit...
			if(mouseHit.collider.gameObject.name == "SurrenderButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//End the game and go back to the main menu (the other player wins upon doing so)
				crosshair.TurnOff();
				crosshair.SearchForCursorToLock = false;
				//Application.LoadLevel("MainMenu");
				SceneManager.LoadScene("MainMenu");
			}
			//Else, if the shuffle button was hit...
			if(mouseHit.collider.gameObject.name == "ShuffleButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				ShuffleHand(player2);
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the draw phase button was hit...
			if(mouseHit.collider.gameObject.name == "DrawPhaseButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the standby phase button was hit...
			if(mouseHit.collider.gameObject.name == "StandbyPhaseButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the main phase 1 button was hit...
			if(mouseHit.collider.gameObject.name == "MainPhase1ButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the battle phase button was hit...
			if(mouseHit.collider.gameObject.name == "BattlePhaseButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard && turnCounter != 1)
			{
				//The monsters that did attack the turn before can now attack next turn (unless card effects say otherwise)
				attack.TurnOnAttacking();
				
				selectedBattlePhase = true;
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the main phase 2 button was hit...
			if(mouseHit.collider.gameObject.name == "MainPhase2ButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Get rid of the attack notifications (from Battle phase to main phase 2)
				DestroyAttackNotifications();
					
				if(phase == Phases.battle)
				{
					selectedMainPhase2 = true;
				}
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the end phase button was hit...
			if(mouseHit.collider.gameObject.name == "EndPhaseButtonP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Get rid of the attack notifications (from Battle phase to end phase)
				DestroyAttackNotifications();
					
				selectedEndPhase = true;
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
				
				if(player2.handSize - player2.handSizeLimit >= 1)
				{
					Discard(player2, player2.handSize - player2.handSizeLimit);
				}
				
				attack.TurnOnBattleChanges();
				usedNormalSummons = 0;
				allowedNormalSummons = 1;
			}
			if(mouseHit.collider.gameObject.name == "ShowHandP2" && Input.GetButtonDown("Fire1"))
			{
				//Show the hand from in front the player
				
				//Show the "Hide hand" button
				hideHandP2.SetActive(true);
				
				//Hide the "Show hand" button
				showHandP2.SetActive(false);
				
				//Turn on collisions
				for(int i = 0; i < player2.cardsInHand.Length && player2.cardsInHand[i] != null; i++)
				{
					player2.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true;
					player2.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = true;
				}
				
				//Show the hand
				player2.gameObject.GetComponent<Camera>().cullingMask ^= 32768; // = 1000000000000000 binary
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			if(mouseHit.collider.gameObject.name == "HideHandP2" && Input.GetButtonDown("Fire1"))
			{
				//Hide the hand from in front the player
				
				//Show the "Show hand" button
				showHandP2.SetActive(true);
				
				//Hide the "Hide hand" button
				hideHandP2.SetActive(false);
				
				//Turn off collisions
				for(int i = 0; i < player2.cardsInHand.Length && player2.cardsInHand[i] != null; i++)
				{
					player2.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
					player2.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = false;
				}
				
				//Hide the hand
				player2.gameObject.GetComponent<Camera>().cullingMask ^= 32768; // = 1000000000000000 binary
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			if(mouseHit.collider.gameObject.name == "ShowInfoP2" && Input.GetButtonDown("Fire1"))
			{
				//Show the "Hide info" button
				hideInfoP2.SetActive(true);
				
				//Hide the "Show info" button
				showInfoP2.SetActive(false);
				
				//show the info
				viewCardObjectP2.SetActive(true);
				viewContentP2.SetActive(true);
				viewDescriptionP2.SetActive(true);
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			if(mouseHit.collider.gameObject.name == "HideInfoP2" && Input.GetButtonDown("Fire1"))
			{
				//Show the "Show info" button
				showInfoP2.SetActive(true);
				
				//Hide the "Hide info" button
				hideInfoP2.SetActive(false);
				
				//Hide the info
				viewCardObjectP2.SetActive(false);
				viewContentP2.SetActive(false);
				viewDescriptionP2.SetActive(false);
				
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
			}
			//Else, if the clicked on thing was a card...
			if(mouseHit.collider.gameObject.name == "CardFront" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard && !movingActivateCard)
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
				
				//Show a menu of all possable actions for the card (if it is manual dueling, show all options)
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().enabled = true;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player1Turn = false;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player2Turn = true;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().cardPosition = mouseHit.collider.gameObject.transform.position;
				mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().CreateMenu();
				oldMouseHit = mouseHit;
				selectedCard = mouseHit.collider.gameObject.transform.parent.gameObject;
				
				//Check if it is the correct/appropreate selected card...
				if(selectedCard.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand)
				{
					//Do nothing
				}
				else
				{
					//Wrong card, get rid of the menu (prevents cheating)
					mouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}

				//Reset the collisions
				ResetCollisions();
			}
			//Else, If it is the overview plane...
			if(mouseHit.collider.gameObject.name == "OverviewP2" && Input.GetButtonDown("Fire1") && player2Turn && !movingSummonCard && !movingBattleChangeCard && !targetingMode)
			{
				Vector2 localPoint = mouseHit.textureCoord;
				//Convert the hit texture coordinates into camera coordinates
				Ray portalRay = topViewCamera.ScreenPointToRay(new Vector2(localPoint.x * topViewCamera.pixelWidth, localPoint.y * topViewCamera.pixelHeight));
				RaycastHit portalHit;
				
				//Test these camera coordinates in another raycast test
				if(Physics.Raycast(portalRay, out portalHit))
				{
					//If that point was a card (another ray determined there was a card)...
					if((portalHit.collider.gameObject.name == "CardFront" || portalHit.collider.gameObject.name == "CardBack"))
					{
						//Destroy the older menu, it is not relevent
						if(oldMouseHit.transform != null)
						{
							oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
						}
						
						//Show a menu of all possable actions for the card (if it is manual dueling, show all options)
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().enabled = true;
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player1Turn = false;
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().player2Turn = true;
						portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().cardPosition = mouseHit.collider.gameObject.transform.position;
						
						oldMouseHit = portalHit;
						selectedCard = portalHit.collider.gameObject.transform.parent.gameObject;
						
						//Check if it is the correct/appropreate selected card...
						if(selectedCard.GetComponent<ExtraCardProperties>().controller == player2)
						{
							//Create menu
							portalHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().CreateMenu();
						}
						else
						{
							//Do nothing
						}
					}
				}
			}
			if(targetingMode && Input.GetButtonDown("Fire1"))
			{
				//Get rid of the attack notifications
				DestroyAttackNotifications();
					
				if(mouseHit.collider.gameObject.name == "OverviewP2")
				{
					Vector2 localPoint = mouseHit.textureCoord;
					//Convert the hit texture coordinates into camera coordinates
					Ray portalRay = topViewCamera.ScreenPointToRay(new Vector2(localPoint.x * topViewCamera.pixelWidth, localPoint.y * topViewCamera.pixelHeight));
					RaycastHit portalHit;
					
					//Test these camera coordinates in another raycast test
					if(Physics.Raycast(portalRay, out portalHit))
					{
						if(attackTargeting)
						{
							if(portalHit.transform.gameObject.GetComponent<ExtraCardProperties>().canBeTargetedByBattle &&
							(portalHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster || portalHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.normalMonster || portalHit.transform.gameObject.GetComponent<Card>().isTrapMonster || portalHit.transform.gameObject.GetComponent<Card>().isSpellMonster) &&
							portalHit.transform.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
							{
								if(portalHit.transform.gameObject.GetComponent<ExtraCardProperties>().controller == player1)
								{
									secondTargetedMonster = portalHit.transform.gameObject;
									
									//If it is face down defense...
									if(secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense)
									{
										//Flip it face up
										movingBattleChangeCard = true;
										startBattleChangeRotation = secondTargetedMonster.transform.rotation;
										endBattleChangeRotation = Quaternion.Euler(90, 270, 0);
										afterFlipBattle = true;
										player1POV = true;
										targetingMode = false;
										secondTargetedMonster.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpDefense;
										
										//Activate effects (if any)
									}
									//If it is face down attack...
									else if(secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack)
									{
										//Flip it face up
										movingBattleChangeCard = true;
										startBattleChangeRotation = secondTargetedMonster.transform.rotation;
										endBattleChangeRotation = Quaternion.Euler(90, 0, 0);
										afterFlipBattle = true;
										player1POV = true;
										targetingMode = false;
										secondTargetedMonster.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
										
										//Activate effects (if any)
									}
									else if(secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense || secondTargetedMonster.gameObject.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack)
									{
										targetingMode = false;
										attack.Battle();
									}
								}
							}
						}
						else if(cardEffectTargeting)
						{
							//Add later...
							targetingMode = false;
						}
						else if(tributeTargeting)
						{
							//Destroy the older menu, it is not relevent
							if(oldMouseHit.transform != null)
							{
								oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
							}
							
							//Check the field for clicks...
							if((portalHit.collider.gameObject.name == "CardFront" || portalHit.collider.gameObject.name == "CardBack") && portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field && !portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//Put the selected cards into an array until the amount is reached
								portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = true;
								selectedCards[selectedAnountToTribute] = portalHit.collider.gameObject.transform.parent.gameObject;
								startingTributePositions[selectedAnountToTribute] = portalHit.collider.gameObject.transform.parent.gameObject.transform.position;
								startingTributeRotations[selectedAnountToTribute] = portalHit.collider.gameObject.transform.parent.gameObject.transform.rotation;
								endTributePosition = graveyardP2.transform.position;
								endTributeRotation = Quaternion.Euler(90, 180, 0);
								
								selectedAnountToTribute++;
								
								tans.CheckForTributableMonsters();
								
								//if amount is reached
								if(neededAmountToTribute == selectedAnountToTribute)
								{
									//Turn on tributing animation and send the array of cards to the graveyard (or banish) area
									movingTributeCards = true;
								}
							}
							else if((portalHit.collider.gameObject.name == "CardFront" || portalHit.collider.gameObject.name == "CardBack") && portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field && portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
							{
								//Already selected then clicked on again means to deselect the selected card
								portalHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = false;
								selectedAnountToTribute--;
								
								//Loop through the selected cards
								for(int i = 0; i < selectedCards.Length; i++)
								{
									//Check if it is not null
									if(selectedCards[i] != null)
									{
										//Check by card name...
										if(selectedCards[i].name == portalHit.collider.gameObject.transform.parent.gameObject.name)
										{
											//Remove the selected card
											selectedCards[i] = null;
											startingTributePositions[i] = new Vector3(0, 0, 0);
											startingTributeRotations[i] = Quaternion.Euler(0, 0, 0);
										}
									}
								}
								
								tans.CheckForTributableMonsters();
							}
						}
					}
				}
				else if(mouseHit.collider.gameObject.name == "CardFront" || mouseHit.collider.gameObject.name == "CardBack")
				{
					if(attackTargeting)
					{
						if(mouseHit.transform.gameObject.GetComponent<ExtraCardProperties>().canBeTargetedByBattle && (mouseHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.effectMonster || mouseHit.transform.gameObject.GetComponent<Card>().cardType == Card.CardType.normalMonster || mouseHit.transform.gameObject.GetComponent<Card>().isTrapMonster || mouseHit.transform.gameObject.GetComponent<Card>().isSpellMonster) && mouseHit.transform.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
						{
							secondTargetedMonster = mouseHit.transform.gameObject;
							targetingMode = false;
						}
					}
					else if(cardEffectTargeting)
					{
						//Add later...
						targetingMode = false;
					}
				}
			}
			//Check for an exit to targeting mode
			if(targetingMode && Input.GetButtonDown("Fire2"))
			{
				//Turn on the attack notifications
				TurnOnAttackNotifications();
					
				//Turn off the targeting notifications
				for(int i = 0; i < 60; i++)
				{
					if(GameObject.Find("targetNotification" + i) != null)
					{
						Destroy(GameObject.Find("targetNotification" + i));
					}
				}
					
				//Exit targeting mode
				targetingMode = false;
				
				if(tributeTargeting)
				{
					tributeTargeting = false;
				}
			}
			if(discardingMode && Input.GetButtonDown("Fire1"))
			{
				//Destroy the older menu, it is not relevent
				if(oldMouseHit.transform != null)
				{
					oldMouseHit.collider.gameObject.transform.parent.GetComponent<ContextMenuScript>().DestroyMenu();
				}
				
				//Check the hand for clicks...
				if((mouseHit.collider.gameObject.name == "CardFront" || mouseHit.collider.gameObject.name == "CardBack") && mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand && !mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
				{
					//Put the selected cards into an array until the amount is reached
					mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = true;
					selectedCards[selectedAnountToDiscard] = mouseHit.collider.gameObject.transform.parent.gameObject;
					startingDiscardPositions[selectedAnountToDiscard] = mouseHit.collider.gameObject.transform.parent.gameObject.transform.position;
					startingDiscardRotations[selectedAnountToDiscard] = mouseHit.collider.gameObject.transform.parent.gameObject.transform.rotation;
					endDiscardPosition = graveyardP2.transform.position;
					endDiscardRotation = Quaternion.Euler(90, 180, 0);
					selectedAnountToDiscard++;
					
					tans.NotifyUserOfDiscardingCards();
					
					//if amount is reached
					if(neededAmountToDiscard == selectedAnountToDiscard)
					{
						//Turn on discarding animation and send the array of cards to the graveyard (or banish) area
						movingDiscardedCards = true;
					}
				}
				else if((mouseHit.collider.gameObject.name == "CardFront" || mouseHit.collider.gameObject.name == "CardBack") && mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.hand && mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected)
				{
					//Already selected then clicked on again means to deselect the selected card
					mouseHit.collider.gameObject.transform.parent.gameObject.GetComponent<ExtraCardProperties>().isSelected = false;
					selectedAnountToDiscard--;
					
					//Loop through the selected cards
					for(int i = 0; i < selectedCards.Length; i++)
					{
						//Check if it is not null
						if(selectedCards[i] != null)
						{
							//Check by card name...
							if(selectedCards[i].name == mouseHit.collider.gameObject.transform.parent.gameObject.name)
							{
								//Remove the selected card
								selectedCards[i] = null;
								startingDiscardPositions[i] = new Vector3(0, 0, 0);
								startingDiscardRotations[i] = Quaternion.Euler(0, 0, 0);
							}
						}
					}
					
					tans.NotifyUserOfDiscardingCards();
				}
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceUpAttackSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(90, 180, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceUpDefenseSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(90, 90, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpDefense;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceDownAttackSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(270, 0, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownAttack;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(mouseHit.collider.gameObject.name == "Card: FaceDownDefenseSelection" && Input.GetButtonDown("Fire1"))
			{
				//Do the summon
				endSummonRotation = Quaternion.Euler(270, 270, 0);
				selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
				summon.Summon();
				
				//Remove the menu
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
			if(special && Input.GetButtonDown("Fire2"))
			{
				//Cancel the special summon
				special = false;
				selectedCard.SendMessage("DestroyMenu", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		
		if(attackingCards && !targetingMode)
		{
			//Turn on the attack notifications
			TurnOnAttackNotifications();
				
			float offset = 0.5f;
			
			if(player1Turn)
			{
				offset = 0.5f;
			}
			else if(player2Turn)
			{
				offset = -0.5f;
			}
		
			if(forward)
			{
				forwardMovement = forwardMovement + Time.deltaTime;
				
				if(MZ1P1_AttackNotification != null)
					MZ1P1_AttackNotification.transform.position = Vector3.Lerp(MZ1P1_AttackNotification_Start, new Vector3(MZ1P1_AttackNotification.transform.position.x, MZ1P1_AttackNotification.transform.position.y, MZ1P1_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ2P1_AttackNotification != null)
					MZ2P1_AttackNotification.transform.position = Vector3.Lerp(MZ2P1_AttackNotification_Start, new Vector3(MZ2P1_AttackNotification.transform.position.x, MZ2P1_AttackNotification.transform.position.y, MZ2P1_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ3P1_AttackNotification != null)
					MZ3P1_AttackNotification.transform.position = Vector3.Lerp(MZ3P1_AttackNotification_Start, new Vector3(MZ3P1_AttackNotification.transform.position.x, MZ3P1_AttackNotification.transform.position.y, MZ3P1_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ4P1_AttackNotification != null)
					MZ4P1_AttackNotification.transform.position = Vector3.Lerp(MZ4P1_AttackNotification_Start, new Vector3(MZ4P1_AttackNotification.transform.position.x, MZ4P1_AttackNotification.transform.position.y, MZ4P1_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ5P1_AttackNotification != null)
					MZ5P1_AttackNotification.transform.position = Vector3.Lerp(MZ5P1_AttackNotification_Start, new Vector3(MZ5P1_AttackNotification.transform.position.x, MZ5P1_AttackNotification.transform.position.y, MZ5P1_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ1P2_AttackNotification != null)
					MZ1P2_AttackNotification.transform.position = Vector3.Lerp(MZ1P2_AttackNotification_Start, new Vector3(MZ1P2_AttackNotification.transform.position.x, MZ1P2_AttackNotification.transform.position.y, MZ1P2_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ2P2_AttackNotification != null)
					MZ2P2_AttackNotification.transform.position = Vector3.Lerp(MZ2P2_AttackNotification_Start, new Vector3(MZ2P2_AttackNotification.transform.position.x, MZ2P2_AttackNotification.transform.position.y, MZ2P2_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ3P2_AttackNotification != null)
					MZ3P2_AttackNotification.transform.position = Vector3.Lerp(MZ3P2_AttackNotification_Start, new Vector3(MZ3P2_AttackNotification.transform.position.x, MZ3P2_AttackNotification.transform.position.y, MZ3P2_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ4P2_AttackNotification != null)
					MZ4P2_AttackNotification.transform.position = Vector3.Lerp(MZ4P2_AttackNotification_Start, new Vector3(MZ4P2_AttackNotification.transform.position.x, MZ4P2_AttackNotification.transform.position.y, MZ4P2_AttackNotification.transform.position.z + offset), forwardMovement);
				if(MZ5P2_AttackNotification != null)
					MZ5P2_AttackNotification.transform.position = Vector3.Lerp(MZ5P2_AttackNotification_Start, new Vector3(MZ5P2_AttackNotification.transform.position.x, MZ5P2_AttackNotification.transform.position.y, MZ5P2_AttackNotification.transform.position.z + offset), forwardMovement);
				
				if(forwardMovement >= 1)
				{
					forward = false;
					backward = true;
					forwardMovement = 0;
				}
			}
			
			if(backward)
			{
				backwardMovement = backwardMovement + Time.deltaTime;
				
				if(MZ1P1_AttackNotification != null)
					MZ1P1_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ1P1_AttackNotification.transform.position.x, MZ1P1_AttackNotification.transform.position.y, MZ1P1_AttackNotification.transform.position.z + offset), MZ1P1_AttackNotification_Start, backwardMovement);
				if(MZ2P1_AttackNotification != null)
					MZ2P1_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ2P1_AttackNotification.transform.position.x, MZ2P1_AttackNotification.transform.position.y, MZ2P1_AttackNotification.transform.position.z + offset), MZ2P1_AttackNotification_Start, backwardMovement);
				if(MZ3P1_AttackNotification != null)
					MZ3P1_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ3P1_AttackNotification.transform.position.x, MZ3P1_AttackNotification.transform.position.y, MZ3P1_AttackNotification.transform.position.z + offset), MZ3P1_AttackNotification_Start, backwardMovement);
				if(MZ4P1_AttackNotification != null)
					MZ4P1_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ4P1_AttackNotification.transform.position.x, MZ4P1_AttackNotification.transform.position.y, MZ4P1_AttackNotification.transform.position.z + offset), MZ4P1_AttackNotification_Start, backwardMovement);
				if(MZ5P1_AttackNotification != null)
					MZ5P1_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ5P1_AttackNotification.transform.position.x, MZ5P1_AttackNotification.transform.position.y, MZ5P1_AttackNotification.transform.position.z + offset), MZ5P1_AttackNotification_Start, backwardMovement);
				if(MZ1P2_AttackNotification != null)
					MZ1P2_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ1P2_AttackNotification.transform.position.x, MZ1P2_AttackNotification.transform.position.y, MZ1P2_AttackNotification.transform.position.z + offset), MZ1P2_AttackNotification_Start, backwardMovement);
				if(MZ2P2_AttackNotification != null)
					MZ2P2_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ2P2_AttackNotification.transform.position.x, MZ2P2_AttackNotification.transform.position.y, MZ2P2_AttackNotification.transform.position.z + offset), MZ2P2_AttackNotification_Start, backwardMovement);
				if(MZ3P2_AttackNotification != null)
					MZ3P2_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ3P2_AttackNotification.transform.position.x, MZ3P2_AttackNotification.transform.position.y, MZ3P2_AttackNotification.transform.position.z + offset), MZ3P2_AttackNotification_Start, backwardMovement);
				if(MZ4P2_AttackNotification != null)
					MZ4P2_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ4P2_AttackNotification.transform.position.x, MZ4P2_AttackNotification.transform.position.y, MZ4P2_AttackNotification.transform.position.z + offset), MZ4P2_AttackNotification_Start, backwardMovement);
				if(MZ5P2_AttackNotification != null)
					MZ5P2_AttackNotification.transform.position = Vector3.Lerp(new Vector3(MZ5P2_AttackNotification.transform.position.x, MZ5P2_AttackNotification.transform.position.y, MZ5P2_AttackNotification.transform.position.z + offset), MZ5P2_AttackNotification_Start, backwardMovement);
				
				if(backwardMovement >= 1)
				{
					forward = true;
					backward = false;
					backwardMovement = 0;
				}
			}
		}
		else if(attackingCards && targetingMode)
		{
			//Turn off attack notifications
			TurnOffAttackNotifications();
		}
	}
	
	//-------------------------------------------------------------------------GUI DISPLAY-----------------------------------------------------------------------------------------------
	void OnGUI()
	{
		//Reference variables
		int width = Screen.width;
		int height = Screen.height;
		
		float boxWidth = 200f;
		float boxHeight = 100f;
		
		float buttonWidth = 50f;
		float buttonHeight = 25f;
		
		//If we are asked if we want to activate any effect or do confermation of things... (The card effect's responsibility to turn this condition on!)
		if(askPlayer)
		{
			//If the question is a general activation question...
			if(generalActivationQuestion)
			{
				//Show the box with buttons
				GUI.Box(new Rect((width / 2) - (boxWidth / 2), (height / 2) - (boxHeight / 2), boxWidth, boxHeight), confirmationOfActivation);
				if(GUI.Button(new Rect((width / 2) - (boxWidth / 2), height / 2, buttonWidth, buttonHeight), "Yes"))
				{
					//Find the card selected card and call the activation function
					//player1.cardsInHand[i].gameObject.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
					generalActivationQuestion = false;
					askPlayer = false;
				}
				
				if(GUI.Button(new Rect(width / 2, height / 2, buttonWidth, buttonHeight), "No"))
				{
					//Do nothing
					noMoreEffects = true;
					generalActivationQuestion = false;
					askPlayer = false;
				}
			}
			
			//If it is a customized question...
			if(customizedQuestion)
			{
				//Show the box
				GUI.Box(new Rect((width / 2) - (boxWidth / 2), (height / 2) - (boxHeight / 2), boxWidth, boxHeight), customizableConfirmation);
			}
		}
	}
	
	//-------------------------------------------------------------------------CUSTOM FUNCTIONS-----------------------------------------------------------------------------------------------
	public void SetupDuel()
	{
		//Set up the duel with both player's decks and hands
		currentDeck.transform.position = mainDeckAreaP1.transform.position;
		currentDeckAI.transform.position = mainDeckAreaP2.transform.position;
		
		//Loop for each of player1's cards
		for(int i = 0; i < currentDeck.deck.Length; i++)
		{
			//Save these materials for testing
			//Material myNewMaterialFront = new Material(Shader.Find("Mobile/VertexLit"));
			//Material myNewMaterialBack = new Material(Shader.Find("Mobile/VertexLit"));

			Material myNewMaterialFront = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
			Material myNewMaterialBack = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
			
			GameObject cardClone = (GameObject)Instantiate(card, new Vector3(currentDeck.transform.position.x, currentDeck.transform.position.y + (i * globalIncrement), currentDeck.transform.position.z), Quaternion.Euler(270, 0, 0));
			cardClone.name = "P1: " + i + ": " + currentDeck.deck[i].cardName;
			cardClone.GetComponent<Card>().SetCard(currentDeck.deck[i]);
			cardClone.AddComponent<ExtraCardProperties>();
			cardClone.GetComponent<ExtraCardProperties>().owner = player1;
			cardClone.AddComponent(System.Type.GetType("_" + currentDeck.deck[i].serial));
			cardClone.AddComponent<ContextMenuScript>();
			cardClone.GetComponent<ContextMenuScript>().enabled = false;
			cardClone.tag = "Card";
			cardClone.layer = 8;
			cardClone.AddComponent<Rigidbody>();
			cardClone.GetComponent<Rigidbody>().isKinematic = true;
			cardClone.GetComponent<NetworkTransform>();
			
			GameObject cardCloneFront = cardClone.transform.GetChild(0).gameObject;
			cardCloneFront.GetComponent<Renderer>().material = myNewMaterialFront;
			cardCloneFront.GetComponent<Renderer>().material.SetTexture("_MainTex", currentDeck.deck[i].frontTexture);
			cardCloneFront.tag = "Card";
			cardCloneFront.layer = 8;
			cardCloneFront.GetComponent<MeshCollider>().convex = true;
			
			GameObject cardCloneBack = cardClone.transform.GetChild(1).gameObject;
			cardCloneBack.GetComponent<Renderer>().material = myNewMaterialBack;
			cardCloneBack.GetComponent<Renderer>().material.SetTexture("_MainTex", currentDeck.deck[i].backTexture);
			cardCloneBack.tag = "Card";
			cardCloneBack.layer = 8;
			cardCloneBack.GetComponent<MeshCollider>().convex = true;
			
			cardClone.gameObject.SendMessage("SetGameAPI", SendMessageOptions.DontRequireReceiver);
		}
		
		currentExtraDeck.transform.position = extraDeckAreaP1.transform.position;
		
		for(int i = 0; i < currentExtraDeck.extraDeck.Length; i++)
		{
			//If this card slot is not null
			if(currentExtraDeck.extraDeck[i] != null)
			{
				//Save these materials for testing
				//Material myNewMaterialFront = new Material(Shader.Find("Mobile/VertexLit"));
				//Material myNewMaterialBack = new Material(Shader.Find("Mobile/VertexLit"));

				Material myNewMaterialFront = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
				Material myNewMaterialBack = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
				
				GameObject cardClone = (GameObject)Instantiate(card, new Vector3(currentExtraDeck.transform.position.x, currentExtraDeck.transform.position.y + (i * globalIncrement), currentExtraDeck.transform.position.z), Quaternion.Euler(270, 0, 0));
				cardClone.name = "P1 Extra: " + i + ": " + currentExtraDeck.extraDeck[i].cardName;
				cardClone.GetComponent<Card>().SetCard(currentExtraDeck.extraDeck[i]);
				cardClone.AddComponent<ExtraCardProperties>();
				cardClone.GetComponent<ExtraCardProperties>().owner = player1;
				cardClone.AddComponent(System.Type.GetType("_" + currentExtraDeck.extraDeck[i].serial));
				cardClone.AddComponent<ContextMenuScript>();
				cardClone.GetComponent<ContextMenuScript>().enabled = false;
				cardClone.tag = "Card";
				cardClone.layer = 8;
				cardClone.AddComponent<Rigidbody>();
				cardClone.GetComponent<Rigidbody>().isKinematic = true;
				cardClone.GetComponent<NetworkTransform>();
				
				GameObject cardCloneFront = cardClone.transform.GetChild(0).gameObject;
				cardCloneFront.GetComponent<Renderer>().material = myNewMaterialFront;
				cardCloneFront.GetComponent<Renderer>().material.SetTexture("_MainTex", currentExtraDeck.extraDeck[i].frontTexture);
				cardCloneFront.tag = "Card";
				cardCloneFront.layer = 8;
				cardCloneFront.GetComponent<MeshCollider>().convex = true;
				
				GameObject cardCloneBack = cardClone.transform.GetChild(1).gameObject;
				cardCloneBack.GetComponent<Renderer>().material = myNewMaterialBack;
				cardCloneBack.GetComponent<Renderer>().material.SetTexture("_MainTex", currentExtraDeck.extraDeck[i].backTexture);
				cardCloneBack.tag = "Card";
				cardCloneBack.layer = 8;
				cardCloneBack.GetComponent<MeshCollider>().convex = true;
				
				cardClone.gameObject.SendMessage("SetGameAPI", SendMessageOptions.DontRequireReceiver);
			}
		}
		
		//Loop for each of player2's cards
		for(int i = 0; i < currentDeckAI.deck.Length; i++)
		{
			//Save these materials for testing
			//Material myNewMaterialFront = new Material(Shader.Find("Mobile/VertexLit"));
			//Material myNewMaterialBack = new Material(Shader.Find("Mobile/VertexLit"));

			Material myNewMaterialFront = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
			Material myNewMaterialBack = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
			
			GameObject cardClone = (GameObject)Instantiate(card, new Vector3(currentDeckAI.transform.position.x, currentDeckAI.transform.position.y + (i * globalIncrement), currentDeckAI.transform.position.z), Quaternion.Euler(270, 180, 0));
			cardClone.name = "P2: " + i + ": " + currentDeckAI.deck[i].cardName;
			cardClone.GetComponent<Card>().SetCard(currentDeckAI.deck[i]);
			cardClone.AddComponent<ExtraCardProperties>();
			cardClone.GetComponent<ExtraCardProperties>().owner = player2;
			cardClone.AddComponent(System.Type.GetType("_" + currentDeckAI.deck[i].serial));
			cardClone.AddComponent<ContextMenuScript>();
			cardClone.GetComponent<ContextMenuScript>().enabled = false;
			cardClone.tag = "Card";
			cardClone.layer = 15;
			cardClone.AddComponent<Rigidbody>();
			cardClone.GetComponent<Rigidbody>().isKinematic = true;
			cardClone.GetComponent<NetworkTransform>();
			
			GameObject cardCloneFront = cardClone.transform.GetChild(0).gameObject;
			cardCloneFront.GetComponent<Renderer>().material = myNewMaterialFront;
			cardCloneFront.GetComponent<Renderer>().material.SetTexture("_MainTex", currentDeckAI.deck[i].frontTexture);
			cardCloneFront.tag = "Card";
			cardCloneFront.layer = 15;
			cardCloneFront.GetComponent<MeshCollider>().convex = true;
			
			GameObject cardCloneBack = cardClone.transform.GetChild(1).gameObject;
			cardCloneBack.GetComponent<Renderer>().material = myNewMaterialBack;
			cardCloneBack.GetComponent<Renderer>().material.SetTexture("_MainTex", currentDeckAI.deck[i].backTexture);
			cardCloneBack.tag = "Card";
			cardCloneBack.layer = 15;
			cardCloneBack.GetComponent<MeshCollider>().convex = true;
			
			cardClone.gameObject.SendMessage("SetGameAPI", SendMessageOptions.DontRequireReceiver);
		}
		
		currentExtraDeckAI.transform.position = extraDeckAreaP2.transform.position;
		
		for(int i = 0; i < currentExtraDeckAI.extraDeck.Length; i++)
		{
			//If this card slot is not null
			if(currentExtraDeckAI.extraDeck[i] != null)
			{
				//Save these materials for testing
				//Material myNewMaterialFront = new Material(Shader.Find("Mobile/VertexLit"));
				//Material myNewMaterialBack = new Material(Shader.Find("Mobile/VertexLit"));

				Material myNewMaterialFront = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
				Material myNewMaterialBack = new Material(Shader.Find("Mobile/Unlit (Supports Lightmap)"));
				
				GameObject cardClone = (GameObject)Instantiate(card, new Vector3(currentExtraDeckAI.transform.position.x, currentExtraDeckAI.transform.position.y + (i * globalIncrement), currentExtraDeckAI.transform.position.z), Quaternion.Euler(270, 180, 0));
				cardClone.name = "P2 Extra: " + i + ": " + currentExtraDeckAI.extraDeck[i].cardName;
				cardClone.GetComponent<Card>().SetCard(currentExtraDeckAI.extraDeck[i]);
				cardClone.AddComponent<ExtraCardProperties>();
				cardClone.GetComponent<ExtraCardProperties>().owner = player2;
				cardClone.AddComponent(System.Type.GetType("_" + currentExtraDeckAI.extraDeck[i].serial));
				cardClone.AddComponent<ContextMenuScript>();
				cardClone.GetComponent<ContextMenuScript>().enabled = false;
				cardClone.tag = "Card";
				cardClone.layer = 15;
				cardClone.AddComponent<Rigidbody>();
				cardClone.GetComponent<Rigidbody>().isKinematic = true;
				cardClone.GetComponent<NetworkTransform>();
				
				GameObject cardCloneFront = cardClone.transform.GetChild(0).gameObject;
				cardCloneFront.GetComponent<Renderer>().material = myNewMaterialFront;
				cardCloneFront.GetComponent<Renderer>().material.SetTexture("_MainTex", currentExtraDeckAI.extraDeck[i].frontTexture);
				cardCloneFront.tag = "Card";
				cardCloneFront.layer = 15;
				cardCloneFront.GetComponent<MeshCollider>().convex = true;
				
				GameObject cardCloneBack = cardClone.transform.GetChild(1).gameObject;
				cardCloneBack.GetComponent<Renderer>().material = myNewMaterialBack;
				cardCloneBack.GetComponent<Renderer>().material.SetTexture("_MainTex", currentExtraDeckAI.extraDeck[i].backTexture);
				cardCloneBack.tag = "Card";
				cardCloneBack.layer = 15;
				cardCloneBack.GetComponent<MeshCollider>().convex = true;
				
				cardClone.gameObject.SendMessage("SetGameAPI", SendMessageOptions.DontRequireReceiver);
			}
		}
		
		//Both players draw until they have 5 cards
		Draw(player1, startingHand, true);
		Draw(player2, startingHand, true);
		
		//Cleanup before official start of duel (Speed up the frames per second)
		card.SetActive(false);
		rps.gameObject.SetActive(false);
		
		//Turn on the mouse look script
		player1.GetComponent<MouseLookScript>().enabled = true;
		player2.GetComponent<MouseLookScript>().enabled = true;
		
		//Turn on crosshair
		crosshair.TurnOn();
		crosshair.SearchForCursorToLock = true;
		
		//Now it is official
		officiallyStarted = true;
	}
	
	//The draw function for drawing cards to the hand
	public void Draw(Player playerDrawing, int amount = 1, bool autoCardPlacement = false)
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		//For the amount to draw...
		for(int i = 1; i <= amount; i++)
		{
			//Depending on the player that is drawing...
			if(playerDrawing == player1)
			{
				//Set up the draw for player 1
				originOfRay = P1DrawRay.transform.position;
				player1Drawing = true;
				player2Drawing = false;
				player1.IncreaseHandSize(1);
			}
			else if(playerDrawing == player2)
			{
				//Set up the draw for player 2
				originOfRay = P2DrawRay.transform.position;
				player1Drawing = false;
				player2Drawing = true;
				player2.IncreaseHandSize(1);
			}
			
			//Raycast from the top to the deck (the closest card that gets hit shall be selected)...
			if(Physics.Raycast(originOfRay, direction, out hit))
			{
				if(hit.distance < maxDistance && hit.collider.gameObject.tag == "Card")
				{
					//Detect the card and store the information
					drawableCard = hit.transform.gameObject;
					startingPosition = drawableCard.transform.position;
					startingRotation = drawableCard.transform.rotation;
					
					//This check is  only for the set up (without the update function)
					if(autoCardPlacement)
					{
						PlaceCard(i);
					}
					//Update should show the card moving and rotating
					else
					{
						//Move the card to the player's hand
						movingDrawCard = true;
					}
					
					//The card is no longer in the deck, it it in the hand now
					drawableCard.GetComponent<ExtraCardProperties>().currentState = ExtraCardProperties.CurrentState.hand;
					
					if(playerDrawing == player1)
					{
						//Loop through the player's hand...
						for(int j = 0; j < player1.cardsInHand.Length; j++)
						{
							//If there is an empty slot...
							if(player1.cardsInHand[j] == null)
							{
								//Add that drawn card to the array and break out of the loop
								player1.cardsInHand[j] = drawableCard.GetComponent<Card>();
								break;
							}
						}
					}
					else if(playerDrawing == player2)
					{
						//Loop through the player's hand...
						for(int j = 0; j < player2.cardsInHand.Length; j++)
						{
							//If there is an empty slot...
							if(player2.cardsInHand[j] == null)
							{
								//Add that drawn card to the array and break out of the loop
								player2.cardsInHand[j] = drawableCard.GetComponent<Card>();
								break;
							}
						}
					}
				}
			}
			else
			{
				//Gameover, there is no card to draw
				if(playerDrawing == player1)
				{
					gameoverReason = "\"Deck Out\" (No cards can be drawn).";
					Gameover(player1);
				}
				else if(playerDrawing == player2)
				{
					gameoverReason = "\"Deck Out\" (No cards can be drawn).";
					Gameover(player2);
				}
			}
		}
	}
	
	//Used to place cards automatically to the hand
	public void PlaceCard(int amount = 1)
	{
		//Check which player is drawing...
		if(player1Drawing)
		{
			//Set the card position
			CalculateCardPositions(drawableCard, player1, (float)player1.handSize, amount, true, (float)startingHand);
			
			//Set the card rotation
			drawableCard.transform.rotation = Quaternion.Euler(0, 0, 0);
			
			player1Drawing = false;
		}
		else if(player2Drawing)
		{
			//Set the card position
			CalculateCardPositions(drawableCard, player2, (float)player2.handSize, amount, true, (float)startingHand);
			
			//Set the card rotation
			drawableCard.transform.rotation = Quaternion.Euler(0, 180, 0);
			
			player2Drawing = false;
		}
	}
	
	//Use this to fix the card distances in the hand
	public float CalculateCardPositions(GameObject card, Player player, float currentAmount, float multiplyer = 1, bool useTotalAmount = false, float totalAmont = 1)
	{
		//Temp variables
		float percentage = 0f;
		GameObject left = null;
		GameObject right = null;
		
		if(player == player1)
		{
			left = P1HandLeftSideLimit;
			right = P1HandRightSideLimit;
		}
		else if(player == player2)
		{
			left = P2HandLeftSideLimit;
			right = P2HandRightSideLimit;
		}
	
		//If we have a total amount and the user wants to determain the card placement
		if(totalAmont > 0 && useTotalAmount)
		{
			//Use dynamic code here... Not supported yet
			card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, currentAmount / totalAmont);
		}
		//Else, if 0 is for the total amount, then the total is undefined...
		else
		{
			//Hard coded values
			switch((int)currentAmount)
			{
			case 0: percentage = 0.5f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 1: percentage = 0.333f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 2: percentage =  0.25f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 3: percentage = 0.2f ; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 4: percentage = 0.166f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 5: percentage = 0.142f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 6: percentage = 0.125f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 7: percentage = 0.111f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 8: percentage = 0.1f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 9: percentage = 0.09f ; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 10: percentage = 0.083f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 11: percentage = 0.076f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 12: percentage = 0.071f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 13: percentage = 0.066f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 14: percentage = 0.062f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 15: percentage = 0.058f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 16: percentage = 0.055f ; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 17: percentage = 0.052f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 18: percentage = 0.05f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			case 19: percentage = 0.047f; card.transform.position = Vector3.Lerp(left.transform.position, right.transform.position, percentage * multiplyer); break;
			}
		}
		
		return percentage;
	}
	
	public void ShuffleDeck(Deck deck)
	{
		//Loop for the array of cards in deck
		for(int i = deck.deck.Length - 1; i > 0; i--)
		{
			//Do algorithum to randomize cards in deck
			int r = Random.Range(0, i + 1);
			Card tmp = deck.deck[i];
			deck.deck[i] = deck.deck[r];
			deck.deck[r] = tmp;
		}
		
		//Then show the result in the scene for the deck (if we officially started dueling)
		if(officiallyStarted)
		{
			//Rearrnge the cards visually
		}
	}
	
	public void ShuffleHand(Player player)
	{
		//Loop for the array of cards in hand
		for(int i = player.cardsInHand.Length - 1; i > 0; i--)
		{
			//Check if there is null...
			if(player.cardsInHand[i] == null)
			{
				//continue if there is null
				continue;
			}
		
			//Do algorithum to randomize cards in hand
			int r = Random.Range(0, i + 1);
			Card tmp = player.cardsInHand[i];
			player.cardsInHand[i] = player.cardsInHand[r];
			player.cardsInHand[r] = tmp;
			
			//Place the cards in the random order
			CalculateCardPositions(player.cardsInHand[i].gameObject, player, i, 1, true, player.handSize);
		}
	}
	
	public void CheckAllCardEffects()
	{
		//Temp variables
		Vector3 originOfRay = new Vector3(0, 0, 0);
		Vector3 direction = Vector3.down;
		
		//Check player1's hand
		for(int i = 0; i < player1.cardsInHand.Length; i++)
		{
			if(player1.cardsInHand[i] != null)
			{
				player1.cardsInHand[i].gameObject.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
			}
		}
		
		//Check player2's hand
		for(int i = 0; i < player2.cardsInHand.Length; i++)
		{
			if(player2.cardsInHand[i] != null)
			{
				player2.cardsInHand[i].gameObject.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
			}
		}
		
		//Check the field
		for(int i = 0; i < positionRays.Length; i++)
		{
			//Get the current ray position
			originOfRay = positionRays[i].transform.position;
			
			//Raycast from the top to the deck (the closest card that gets hit shall be selected)...
			if(Physics.Raycast(originOfRay, direction, out hit))
			{
				if(hit.distance < maxDistance && hit.collider.gameObject.tag == "Card")
				{
					//Detect the card and store the information (if nessesary...)
					targetableCard = hit.transform.gameObject;
					
					//Call the functions that may be used
					targetableCard.SendMessage("ActivationEffect", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		
		//Check player1's graveyard
		
		//Check player2's graveyard
		
		//Check player1's deck
		
		//Check player2's deck
	}
	
	public void BattlePositionChange()
	{
		//Why is there an extra check here? fix later...
		if(player1Turn)
		{
			player1POV = true;
		}
		else if(player2Turn)
		{
			player2POV = true;
		}
		
		//If the selected card is in face up attack mode -> face up defense mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack && goingToFaceUpDefense)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 270, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 90, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpDefense;
		}
		
		//If the selected card is in face up attack mode -> face down defense mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack && goingToFaceDownDefense)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 270, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 90, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
		}
		
		//If the selected card is in face up attack mode -> face down attack mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpAttack && goingToFaceDownAttack)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 0, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 180, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownAttack;
		}
		
		//If the selected card is in face up defense mode -> face up attack mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense && goingToFaceUpAttack)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 0, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 180, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
		}
		
		//If the selected card is in face up defense mode -> face down defense mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceUpDefense && goingToFaceDownDefense)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 270, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 90, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
		}
		
		//If the selected card is in face down defense mode -> face up attack mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense && goingToFaceUpAttack)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 0, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 180, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
		}
		
		//If the selected card is in face down defense mode -> face up defense mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownDefense && goingToFaceUpDefense)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 270, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 90, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpDefense;
		}
		
		//If the selected card is in face down attack mode -> face up attack mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack && goingToFaceUpAttack)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 180, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(90, 180, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceUpAttack;
		}
		
		//If the selected card is in face down attack mode -> face down defense mode
		if(selectedCard.GetComponent<ExtraCardProperties>().positions == ExtraCardProperties.Positions.faceDownAttack && goingToFaceDownDefense)
		{
			if(player1POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 270, 0);
			}
			else if(player2POV)
			{
				endBattleChangeRotation = Quaternion.Euler(-90, 90, 0);
			}
			
			selectedCard.GetComponent<ExtraCardProperties>().positions = ExtraCardProperties.Positions.faceDownDefense;
		}
		
		//Make the animated card move
		startBattleChangeRotation = selectedCard.transform.rotation;
		movingBattleChangeCard = true;
	}
	
	public void Discard(Player player, int amount = 1, bool randomDiscard = false)
	{
		//We are currently discarding
		discardingMode = true;
		
		//If the discarding is random...
		if(randomDiscard)
		{
			//Let the computer algorithum decide what is discarded from the hand
		}
		//Else, if the discarding is not random
		else
		{
			//Let the player choose what to discard from their hand
			tans.NotifyUserOfDiscardingCards();
			neededAmountToDiscard = amount;
		}
	}
	
	//Checks for the winning/losing condition
	public void CheckForWinCondition()
	{
		//If player 1 lost all thier life points...
		if(player1.lifePoints <= 0)
		{
			//Player 1 lost
			gameoverReason = "loss of Life Points.";
			Gameover(player1);
		}
		//Else, if player 2 lost all thier life points...
		else if(player2.lifePoints <= 0)
		{
			//Player 2 lost
			gameoverReason = "loss of Life Points.";
			Gameover(player2);
		}
	}
	
	//Do something special for this gameover
	public void Gameover(Player losingPlayer)
	{
		//No need for a crosshair anymore
		crosshair.TurnOff();
		crosshair.SearchForCursorToLock = false;
		
		//Show message saying that the "losingPlayer" lost
		print(losingPlayer.name + " has lost by " + gameoverReason);
		
		//Show the players if they want to save the replay
		
		//Then exit duel mode last
		//Application.LoadLevel("MainMenu");
		SceneManager.LoadScene("MainMenu");
	}
	
	public static Phases GetPhase()
	{
		if(phaseStatic == Phases.draw)
		{
			return Phases.draw;
		}
		else if(phaseStatic == Phases.standby)
		{
			return Phases.standby;
		}
		else if(phaseStatic == Phases.main1)
		{
			return Phases.main1;
		}
		else if(phaseStatic == Phases.battle)
		{
			return Phases.battle;
		}
		else if(phaseStatic == Phases.main2)
		{
			return Phases.main2;
		}
		else if(phaseStatic == Phases.end)
		{
			return Phases.end;
		}
		else
		{
			return Phases.draw;
		}
	}
	
	public string GetPhaseString()
	{
		if(phaseStatic == Phases.draw)
		{
			return "Draw Phase";
		}
		else if(phaseStatic == Phases.standby)
		{
			return "Standby Phase";
		}
		else if(phaseStatic == Phases.main1)
		{
			return "Main Phase 1";
		}
		else if(phaseStatic == Phases.battle)
		{
			return "Battle Phase";
		}
		else if(phaseStatic == Phases.main2)
		{
			return "Main Phase 2";
		}
		else if(phaseStatic == Phases.end)
		{
			return "End Phase";
		}
		else
		{
			return "Draw Phase";
		}
	}
	
	public static BattleSteps GetBattleStep()
	{
		if(battleStepsStatic == BattleSteps.startStep)
		{
			return BattleSteps.startStep;
		}
		else if(battleStepsStatic == BattleSteps.battleStep)
		{
			return BattleSteps.battleStep;
		}
		else if(battleStepsStatic == BattleSteps.damageStep)
		{
			return BattleSteps.damageStep;
		}
		else if(battleStepsStatic == BattleSteps.endStep)
		{
			return BattleSteps.endStep;
		}
		else
		{
			return BattleSteps.startStep;
		}
	}
	
	public string GetBattleStepString()
	{
		if(battleStepsStatic == BattleSteps.startStep)
		{
			return "Start Step";
		}
		else if(battleStepsStatic == BattleSteps.battleStep)
		{
			return "Battle Step";
		}
		else if(battleStepsStatic == BattleSteps.damageStep)
		{
			return "Damage Step";
		}
		else if(battleStepsStatic == BattleSteps.endStep)
		{
			return "End Step";
		}
		else
		{
			return "Start Step";
		}
	}
	
	private void TurnOnAttackNotifications()
	{
		if(MZ1P1_AttackNotification != null)
			MZ1P1_AttackNotification.SetActive(true);
		if(MZ2P1_AttackNotification != null)
			MZ2P1_AttackNotification.SetActive(true);
		if(MZ3P1_AttackNotification != null)
			MZ3P1_AttackNotification.SetActive(true);
		if(MZ4P1_AttackNotification != null)
			MZ4P1_AttackNotification.SetActive(true);
		if(MZ5P1_AttackNotification != null)
			MZ5P1_AttackNotification.SetActive(true);
		if(MZ1P2_AttackNotification != null)
			MZ1P2_AttackNotification.SetActive(true);
		if(MZ2P2_AttackNotification != null)
			MZ2P2_AttackNotification.SetActive(true);
		if(MZ3P2_AttackNotification != null)
			MZ3P2_AttackNotification.SetActive(true);
		if(MZ4P2_AttackNotification != null)
			MZ4P2_AttackNotification.SetActive(true);
		if(MZ5P2_AttackNotification != null)
			MZ5P2_AttackNotification.SetActive(true);
	}
	
	private void TurnOffAttackNotifications()
	{
		if(MZ1P1_AttackNotification != null)
			MZ1P1_AttackNotification.SetActive(false);
		if(MZ2P1_AttackNotification != null)
			MZ2P1_AttackNotification.SetActive(false);
		if(MZ3P1_AttackNotification != null)
			MZ3P1_AttackNotification.SetActive(false);
		if(MZ4P1_AttackNotification != null)
			MZ4P1_AttackNotification.SetActive(false);
		if(MZ5P1_AttackNotification != null)
			MZ5P1_AttackNotification.SetActive(false);
		if(MZ1P2_AttackNotification != null)
			MZ1P2_AttackNotification.SetActive(false);
		if(MZ2P2_AttackNotification != null)
			MZ2P2_AttackNotification.SetActive(false);
		if(MZ3P2_AttackNotification != null)
			MZ3P2_AttackNotification.SetActive(false);
		if(MZ4P2_AttackNotification != null)
			MZ4P2_AttackNotification.SetActive(false);
		if(MZ5P2_AttackNotification != null)
			MZ5P2_AttackNotification.SetActive(false);
	}
	
	private void DestroyAttackNotifications()
	{
		if(MZ1P1_AttackNotification != null)
			Destroy(MZ1P1_AttackNotification);
		if(MZ2P1_AttackNotification != null)
			Destroy(MZ2P1_AttackNotification);
		if(MZ3P1_AttackNotification != null)
			Destroy(MZ3P1_AttackNotification);
		if(MZ4P1_AttackNotification != null)
			Destroy(MZ4P1_AttackNotification);
		if(MZ5P1_AttackNotification != null)
			Destroy(MZ5P1_AttackNotification);
		if(MZ1P2_AttackNotification != null)
			Destroy(MZ1P2_AttackNotification);
		if(MZ2P2_AttackNotification != null)
			Destroy(MZ2P2_AttackNotification);
		if(MZ3P2_AttackNotification != null)
			Destroy(MZ3P2_AttackNotification);
		if(MZ4P2_AttackNotification != null)
			Destroy(MZ4P2_AttackNotification);
		if(MZ5P2_AttackNotification != null)
			Destroy(MZ5P2_AttackNotification);
	}
	
	public void ViewCard()
	{
		if(player1Turn)
		{
			viewCardObjectP1.GetComponent<Renderer>().material.SetTexture("_MainTex", selectedCard.GetComponent<Card>().GetCardFrontTexture());
			viewContentP1.GetComponent<TextMesh>().text = ResolveTextSize(GenerateCardContent(), 100);
			viewDescriptionP1.GetComponent<TextMesh>().text = ResolveTextSize(GetCardDescription(), 70);
		}
		else if(player2Turn)
		{
			viewCardObjectP2.GetComponent<Renderer>().material.SetTexture("_MainTex", selectedCard.GetComponent<Card>().GetCardFrontTexture());
			viewContentP2.GetComponent<TextMesh>().text = ResolveTextSize(GenerateCardContent(), 100);
			viewDescriptionP2.GetComponent<TextMesh>().text = ResolveTextSize(GetCardDescription(), 70);
		}
	}
	
	// Wrap text by line height
	private string ResolveTextSize(string input, int lineLength)
	{
		// Split string by char " "         
		string[] words = input.Split(" "[0]);
		
		// Prepare result
		string result = "";
		
		// Temp line string
		string line = "";
		
		// for each all words        
		foreach(string s in words)
		{
			// Append current word into line
			string temp = line + " " + s;
			
			// If line length is bigger than lineLength
			if(temp.Length > lineLength)
			{
				
				// Append current line into result
				result += line + "\n";
				// Remain word append into new line
				line = s;
			}
			// Append current word into current line
			else
			{
				line = temp;
			}
		}
		
		// Append last line into result        
		result += line;
		
		// Remove first " " char
		return result.Substring(1,result.Length-1);
	}
	
	//Sets the card information to the text box area
	public string GenerateCardContent()
	{
		//Temp variables
		string name = selectedCard.GetComponent<Card>().GetCardName();
		string cardType = selectedCard.GetComponent<Card>().GetCardType().ToString();
		string serial = selectedCard.GetComponent<Card>().GetSerial().ToString();
		string level = selectedCard.GetComponent<Card>().GetLevel().ToString();
		string rank = selectedCard.GetComponent<Card>().GetRank().ToString();
		string attrabute = selectedCard.GetComponent<Card>().GetAttrabute().ToString();
		string monsterType = selectedCard.GetComponent<Card>().GetMonsterType().ToString();
		string monsterSubType = selectedCard.GetComponent<Card>().GetMonsterSubType().ToString();
		string spellType = selectedCard.GetComponent<Card>().GetSpellType().ToString();
		string trapType = selectedCard.GetComponent<Card>().GetTrapType().ToString();
		string attack = selectedCard.GetComponent<Card>().GetAttack().ToString();
		string defense = selectedCard.GetComponent<Card>().GetDefense().ToString();
		string pendulum = selectedCard.GetComponent<Card>().isPendulum.ToString();
		string tempText = "";
		
		//Make the words more presentable
		//Card Type
		switch(cardType)
		{
		case "normalMonster": cardType = "Normal Monster"; break;
		case "effectMonster": cardType = "Effect Monster"; break;
		case "tokenMonster": cardType = "Token Monster"; break;
		case "ritualMonster": cardType = "Ritual Monster"; break;
		case "fusionMonster": cardType = "Fusion Monster"; break;
		case "synchroMonster": cardType = "Synchro Monster"; break;
		case "xyzMonster": cardType = "Xyz Monster"; break;
		}
		
		//Attrabute
		switch(attrabute)
		{
		case "light": attrabute = "Light"; break;
		case "dark": attrabute = "Dark"; break;
		case "fire": attrabute = "Fire"; break;
		case "water": attrabute = "Water"; break;
		case "wind": attrabute = "Wind"; break;
		case "earth": attrabute = "Earth"; break;
		case "spell": attrabute = "Spell"; break;
		case "trap": attrabute = "Trap"; break;
		case "divine": attrabute = "Divine"; break;
		}
		
		//Monster Type
		switch(monsterType)
		{
		case "none": monsterType = "None"; break;
		case "aqua": monsterType = "Aqua"; break;
		case "beast": monsterType = "Beast"; break;
		case "beastWarrior": monsterType = "Beast-Warrior"; break;
		case "dinosaur": monsterType = "Dinosaur"; break;
		case "divineBeast": monsterType = "Divine-Beast"; break;
		case "dragon": monsterType = "Dragon"; break;
		case "fairy": monsterType = "Fairy"; break;
		case "fiend": monsterType = "Fiend"; break;
		case "fish": monsterType = "Fish"; break;
		case "insect": monsterType = "Insect"; break;
		case "machine": monsterType = "Machine"; break;
		case "phychic": monsterType = "Phychic"; break;
		case "plant": monsterType = "Plant"; break;
		case "pyro": monsterType = "Pyro"; break;
		case "reptile": monsterType = "Reptile"; break;
		case "rock": monsterType = "Rock"; break;
		case "seaSerpent": monsterType = "Sea Serpent"; break;
		case "spellcaster": monsterType = "Spellcaster"; break;
		case "thunder": monsterType = "Thunder"; break;
		case "warrior": monsterType = "Warrior"; break;
		case "wingedBeast": monsterType = "Winged-Beast"; break;
		case "wyrm": monsterType = "Wyrm"; break;
		case "zombie": monsterType = "Zombie"; break;
		}
		
		//Monster Sub-Type
		switch(monsterSubType)
		{
		case "none": monsterSubType = "None"; break;
		case "gemini": monsterSubType = "Gemini"; break;
		case "spirit": monsterSubType = "Spirit"; break;
		case "toon": monsterSubType = "Toon"; break;
		case "tuner": monsterSubType = "Tuner"; break;
		case "union": monsterSubType = "Union"; break;
		}
		
		//Spell Type
		switch(spellType)
		{
		case "none": spellType = "None"; break;
		case "normal": spellType = "Normal"; break;
		case "quickPlay": spellType = "Quick Play"; break;
		case "equip": spellType = "Equip"; break;
		case "continuous": spellType = "Continuous"; break;
		case "field": spellType = "Field"; break;
		case "ritual": spellType = "Ritual"; break;
		}
		
		//Trap Type
		switch(trapType)
		{
		case "none": trapType = "None"; break;
		case "normal": trapType = "Normal"; break;
		case "continuous": trapType = "Continuous"; break;
		case "counter": trapType = "Counter"; break;
		}
		
		//Do further clean up to the text to remove unnessesary information
		tempText = tempText + "Name: " + name + "\n\n" +
			"Card Type: " + cardType + "\n\n" +
				"Serial Number: " + serial + "\n\n";
		
		if(level != "0")
		{
			tempText = tempText + "Level: " + level + "\n\n";
		}
		
		if(rank != "0")
		{
			tempText = tempText + "Rank: " + rank + "\n\n";
		}
		
		tempText = tempText + "Attrabute: " + attrabute + "\n\n";
		
		if(monsterType != "None")
		{
			tempText = tempText + "Monster Type: " + monsterType + "\n\n";
		}
		
		if(monsterSubType != "None")
		{
			tempText = tempText + "Monster Sub-Attrabute: " + monsterSubType + "\n\n";
		}
		
		if(spellType != "None")
		{
			tempText = tempText + "Spell Type: " + spellType + "\n\n";
		}
		
		if(trapType != "None")
		{
			tempText = tempText + "Trap Type: " + trapType + "\n\n";
		}
		
		if(spellType == "None" && trapType == "None")
		{
			tempText = tempText + "Attack: " + attack + "\n\n" +
				"Defense: " + defense + "\n\n";
		}
		
		tempText = tempText + "Pendulum: " + pendulum;
		
		//Set the corrected text into the text box area
		return tempText;
	}
	
	public string GetCardDescription()
	{
		string description = selectedCard.GetComponent<Card>().GetDescription();
			
		return "Description: " + description;
	}

	public void ResetCollisions()
	{
		//Turn off collisions (player 1)
		for(int i = 0; i < player1.cardsInHand.Length && player1.cardsInHand[i] != null; i++)
		{
			player1.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
			player1.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = false;
		}

		//Turn on collisions (player 1)
		for(int i = 0; i < player1.cardsInHand.Length && player1.cardsInHand[i] != null; i++)
		{
			player1.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true;
			player1.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = true;
		}

		//Turn off collisions (player 2)
		for(int i = 0; i < player2.cardsInHand.Length && player2.cardsInHand[i] != null; i++)
		{
			player2.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = false;
			player2.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = false;
		}

		//Turn on collisions (player 2)
		for(int i = 0; i < player2.cardsInHand.Length && player2.cardsInHand[i] != null; i++)
		{
			player2.cardsInHand[i].gameObject.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = true;
			player2.cardsInHand[i].gameObject.transform.GetChild(1).gameObject.GetComponent<MeshCollider>().enabled = true;
		}
	}
}