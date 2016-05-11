using UnityEngine;
using System.Collections;

//The Card itself (with card information)
public class Card : MonoBehaviour
{
	//Card variables
	[Tooltip("The name of the card.")]
	public string cardName = "";
	
	public enum CardType {spell, trap, normalMonster, effectMonster, tokenMonster, ritualMonster, fusionMonster, synchroMonster, xyzMonster};
	[Tooltip("The type of the card.")] public CardType cardType;
	
	[Tooltip("The serial number of the card.")]
	public int serial = 0;
	[Tooltip("The level of the card.")]
	public int level = 0;
	[Tooltip("The rank of the card.")]
	public int rank = 0;
	
	public enum Attribute {light, dark, fire, water, wind, earth, spell, trap, divine};
	[Tooltip("The attribute of the card.")] public Attribute attribute;
	
	public enum MonsterType {none = 0, aqua, beast, beastWarrior, dinosaur, divineBeast, dragon, fairy, fiend, fish, insect, machine, psychic, plant, pyro, reptile, rock, seaSerpent, spellcaster, thunder, warrior, wingedBeast, wyrm, zombie};
	[Tooltip("The monster type of the card.")] public MonsterType monsterType;
	
	public enum MonsterSubType {none = 0, gemini, spirit, toon, tuner, union};
	[Tooltip("The monster sub-type of the card.")] public MonsterSubType monsterSubType;
	
	public enum SpellType {none = 0, normal, quickPlay, equip, continuous, field, ritual};
	[Tooltip("The spell type of the card.")] public SpellType spellType;
	
	public enum TrapType {none = 0, normal, continuous, counter};
	[Tooltip("The trap type of the card.")] public TrapType trapType;
	
	[Tooltip("The attack of the card.")]
	public int attack = 0;
	[Tooltip("The defense of the card.")]
	public int defense = 0;
	
	[Tooltip("The front of the card.")]
	public GameObject front;
	[Tooltip("The back of the card.")]
	public GameObject back;
	
	[Tooltip("The front texture of the card.")]
	public Texture frontTexture;
	[Tooltip("The back texture of the card.")]
	public Texture backTexture;
	
	[Tooltip("The effect/flavor text of the card.")]
	public string descriptionText = "";
	
	[Tooltip("The OPTIONAL model of the card. (Not all cards have a model, but all cards MAY have an effect (with a separate script or animation for it)).")]
	public GameObject model = null;
	
	[Tooltip("Is this card a pendulum?")]
	public bool isPendulum = false;
	
	[Tooltip("Does this pendulum have a description? If so, provide one here.")]
	public string pendulumDescription = "";
	
	[Tooltip("Is this card a spell card and a monster card?")]
	public bool isSpellMonster = false;
	
	[Tooltip("Is this card a trap card and a monster card?")]
	public bool isTrapMonster = false;
	
	//------------GETTERS----------------
	public string GetCardName()
	{
		return this.cardName;
	}
	
	public CardType GetCardType()
	{
		return this.cardType;
	}
	
	public int GetSerial()
	{
		return this.serial;
	}
	
	public int GetLevel()
	{
		return this.level;
	}
	
	public int GetRank()
	{
		return this.rank;
	}
	
	public Attribute GetAttrabute()
	{
		return this.attribute;
	}
	
	public MonsterType GetMonsterType()
	{
		return this.monsterType;
	}
	
	public MonsterSubType GetMonsterSubType()
	{
		return this.monsterSubType;
	}
	
	public SpellType GetSpellType()
	{
		return this.spellType;
	}
	
	public TrapType GetTrapType()
	{
		return this.trapType;
	}
	
	public int GetAttack()
	{
		return this.attack;
	}
	
	public int GetDefense()
	{
		return this.defense;
	}
	
	public string GetDescription()
	{
		return this.descriptionText;
	}
	
	public Texture GetCardFront()
	{
		return this.front.GetComponent<Renderer>().material.GetTexture("_MainTex");
	}
	
	public Texture GetCardBack()
	{
		return this.back.GetComponent<Renderer>().material.GetTexture("_MainTex");
	}
	
	public Texture GetCardFrontTexture()
	{
		return frontTexture;
	}
	
	public Texture GetCardBackTexture()
	{
		return backTexture;
	}
	
	public GameObject GetCardModel()
	{
		return this.model;
	}
	
	public Card GetCard()
	{
		return this;
	}
	
	//------------SETTERS----------------
	public void SetCardName(string name)
	{
		this.cardName = name;
	}
	
	public void SetCardType(CardType cardType)
	{
		this.cardType = cardType;
	}
	
