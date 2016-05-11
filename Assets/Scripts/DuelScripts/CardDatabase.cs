using UnityEngine;
using System.Collections;
using System.IO;

//Holds all the information for each card, determained by the serial number
public class CardDatabase : MonoBehaviour
{
	//Card database variable
	#if UNITY_EDITOR
		static string filesLocation = "C:\\Program Files (x86)\\YGOPro\\pics";
		static string modelsLocation = Application.dataPath + "/../models";
	#elif UNITY_STANDALONE_OSX
		static string filesLocation = Application.dataPath + "/../../pics";
		static string modelsLocation = Application.dataPath + "/../../models";
	#elif UNITY_STANDALONE
		static string filesLocation = Application.dataPath + "/../pics";
		static string modelsLocation = Application.dataPath + "/../models";
	#endif
	
	static AssetBundle assetBundle = null;
	static AssetBundle assetBundle2 = null;
	static string assetBundleName = "modelsbundle";
	//static string assetBundleMonsters = "monstersbundle";
	//static string assetBundleSpells = "spellsbundle";
	//static string assetBundleTraps = "trapsbundle";
	static string assetBundleOther = "otherbundle";
	static bool assetsLoaded = false;
	
	static string[] imageFiles = new string[10000];
	
	public Texture cardBackTexture;
	
	public static string GetFileLocation()
	{
		return filesLocation;
	}

	//Gets the card information (name, level, rank, Attribute, type, sub-types, attack, defense)
	public static Card GetCardInfo(int serialNumber, Card card)
	{
		//Temp card
		Card c = card;
		
		//Temp variables
		string fileString = "";
		string fileString2 = "";
		
		//Find the card face
		Texture2D tex = new Texture2D(2, 2);
		
		//Loop through all the string files
		for(int i = 0; i < imageFiles.Length; i++)
		{
			//If the image files are NOT null...
			if(imageFiles[i] != null)
			{
				//String manipulation (early)
				fileString = imageFiles[i].Substring(filesLocation.Length + 1);
				fileString2 = fileString.Remove(fileString.IndexOf(".jpg"));
			}
		
			//If I miss the "imageFiles" for whatever reason (it became null), become robust and try filling out the array dynamically...
			if(imageFiles[i] == null)
			{
				//Fill up the array
				imageFiles = Directory.GetFiles(filesLocation, "*.jpg", SearchOption.TopDirectoryOnly);
				
				//String manipulation (late)
				fileString = imageFiles[i].Substring(filesLocation.Length + 1);
				fileString2 = fileString.Remove(fileString.IndexOf(".jpg"));
				
				//THEN, if the serial number is equal to the image file name (without extention)...
				if(serialNumber == int.Parse(fileString2))
				{
					//Pick the one that matches the card art
					tex.LoadImage(File.ReadAllBytes(imageFiles[i]));
					break;
				}
			}
			//Else, if the serial number is equal to the image file name (without extention)...
			else if(serialNumber == int.Parse(fileString2))
			{
				//Pick the one that matches the card art
				tex.LoadImage(File.ReadAllBytes(imageFiles[i]));
				break;
			}
		}
		
		c.SetCardFrontTexture((Texture)tex);
		c.SetCardBackTexture((Texture)Resources.Load("cover", typeof(Texture)));
		
		//Find the coorisponding model for the card
		//c.SetCardModel((GameObject)Resources.Load("Models/" + serialNumber, typeof(GameObject)));
		
		if(!assetsLoaded)
		{
			if(Directory.Exists(modelsLocation))
			{
				if(File.Exists(modelsLocation + "/" + assetBundleName + ".unity3d"))
				{
					//assetBundle = AssetBundle.CreateFromFile(modelsLocation + "/" + assetBundleName + ".unity3d");
					assetBundle = AssetBundle.LoadFromFile(modelsLocation + "/" + assetBundleName + ".unity3d");
				}
				
				if(File.Exists(modelsLocation + "/" + assetBundleOther + ".unity3d"))
				{
					//assetBundle2 = AssetBundle.CreateFromFile(modelsLocation + "/" + assetBundleOther + ".unity3d");
					assetBundle2 = AssetBundle.LoadFromFile(modelsLocation + "/" + assetBundleOther + ".unity3d");
				}
			}
				
			assetsLoaded = true;
		}
		
		if(assetBundle != null && assetBundle.Contains("" + serialNumber))
		{
			c.SetCardModel((GameObject)assetBundle.LoadAsset("" + serialNumber));
		}
		
		if(assetBundle2 != null && assetBundle2.Contains("" + serialNumber))
		{
			c.SetCardModel((GameObject)assetBundle2.LoadAsset("" + serialNumber));
		}
		
		//Search for the card by serial number
		switch(serialNumber)
		{
//------------------------------------------OTHER-----------------------------------------------
//------------------------------------------PENDULUMS-----------------------------------------------
#region
#endregion
//------------------------------------------TOKENS-----------------------------------------------
#region
#endregion
//------------------------------------------ANIME ONLY-----------------------------------------------
#region
		case 0: break;
#endregion
		}
		
		return card;
	}
}