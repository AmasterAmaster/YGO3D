using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;						//USE: File access
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;						//USE: open dialog.
#endif

public class LoadLevelScript : MonoBehaviour
{
	//Variables for Main Menu
	public Deck currentDeck;
	public ExtraDeck currentExtraDeck;
	public SideDeck currentSideDeck;
	public Deck currentDeckAI;
	public ExtraDeck currentExtraDeckAI;
	public SideDeck currentSideDeckAI;
	private int mainDeckCount = 0;
	private int extraDeckCount = 0;
	private int sideDeckCount = 0;
	public Text deckText;
	public Text deckTextAI;
	public InputField input;
	public InputField inputAI;
	public Text notificationText;
	
	public bool mainDeckGrid = true;
	public bool extraDeckGrid = false;
	public bool sideDeckGrid = false;
	
	void Start()
	{
		//Find the deck everytime the user returns to the main menu
		if(GameObject.Find("CurrentDeck") != null)
		{
			if(GameObject.Find("CurrentDeck").GetComponent<Deck>() != null)
			{
				currentDeck = GameObject.Find("CurrentDeck").GetComponent<Deck>();
			}
			
			if(GameObject.Find("CurrentDeck").GetComponent<ExtraDeck>() != null)
			{
				currentExtraDeck = GameObject.Find("CurrentDeck").GetComponent<ExtraDeck>();
			}
			
			if(GameObject.Find("CurrentDeck").GetComponent<SideDeck>() != null)
			{
				currentSideDeck = GameObject.Find("CurrentDeck").GetComponent<SideDeck>();
			}
		}
			
		if(GameObject.Find("AICurrentDeck") != null)
		{
			if(GameObject.Find("AICurrentDeck").GetComponent<Deck>() != null)
			{
				currentDeckAI = GameObject.Find("AICurrentDeck").GetComponent<Deck>();
			}
			
			if(GameObject.Find("AICurrentDeck").GetComponent<ExtraDeck>() != null)
			{
				currentExtraDeckAI = GameObject.Find("AICurrentDeck").GetComponent<ExtraDeck>();
			}
			
			if(GameObject.Find("AICurrentDeck").GetComponent<SideDeck>() != null)
			{
				currentSideDeckAI = GameObject.Find("AICurrentDeck").GetComponent<SideDeck>();
			}
		}
	}

