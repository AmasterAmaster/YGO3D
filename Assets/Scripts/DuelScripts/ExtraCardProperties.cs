using UnityEngine;
using System.Collections;

public class ExtraCardProperties : MonoBehaviour
{
	//Extra card property variables
	public enum CurrentState {deck = 0, hand, field, graveyard, banished, extraDeck};
	[Tooltip("The current state of this card.")] public CurrentState currentState;
	
	public enum Positions {none = 0, faceUpAttack, faceUpDefense, faceDownAttack, faceDownDefense, set, active};
	[Tooltip("The current position of this card.")] public Positions positions;
	
	[Tooltip("The treated name of this card. (Used for some monster effects).")]
	public string cardTreatedName = "";
	
	public enum SubAttrabute {none = 0, light, dark, fire, water, wind, earth, spell, trap, divine};
	[Tooltip("The sub-attrabute of this card. (Some monsters can have more than one original attrabute).")] public SubAttrabute subAttrabute;
	
	[Tooltip("Who is the owner of this card? (The player that actually owns the card.)")]
	public Player owner;
	
	[Tooltip("Who is the controller of this card? (The player that is currently controlling the card.)")]
	public Player controller;
	
	[Tooltip("What slot is this card at on the field.)")]
	public string slot;
	
	[Tooltip("Can this card attack?")]
	public bool canAttack = true;
	
	[Tooltip("Can this card attack directly?")]
	public bool canAttackDirectly = false;
	
	[Tooltip("Can this card attack in defense mode?")]
	public bool canAttackInDefenseMode = false;
	
	[Tooltip("Can this card change battle positions?")]
	public bool canChangeBattlePositions = true;
	
	[Tooltip("Can this card be negated?")]
	public bool canBeNegated = false;
	
	[Tooltip("Is this card negated?")]
	public bool effectNegated = false;
	
	[Tooltip("Can this card be activated?")]
	public bool canActivate = true;
	
	[Tooltip("Can this card be targeted by attacks?")]
	public bool canBeTargetedByBattle = true;
	
	[Tooltip("Can this card be targeted by effects?")]
	public bool canBeTargetedByCardeffect = true;
	
	[Tooltip("Can this card be Set?")]
	public bool setable = true;
	
	[Tooltip("is this card Set?")]
	public bool isSet = false;
	
	[Tooltip("Can this card be Normal Summoned?")]
	public bool normalSummonable = true;
	
	[Tooltip("Was this card Normal Summoned?")]
	public bool wasNormalSummoned = false;
	
	[Tooltip("Can this card be Tribute Summoned?")]
	public bool tributeSummonable = true;
	
	[Tooltip("Can this card be Tributed?")]
	public bool canBeTributed = true;
	
	[Tooltip("Was this card Tribute Summoned?")]
	public bool wasTributeSummoned = false;
	
	[Tooltip("Was this card be used for Tribute Summons?")]
	public bool wasUsedForTributeSummons = false;
	
	[Tooltip("Can this card be Special Summoned?")]
	public bool specialSummonable = true;
	
	[Tooltip("Was this card Special Summoned?")]
	public bool wasSpecialSummoned = false;
	
	[Tooltip("Does this card allow for the player to pick what position to be Special Summoned?")]
	public bool playerChoiceOnSpecialSummon = true;
	
	[Tooltip("Can this card be Ritual Summoned?")]
	public bool ritualSummonable = true;
	
	[Tooltip("Can this card be used for Ritual Summons?")]
	public bool canBeUsedForRitualSummons = true;
	
	[Tooltip("Was this card be used for Ritual Summons?")]
	public bool wasUsedForRitualSummons = false;
	
	[Tooltip("Can this card be Fusion Summoned?")]
	public bool fusionSummonable = true;
	
	[Tooltip("Can this card be used for Fusion Summons?")]
	public bool canBeUsedForFusionSummons = true;
	
	[Tooltip("Was this card be used for Fusion Summons?")]
	public bool wasUsedForFusionSummons = false;
	
	[Tooltip("Can this card be Synchro Summoned?")]
	public bool synchroSummonable = true;
	
	[Tooltip("Can this card be used for Synchro Summons?")]
	public bool canBeUsedForSynchroSummons = true;
	
	[Tooltip("Was this card be used for Synchro Summons?")]
	public bool wasUsedForSynchroSummons = false;
	
	[Tooltip("Can this card be Xyz Summoned?")]
	public bool xyzSummonable = true;
	
	[Tooltip("Can this card be used for Xyz Summons?")]
	public bool canBeUsedForXYZSummons = true;
	
	[Tooltip("Was this card be used for Xyz Summons?")]
	public bool wasUsedForXYZSummons = false;
	
	[Tooltip("How long this card has been on the field.")]
	public int numberOfTurnsFaceUpOnField = 0;
	
	[Tooltip("How many times has this effect been activated.")]
	public int numberOfTimesOfActivatedEffect = 0;
	
	[Tooltip("Is this card selected?")]
	public bool isSelected = false;
	
	//Counter variables
	[Tooltip("The Genaric Counters on this card.")]
	public int genaricCounters = 0;
	
	[Tooltip("The A-Counters on this card.")]
	public int aCounter = 0;
	
	[Tooltip("The Balloon Counters on this card.")]
	public int balloonCounter = 0;
	
