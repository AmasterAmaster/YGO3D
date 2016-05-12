using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;						//USE: File access
using UnityEngine.SceneManagement;
using System.Configuration;
using System.Threading;
using UnityEngine.Networking;

public class OptionsScript : MonoBehaviour
{
	//Carryover variables (Netowrking)
	public bool startedMultiplayerGame = false;
	public bool hostingPlayer = false;
	public bool joiningPlayer = false;
	public bool spectatingPlayer = false;
	public Deck currentDeck;
	public NetworkManager networkManager;
	public string serverIp = "127.0.0.1";
	public int serverPort = 7777;

	//Options variables (first page)
	public Toggle computerVersionToggle;
	public Toggle hololensVersionToggle;
	public Toggle hologramStageVersionToggle;
	
	public InputField lifePointsInputField;
	public InputField timeLimitInputField;
	public InputField startingHandInputField;
	public InputField cardsPerDrawInputField;
	
	public Toggle tcgOcgToggle;
	public Toggle tcgToggle;
	public Toggle ocgToggle;
	public Toggle unspecifiedToggle;
	public Toggle animeToggle;
	public Toggle turboDuelToggle;
	
	public Toggle singleDuelToggle;
	public Toggle matchToggle;
	public Toggle tagDuelToggle;
	
	public Toggle useObsoleteRulingsToggle;
	public Toggle dontCheckDeckToggle;
	public Toggle doNotShuffleDeckToggle;
	public Toggle manualDuelingToggle;
	
	public InputField AIDeckDefaultSelectionInputField;
	
	public Toggle aggresiveToggle;
	public Toggle stallToggle;
	public Toggle defensiveToggle;
	public Toggle dynamicToggle;
	public Toggle randomToggle;
	public Toggle impulseToggle;
	
	public Toggle cheatingAIToggle;
	
	public InputField nicknameInputField;
	public InputField serverInputField;
	public InputField passwordInputField;
	
	public Text overallApplicationSettingsText;
	public Text duelingOptionsText;
	public Text AIOptionsText;
	public Text multiplayerOptionsText;
	
	public Text lifePointsText;
	public Text timeLimitText;
	public Text startingHandText;
	public Text cardsPerDrawText;
	public Text duelModeText;
	public Text allowedCardsText;
	
	public Text AIDeckDefaultSelectionText;
	public Text AIBehaviorText;
	
	public Text nicknameText;
	public Text serverText;
	public Text passwordText;
	
	public Button moreOptions;
	public Button hostNow;
	public Button joinNow;
	public Button backOptionsButton;
	public Button saveButton;
	public Button exitButton;
	
	//Options variables (second page)
	public Slider brightnessSlider;
	public Slider musicSlider;
	public Slider soundEffectsSlider;
	public Slider cameraSensitivitySlider;
	
	public InputField deckExtensionInputField;
	
	public Toggle autoCardPlacementToggle;
	public Toggle randomCardPlacementToggle;
	public Toggle muteOpponentToggle;
	public Toggle muteSpectatorsToggle;
	public Toggle autoChainOrderToggle;
	public Toggle noDelayForChainToggle;
	public Toggle askToSaveReplayToggle;
	public Toggle showDebugToggle;
	public Toggle showFPSToggle;
	public Toggle showLinesToggle;
	
	public Text brightnessText;
	public Text musicText;
	public Text soundEffectsText;
	public Text cameraSensitivityText;
	public Text deckFileExtensionText;
	
	//Option value variables
	public bool computerVersion = true;
	public bool hololensVersion = false;
	public bool hologramStageVersion = false;
	
	public int lifePoints = 8000;
	public int timeLimit = 180;
	public int startingHand = 5;
	public int cardsPerDraw = 1;
	
	public bool tcgOcg = true;
	public bool tcg = false;
	public bool ocg = false;
	public bool unspecified = false;
	public bool anime = false;
	public bool turboDuel = false;
	
	public bool singleDuel = true;
	public bool match = false;
	public bool tagDuel = false;
	
	public bool useObsoleteRulings = false;
	public bool dontCheckDeck = false;
	public bool doNotShuffleDeck = false;
	public bool manualDueling = false;
	
	public string AIDeckDefaultSelection = "";
	
	public bool aggresive = false;
	public bool stall = false;
	public bool defensive = false;
	public bool dynamic = true;
	public bool random = false;
	public bool impulse = false;
	
	public bool cheatingAI = false;
	
	public string nickname = "";
	public string server = "";
	public string password = "";
	
	public float brightness = 0.5f;
	public float music = 0f;
	public float soundEffects = 0f;
	public float cameraSensitivity = 1f;
	
	public string deckExtension = ".txt";
	
	public bool autoCardPlacement = true;
	public bool randomCardPlacement = false;
	public bool muteOpponent = true;
	public bool muteSpectators = true;
	public bool autoChainOrder = true;
	public bool noDelayForChain = false;
	public bool askToSaveReplay = false;
	public bool showDebug = false;
	public bool showFPS = false;
	public bool showLines = false;

	//Other variables
	public bool doOnce = true;
	
