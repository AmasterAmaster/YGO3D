using UnityEngine;
using System.Collections;

public class GameAPI : MonoBehaviour
{
	//Components for game API
	public MainGameScript mgs;
	public SummonScript summon;
	public AttackScript attack;
	public FieldScript field;
	public TargetAndNotificationSystemScript tans;
	public ActivateScript activate;
	
	//------------------------GETTERS------------------------------
	public int GetCurrentTurn()
	{
		return mgs.turnCounter;
	}
	
	public Player GetCurrentPlayersTurn()
	{
		if(mgs.player1Turn)
			return mgs.player1;
		else if(mgs.player2Turn)
			return mgs.player2;
		else
			return null;
	}
	
	public string GetCurrentPhase()
	{
		return mgs.GetPhaseString();
	}
	
	public string GetCurrentBattleStep()
	{
		return mgs.GetBattleStepString();
	}
	
	//------------------------CORE GAME API------------------------
	public void Draw(Player playerDrawing, int amount = 1, bool autoCardPlacement = false)
	{
		mgs.Draw(playerDrawing, amount, autoCardPlacement);
	}
	
	public void Discard(Player player, int amount = 1, bool randomDiscard = false)
	{
		mgs.Discard(player, amount, randomDiscard);
	}
	
	public void Destroy()
	{
		//mgs.Destroy();
	}
	
	public void Banish()
	{
		//mgs.Banish();
	}
	
	public void ShuffleDeck(Deck deck)
	{
		mgs.ShuffleDeck(deck);
	}
	
	public void ShuffleHand(Player player)
	{
		mgs.ShuffleHand(player);
	}
	
	public void CheckAllCardEffects()
	{
		mgs.CheckAllCardEffects();
	}
	
	public void BattlePositionChange()
	{
		mgs.BattlePositionChange();
	}
	
	public void CheckForWinCondition()
	{
		mgs.CheckForWinCondition();
	}
	
	public void Gameover(Player losingPlayer)
	{
		mgs.Gameover(losingPlayer);
	}
	
	public void ViewCard()
	{
		mgs.ViewCard();
	}
	
	//------------------------FIELD API------------------------
	public void CheckFieldValidity()
	{
		field.CheckFieldValidity();
	}
	
	//------------------------SUMMONING API------------------------
	public void Summon()
	{
		summon.Summon();
	}
	
	public void RitualSummon()
	{
		summon.RitualSummon();
	}
	
	public void FusionSummon()
	{
		summon.FusionSummon();
	}
	
	public void SynchroSummon()
	{
		summon.SynchroSummon();
	}
	
	public void XYZSummon()
	{
		summon.XYZSummon();
	}
	
	public void PendulumSummon()
	{
		summon.PendulumSummon();
	}
	
	//------------------------ATTACK/BATTLE API------------------------
	public void Attack()
	{
		attack.Attack();
	}
	
	public void Battle()
	{
		attack.Battle();
	}
	
	public void TurnOnBattleChanges()
	{
		attack.TurnOnBattleChanges();
	}
	
	public void TurnOnAttacking()
	{
		attack.TurnOnAttacking();
	}
	
	//------------------------TARGET/NOTIFCATION SYSTEM API------------------------
	public void CheckForTributableMonsters()
	{
		tans.CheckForTributableMonsters();
	}
	
	public void NotifyUserOfAttackingCards()
	{
		tans.NotifyUserOfAttackingCards();
	}
	
	public void NotifyUserOfTargetableCardsForAttacking()
	{
		tans.NotifyUserOfTargetableCardsForAttacking();
	}
	
	//Should be a catch all for any target from any card effect
	public void CheckForTargetableCards(string command = "")
	{
		tans.CheckForTargetableCards(command);
	}
	
	public void NotifyUserOfDiscardingCards()
	{
		tans.NotifyUserOfDiscardingCards();
	}
	
	//------------------------ACTIVATE API------------------------
	public void Activate()
	{
		activate.Activate();
	}
	
	public void ActivateMonsterEffect()
	{
		activate.ActivateMonsterEffect();
	}
	
	public void ActivateSpellOrTrapCardFromField()
	{
		activate.ActivateSpellOrTrapCardFromField();
	}
	
	public void ActivateSpellOrTrapCardFromHand()
	{
		activate.ActivateSpellOrTrapCardFromHand();
	}
	
	//------------------------CHAIN API------------------------
}