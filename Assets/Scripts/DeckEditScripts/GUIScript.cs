using UnityEngine;
using System.Collections;
using System.IO;						//USE: File access
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;						//USE: open dialog.
#endif

[ExecuteInEditMode]
public class GUIScript : MonoBehaviour
{
	//Variables
	public string serialSearchText = "89631139";
	public string nameSearchText = "Name Search";
	public int mainDeckWidth = 10;
	public int mainDeckHeight = 6;
	public int extraDeckWidth = 5;
	public int extraDeckHeight = 3;
	public int sideDeckWidth = 5;
	public int sideDeckHeight = 3;
	public Texture t = null;
	private int mainDeckCounter = 0;
	private int extraDeckCounter = 0;
	private int sideDeckCounter = 0;
	public Texture[] visualCardsMain = new Texture[60];
	public Texture[] visualCardsExtra = new Texture[15];
	public Texture[] visualCardsSide = new Texture[15];
	public int[] cardNumbersMain = new int[60];
	public int[] cardNumbersExtra = new int[15];
	public int[] cardNumbersSide = new int[15];
	private int mainDeckCount = 0;
	private int extraDeckCount = 0;
	private int sideDeckCount = 0;
	public string DeckName = "Default_Deck";
	
	static string[] imageFiles = new string[10000];
	//Temp variables
	string fileString = "";
	string fileString2 = "";
	
	public bool mainDeckGrid = true;
	public bool extraDeckGrid = false;
	public bool sideDeckGrid = false;
	