	//Update is called once per frame
	void Update()
	{
		//If we are in options...
		if(SceneManager.GetActiveScene().buildIndex == 4)
		{
			int result;

			if(doOnce)
			{
				//Find objects in this menu
				FindAllObjects();

				//Load all the options
				LoadOptions();

				//Set all values after finding all objects
				SetAllOptionValues();

				//Fix the buttons (deleting the other OptionsManager script removed all button functionallity)
				FixButtons();
			}
			
			//Continuously set the values (to be saved later if needed or used only one time)
			computerVersion = computerVersionToggle.isOn;
			hololensVersion = hololensVersionToggle.isOn;
			hologramStageVersion = hologramStageVersionToggle.isOn;
			if(int.TryParse(lifePointsInputField.text, out result))
			{
				lifePoints = int.Parse(lifePointsInputField.text);
			}
			else
			{
				lifePoints = 8000;
			}
			if(int.TryParse(timeLimitInputField.text, out result))
			{
				timeLimit = int.Parse(timeLimitInputField.text);
			}
			else
			{
				timeLimit = 180;
			}
			if(int.TryParse(startingHandInputField.text, out result))
			{
				startingHand = int.Parse(startingHandInputField.text);
			}
			else
			{
				startingHand = 5;
			}
			if(int.TryParse(cardsPerDrawInputField.text, out result))
			{
				cardsPerDraw = int.Parse(cardsPerDrawInputField.text);
			}
			else
			{
				cardsPerDraw = 1;
			}
			tcgOcg = tcgOcgToggle.isOn;
			tcg = tcgToggle.isOn;
			ocg = ocgToggle.isOn;
			unspecified = unspecifiedToggle.isOn;
			anime = animeToggle.isOn;
			turboDuel = turboDuelToggle.isOn;
			singleDuel = singleDuelToggle.isOn;
			match = matchToggle.isOn;
			tagDuel = tagDuelToggle.isOn;
			useObsoleteRulings = useObsoleteRulingsToggle.isOn;
			dontCheckDeck = dontCheckDeckToggle.isOn;
			doNotShuffleDeck = doNotShuffleDeckToggle.isOn;
			manualDueling = manualDuelingToggle.isOn;
			AIDeckDefaultSelection = AIDeckDefaultSelectionInputField.text;
			aggresive = aggresiveToggle.isOn;
			stall = stallToggle.isOn;
			defensive = defensiveToggle.isOn;
			dynamic = dynamicToggle.isOn;
			random = randomToggle.isOn;
			impulse = impulseToggle.isOn;
			cheatingAI = cheatingAIToggle.isOn;
			nickname = nicknameInputField.text;
			server = serverInputField.text;
			password = passwordInputField.text;
			
			brightness = brightnessSlider.value;
			music = musicSlider.value;
			soundEffects = soundEffectsSlider.value;
			cameraSensitivity = cameraSensitivitySlider.value;
			deckExtension = deckExtensionInputField.text;
			autoCardPlacement = autoCardPlacementToggle.isOn;
			randomCardPlacement = randomCardPlacementToggle.isOn;
			muteOpponent = muteOpponentToggle.isOn;
			muteSpectators = muteSpectatorsToggle.isOn;
			autoChainOrder = autoChainOrderToggle.isOn;
			noDelayForChain = noDelayForChainToggle.isOn;
			askToSaveReplay = askToSaveReplayToggle.isOn;
			showDebug = showDebugToggle.isOn;
			showFPS = showFPSToggle.isOn;
			showLines = showLinesToggle.isOn;

			if(doOnce)
			{
				//Clean up for page seperation
				TurnOffUneededObjects();
				doOnce = false;
			}
		}
	}

	public void ExitOptions()
	{
		//Reset the do once action for options only
		doOnce = true;

		//Return to the main menu
		SceneManager.LoadScene("MainMenu");
	}