	public void SetSerial(int serial)
	{
		this.serial = serial;
	}
	
	public void SetLevel(int level)
	{
		this.level = level;
	}
	
	public void SetRank(int rank)
	{
		this.rank = rank;
	}
	
	public void SetAttrabute(Attribute attrabute)
	{
		this.attribute = attrabute;
	}
	
	public void SetMonsterType(MonsterType monsterType)
	{
		this.monsterType = monsterType;
	}
	
	public void SetMonsterSubType(MonsterSubType monsterSubType)
	{
		this.monsterSubType = monsterSubType;
	}
	
	public void SetSpellType(SpellType spellType)
	{
		this.spellType = spellType;
	}
	
	public void SetTrapType(TrapType trapType)
	{
		this.trapType = trapType;
	}
	
	public void SetAttack(int attack)
	{
		this.attack = attack;
	}
	
	public void SetDefense(int defense)
	{
		this.defense = defense;
	}
	
	public void SetDescription(string text)
	{
		this.descriptionText = text;
	}
	
	public void SetCardFront(Texture cardFront)
	{
		if(cardFront == null)
		{
			Debug.LogError("NULL: This card texture is null, cannot be put on this card! (Front of card)");
		}
		else if(this.front == null)
		{
			Debug.LogError("NULL: The front of the card is null, there is no where to put this texture.");
		}
		else if(cardFront != null && this.front != null)
		{
			this.front.GetComponent<Renderer>().material.SetTexture("_MainTex", cardFront);
		}
	}
	
	public void SetCardBack(Texture cardBack)
	{
		if(cardBack == null)
		{
			Debug.LogError("NULL: This card texture is null, cannot be put on this card! (Back of card)");
		}
		else if(this.back == null)
		{
			Debug.LogError("NULL: The back of the card is null, there is no where to put this texture.");
		}
		else if(cardBack != null && this.back != null)
		{
			this.back.GetComponent<Renderer>().material.SetTexture("_MainTex", cardBack);
		}
	}
	
	public void SetCardFrontTexture(Texture cardFront)
	{
		this.frontTexture = cardFront;
		
		//Do something extra if the front of the card is not null...
		if(this.front != null)
		{
			this.front.GetComponent<Renderer>().material.SetTexture("_MainTex", cardFront);
		}
	}
	
	public void SetCardBackTexture(Texture cardBack)
	{
		this.backTexture = cardBack;
		
		//Do something extra if the back of the card is not null...
		if(this.back != null)
		{
			this.back.GetComponent<Renderer>().material.SetTexture("_MainTex", cardBack);
		}
	}
	
	public void SetCardModel(GameObject cardModel)
	{
		this.model = cardModel;
	}
	
	public void SetCard(Card card)
	{
		this.cardName = card.cardName;
		this.cardType = card.cardType;
		this.serial = card.serial;
		this.level = card.level;
		this.rank = card.rank;
		this.attribute = card.attribute;
		this.monsterType = card.monsterType;
		this.monsterSubType = card.monsterSubType;
		this.spellType = card.spellType;
		this.trapType = card.trapType;
		this.attack = card.attack;
		this.defense = card.defense;
		this.descriptionText = card.descriptionText;
		this.isPendulum = card.isPendulum;
		this.pendulumDescription = card.pendulumDescription;
		this.isSpellMonster = card.isSpellMonster;
		this.isTrapMonster = card.isTrapMonster;
		this.frontTexture = card.frontTexture;
		this.backTexture = card.backTexture;
		this.model = card.model;
	}
	
	//Sets all the fields on this card (used for the database to set all the cards)
	public void SetAllFields(string cardName, CardType cardType, int serial, int level, int rank, Attribute attribute, MonsterType monsterType, MonsterSubType monsterSubType, SpellType spellType, TrapType trapType, int attack, int defense, string descriptionText, bool isPendulum = false, string pendulumDescription = "", bool isSpellMonster = false, bool isTrapMonster = false)
	{
		this.cardName = cardName;
		this.cardType = cardType;
		this.serial = serial;
		this.level = level;
		this.rank = rank;
		this.attribute = attribute;
		this.monsterType = monsterType;
		this.monsterSubType = monsterSubType;
		this.spellType = spellType;
		this.trapType = trapType;
		this.attack = attack;
		this.defense = defense;
		this.descriptionText = descriptionText;
		this.isPendulum = isPendulum;
		this.pendulumDescription = pendulumDescription;
		this.isSpellMonster = isSpellMonster;
		this.isTrapMonster = isTrapMonster;
	}
}