	void OnGUI()
	{
		//----------------Deck (Visual/numerical/searching)------------------
		serialSearchText = GUI.TextArea(new Rect(0, 0, 200, 30), serialSearchText, 12);
		nameSearchText = GUI.TextArea(new Rect(0, 30, 200, 30), nameSearchText, 50);
		
		//Search buttons
		if(GUI.Button(new Rect(200, 0, 100, 25), "Add Number"))
		{
			if((mainDeckCount <= 59 && mainDeckGrid) || (extraDeckCount <= 14 && extraDeckGrid) || (sideDeckCount <= 14 && sideDeckGrid))
			{
				//Search for the picture by number
				int number = int.Parse(serialSearchText);
				
				//Get the texture
				Texture2D tex = new Texture2D(2, 2);
				
				//Loop through all the string files
				for(int i = 0; i < imageFiles.Length; i++)
				{
					//If the image files are NOT null...
					if(imageFiles[i] != null)
					{
						//String manipulation (early)
						fileString = imageFiles[i].Substring(CardDatabase.GetFileLocation().Length + 1);
						fileString2 = fileString.Remove(fileString.IndexOf(".jpg"));
					}
					
					//If I miss the "imageFiles" for whatever reason (it became null), become robust and try filling out the array dynamically...
					if(imageFiles[i] == null)
					{
						//Fill up the array
						//If you get an ERROR here... you are not developing for the standalone platform (the asset bundels are causing something to break here with the file systems...)
						imageFiles = Directory.GetFiles(CardDatabase.GetFileLocation(), "*.jpg", SearchOption.TopDirectoryOnly);
						
						//String manipulation (late)
						fileString = imageFiles[i].Substring(CardDatabase.GetFileLocation().Length + 1);
						fileString2 = fileString.Remove(fileString.IndexOf(".jpg"));
						
						//THEN, if the serial number is equal to the image file name (without extention)...
						if(number == int.Parse(fileString2))
						{
							//Pick the one that matches the card art
							tex.LoadImage(File.ReadAllBytes(imageFiles[i]));
							break;
						}
					}
					//Else, if the serial number is equal to the image file name (without extention)...
					else if(number == int.Parse(fileString2))
					{
						//Pick the one that matches the card art
						tex.LoadImage(File.ReadAllBytes(imageFiles[i]));
						break;
					}
				}
				
				//Put the texture into the visual deck (main)
				if(mainDeckGrid)
				{
					visualCardsMain[mainDeckCount] = (Texture)tex;
					cardNumbersMain[mainDeckCount] = number;
				
					//Increment the deck count
					mainDeckCount++;
				}
				
				//Put the texture into the visual deck (extra)
				if(extraDeckGrid)
				{
					visualCardsExtra[extraDeckCount] = (Texture)tex;
					cardNumbersExtra[extraDeckCount] = number;
					
					//Increment the deck count
					extraDeckCount++;
				}
				
				//Put the texture into the visual deck (side)
				if(sideDeckGrid)
				{
					visualCardsSide[sideDeckCount] = (Texture)tex;
					cardNumbersSide[sideDeckCount] = number;
					
					//Increment the deck count
					sideDeckCount++;
				}
			}
		}
		
		if(GUI.Button(new Rect(200, 30, 100, 25), "Add Name"))
		{
			//Search for the picture by name (Not supported yet...)
		}
		
		if(GUI.Button(new Rect(300, 30, 100, 25), "Clear Deck"))
		{
			ClearDeck();
		}
		//----------------Deck (Visual/numerical/searching)------------------
		
		
		
		//-------------------File operations for decks------------------------
		DeckName = GUI.TextArea(new Rect(500, 0, 200, 30), DeckName, 50);
	
		//To save a created deck...
		if(GUI.Button(new Rect(300, 0, 100, 25), "Save Deck"))
		{
			//Get all the serial numbers into one string
			string serialNumbers = "";
			
			//Add tags to separate the main cards, extra cards and side cards
			serialNumbers = serialNumbers + "#created by ..." + "\n";
			serialNumbers = serialNumbers + "#main" + "\n";
			
			//Loop for each card (main)
			for(int i = 0; i < cardNumbersMain.Length; i++)
			{
				//If the number is 0, you can skip
				if(cardNumbersMain[i] == 0)
				{
					continue;
				}
				
				//Save it into the same string
				serialNumbers = serialNumbers + cardNumbersMain[i] + "\n";
			}
			
			//Add tags to separate the main cards, extra cards and side cards
			serialNumbers = serialNumbers + "#extra" + "\n";
			
			//Loop for each card (extra)
			for(int i = 0; i < cardNumbersExtra.Length; i++)
			{
				//If the number is 0, you can skip
				if(cardNumbersExtra[i] == 0)
				{
					continue;
				}
				
				//Save it into the same string
				serialNumbers = serialNumbers + cardNumbersExtra[i] + "\n";
			}
			
			//Add tags to separate the main cards, extra cards and side cards
			serialNumbers = serialNumbers + "!side" + "\n";
			
			//Loop for each card (side)
			for(int i = 0; i < cardNumbersSide.Length; i++)
			{
				//If the number is 0, you can skip
				if(cardNumbersSide[i] == 0)
				{
					continue;
				}
				
				//Save it into the same string
				serialNumbers = serialNumbers + cardNumbersSide[i] + "\n";
			}
			
			string directory = "";
			
			//Check OS
			#if UNITY_EDITOR
			directory = Application.dataPath + "/../deck/";
			#elif UNITY_STANDALONE_OSX
			directory = Application.dataPath + "/../../deck/";
			#elif UNITY_STANDALONE
			directory = Application.dataPath + "/../deck/";
			#endif
			
			string extention = ".txt";
			if(!Directory.Exists(directory))
			{
				//Make the directory
				Directory.CreateDirectory(directory);
			}
			
			//Make a text file with a bunch of serial numbers
			File.WriteAllText(directory + DeckName + extention, serialNumbers);
		}
		
		//To Load a created deck...
		if(GUI.Button(new Rect(400, 0, 100, 25), "Load Deck"))
		{
			//Find a deck with a bunch of serial numbers
			string filename = Application.dataPath;
			File.SetAttributes(filename, FileAttributes.Normal);
			
			#if UNITY_EDITOR
			string directory = Application.dataPath;
			string extention = "txt";
			filename = EditorUtility.OpenFilePanel("Load Deck", directory, extention);
			
			#elif UNITY_STANDALONE_OSX
			filename = Application.dataPath + "/../../deck/" + DeckName + ".txt";
			#elif UNITY_STANDALONE
			filename = Application.dataPath + "/../deck/" + DeckName + ".txt";
			
//			FileBrowser fb = new FileBrowser(filename);
//			if(fb.draw()){
//				if(fb.outputFile == null){
//					Debug.Log("Cancel hit");
//				}else{
//					Debug.Log("Ouput File = \""+fb.outputFile.ToString()+"\"");
//					/*the outputFile variable is of type FileInfo from the .NET library "http://msdn.microsoft.com/en-us/library/system.io.fileinfo.aspx"*/
//					filename = fb.outputFile.ToString();
//				}
//			}

//			string fileName = OpenFileDialogWrapper.GetFileName(Application.dataPath, "Load Deck", "Textfile(*.txt/*.ydk)", "txt\0*.ydk");
			#endif
			
			string fileContents = File.ReadAllText(filename);
			string overwriteTxt = "";
			
			//Clear the deck for the incoming loaded deck
			ClearDeck();
			
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
					
					//Parse the number
					int number = int.Parse(overwriteTxt);
					
					//Check each line of serial numbers
					Texture2D tex = new Texture2D(2, 2);
					
					//Loop through all the string files
					for(int i = 0; i < imageFiles.Length; i++)
					{
						//If the image files are NOT null...
						if(imageFiles[i] != null)
						{
							//String manipulation (early)
							fileString = imageFiles[i].Substring(CardDatabase.GetFileLocation().Length + 1);
							fileString2 = fileString.Remove(fileString.IndexOf(".jpg"));
						}
						
						//If I miss the "imageFiles" for whatever reason (it became null), become robust and try filling out the array dynamically...
						if(imageFiles[i] == null)
						{
							//Fill up the array
							imageFiles = Directory.GetFiles(CardDatabase.GetFileLocation(), "*.jpg", SearchOption.TopDirectoryOnly);
							
							//String manipulation (late)
							fileString = imageFiles[i].Substring(CardDatabase.GetFileLocation().Length + 1);
							fileString2 = fileString.Remove(fileString.IndexOf(".jpg"));
							
							//THEN, if the serial number is equal to the image file name (without extention)...
							if(number == int.Parse(fileString2))
							{
								//Pick the one that matches the card art
								tex.LoadImage(File.ReadAllBytes(imageFiles[i]));
								break;
							}
						}
						//Else, if the serial number is equal to the image file name (without extention)...
						else if(number == int.Parse(fileString2))
						{
							//Pick the one that matches the card art
							tex.LoadImage(File.ReadAllBytes(imageFiles[i]));
							break;
						}
					}
					
					if(mainDeckGrid)
					{
						visualCardsMain[mainDeckCount] = tex;
						cardNumbersMain[mainDeckCount] = number;
						
						//Increment the deck count
						mainDeckCount++;
					}
					
					if(extraDeckGrid)
					{
						visualCardsExtra[extraDeckCount] = tex;
						cardNumbersExtra[extraDeckCount] = number;
						
						//Increment the deck count
						extraDeckCount++;
					}
					
					if(sideDeckGrid)
					{
						visualCardsSide[sideDeckCount] = tex;
						cardNumbersSide[sideDeckCount] = number;
						
						//Increment the deck count
						sideDeckCount++;
					}
				}
				
				//After all the loading is done, show the main deck grid
				mainDeckGrid = true;
				extraDeckGrid = false;
				sideDeckGrid = false;
			}
		}
		//-------------------File operations for decks------------------------
		
		
		
		//--------------Grid Change----------------------
		if(GUI.Button(new Rect(700, 0, 100, 50), "Main Deck"))
		{
			mainDeckGrid = true;
			extraDeckGrid = false;
			sideDeckGrid = false;
		}
		
		if(GUI.Button(new Rect(800, 0, 100, 50), "Extra Deck"))
		{
			mainDeckGrid = false;
			extraDeckGrid = true;
			sideDeckGrid = false;
		}
		
		if(GUI.Button(new Rect(900, 0, 100, 50), "Side Deck"))
		{
			mainDeckGrid = false;
			extraDeckGrid = false;
			sideDeckGrid = true;
		}
		//--------------Grid Change----------------------
		
		
		
		//--------------Grid of cards----------------------
		Rect r = new Rect(300, 60, Screen.width - 300, Screen.height - 65);
		GUI.BeginGroup(r);

		if(mainDeckGrid)
		{
			float boxWidth = r.width / mainDeckWidth;
			float boxHeight = r.height / mainDeckHeight;
			
			for(int i = 0; i < mainDeckHeight; i++)
			{
				for(int j = 0; j < mainDeckWidth; j++)
				{
					GUI.Box(new Rect(j * (boxWidth), i * (boxHeight), boxWidth, boxHeight), visualCardsMain[mainDeckCounter]);
					//Counter
					mainDeckCounter++;
				}
			}
		}
		
		if(extraDeckGrid)
		{
			float boxWidth = r.width / sideDeckWidth;
			float boxHeight = r.height / sideDeckHeight;
			
			for(int i = 0; i < sideDeckHeight; i++)
			{
				for(int j = 0; j < sideDeckWidth; j++)
				{
					GUI.Box(new Rect(j * (boxWidth), i * (boxHeight), boxWidth, boxHeight), visualCardsExtra[extraDeckCounter]);
					//Counter
					extraDeckCounter++;
				}
			}
		}
		
		if(sideDeckGrid)
		{
			float boxWidth = r.width / sideDeckWidth;
			float boxHeight = r.height / sideDeckHeight;
			
			for(int i = 0; i < sideDeckHeight; i++)
			{
				for(int j = 0; j < sideDeckWidth; j++)
				{
					GUI.Box(new Rect(j * (boxWidth), i * (boxHeight), boxWidth, boxHeight), visualCardsSide[sideDeckCounter]);
					//Counter
					sideDeckCounter++;
				}
			}
		}
		
		//Reset counter for visual deck
		mainDeckCounter = 0;
		extraDeckCounter = 0;
		sideDeckCounter = 0;
		
		GUI.EndGroup();
		//--------------Grid of cards----------------------
		
		
		
		//--------------Other Operations----------------------
		//Mouse events