	public void FindAllObjects()
	{
		//Check if we are in Options
		if(SceneManager.GetActiveScene().buildIndex == 4)
		{
			//Turn everything on to find everything (fail safe - implement later)
			//TurnOnEverything();

			//Find all the nessesary components needed for options
			computerVersionToggle = GameObject.Find("ComputerVersionToggle").GetComponent<Toggle>();
			hololensVersionToggle = GameObject.Find("HololensVersionToggle").GetComponent<Toggle>();
			hologramStageVersionToggle = GameObject.Find("HologramStageVersionToggle").GetComponent<Toggle>();
			lifePointsInputField = GameObject.Find("LifePointInputField").GetComponent<InputField>();
			timeLimitInputField = GameObject.Find("TimeLimitInputField").GetComponent<InputField>();
			startingHandInputField = GameObject.Find("StartingHandInputField").GetComponent<InputField>();
			cardsPerDrawInputField = GameObject.Find("CardsPerDrawInputField").GetComponent<InputField>();
			tcgOcgToggle = GameObject.Find("TCG/OCGToggle").GetComponent<Toggle>();
			tcgToggle = GameObject.Find("TCGToggle").GetComponent<Toggle>();
			ocgToggle = GameObject.Find("OCGToggle").GetComponent<Toggle>();
			unspecifiedToggle = GameObject.Find("UnspecifiedToggle").GetComponent<Toggle>();
			animeToggle = GameObject.Find("AnimeToggle").GetComponent<Toggle>();
			turboDuelToggle = GameObject.Find("TurboDuelToggle").GetComponent<Toggle>();
			singleDuelToggle = GameObject.Find("SingleDuelToggle").GetComponent<Toggle>();
			matchToggle = GameObject.Find("MatchToggle").GetComponent<Toggle>();
			tagDuelToggle = GameObject.Find("TagToggle").GetComponent<Toggle>();
			useObsoleteRulingsToggle = GameObject.Find("UseObsoleteRulingsToggle").GetComponent<Toggle>();
			dontCheckDeckToggle = GameObject.Find("DontCheckDeckToggle").GetComponent<Toggle>();
			doNotShuffleDeckToggle = GameObject.Find("DoNotShuffleDeckToggle").GetComponent<Toggle>();
			manualDuelingToggle = GameObject.Find("ManualDuelingToggle").GetComponent<Toggle>();
			AIDeckDefaultSelectionInputField = GameObject.Find("AIDeckDefaultSelectionInputField").GetComponent<InputField>();
			aggresiveToggle = GameObject.Find("AggresiveToggle").GetComponent<Toggle>();
			stallToggle = GameObject.Find("StallToggle").GetComponent<Toggle>();
			defensiveToggle = GameObject.Find("DefensiveToggle").GetComponent<Toggle>();
			dynamicToggle = GameObject.Find("DynamicToggle").GetComponent<Toggle>();
			randomToggle = GameObject.Find("RandomToggle").GetComponent<Toggle>();
			impulseToggle = GameObject.Find("ImpulseToggle").GetComponent<Toggle>();
			cheatingAIToggle = GameObject.Find("CheatingAIToggle").GetComponent<Toggle>();
			nicknameInputField = GameObject.Find("NicknameInputField").GetComponent<InputField>();
			serverInputField = GameObject.Find("ServerInputField").GetComponent<InputField>();
			passwordInputField = GameObject.Find("PasswordInputField").GetComponent<InputField>();

			overallApplicationSettingsText = GameObject.Find("OverallApplicationSettingsText").GetComponent<Text>();
			duelingOptionsText = GameObject.Find("DuelingOptionsText").GetComponent<Text>();
			AIOptionsText = GameObject.Find("AIOptionsText").GetComponent<Text>();
			multiplayerOptionsText = GameObject.Find("MultiplayerOptionsText").GetComponent<Text>();
			lifePointsText = GameObject.Find("LifePointsText").GetComponent<Text>();
			timeLimitText = GameObject.Find("TimeLimitText").GetComponent<Text>();
			startingHandText = GameObject.Find("StartingHandText").GetComponent<Text>();
			cardsPerDrawText = GameObject.Find("CardsPerDrawText").GetComponent<Text>();
			duelModeText = GameObject.Find("DuelModeText").GetComponent<Text>();
			allowedCardsText = GameObject.Find("AllowedCardsText").GetComponent<Text>();
			AIDeckDefaultSelectionText = GameObject.Find("AIDeckDefaultSelectionText").GetComponent<Text>();
			AIBehaviorText = GameObject.Find("AIBehaviorText").GetComponent<Text>();
			nicknameText = GameObject.Find("NicknameText").GetComponent<Text>();
			serverText = GameObject.Find("ServerText").GetComponent<Text>();
			passwordText = GameObject.Find("PasswordText").GetComponent<Text>();
			moreOptions = GameObject.Find("MoreOptionsButton").GetComponent<Button>();
			hostNow = GameObject.Find("HostNowButton").GetComponent<Button>();
			joinNow = GameObject.Find("JoinNowButton").GetComponent<Button>();
			saveButton = GameObject.Find("SaveOptionsButton").GetComponent<Button>();
			exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

			backOptionsButton = GameObject.Find("BackOptionsButton").GetComponent<Button>();
			brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
			musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
			soundEffectsSlider = GameObject.Find("SoundEffectsSlider").GetComponent<Slider>();
			cameraSensitivitySlider = GameObject.Find("CameraSensitivitySlider").GetComponent<Slider>();
			deckExtensionInputField = GameObject.Find("DeckExtensionInputField").GetComponent<InputField>();
			autoCardPlacementToggle = GameObject.Find("AutoCardPlacementToggle").GetComponent<Toggle>();
			randomCardPlacementToggle = GameObject.Find("RandomCardPlacementToggle").GetComponent<Toggle>();
			muteOpponentToggle = GameObject.Find("MuteOpponentToggle").GetComponent<Toggle>();
			muteSpectatorsToggle = GameObject.Find("MuteSpectatorsToggle").GetComponent<Toggle>();
			autoChainOrderToggle = GameObject.Find("AutoChainOrderToggle").GetComponent<Toggle>();
			noDelayForChainToggle = GameObject.Find("NoDelayForChainToggle").GetComponent<Toggle>();
			askToSaveReplayToggle = GameObject.Find("AskToSaveReplayToggle").GetComponent<Toggle>();
			showDebugToggle = GameObject.Find("ShowDebugToggle").GetComponent<Toggle>();
			showFPSToggle = GameObject.Find("ShowFPSToggle").GetComponent<Toggle>();
			showLinesToggle = GameObject.Find("ShowLinesToggle").GetComponent<Toggle>();

			brightnessText = GameObject.Find("BrightnessText").GetComponent<Text>();
			musicText = GameObject.Find("MusicText").GetComponent<Text>();
			soundEffectsText = GameObject.Find("SoundEffectsText").GetComponent<Text>();
			cameraSensitivityText = GameObject.Find("CameraSensitivityText").GetComponent<Text>();
			deckFileExtensionText = GameObject.Find("DeckFileExtensionText").GetComponent<Text>();
		}
	}

	public void TurnOnEverything()
	{
		//Turn on all the first page options
		computerVersionToggle.gameObject.SetActive(true);
		hololensVersionToggle.gameObject.SetActive(true);
		hologramStageVersionToggle.gameObject.SetActive(true);
		lifePointsInputField.gameObject.SetActive(true);
		timeLimitInputField.gameObject.SetActive(true);
		startingHandInputField.gameObject.SetActive(true);
		cardsPerDrawInputField.gameObject.SetActive(true);
		tcgOcgToggle.gameObject.SetActive(true);
		tcgToggle.gameObject.SetActive(true);
		ocgToggle.gameObject.SetActive(true);
		unspecifiedToggle.gameObject.SetActive(true);
		animeToggle.gameObject.SetActive(true);
		turboDuelToggle.gameObject.SetActive(true);
		singleDuelToggle.gameObject.SetActive(true);
		matchToggle.gameObject.SetActive(true);
		tagDuelToggle.gameObject.SetActive(true);
		useObsoleteRulingsToggle.gameObject.SetActive(true);
		dontCheckDeckToggle.gameObject.SetActive(true);
		doNotShuffleDeckToggle.gameObject.SetActive(true);
		manualDuelingToggle.gameObject.SetActive(true);
		AIDeckDefaultSelectionInputField.gameObject.SetActive(true);
		aggresiveToggle.gameObject.SetActive(true);
		stallToggle.gameObject.SetActive(true);
		defensiveToggle.gameObject.SetActive(true);
		dynamicToggle.gameObject.SetActive(true);
		randomToggle.gameObject.SetActive(true);
		impulseToggle.gameObject.SetActive(true);
		cheatingAIToggle.gameObject.SetActive(true);
		nicknameInputField.gameObject.SetActive(true);
		serverInputField.gameObject.SetActive(true);
		passwordInputField.gameObject.SetActive(true);

		overallApplicationSettingsText.gameObject.SetActive(true);
		duelingOptionsText.gameObject.SetActive(true);
		AIOptionsText.gameObject.SetActive(true);
		multiplayerOptionsText.gameObject.SetActive(true);
		lifePointsText.gameObject.SetActive(true);
		timeLimitText.gameObject.SetActive(true);
		startingHandText.gameObject.SetActive(true);
		cardsPerDrawText.gameObject.SetActive(true);
		duelModeText.gameObject.SetActive(true);
		allowedCardsText.gameObject.SetActive(true);
		AIDeckDefaultSelectionText.gameObject.SetActive(true);
		AIBehaviorText.gameObject.SetActive(true);
		nicknameText.gameObject.SetActive(true);
		serverText.gameObject.SetActive(true);
		passwordText.gameObject.SetActive(true);
		passwordText.enabled = true;
		moreOptions.gameObject.SetActive(true);
		hostNow.gameObject.SetActive(true);
		saveButton.gameObject.SetActive(true);

		//Turn on all the next page options
		backOptionsButton.gameObject.SetActive(true);
		brightnessSlider.gameObject.SetActive(true);
		musicSlider.gameObject.SetActive(true);
		soundEffectsSlider.gameObject.SetActive(true);
		cameraSensitivitySlider.gameObject.SetActive(true);
		deckExtensionInputField.gameObject.SetActive(true);
		autoCardPlacementToggle.gameObject.SetActive(true);
		randomCardPlacementToggle.gameObject.SetActive(true);
		muteOpponentToggle.gameObject.SetActive(true);
		muteSpectatorsToggle.gameObject.SetActive(true);
		autoChainOrderToggle.gameObject.SetActive(true);
		noDelayForChainToggle.gameObject.SetActive(true);
		askToSaveReplayToggle.gameObject.SetActive(true);
		showDebugToggle.gameObject.SetActive(true);
		showFPSToggle.gameObject.SetActive(true);
		showLinesToggle.gameObject.SetActive(true);

		brightnessText.gameObject.SetActive(true);
		musicText.gameObject.SetActive(true);
		soundEffectsText.gameObject.SetActive(true);
		cameraSensitivityText.gameObject.SetActive(true);
		deckFileExtensionText.gameObject.SetActive(true);
	}