	public void LoadLevel(int level)
	{
		switch(level)
		{
		case 0:
			//Gallery
			//Application.LoadLevel("Gallery");
			SceneManager.LoadScene("Gallery");
			break;
		case 1:
			//Duel
			if(currentDeck.deck[0] == null)
			{
				//Deck is not selected
				notificationText.text = "Deck is not selected!";
			}
			else if(currentDeck.deck[39] == null)
			{
				//Deck is not valid
				notificationText.text = "Deck is not valid!";
			}
			else if(currentDeckAI.deck[0] == null)
			{
				//Deck is not valid
				notificationText.text = "AI Deck is not selected!";
			}
			else if(currentDeckAI.deck[39] == null)
			{
				//Deck is not valid
				notificationText.text = "AI Deck is not valid!";
			}
			else
			{
				//Go to the duel
				//Application.LoadLevel("DuelField");
				SceneManager.LoadScene("DuelField");
			}
			break;
		case 2:
			//Deck Edit
			//Application.LoadLevel("DeckEdit");
			SceneManager.LoadScene("DeckEdit");
			break;
		case 3:
			//Exit
			Application.Quit();
			break;
		case 4:
			//Deck Selection
			
			//Reset the deck count
			mainDeckCount = 0;
			extraDeckCount = 0;
			sideDeckCount = 0;
			
			//Find a deck with a bunch of serial numbers
			string filename = Application.dataPath;
			
			#if UNITY_EDITOR
			string directory = Application.dataPath;
			string extention = "txt";
			filename = EditorUtility.OpenFilePanel("Deck Selection", directory, extention);
			
			#elif UNITY_STANDALONE_OSX
			filename = Application.dataPath + "/../../deck/" + input.text + ".txt";
			#elif UNITY_STANDALONE
			filename = Application.dataPath + "/../deck/" + input.text + ".txt";
			#endif
			
			//Clear the current deck for the incoming loaded deck
			currentDeck.ResetDeckSize();
			
			ReadInputFile(filename, currentDeck, currentExtraDeck, currentSideDeck);
			
			//Show the user what the current deck is
			int pathLength = Application.dataPath.Length;
			
			#if UNITY_EDITOR
			string remainingString = filename.Substring(pathLength + 1);
			#elif UNITY_STANDALONE_OSX
			string remainingString = filename.Substring(pathLength + 12);
			#elif UNITY_STANDALONE
			string remainingString = filename.Substring(pathLength + 9);
			#endif
			
			string correctName = remainingString.Remove(remainingString.IndexOf(".txt"));
			deckText.text = "Selected: " + correctName;
			currentDeck.deckName = correctName;
			break;
		case 5:
			//Deck Selection (AI)
			
			//Reset the deck count
			mainDeckCount = 0;
			extraDeckCount = 0;
			sideDeckCount = 0;
			
			//Find a deck with a bunch of serial numbers
			string filenameAI = Application.dataPath;
			
			#if UNITY_EDITOR
			string directoryAI = Application.dataPath;
			string extentionAI = "txt";
			filenameAI = EditorUtility.OpenFilePanel("Deck Selection (AI)", directoryAI, extentionAI);
			
			#elif UNITY_STANDALONE_OSX
			filenameAI = Application.dataPath + "/../../deck/" + input.text + ".txt";
			#elif UNITY_STANDALONE
			filenameAI = Application.dataPath + "/../deck/" + input.text + ".txt";
			#endif
			
			//Clear the current deck for the incoming loaded deck
			currentDeckAI.ResetDeckSize();
			
			ReadInputFile(filenameAI, currentDeckAI, currentExtraDeckAI, currentSideDeckAI);
			
			//Show the user what the current deck is
			int pathLengthAI = Application.dataPath.Length;
			
			#if UNITY_EDITOR
			string remainingStringAI = filenameAI.Substring(pathLengthAI + 1);
			#elif UNITY_STANDALONE_OSX
			string remainingStringAI = filenameAI.Substring(pathLengthAI + 12);
			#elif UNITY_STANDALONE
			string remainingStringAI = filenameAI.Substring(pathLengthAI + 9);
			#endif
			
			string correctNameAI = remainingStringAI.Remove(remainingStringAI.IndexOf(".txt"));
			deckTextAI.text = "AI Selected: " + correctNameAI;
			currentDeckAI.deckName = correctNameAI;
			break;
		case 6:
			//Options
			//Application.LoadLevel("Options");
			SceneManager.LoadScene("Options");
			break;
		case 7:
			//Credits
			//Application.LoadLevel("Credits");
			SceneManager.LoadScene("Credits");
			break;
		case 8:
			//Main Menu
			//Application.LoadLevel("MainMenu");
			SceneManager.LoadScene("MainMenu");
			break;
		}
	}
	