//		if(Event.current.type == EventType.Repaint && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
//		{
//			GUILayout.Label("Mouse over! " + GUILayoutUtility.GetLastRect());//toString());
//		}
//		else
//		{
//			GUILayout.Label("Mouse somewhere else");
//		}
		
		if(GUI.Button(new Rect(400, 30, 100, 25), "Main Menu"))
		{
			//Return to Main Menu
			//Application.LoadLevel("MainMenu"); //Outdated since unity version 5.3
			SceneManager.LoadScene("MainMenu");
		}
		//--------------Other Operations----------------------
	}
	
	public void ClearDeck()
	{
		//Clear the deck list
		for(int i = 0; i < visualCardsMain.Length; i++)
		{
			//Remove the card pictures
			visualCardsMain[i] = null;
			cardNumbersMain[i] = 0;
		}
		
		for(int i = 0; i < visualCardsExtra.Length; i++)
		{
			//Remove the card pictures
			visualCardsExtra[i] = null;
			cardNumbersExtra[i] = 0;
		}
		
		for(int i = 0; i < visualCardsSide.Length; i++)
		{
			//Remove the card pictures
			visualCardsSide[i] = null;
			cardNumbersSide[i] = 0;
		}
		
		//Reset the deck count
		mainDeckCount = 0;
		extraDeckCount = 0;
		sideDeckCount = 0;
	}
}