	public void TurnOffUneededObjects()
	{
		//Turn off unneeded options
		backOptionsButton.gameObject.SetActive(false);
		brightnessSlider.gameObject.SetActive(false);
		musicSlider.gameObject.SetActive(false);
		soundEffectsSlider.gameObject.SetActive(false);
		cameraSensitivitySlider.gameObject.SetActive(false);
		deckExtensionInputField.gameObject.SetActive(false);
		autoCardPlacementToggle.gameObject.SetActive(false);
		randomCardPlacementToggle.gameObject.SetActive(false);
		muteOpponentToggle.gameObject.SetActive(false);
		muteSpectatorsToggle.gameObject.SetActive(false);
		autoChainOrderToggle.gameObject.SetActive(false);
		noDelayForChainToggle.gameObject.SetActive(false);
		askToSaveReplayToggle.gameObject.SetActive(false);
		showDebugToggle.gameObject.SetActive(false);
		showFPSToggle.gameObject.SetActive(false);
		showLinesToggle.gameObject.SetActive(false);

		brightnessText.gameObject.SetActive(false);
		musicText.gameObject.SetActive(false);
		soundEffectsText.gameObject.SetActive(false);
		cameraSensitivityText.gameObject.SetActive(false);
		deckFileExtensionText.gameObject.SetActive(false);
	}

	public void SetAllOptionValues()
	{
		//Set the values after finding the option compoents (with default values or from file)
		computerVersionToggle.isOn = computerVersion;
		hololensVersionToggle.isOn = hololensVersion;
		hologramStageVersionToggle.isOn = hologramStageVersion;
		lifePointsInputField.text = "" + lifePoints;
		timeLimitInputField.text = "" + timeLimit;
		startingHandInputField.text = "" + startingHand;
		cardsPerDrawInputField.text = "" + cardsPerDraw;
		tcgOcgToggle.isOn = tcgOcg;
		tcgToggle.isOn = tcg;
		ocgToggle.isOn = ocg;
		unspecifiedToggle.isOn = unspecified;
		animeToggle.isOn = anime;
		turboDuelToggle.isOn = turboDuel;
		singleDuelToggle.isOn = singleDuel;
		matchToggle.isOn = match;
		tagDuelToggle.isOn = tagDuel;
		useObsoleteRulingsToggle.isOn = useObsoleteRulings;
		dontCheckDeckToggle.isOn = dontCheckDeck;
		doNotShuffleDeckToggle.isOn = doNotShuffleDeck;
		manualDuelingToggle.isOn = manualDueling;
		AIDeckDefaultSelectionInputField.text = AIDeckDefaultSelection;
		aggresiveToggle.isOn = aggresive;
		stallToggle.isOn = stall;
		defensiveToggle.isOn = defensive;
		dynamicToggle.isOn = dynamic;
		randomToggle.isOn = random;
		impulseToggle.isOn = impulse;
		cheatingAIToggle.isOn = cheatingAI;
		nicknameInputField.text = nickname;
		serverInputField.text = server;
		passwordInputField.text = password;

		brightnessSlider.value = brightness;
		musicSlider.value = music;
		soundEffectsSlider.value = soundEffects;
		cameraSensitivitySlider.value = cameraSensitivity;
		deckExtensionInputField.text = deckExtension;
		autoCardPlacementToggle.isOn = autoCardPlacement;
		randomCardPlacementToggle.isOn = randomCardPlacement;
		muteOpponentToggle.isOn = muteOpponent;
		muteSpectatorsToggle.isOn = muteSpectators;
		autoChainOrderToggle.isOn = autoChainOrder;
		noDelayForChainToggle.isOn = noDelayForChain;
		askToSaveReplayToggle.isOn = askToSaveReplay;
		showDebugToggle.isOn = showDebug;
		showFPSToggle.isOn = showFPS;
		showLinesToggle.isOn = showLines;
	}

	public void FixButtons()
	{
		moreOptions.onClick.AddListener(delegate {NextPage();});
		backOptionsButton.onClick.AddListener(delegate {PreviousPage();});
		saveButton.onClick.AddListener(delegate {SaveOptions();});
		hostNow.onClick.AddListener(delegate {HostGame();});
		joinNow.onClick.AddListener(delegate {JoinGame();});
		exitButton.onClick.AddListener(delegate {ExitOptions();});
	}