	public void ReadInputFile(string filename, Deck deckRef, ExtraDeck extraDeckRef, SideDeck sideDeckRef)
	{
		//Check if there are child transforms already in this deck...
		if(deckRef.gameObject.transform.childCount > 0)
		{
			//Loop and remove the child objects
			for(int i = 0; i < deckRef.gameObject.transform.childCount; i++)
			{
				//Destroy the child objects
				Destroy(deckRef.gameObject.transform.GetChild(i).gameObject);
			}
		}
	
		//Temp variables
		string fileContents = File.ReadAllText(filename);
		string overwriteTxt = "";
		
		//Go through each number and add that card to a deck
		StringReader reader = new StringReader(fileContents);
		if(reader != null)
		{
			//Read each line from the file
			while((overwriteTxt = reader.ReadLine()) != null)
			{
				//Check for "junk text"
				if(overwriteTxt == "#created by ...")
				{
					//Continue becuase this is "junk text"
					continue;
				}
				
				//Check for main cards
				if(overwriteTxt == "#main")
				{
					//Set the cards up to be placed in the main deck
					mainDeckGrid = true;
					extraDeckGrid = false;
					sideDeckGrid = false;
					
					//Continue
					continue;
				}
				
				//Check for extra cards
				if(overwriteTxt == "#extra")
				{
					//Set the cards up to be placed in the extra deck
					mainDeckGrid = false;
					extraDeckGrid = true;
					sideDeckGrid = false;
					
					//Continue
					continue;
				}
				
				//Check for side cards
				if(overwriteTxt == "!side")
				{
					//Set the cards up to be placed in the side deck
					mainDeckGrid = false;
					extraDeckGrid = false;
					sideDeckGrid = true;
					
					//Continue
					continue;
				}
				
				//Check each line of serial numbers
				int number = int.Parse(overwriteTxt);
				
				//Create the card
				GameObject card = new GameObject(overwriteTxt);
				card.AddComponent<Card>();
				
				if(mainDeckGrid)
				{
					//If the main deck is too small while making the current deck (Beware of "Off-By-One" problem...)
					if(mainDeckCount == deckRef.deck.Length)
					{
						//Make the main deck larger by the difference of Deck Count
						deckRef.IncreaseDeckSize();
					}
					
					//Get the information from the card database and store it into the main deck
					//deckRef.deck[mainDeckCount] = CardDatabase.GetCardInfo(number, card.GetComponent<Card>());
					deckRef.deck[mainDeckCount] = CardDatabaseMonsters.GetMonsterCardInfo(number, card.GetComponent<Card>());		//Check Monster cards first
					
					if(card.GetComponent<Card>().serial == 0)
					{
						deckRef.deck[mainDeckCount] = CardDatabaseSpells.GetSpellCardInfo(number, card.GetComponent<Card>());		//Then Spells
						
						if(card.GetComponent<Card>().serial == 0)
						{
							deckRef.deck[mainDeckCount] = CardDatabaseTraps.GetTrapCardInfo(number, card.GetComponent<Card>());	//Then Traps
							
							if(card.GetComponent<Card>().serial == 0)
							{
								deckRef.deck[mainDeckCount] = CardDatabase.GetCardInfo(number, card.GetComponent<Card>());			//Then Pendulums, Tokens, Anime, etc...
							}
						}
					}
					
					//Increment the deck count
					mainDeckCount++;
				}
				
				if(extraDeckGrid)
				{
					//If the main deck is too small while making the current deck (Beware of "Off-By-One" problem...)
					if(extraDeckCount == extraDeckRef.extraDeck.Length)
					{
						//Make the main deck larger by the difference of Deck Count
						extraDeckRef.IncreaseExtraDeckSize();
					}
					
					//Get the information from the card database and store it into the main deck
					//extraDeckRef.extraDeck[extraDeckCount] = CardDatabase.GetCardInfo(number, card.GetComponent<Card>());
					extraDeckRef.extraDeck[extraDeckCount] = CardDatabaseMonsters.GetMonsterCardInfo(number, card.GetComponent<Card>());		//Check Monster cards first
					
					if(card.GetComponent<Card>().serial == 0)
					{
						extraDeckRef.extraDeck[extraDeckCount] = CardDatabaseSpells.GetSpellCardInfo(number, card.GetComponent<Card>());		//Then Spells
						
						if(card.GetComponent<Card>().serial == 0)
						{
							extraDeckRef.extraDeck[extraDeckCount] = CardDatabaseTraps.GetTrapCardInfo(number, card.GetComponent<Card>());	//Then Traps
							
							if(card.GetComponent<Card>().serial == 0)
							{
								extraDeckRef.extraDeck[extraDeckCount] = CardDatabase.GetCardInfo(number, card.GetComponent<Card>());			//Then Pendulums, Tokens, Anime, etc...
							}
						}
					}
					
					//Increment the deck count
					extraDeckCount++;
				}
				
				if(sideDeckGrid)
				{
					//If the main deck is too small while making the current deck (Beware of "Off-By-One" problem...)
					if(sideDeckCount == sideDeckRef.sideDeck.Length)
					{
						//Make the main deck larger by the difference of Deck Count
						sideDeckRef.IncreaseSideDeckSize();
					}
					
					//Get the information from the card database and store it into the main deck
					//sideDeckRef.sideDeck[sideDeckCount] = CardDatabase.GetCardInfo(number, card.GetComponent<Card>());
					sideDeckRef.sideDeck[sideDeckCount] = CardDatabaseMonsters.GetMonsterCardInfo(number, card.GetComponent<Card>());	//Check Monster cards first
					
					if(card.GetComponent<Card>().serial == 0)
					{
						sideDeckRef.sideDeck[sideDeckCount] = CardDatabaseSpells.GetSpellCardInfo(number, card.GetComponent<Card>());	//Then Spells
						
						if(card.GetComponent<Card>().serial == 0)
						{
							sideDeckRef.sideDeck[sideDeckCount] = CardDatabaseTraps.GetTrapCardInfo(number, card.GetComponent<Card>());	//Then Traps
							
							if(card.GetComponent<Card>().serial == 0)
							{
								sideDeckRef.sideDeck[sideDeckCount] = CardDatabase.GetCardInfo(number, card.GetComponent<Card>());		//Then Pendulums, Tokens, Anime, etc...
							}
						}
					}
					
					//Increment the deck count
					sideDeckCount++;
				}
				
				//Parent the card to the deck
				card.transform.parent = deckRef.transform;
			}
		}
	}
}