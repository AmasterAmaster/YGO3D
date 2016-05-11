using UnityEngine;
using System.Collections;
using System.IO;

//Holds all the information for each card, determained by the serial number
public class CardDatabaseKolrickCopy : MonoBehaviour
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
	static string assetBundleName = "modelsbundle";
	//static string assetBundleMonsters = "monstersbundle";
	//static string assetBundleSpells = "spellsbundle";
	//static string assetBundleTraps = "trapsbundle";
	//static string assetBundleOther = "otherbundle";
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
				//assetBundle = AssetBundle.CreateFromFile(modelsLocation + "/" + assetBundleName + ".unity3d");
				assetBundle = AssetBundle.LoadFromFile(modelsLocation + "/" + assetBundleName + ".unity3d");
			}
				
			assetsLoaded = true;
		}
		
		if(assetBundle != null && assetBundle.Contains("" + serialNumber))
		{
			c.SetCardModel((GameObject)assetBundle.LoadAsset("" + serialNumber));
		}
		
		//Search for the card by serial number
		switch(serialNumber)
		{
////------------------------------------------MONSTERS-----------------------------------------------

//------------------------------------------Thunder-----------------------------------------------
#region
#endregion
//------------------------------------------Toon-----------------------------------------------
#region
		
#endregion
//------------------------------------------Tuner-----------------------------------------------
#region
#endregion
//------------------------------------------Union-----------------------------------------------
#region
#endregion
//------------------------------------------Warrior-----------------------------------------------
#region
#endregion
//------------------------------------------Winged-Beast-----------------------------------------------
#region
#endregion
//------------------------------------------Wyrm-----------------------------------------------
#region
#endregion	
//------------------------------------------Zombie-----------------------------------------------
#region
#endregion
//------------------------------------------RITUALS-----------------------------------------------
#region
		
#endregion
//------------------------------------------FUSIONS-----------------------------------------------
#region
#endregion
//------------------------------------------SYNCHROS-----------------------------------------------
#region
#endregion
//------------------------------------------XYZs-----------------------------------------------
#region
#endregion
//------------------------------------------PENDULUMS-----------------------------------------------
#region
#endregion
//------------------------------------------TOKENS-----------------------------------------------
#region
#endregion
//------------------------------------------SPELLS-----------------------------------------------
//------------------------------------------Normal-----------------------------------------------
#region
#endregion
//------------------------------------------Quick-Play-----------------------------------------------
#region
#endregion
//------------------------------------------Continuous-----------------------------------------------
#region
#endregion
//------------------------------------------Field-----------------------------------------------
#region	
#endregion
//------------------------------------------Equip-----------------------------------------------
#region
#endregion
//------------------------------------------Ritual-----------------------------------------------
#region		                             
#endregion
//------------------------------------------TRAPS-----------------------------------------------
//------------------------------------------Normal-----------------------------------------------
#region
#endregion
//------------------------------------------Continuous-----------------------------------------------
#region
		//case 53112492: c.SetAllFields("Anti-Spell", Card.CardType.trap, serialNumber, 4, 0, Card.Attribute.water, Card.MonsterType.aqua, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.none, 1800, 1300, "Discard 1 WATER monster fro

#endregion
//------------------------------------------Counter-----------------------------------------------
#region
		case 53112492: c.SetAllFields("Anti-Spell", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Remove 2 Spell Counters on your side of the field to negate the activation of a Spell Card and destroy it."); break;
		case 79649195: c.SetAllFields("Armor Break", Card.CardType.trap, serialNumber,0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation of an Equip Spell Card and destroy it."); break;
		case 76407432: c.SetAllFields("Assault Counter", Card.CardType.trap, serialNumber,0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only while you control an \"/Assault Mode\" monster. Negate the activation of a Spell Card, Trap Card, or Effect Monster's effect and destroy it."); break;
		case 78783370: c.SetAllFields("Barrel Behind The Door", Card.CardType.trap, serialNumber,0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0,"Activate only when a card's effect that would inflict damage to you is activated. Your opponent takes the damage instead."); break;
		case 94662235: c.SetAllFields("Bending Destiny", Card.CardType.trap, serialNumber,0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only if all face-up monsters you control are \"Fortune Lady\" monsters. Negate the activation of a Spell or Trap Card, or the Normal Summon of a monster, and remove that card from play. The card removed from play by this effect is returned to its owner's hand during the End Phase."); break;
		case 50323155: c.SetAllFields("Black Horn Of Heaven", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent would Special Summon exactly 1 monster: Negate the Special Summon, and if you do, destroy it."); break;
		case 86690572: c.SetAllFields("Burgeoning Whirlflame", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Trap Card is activated: Send 1 \"Laval\" monster from your hand to the Graveyard; negate the activation and destroy it. If this card is in the Graveyard: You can banish 2 FIRE monsters from your Graveyard; add this card to your hand."); break;
		case 59957503: c.SetAllFields("Cash Back", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell/Trap Card, or monster effect, by paying their Life Points: Negate the activation, and if you do, return it to the Deck."); break;
		case 82382815: c.SetAllFields("Champion's Vigilance", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "If you control a Level 7 or higher Normal Monster, when a monster would be Summoned OR a Spell/Trap Card is activated: Negate the Summon or activation, and if you do, destroy that card."); break;
		case 11593137: c.SetAllFields("Chaos Trap Hole", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a LIGHT or DARK monster would be Summoned: Pay 2000 Life Points, negate the Summon, and if you do, banish it."); break;
		case 42309337: c.SetAllFields("Counter Counter", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation of a Counter Trap Card, and destroy it."); break;
		case 24566654: c.SetAllFields("Crimson Fire", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell/Trap Card that would inflict damage to you, while you control a \"Red Dragon Archfiend\": Your opponent takes twice the effect damage you would have taken, instead."); break;
		case 02926176: c.SetAllFields("Curse Of Royal", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation and the effect of a Spell or Trap Card that includes the effect of destroying 1 Spell or Trap Card and destroy it."); break;
		case 58851034: c.SetAllFields("Cursed Seal Of The Forbidden Spell", Card.CardType.trap, serialNumber,0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell Card is activated: Discard 1 Spell Card; negate the activation, and if you do, destroy it, and if you do that, your opponent cannot activate Spell Cards with that name for the rest of this Duel."); break;
		case 46031686: c.SetAllFields("Damage Polarizer", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only when an effect that inflicts damage is activated. Negate its activation and effect, and each player draws 1 card."); break;
		case 77538567: c.SetAllFields("Dark Bribe", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When an opponent's Spell/Trap Card is activated: Your opponent draws 1 card, also negate the Spell/Trap activation, and if you do, destroy it."); break; 
		case 05562461: c.SetAllFields("Dark Illusion", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation of a Spell Card, Trap Card, or Effect Monster's effect that targets a face-up DARK monster, and destroy that card."); break;
		case 98956134: c.SetAllFields("Destuction Jammer", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Discard 1 card. Negate the activation of a Spell Card, Trap Card, or Effect Monster's effect that destroys a monster(s) on the field, and destroy that card."); break;
		case 59905358: c.SetAllFields("Dice Try", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell/Trap Card, or monster effect, that requires a dice roll: Negate that activation, and if you do, destroy it.");break ;
		case 26834022: c.SetAllFields("Disarm", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Return 1 \"Gladiator Beast\" card from your hand to the Deck, and negate the activation and effect of a Spell Card, and destroy it."); break;
		case 81066751: c.SetAllFields("Divine Punishment", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only if \"The Sanctuary in the Sky\" is face-up on the field. Negate the activation of a Spell Card, Trap Card, or Effect Monster's effect, and destroy that card."); break;
		case 49010598: c.SetAllFields("Divine Wrath", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster effect is activated: Discard 1 card; negate the activation, and if you do, destroy that monster."); break;
		case 06260554: c.SetAllFields("Do A Barrel Roll", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell Card, Trap Card, or monster effect is activated: Tribute all \"Mecha Phantom Beast Tokens\" you control; negate the activation, and if you do, destroy it."); break;
		case 04440873: c.SetAllFields("Drastic Drop Off", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent adds a card(s) from their Deck to their hand, including drawing: They must discard 1 of those cards."); break;
		case 29934351: c.SetAllFields("Earthbound Wave", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell/Trap Card while you control a \"Earthbound Immortal\" monster: Negate the activation, and if you do, destroy it."); break;
		case 68456353: c.SetAllFields("Exterio's Fang", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only if you control a face-up \"Naturia\" monster and have at least 1 card in your hand. Negate the activation of an opponent's Spell/Trap Card and destroy it. Then, send 1 card from your hand to the Graveyard."); break;
		case 71060915: c.SetAllFields("Feather Wind", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell/Trap Card is activated while you control a face-up \"Elemental HERO Avian\": Negate the activation and destroy it."); break;
		case 60718396: c.SetAllFields("Flemvell Counter", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell/Trap Card is activated: Banish 1 FIRE monster with 200 DEF from your Graveyard; negate the activation, and if you do, destroy it."); break;
		case 43340443: c.SetAllFields("Forced Back", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the Normal Summon or Flip Summon of a monster and return the monster to its owner's hand."); break;
		case 73026394: c.SetAllFields("Fusion Guard", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only when an effect that inflicts damage is activated. Negate its activation and effect, and randomly send 1 Fusion Monster from your Extra Deck to the Graveyard."); break;
		case 81601517: c.SetAllFields("Gemini Counter",Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Change 1 face-up Gemini monster you control to face-down Defense Position and negate the activation of an opponent's Spell Card, and destroy it."); break;
		case 96216229: c.SetAllFields("Gladiator Beast War Chariot",  Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When an Effect Monster's effect is activated, if you control a face-up \"Gladiator Beast\" monster: Negate the activation and destroy it."); break;
		case 69632396: c.SetAllFields("Goblin Out Of The Frying Pan", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Pay 500 Life Points. Negate the activation of a Spell Card and return it to the owner's hand."); break;
		case 07811875: c.SetAllFields("Gravity Collapse", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate by sending 1 face-up Synchro Monster you control to the Graveyard when your opponent Summons a monster. Negate the Summon and destroy that card. Your opponent cannot Summon a monster until the End Phase of this turn."); break;
		case 85854214: c.SetAllFields("Hero's Rule 2", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation of a Spell, Trap, or Effect Monster's effect that targets a card(s) in the Graveyard, and destroy it."); break;
		case 98069388: c.SetAllFields("Horn Of Heaven", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster would be Summoned: Tribute 1 monster; negate the Summon, and if you do, destroy that monster."); break;
		case 09059700: c.SetAllFields("Infernity Barrier", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell/Trap Card, or monster effect, while you control a face-up Attack Position \"Infernity\" monster and have no cards in your hand: Negate the activation, and if you do, destroy that card."); break;
		case 59695933: c.SetAllFields("Intercept", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only when a monster is Tribute Summoned with 1 Tribute. Take control of that monster."); break;
		case 34545235: c.SetAllFields("Iron Core Luster", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Reveal 1 \"Iron Core of Koa'ki Meiru\" in your hand. Negate the activation of your opponent's Spell or Trap Card and destroy it."); break;
		case 55256016: c.SetAllFields("Judgment Of Anubis", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell Card that would destroy a Spell/Trap Card(s) on the field: Discard 1 card; negate the activation, and if you do, destroy it, then you can destroy 1 face-up monster your opponent controls, and if you do that, inflict damage to your opponent equal to the destroyed monster's ATK on the field."); break;
		case 02924048: c.SetAllFields("Karakuri Cash Shed", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only if you control a \"Karakuri\" monster in face-up Defense Position. Negate the activation of an opponent's Spell/Trap Card and destroy it."); break;
		case 75833426: c.SetAllFields("Madolche Tea Break", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell/Trap Card is activated, if you have no monsters in your Graveyard: Negate the activation, and if you do, return it to the hand, then, if you control a face-up \"Madolche Puddingcess\", you can destroy 1 card your opponent controls."); break;
		case 59344077: c.SetAllFields("Magic Drain", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell Card: They can discard 1 Spell Card to negate this card's effect, otherwise negate the activation of their Spell Card, and if you do, destroy it."); break;
		case 77414722: c.SetAllFields("Magic Jammer", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell Card is activated: Discard 1 card; negate the activation, and if you do, destroy it."); break;
		case 06137095: c.SetAllFields("Malfunction", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Pay 500 Life Points. Negate the activation of a Trap Card and return it to its original position."); break;
		case 59718521: c.SetAllFields("Mind Over Matter", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster would be Normal or Special Summoned, OR a Spell/Trap Card is activated: Tribute 1 Psychic-Type monster; negate the Summon or activation, and if you do, destroy that card."); break;
		case 77229910: c.SetAllFields("Morphtronic Forcefield", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation of a Spell or Trap Card that would destroy a face-up \"Morphtronic\" monster you control and destroy it. Add 1 \"Morphtronic\" card from your Deck to your hand."); break;
		case 41458579: c.SetAllFields("Musakani Magatama", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell Card, Trap Card, or monster effect that destroys a card(s), while you control a face-up \"Six Samurai\" monster: Negate the activation, and if you do, destroy it."); break;
		case 14315573: c.SetAllFields("Negate Attack", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When an opponent's monster declares an attack: Target the attacking monster; negate the attack, then end the Battle Phase."); break;
		case 55117418: c.SetAllFields("Nega-Ton Corepanel", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only if you control a face-up \"Koa'ki Meiru\" monster and have \"Iron Core of Koa'ki Meiru\" in your Graveyard. Negate the activation of an Effect Monster's effect, and destroy it."); break;
		case 87313164: c.SetAllFields("Oh F!sh!", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When an Effect Monster's effect activates: Shuffle 1 of your banished Fish, Sea Serpent, or Aqua-Type monsters into the Main Deck; negate the activation and destroy it."); break;
		case 20140382: c.SetAllFields("Overwhelm", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only while you control a face-up Level 7 or higher monster that was Tribute Summoned. Negate the activation of a Trap Card or an Effect Monster's effect and destroy that card."); break;
		case 52228131: c.SetAllFields("Parry", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Return 1 \"Gladiator Beast\" card from your hand to the Deck, and negate the activation and effect of a Trap Card, and destroy it."); break;
		case 91078716: c.SetAllFields("Pollinosis", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster would be Normal or Special Summoned, OR a Spell/Trap Card is activated: Tribute 1 Plant-Type monster; negate the Summon or activation, and if you do, destroy that card."); break;
		case 34717238: c.SetAllFields("Pulling The Rug", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation and effect of an Effect Monster whose effect activated when a monster was Normal Summoned (even itself), and destroy that Effect Monster."); break;
		case 00983995: c.SetAllFields("Rebound", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Spell Card, Trap Card, or monster effect that returns a card(s) from the field to the hand: Negate that effect, and if you do, send 1 card from your opponent's hand (at random) or from their side of the field to the Graveyard. When this Set card is destroyed by your opponent's card effect and sent to the Graveyard: Draw 1 card."); break;
		case 70344351: c.SetAllFields("Riryoku Field", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell Card that targets exactly 1 monster on the field (and no other cards) is activated: Negate the activation, and if you do, destroy it."); break;
		case 56058888: c.SetAllFields("Royal Surrender", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only when your opponent activates a Continuous Trap Card. Negate the activation and the effect of the card and destroy it."); break;
		case 44901281: c.SetAllFields("Saber Hole", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only while you control a face-up \"X-Saber\" monster. Negate the Summon of a monster and destroy it."); break;
		case 03819470: c.SetAllFields("Seven Tools Of The Bandit", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Trap Card is activated: Pay 1000 LP; negate the activation, and if you do, destroy it."); break;
		case 89563150: c.SetAllFields("Shining Silver Force", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only when your opponent activates a Trap Card that inflicts damage. Negate the activation of that card and destroy it and all face-up Spell/Trap Cards your opponent controls."); break;
		case 93396832: c.SetAllFields("Smashing Horn", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster effect or Trap Card is activated that negates the Normal or Special Summon of a monster(s): Negate the activation and destroy it."); break;
		case 80678380: c.SetAllFields("Snake Deity's Command", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate by showing your opponent 1 \"Venom\" monster in your hand. Negate the activation and effect of an opponent's Spell Card, and destroy it."); break;
		case 41420027: c.SetAllFields("Solemn Judgment", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster would be Summoned, OR a Spell/Trap Card is activated: Pay half your Life Points; negate the Summon or activation, and if you do, destroy that card."); break;
		case 84749824: c.SetAllFields("Solemn Warning", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster(s) would be Summoned, OR when a Spell Card, Trap Card, or monster effect is activated that includes an effect that Special Summons a monster(s): Pay 2000 LP; negate the Summon or activation, and if you do, destroy that card."); break;
		case 38275183: c.SetAllFields("Spell Shield Type-8", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate 1 of these effects. ● When a Spell Card is activated that targets exactly 1 monster on the field: Negate the activation, and if you do, destroy it. ● When a Spell Card is activated: Send 1 Spell Card from your hand to the Graveyard; negate the activation, and if you do, destroy it."); break;
		case 10069180: c.SetAllFields("Spell-Stopping Statute", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only when your opponent activates a Continuous Spell Card. Negate the activation and the effect of the card and destroy it."); break;
		case 29735721: c.SetAllFields("Spell Vanishing", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Discard 2 cards from your hand. Negate the activation of a Spell Card and destroy it. Also, look at your opponent's hand and Deck and if you find any Spell Cards of the same name as the destroyed Spell Card, send all of them to the Graveyard."); break;
		case 10651797: c.SetAllFields("Swallow Flip", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Negate the activation of an Effect Monster's effect that activates when a monster is Special Summoned (including itself), and destroy it."); break;
		case 99188141: c.SetAllFields("The Huge Revolution Is Over", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell Card, Trap Card, or Effect Monster's effect is activated that destroys 2 or more cards on the field: Negate the activation and banish it.");break;
		case 30888983: c.SetAllFields("The Selection", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Pay 1000 Life Points. Negate the Summon of a monster that has the same Type as a monster on the field, and destroy it."); break;
		case 19252988: c.SetAllFields("Trap Jammer", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a Trap Card during the Battle Phase: Negate the activation, and if you do, destroy it."); break;
		case 03055837: c.SetAllFields("Trap Of Board Eraser", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "You can only activate this card when an effect that inflicts damage to your Life Points is activated (except Battle Damage). Negate the Effect Damage you receive and your opponent then selects 1 card from his/her hand and discards it."); break;
		case 03149764: c.SetAllFields("Tutan Mask", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell/Trap Card is activated that targets exactly 1 Zombie-Type monster on the field (and no other cards): Negate the activation, and if you do, destroy it."); break;
		case 86474024: c.SetAllFields("United Front", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Trap Card is activated while you control 2 or more face-up monsters with the same Level: Negate the activation and destroy it."); break;
		case 24838456: c.SetAllFields("Vanity's Call", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "Activate only as Chain Link 4 or higher. Pay half your Life Points. Negate the activation and effects of all other cards in the same Chain and destroy them."); break;
		case 32233746: c.SetAllFields("Vanquishing Light", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a monster(s) would be Summoned: Tribute 1 \"Lightsworn\" monster; negate the Summon, and if you do, destroy that monster(s)."); break;
		case 56993276: c.SetAllFields("Wattcancel", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent would Normal or Special Summon a monster: Discard 1 \"Watt\" monster; negate the Summon and destroy it."); break;
		case 44487250: c.SetAllFields("Xyz Block", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When your opponent activates a monster effect: Detach 1 Xyz Material from a monster you control; negate the activation, and if you do, destroy that monster."); break;
		case 02371506: c.SetAllFields("Xyz Reflect", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0,"When a Spell Card, Trap Card, or monster effect is activated that targets a face-up Xyz Monster(s) on the field: Negate the activation, and if you do, destroy it, then inflict 800 damage to your opponent."); break;
		case 54903668: c.SetAllFields("Yosenjus' Secret Move", Card.CardType.trap, serialNumber, 0, 0, Card.Attribute.trap, Card.MonsterType.none, Card.MonsterSubType.none, Card.SpellType.none, Card.TrapType.counter, 0, 0, "When a Spell/Trap Card, or monster effect, is activated while you control at least 1 \"Yosenju\" card, and all face-up monsters you control are \"Yosenju\" monsters: Negate the activation, and if you do, destroy that card."); break;
#endregion
			                             
		}
		
		return card;
	}
}