	public void NextPage()
	{
		//Turn off all the current page options
		computerVersionToggle.gameObject.SetActive(false);
		hololensVersionToggle.gameObject.SetActive(false);
		hologramStageVersionToggle.gameObject.SetActive(false);
		lifePointsInputField.gameObject.SetActive(false);
		timeLimitInputField.gameObject.SetActive(false);
		startingHandInputField.gameObject.SetActive(false);
		cardsPerDrawInputField.gameObject.SetActive(false);
		tcgOcgToggle.gameObject.SetActive(false);
		tcgToggle.gameObject.SetActive(false);
		ocgToggle.gameObject.SetActive(false);
		unspecifiedToggle.gameObject.SetActive(false);
		animeToggle.gameObject.SetActive(false);
		turboDuelToggle.gameObject.SetActive(false);
		singleDuelToggle.gameObject.SetActive(false);
		matchToggle.gameObject.SetActive(false);
		tagDuelToggle.gameObject.SetActive(false);
		useObsoleteRulingsToggle.gameObject.SetActive(false);
		dontCheckDeckToggle.gameObject.SetActive(false);
		doNotShuffleDeckToggle.gameObject.SetActive(false);
		manualDuelingToggle.gameObject.SetActive(false);
		AIDeckDefaultSelectionInputField.gameObject.SetActive(false);
		aggresiveToggle.gameObject.SetActive(false);
		stallToggle.gameObject.SetActive(false);
		defensiveToggle.gameObject.SetActive(false);
		dynamicToggle.gameObject.SetActive(false);
		randomToggle.gameObject.SetActive(false);
		impulseToggle.gameObject.SetActive(false);
		cheatingAIToggle.gameObject.SetActive(false);
		nicknameInputField.gameObject.SetActive(false);
		serverInputField.gameObject.SetActive(false);
		passwordInputField.gameObject.SetActive(false);
		passwordText.gameObject.SetActive(false);
		
		overallApplicationSettingsText.gameObject.SetActive(false);
		duelingOptionsText.gameObject.SetActive(false);
		AIOptionsText.gameObject.SetActive(false);
		multiplayerOptionsText.gameObject.SetActive(false);
		lifePointsText.gameObject.SetActive(false);
		timeLimitText.gameObject.SetActive(false);
		startingHandText.gameObject.SetActive(false);
		cardsPerDrawText.gameObject.SetActive(false);
		duelModeText.gameObject.SetActive(false);
		allowedCardsText.gameObject.SetActive(false);
		AIDeckDefaultSelectionText.gameObject.SetActive(false);
		AIBehaviorText.gameObject.SetActive(false);
		nicknameText.gameObject.SetActive(false);
		serverText.gameObject.SetActive(false);
		passwordText.gameObject.SetActive(false);
		moreOptions.gameObject.SetActive(false);
		hostNow.gameObject.SetActive(false);
		joinNow.gameObject.SetActive(false);
		
		//Turn on all the next page options
		backOptionsButton.gameObject.SetActive(true);
		brightnessSlider.gameObject.SetActive(true);
		musicSlider.gameObject.SetActive(true);
		soundEffectsSlider.gameObject.SetActive(true);
		cameraSensitivitySlider.gameObject.SetActive(true);
		deckExtensionInputField.gameObject.SetActive(true);
		autoCardPlacementToggle.gameObject.SetActive(true);
		randomCardPlacementToggle.gameObject.SetActive(true);
		muteOpponentToggle.gameObject.SetActive(true);
		muteSpectatorsToggle.gameObject.SetActive(true);
		autoChainOrderToggle.gameObject.SetActive(true);
		noDelayForChainToggle.gameObject.SetActive(true);
		askToSaveReplayToggle.gameObject.SetActive(true);
		showDebugToggle.gameObject.SetActive(true);
		showFPSToggle.gameObject.SetActive(true);
		showLinesToggle.gameObject.SetActive(true);
		
		brightnessText.gameObject.SetActive(true);
		musicText.gameObject.SetActive(true);
		soundEffectsText.gameObject.SetActive(true);
		cameraSensitivityText.gameObject.SetActive(true);
		deckFileExtensionText.gameObject.SetActive(true);
	}
	
	public void PreviousPage()
	{
		//Turn off all the current page options
		backOptionsButton.gameObject.SetActive(false);
		brightnessSlider.gameObject.SetActive(false);
		musicSlider.gameObject.SetActive(false);
		soundEffectsSlider.gameObject.SetActive(false);
		cameraSensitivitySlider.gameObject.SetActive(false);
		deckExtensionInputField.gameObject.SetActive(false);
		autoCardPlacementToggle.gameObject.SetActive(false);
		randomCardPlacementToggle.gameObject.SetActive(false);
		muteOpponentToggle.gameObject.SetActive(false);
		muteSpectatorsToggle.gameObject.SetActive(false);
		autoChainOrderToggle.gameObject.SetActive(false);
		noDelayForChainToggle.gameObject.SetActive(false);
		askToSaveReplayToggle.gameObject.SetActive(false);
		showDebugToggle.gameObject.SetActive(false);
		showFPSToggle.gameObject.SetActive(false);
		showLinesToggle.gameObject.SetActive(false);
		
		brightnessText.gameObject.SetActive(false);
		musicText.gameObject.SetActive(false);
		soundEffectsText.gameObject.SetActive(false);
		cameraSensitivityText.gameObject.SetActive(false);
		deckFileExtensionText.gameObject.SetActive(false);
		
		//Turn on all the last page options
		computerVersionToggle.gameObject.SetActive(true);
		hololensVersionToggle.gameObject.SetActive(true);
		hologramStageVersionToggle.gameObject.SetActive(true);
		lifePointsInputField.gameObject.SetActive(true);
		timeLimitInputField.gameObject.SetActive(true);
		startingHandInputField.gameObject.SetActive(true);
		cardsPerDrawInputField.gameObject.SetActive(true);
		tcgOcgToggle.gameObject.SetActive(true);
		tcgToggle.gameObject.SetActive(true);
		ocgToggle.gameObject.SetActive(true);
		unspecifiedToggle.gameObject.SetActive(true);
		animeToggle.gameObject.SetActive(true);
		turboDuelToggle.gameObject.SetActive(true);
		singleDuelToggle.gameObject.SetActive(true);
		matchToggle.gameObject.SetActive(true);
		tagDuelToggle.gameObject.SetActive(true);
		useObsoleteRulingsToggle.gameObject.SetActive(true);
		dontCheckDeckToggle.gameObject.SetActive(true);
		doNotShuffleDeckToggle.gameObject.SetActive(true);
		manualDuelingToggle.gameObject.SetActive(true);
		AIDeckDefaultSelectionInputField.gameObject.SetActive(true);
		aggresiveToggle.gameObject.SetActive(true);
		stallToggle.gameObject.SetActive(true);
		defensiveToggle.gameObject.SetActive(true);
		dynamicToggle.gameObject.SetActive(true);
		randomToggle.gameObject.SetActive(true);
		impulseToggle.gameObject.SetActive(true);
		cheatingAIToggle.gameObject.SetActive(true);
		nicknameInputField.gameObject.SetActive(true);
		serverInputField.gameObject.SetActive(true);
		passwordInputField.gameObject.SetActive(true);
		
		overallApplicationSettingsText.gameObject.SetActive(true);
		duelingOptionsText.gameObject.SetActive(true);
		AIOptionsText.gameObject.SetActive(true);
		multiplayerOptionsText.gameObject.SetActive(true);
		lifePointsText.gameObject.SetActive(true);
		timeLimitText.gameObject.SetActive(true);
		startingHandText.gameObject.SetActive(true);
		cardsPerDrawText.gameObject.SetActive(true);
		duelModeText.gameObject.SetActive(true);
		allowedCardsText.gameObject.SetActive(true);
		AIDeckDefaultSelectionText.gameObject.SetActive(true);
		AIBehaviorText.gameObject.SetActive(true);
		nicknameText.gameObject.SetActive(true);
		serverText.gameObject.SetActive(true);
		passwordText.gameObject.SetActive(true);
		moreOptions.gameObject.SetActive(true);
		hostNow.gameObject.SetActive(true);
		joinNow.gameObject.SetActive(true);
	}
	