	[Tooltip("The Black Feather Counters on this card.")]
	public int blackFeatherCounter = 0;
	
	[Tooltip("The Bushido Counters on this card.")]
	public int bushidoCounter = 0;
	
	[Tooltip("The Chaos Counters on this card.")]
	public int chaosCounter = 0;
	
	[Tooltip("The Chronicle Counters on this card.")]
	public int chronicleCounter = 0;
	
	[Tooltip("The Clock Counters on this card.")]
	public int clockCounter = 0;
	
	[Tooltip("The Crystal Counters on this card.")]
	public int crystalCounter = 0;
	
	[Tooltip("The D Counters on this card.")]
	public int dCounter = 0;
	
	[Tooltip("The Destiny Counters on this card.")]
	public int destinyCounter = 0;
	
	[Tooltip("The Dragonic Counters on this card.")]
	public int dragonicCounter = 0;
	
	[Tooltip("The Earthbound Immortal Counters on this card.")]
	public int earthboundImmortalCounter = 0;
	
	[Tooltip("The Flower Counters on this card.")]
	public int flowerCounter = 0;
	
	[Tooltip("The Fog Counters on this card.")]
	public int fogCounter = 0;
	
	[Tooltip("The Gate Counters on this card.")]
	public int gateCounter = 0;
	
	[Tooltip("The Genex Counters on this card.")]
	public int genexCounter = 0;
	
	[Tooltip("The Greed Counters on this card.")]
	public int greedCounter = 0;
	
	[Tooltip("The Guard Counters on this card.")]
	public int guardCounter = 0;
	
	[Tooltip("The Hi-Five The Sky Counters on this card.")]
	public int hiFiveTheSkyCounter = 0;
	
	[Tooltip("The Hyper-Venom Counters on this card.")]
	public int hyperVenomCounter = 0;
	
	[Tooltip("The Ice Counters on this card.")]
	public int iceCounter = 0;
	
	[Tooltip("The Junk Counters on this card.")]
	public int junkCounter = 0;
	
	[Tooltip("The Karakuri Counters on this card.")]
	public int karakuriCounter = 0;
	
	[Tooltip("The Morph Counters on this card.")]
	public int morphCounter = 0;
	
	[Tooltip("The Nut Counters on this card.")]
	public int nutCounter = 0;
	
	[Tooltip("The Ocean Counters on this card.")]
	public int oceanCounter = 0;
	
	[Tooltip("The Payback Counters on this card.")]
	public int paybackCounter = 0;
	
	[Tooltip("The Performage Counters on this card.")]
	public int performageCounter = 0;
	
	[Tooltip("The Plant Counters on this card.")]
	public int plantCounter = 0;
	
	[Tooltip("The Psychic Counters on this card.")]
	public int psychicCounter = 0;
	
	[Tooltip("The Pumpkin Counters on this card.")]
	public int pumpkinCounter = 0;
	
	[Tooltip("The Rising Sun Counters on this card.")]
	public int risingSunCounter = 0;
	
	[Tooltip("The Shark Counters on this card.")]
	public int sharkCounter = 0;
	
	[Tooltip("The Shine Counters on this card.")]
	public int shineCounter = 0;
	
	[Tooltip("The Spell Counters on this card.")]
	public int spellCounter = 0;
	
	[Tooltip("The Spellstone Counters on this card.")]
	public int spellstoneCounter = 0;
	
	[Tooltip("The String Counters on this card.")]
	public int stringCounter = 0;
	
	[Tooltip("The Symphonic Counters on this card.")]
	public int symphonicCounter = 0;
	
	[Tooltip("The Thunder Counters on this card.")]
	public int thunderCounter = 0;
	
	[Tooltip("The Venom Counters on this card.")]
	public int venomCounter = 0;
	
	[Tooltip("The Wedge Counters on this card.")]
	public int wedgeCounter = 0;
	
	[Tooltip("The Worm Counters on this card.")]
	public int wormCounter = 0;
	
	[Tooltip("The Yosen Counters on this card.")]
	public int yosenCounter = 0;
	
	[Tooltip("The You Got It Boss! Counters on this card.")]
	public int youGotItBossCounter = 0;
	
	//----------------------Getters----------------------
	public string GetCardTreatedName()
	{
		return this.cardTreatedName;
	}
	
	public SubAttrabute GetSubAttrabute()
	{
		return this.subAttrabute;
	}
	
	//----------------------Setters----------------------
	public void SetCardTreatedName(string name)
	{
		this.cardTreatedName = name;
	}
	
	public void SetSubAttrabute(SubAttrabute subAttrabute)
	{
		this.subAttrabute = subAttrabute;
	}
	
	//----------------------Other Functions----------------------
	
//	public Card CalculateACounterModifier(Card card)
//	{
//		//Calculate the attack and defense points
//		int tempAttack = card.attack - (300 * aCounters);
//		int tempDefense = card.defense - (300 * aCounters);
//		
//		//If the attack or defense are lower than 0...
//		if(tempAttack < 0)
//		{
//			tempAttack = 0;
//		}
//		
//		if(tempDefense < 0)
//		{
//			tempDefense = 0;
//		}
//		
//		//Store the new values into the card
//		card.attack = tempAttack;
//		card.defense = tempDefense;
//	
//		//Return the card modifications
//		return card;
//	}
}