using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class GalleryMenuScript : MonoBehaviour
{
	//Variables
	public GameObject galleryCard;
	public GameObject galleryModel;
	public GameObject placeHolder;
	public Transform placeholderPosition;
	
	//Aspect ratio variables
	public int width = 800;
	public int height = 600;
	
	//Scrollview
	public Vector2 scrollposition;
	public Vector2 scrollposition2;
	public string innerText = "Card information box (Enter a serial number)";
	public Vector2 pos = new Vector2(40,60);
	public Vector2 size = new Vector2(60,20);
	
	//Textarea
	public string searchText = "89631139";
	
	void Update()
	{
		if(Input.GetKeyDown("enter"))
		{
			//Parse the number from the search
			//int number = int.Parse(searchText);
			
			//Search the database using the typed out text
			//CardDatabase.GetCardInfo(number);
			
			//Reset the position of the gallery card
			//galleryCard.transform.rotation = Quaternion.identity;
			//galleryModel.transform.rotation = Quaternion.identity;
		}
	}

	void OnGUI()
	{
		float screenX = Screen.width;
		float screenY = Screen.height;
	
		float x = size.x * screenX / width;
		float y = size.y * screenY / height;
		
		//------------SEARCHBAR-----------
		searchText = GUI.TextArea(new Rect(0, 0, 200, 60), searchText, 12);
		//------------SEARCHBAR-----------
		
		
		//------------SEARCHBUTTON-----------
		if(GUI.Button(new Rect(200, 0, 150, 60), "Search By\nSerial Number"))
		{
			//Parse the number from the search
			int number = int.Parse(searchText);
			
			//Search the database using the typed out text
			CardDatabaseMonsters.GetMonsterCardInfo(number, galleryCard.GetComponent<Card>());		//Check Monster cards first
			
			if(galleryCard.GetComponent<Card>().serial == 0)
			{
				CardDatabaseSpells.GetSpellCardInfo(number, galleryCard.GetComponent<Card>());		//Then Spells
				
				if(galleryCard.GetComponent<Card>().serial == 0)
				{
					CardDatabaseTraps.GetTrapCardInfo(number, galleryCard.GetComponent<Card>());	//Then Traps
					
					if(galleryCard.GetComponent<Card>().serial == 0)
					{
						CardDatabase.GetCardInfo(number, galleryCard.GetComponent<Card>());			//Then Pendulums, Tokens, Anime, etc...
					}
				}
			}
			
			//Show card information
			SetCardInformationBox();
			
			//If the model is in this game...
			if(galleryCard.GetComponent<Card>().GetCardModel() != null)
			{
				//Show the model
				GameObject placeHolderTemp = null;
				placeholderPosition = placeHolder.transform;
				placeHolderTemp = (GameObject)Instantiate(galleryCard.GetComponent<Card>().GetCardModel(), placeholderPosition.position, galleryCard.transform.rotation);
				if(placeHolderTemp != null)
				{
					Destroy(placeHolder);
					placeHolder = placeHolderTemp;
					placeHolder.transform.parent = galleryModel.transform;
				}
			}
			else
			{
				Debug.Log("There is no model for this card yet!");
			}
		}
		//------------SEARCHBUTTON-----------
		
		
		//------------MENU----------------
		//Start scrollview							//10	300
		scrollposition = GUI.BeginScrollView(new Rect(pos.x, pos.y, x - 30, y - 80), scrollposition, new Rect(pos.x, pos.y, x, y), true, true);
		
		//Put something inside the ScrollView			400	400
		innerText = GUI.TextArea(new Rect(pos.x, pos.y, x - 25, y), innerText);
	
		//End the ScrollView
		GUI.EndScrollView();
		
		//Make a list of buttons to click on that can also dynamically change depending on the search results.
		//Upon clicking on the button, it should show the selected card.
		//------------MENU----------------
		
		
		//------------CARDINFO---------------
		//Start scrollview
		//scrollposition2 = GUI.BeginScrollView(new Rect(pos.x + x, y - 80, 50, 50), scrollposition2, new Rect(pos.x + x, y, 50, 50), true, true);
		
		//Text
		//innerText2 = GUI.TextArea(new Rect(pos.x + x, y - 80, 50, 50), innerText2);
		
		//End the ScrollView
		//GUI.EndScrollView();
		//------------CARDINFO---------------
		
		
		//------------Gallery Actions-----------
		GUI.Label(new Rect((screenX / 2) - 350, screenY - 75, 300, 25), "Gallery Actions:");
		
		if(GUI.Button(new Rect((screenX / 2) - 250, screenY - 50, 100, 50), "Toggle\nRotate"))
		{
			//Turns on or off the rotation for the cards
			galleryCard.GetComponent<RotaterCustom>().enabled = !galleryCard.GetComponent<RotaterCustom>().enabled;
			galleryModel.GetComponent<RotaterCustom>().enabled = !galleryModel.GetComponent<RotaterCustom>().enabled;
		}

		if(GUI.Button(new Rect((screenX / 2) - 150, screenY - 50, 100, 50), "Reset\nRotation"))
		{
			//Reset the position
			galleryCard.transform.rotation = Quaternion.identity;
			galleryModel.transform.rotation = Quaternion.identity;
		}

		if(GUI.Button(new Rect((screenX / 2) - 350, screenY - 50, 100, 50), "Main\nMenu"))
		{
			//Application.LoadLevel("MainMenu");
			SceneManager.LoadScene("MainMenu");
		}
		//------------Gallery Actions-----------
		
		
		//------------Animation Buttons-----------
		GUI.Label(new Rect((screenX / 2) + 50, screenY - 100, 300, 25), "Animations, Effects, & Backgrounds:");
		
		if(GUI.Button(new Rect((screenX / 2) + 50, screenY - 50, 150, 25), "Normal Summon"))
		{
			//Play normal summon animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 50, screenY - 25, 150, 25), "Special Summon"))
		{
			//Play special summon animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 200, screenY - 50, 150, 25), "Attack"))
		{
			//Play attack animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 200, screenY - 25, 150, 25), "Destroyed"))
		{
			//Play destroyed animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 350, screenY - 50, 150, 25), "Special Attack"))
		{
			//Play special attack animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 350, screenY - 25, 150, 25), "Idle"))
		{
			//Play idle animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 500, screenY - 50, 150, 25), "Spell/Trap Effect"))
		{
			//Play Spell/Trap effect animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 500, screenY - 25, 150, 25), "Field Background"))
		{
			//Show field spell background
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 50, screenY - 75, 300, 25), "Switch To Attack Mode"))
		{
			//Play Spell/Trap effect animation
		}
		
		if(GUI.Button(new Rect((screenX / 2) + 350, screenY - 75, 300, 25), "Switch To Defense Mode"))
		{
			//Show field spell background
		}
		//------------Animation Buttons-----------
	}
	
	//Sets the card information to the text box area
	public void SetCardInformationBox()
	{
		//Temp variables
		string name = galleryCard.GetComponent<Card>().GetCardName();
		string cardType = galleryCard.GetComponent<Card>().GetCardType().ToString();
		string serial = galleryCard.GetComponent<Card>().GetSerial().ToString();
		string level = galleryCard.GetComponent<Card>().GetLevel().ToString();
		string rank = galleryCard.GetComponent<Card>().GetRank().ToString();
		string attrabute = galleryCard.GetComponent<Card>().GetAttrabute().ToString();
		string monsterType = galleryCard.GetComponent<Card>().GetMonsterType().ToString();
		string monsterSubType = galleryCard.GetComponent<Card>().GetMonsterSubType().ToString();
		string spellType = galleryCard.GetComponent<Card>().GetSpellType().ToString();
		string trapType = galleryCard.GetComponent<Card>().GetTrapType().ToString();
		string attack = galleryCard.GetComponent<Card>().GetAttack().ToString();
		string defense = galleryCard.GetComponent<Card>().GetDefense().ToString();
		string pendulum = galleryCard.GetComponent<Card>().isPendulum.ToString();
		string description = galleryCard.GetComponent<Card>().GetDescription();
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
		
		tempText = tempText + "Pendulum: " + pendulum + "\n\n" +
					"Description: " + description;
		
		//Set the corrected text into the text box area
		innerText = tempText;
	}
}