	public void SaveOptions()
	{
		//Save the options into a file for further use (example, starting the application again with the same settings)
		string optionStrings = "";
		
		//Get all the information of the options
		optionStrings = optionStrings + "Computer Version: " + computerVersion + "\n" +
						"Hololens Version: " + hololensVersion + "\n" +
						"Hologram Stage Version: " + hologramStageVersion + "\n" +
						"Life Points: " + lifePoints + "\n" +
						"Time Limit: " + timeLimit + "\n" +
						"Starting Hand: " + startingHand + "\n" +
						"Cards Per Draw: " + cardsPerDraw + "\n" +
						"TCG/OCG: " + tcgOcg + "\n" +
						"TCG: " + tcg + "\n" +
						"OCG: " + ocg + "\n" +
						"Unspecified: " + unspecified + "\n" +
						"Anime: " + anime + "\n" +
						"Turbo Duel: " + turboDuel + "\n" +
						"Single Duel: " + singleDuel + "\n" +
						"Match: " + match + "\n" +
						"Tag Duel: " + tagDuel + "\n" +
						"Use Obsolete Rulings: " + useObsoleteRulings + "\n" +
						"Dont Check Deck: " + dontCheckDeck + "\n" +
						"Do Not Shuffle Deck: " + doNotShuffleDeck + "\n" +
						"Manual Dueling: " + manualDueling + "\n" +
						"AIDeckDefaultSelection: " + AIDeckDefaultSelection + "\n" +
						"Aggresive: " + aggresive + "\n" +
						"Stall: " + stall + "\n" +
						"Defensive: " + defensive + "\n" +
						"Dynamic: " + dynamic + "\n" +
						"Random: " + random + "\n" +
						"Impulse: " + impulse + "\n" +
						"Cheating AI: " + cheatingAI + "\n" +
						"Nickname: " + nickname + "\n" +
						"Server: " + server + "\n" +
						"Password: " + password + "\n\n" +
						
						"Brightness: " + brightness + "\n" +
						"Music: " + music + "\n" +
						"Sound Effects: " + soundEffects + "\n" +
						"Camera Sensitivity: " + cameraSensitivity + "\n" +
						"Deck Extension: " + deckExtension + "\n" +
						"Auto Card Placement: " + autoCardPlacement + "\n" +
						"Random Card Placement: " + randomCardPlacement + "\n" +
						"Mute Opponent: " + muteOpponent + "\n" +
						"Mute Spectators: " + muteSpectators + "\n" +
						"Auto Chain Order: " + autoChainOrder + "\n" +
						"No Delay For Chain: " + noDelayForChain + "\n" +
						"Ask To Save Replay: " + askToSaveReplay + "\n" +
						"Show Debug: " + showDebug + "\n" +
						"Show FPS: " + showFPS + "\n" +
						"Show Lines: " + showLines + "\n";
						
		//Save it to a place within the datapath
		string directory = "";
		
		//Check OS
		#if UNITY_EDITOR
		directory = Application.dataPath + "\\";
		#elif UNITY_STANDALONE_OSX
		directory = Application.dataPath + "/";
		#elif UNITY_STANDALONE
		directory = Application.dataPath + "/";
		#endif
		
		string extention = ".txt";
		if(!Directory.Exists(directory))
		{
			//Make the directory
			Directory.CreateDirectory(directory);
		}
		
		//Make a text file with a bunch of serial numbers
		File.WriteAllText(directory + "__Options__" + extention, optionStrings);
	}
	
	public void LoadOptions()
	{
		//Search every part of this file
		string filename = Application.dataPath;
		
		#if UNITY_EDITOR
		filename = Application.dataPath + "\\__Options__" + ".txt";
		#elif UNITY_STANDALONE_OSX
		filename = Application.dataPath + "/__Options__" + ".txt";
		#elif UNITY_STANDALONE
		filename = Application.dataPath + "\\__Options__" + ".txt";
		#endif
		
		//If the options file exists...
		if(File.Exists(filename))
		{
			string fileContents = File.ReadAllText(filename);
			string overwriteTxt = "";
			string testTxt = "";
			
			StringReader reader = new StringReader(fileContents);
			if(reader != null)
			{
				//Read each line from the file
				while((overwriteTxt = reader.ReadLine()) != null)
				{
					//Check for "junk text"
					if(overwriteTxt.Contains("Computer Version: "))
					{
						testTxt = overwriteTxt.Substring(18);
						if(testTxt == "True")
						{
							computerVersion = true;
						}
						else if(testTxt == "False")
						{
							computerVersion = false;
						}
					}
					else if(overwriteTxt.Contains("Hololens Version: "))
					{
						testTxt = overwriteTxt.Substring(18);
						if(testTxt == "True")
						{
							hololensVersion = true;
						}
						else if(testTxt == "False")
						{
							hololensVersion = false;
						}
					}
					else if(overwriteTxt.Contains("Hologram Stage Version: "))
					{
						testTxt = overwriteTxt.Substring(24);
						if(testTxt == "True")
						{
							hologramStageVersion = true;
						}
						else if(testTxt == "False")
						{
							hologramStageVersion = false;
						}
					}
					else if(overwriteTxt.Contains("Life Points: "))
					{
						lifePoints = int.Parse(overwriteTxt.Substring(13));
					}
					else if(overwriteTxt.Contains("Time Limit: "))
					{
						timeLimit = int.Parse(overwriteTxt.Substring(12));
					}
					else if(overwriteTxt.Contains("Starting Hand: "))
					{
						startingHand = int.Parse(overwriteTxt.Substring(15));
					}
					else if(overwriteTxt.Contains("Cards Per Draw: "))
					{
						cardsPerDraw = int.Parse(overwriteTxt.Substring(16));
					}
					else if(overwriteTxt.Contains("TCG/OCG: "))
					{
						testTxt = overwriteTxt.Substring(9);
						if(testTxt == "True")
						{
							tcgOcg = true;
						}
						else if(testTxt == "False")
						{
							tcgOcg = false;
						}
					}
					else if(overwriteTxt.Contains("TCG: "))
					{
						testTxt = overwriteTxt.Substring(5);
						if(testTxt == "True")
						{
							tcg = true;
						}
						else if(testTxt == "False")
						{
							tcg = false;
						}
					}
					else if(overwriteTxt.Contains("OCG: "))
					{
						testTxt = overwriteTxt.Substring(5);
						if(testTxt == "True")
						{
							ocg = true;
						}
						else if(testTxt == "False")
						{
							ocg = false;
						}
					}
					else if(overwriteTxt.Contains("Unspecified: "))
					{
						testTxt = overwriteTxt.Substring(13);
						if(testTxt == "True")
						{
							unspecified = true;
						}
						else if(testTxt == "False")
						{
							unspecified = false;
						}
					}
					else if(overwriteTxt.Contains("Anime: "))
					{
						testTxt = overwriteTxt.Substring(7);
						if(testTxt == "True")
						{
							anime = true;
						}
						else if(testTxt == "False")
						{
							anime = false;
						}
					}
					else if(overwriteTxt.Contains("Turbo Duel: "))
					{
						testTxt = overwriteTxt.Substring(12);
						if(testTxt == "True")
						{
							turboDuel = true;
						}
						else if(testTxt == "False")
						{
							turboDuel = false;
						}
					}
					else if(overwriteTxt.Contains("Single Duel: "))
					{
						testTxt = overwriteTxt.Substring(13);
						if(testTxt == "True")
						{
							singleDuel = true;
						}
						else if(testTxt == "False")
						{
							singleDuel = false;
						}
					}
					else if(overwriteTxt.Contains("Match: "))
					{
						testTxt = overwriteTxt.Substring(7);
						if(testTxt == "True")
						{
							match = true;
						}
						else if(testTxt == "False")
						{
							match = false;
						}
					}
					else if(overwriteTxt.Contains("Tag Duel: "))
					{
						testTxt = overwriteTxt.Substring(10);
						if(testTxt == "True")
						{
							tagDuel = true;
						}
						else if(testTxt == "False")
						{
							tagDuel = false;
						}
					}
					else if(overwriteTxt.Contains("Use Obsolete Rulings: "))
					{
						testTxt = overwriteTxt.Substring(22);
						if(testTxt == "True")
						{
							useObsoleteRulings = true;
						}
						else if(testTxt == "False")
						{
							useObsoleteRulings = false;
						}
					}
					else if(overwriteTxt.Contains("Dont Check Deck: "))
					{
						testTxt = overwriteTxt.Substring(17);
						if(testTxt == "True")
						{
							dontCheckDeck = true;
						}
						else if(testTxt == "False")
						{
							dontCheckDeck = false;
						}
					}
					else if(overwriteTxt.Contains("Do Not Shuffle Deck: "))
					{
						testTxt = overwriteTxt.Substring(21);
						if(testTxt == "True")
						{
							doNotShuffleDeck = true;
						}
						else if(testTxt == "False")
						{
							doNotShuffleDeck = false;
						}
					}
					else if(overwriteTxt.Contains("Manual Dueling: "))
					{
						testTxt = overwriteTxt.Substring(16);
						if(testTxt == "True")
						{
							manualDueling = true;
						}
						else if(testTxt == "False")
						{
							manualDueling = false;
						}
					}
					else if(overwriteTxt.Contains("AIDeckDefaultSelection: "))
					{
						AIDeckDefaultSelection = overwriteTxt.Substring(24);
					}
					else if(overwriteTxt.Contains("Aggresive: "))
					{
						testTxt = overwriteTxt.Substring(11);
						if(testTxt == "True")
						{
							aggresive = true;
						}
						else if(testTxt == "False")
						{
							aggresive = false;
						}
					}
					else if(overwriteTxt.Contains("Stall: "))
					{
						testTxt = overwriteTxt.Substring(7);
						if(testTxt == "True")
						{
							stall = true;
						}
						else if(testTxt == "False")
						{
							stall = false;
						}
					}
					else if(overwriteTxt.Contains("Defensive: "))
					{
						testTxt = overwriteTxt.Substring(11);
						if(testTxt == "True")
						{
							defensive = true;
						}
						else if(testTxt == "False")
						{
							defensive = false;
						}
					}
					else if(overwriteTxt.Contains("Dynamic: "))
					{
						testTxt = overwriteTxt.Substring(9);
						if(testTxt == "True")
						{
							dynamic = true;
						}
						else if(testTxt == "False")
						{
							dynamic = false;
						}
					}
					else if(overwriteTxt.Contains("Random: "))
					{
						testTxt = overwriteTxt.Substring(8);
						if(testTxt == "True")
						{
							random = true;
						}
						else if(testTxt == "False")
						{
							random = false;
						}
					}
					else if(overwriteTxt.Contains("Impulse: "))
					{
						testTxt = overwriteTxt.Substring(9);
						if(testTxt == "True")
						{
							impulse = true;
						}
						else if(testTxt == "False")
						{
							impulse = false;
						}
					}
					else if(overwriteTxt.Contains("Cheating AI: "))
					{
						testTxt = overwriteTxt.Substring(13);
						if(testTxt == "True")
						{
							cheatingAI = true;
						}
						else if(testTxt == "False")
						{
							cheatingAI = false;
						}
					}
					else if(overwriteTxt.Contains("Nickname: "))
					{
						nickname = overwriteTxt.Substring(10);
					}
					else if(overwriteTxt.Contains("Server: "))
					{
						server = overwriteTxt.Substring(8);
					}
					else if(overwriteTxt.Contains("Password: "))
					{
						password = overwriteTxt.Substring(10);
					}
					else if(overwriteTxt.Contains("Brightness: "))
					{
						brightness = float.Parse(overwriteTxt.Substring(12));
					}
					else if(overwriteTxt.Contains("Music: "))
					{
						music = float.Parse(overwriteTxt.Substring(7));
					}
					else if(overwriteTxt.Contains("Sound Effects: "))
					{
						soundEffects = float.Parse(overwriteTxt.Substring(15));
					}
					else if(overwriteTxt.Contains("Camera Sensitivity: "))
					{
						cameraSensitivity = float.Parse(overwriteTxt.Substring(20));
					}
					else if(overwriteTxt.Contains("Deck Extension: "))
					{
						deckExtension = overwriteTxt.Substring(16);
					}
					else if(overwriteTxt.Contains("Auto Card Placement: "))
					{
						testTxt = overwriteTxt.Substring(21);
						if(testTxt == "True")
						{
							autoCardPlacement = true;
						}
						else if(testTxt == "False")
						{
							autoCardPlacement = false;
						}
					}
					else if(overwriteTxt.Contains("Random Card Placement: "))
					{
						testTxt = overwriteTxt.Substring(23);
						if(testTxt == "True")
						{
							randomCardPlacement = true;
						}
						else if(testTxt == "False")
						{
							randomCardPlacement = false;
						}
					}
					else if(overwriteTxt.Contains("Mute Opponent: "))
					{
						testTxt = overwriteTxt.Substring(15);
						if(testTxt == "True")
						{
							muteOpponent = true;
						}
						else if(testTxt == "False")
						{
							muteOpponent = false;
						}
					}
					else if(overwriteTxt.Contains("Mute Spectators: "))
					{
						testTxt = overwriteTxt.Substring(17);
						if(testTxt == "True")
						{
							muteSpectators = true;
						}
						else if(testTxt == "False")
						{
							muteSpectators = false;
						}
					}
					else if(overwriteTxt.Contains("Auto Chain Order: "))
					{
						testTxt = overwriteTxt.Substring(18);
						if(testTxt == "True")
						{
							autoChainOrder = true;
						}
						else if(testTxt == "False")
						{
							autoChainOrder = false;
						}
					}
					else if(overwriteTxt.Contains("No Delay For Chain: "))
					{
						testTxt = overwriteTxt.Substring(20);
						if(testTxt == "True")
						{
							noDelayForChain = true;
						}
						else if(testTxt == "False")
						{
							noDelayForChain = false;
						}
					}
					else if(overwriteTxt.Contains("Ask To Save Replay: "))
					{
						testTxt = overwriteTxt.Substring(20);
						if(testTxt == "True")
						{
							askToSaveReplay = true;
						}
						else if(testTxt == "False")
						{
							askToSaveReplay = false;
						}
					}
					else if(overwriteTxt.Contains("Show Debug: "))
					{
						testTxt = overwriteTxt.Substring(12);
						if(testTxt == "True")
						{
							showDebug = true;
						}
						else if(testTxt == "False")
						{
							showDebug = false;
						}
					}
					else if(overwriteTxt.Contains("Show FPS: "))
					{
						testTxt = overwriteTxt.Substring(10);
						if(testTxt == "True")
						{
							showFPS = true;
						}
						else if(testTxt == "False")
						{
							showFPS = false;
						}
					}
					else if(overwriteTxt.Contains("Show Lines: "))
					{
						testTxt = overwriteTxt.Substring(12);
						if(testTxt == "True")
						{
							showLines = true;
						}
						else if(testTxt == "False")
						{
							showLines = false;
						}
					}
				}
			}
		}
	}

	public void HostGame()
	{
		//Get the network manager
		networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

		//Check if the player has a valid deck selected
		currentDeck = GameObject.Find("CurrentDeck").GetComponent<Deck>();

		if(currentDeck.deck[0] == null)
		{
			//Deck is not selected
//			notificationText.text = "Deck is not selected!";
			return;
		}
		else if(currentDeck.deck[39] == null)
		{
			//Deck is not valid
//			notificationText.text = "Deck is not valid!";
			return;
		}

		//Let the GameManager know that this is a multiplayer game (and setup the game accordingly)
		startedMultiplayerGame = true;
		hostingPlayer = true;

		//Create an internet/LAN connection to this client (make this client a server)
		networkManager.StartHost();

		//Start the game
		SceneManager.LoadScene("DuelField");
	}

	public void JoinGame()
	{
		//Get the network manager
		networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

		//Check if the player has a valid deck selected
		currentDeck = GameObject.Find("CurrentDeck").GetComponent<Deck>();

		if(currentDeck.deck[0] == null)
		{
			//Deck is not selected
//			notificationText.text = "Deck is not selected!";
			return;
		}
		else if(currentDeck.deck[39] == null)
		{
			//Deck is not valid
//			notificationText.text = "Deck is not valid!";
			return;
		}

		//Let the GameManager know that this is a multiplayer game (and setup the game accordingly)
		startedMultiplayerGame = true;
		joiningPlayer = true;

		//Create an internet/LAN connection from this client to a server (join a server)
		networkManager.StartClient();

		//Join a game
		SceneManager.LoadScene("DuelField");

		//If the game is already started and both player spots filled...
			//Enter Spectator Mode
			//spectatingPlayer = true;
	}
}