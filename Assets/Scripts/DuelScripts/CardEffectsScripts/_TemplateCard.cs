using UnityEngine;
using System.Collections;

/**
/ With this template class, each and every card can be created with some simple logic (to make actions happen).
/
/ To activate a card effect during any phase, put this in the generic activation override function (in this case, on the draw phase and on the field only):

/ if(gapi.GetCurrentPhase() == "Draw Phase" && this.gameObject.GetComponent<ExtraCardProperties>().currentState == ExtraCardProperties.CurrentState.field)
/ {
/     DrawPhaseEffect();
/ }
/
/ To activate a card effect during a summon:
/
/ To...
/
*/
public class _TemplateCard : CardEffectBaseClass
{
	//-----------------------Reference to GameAPI----------------------------------------------------------
	public GameAPI gapi;
	
	//Sets the Game API
	override public void SetGameAPI()
	{
		if(GameObject.Find("GameManager").GetComponent<GameAPI>() != null)
			gapi = GameObject.Find("GameManager").GetComponent<GameAPI>();
	}
	
	//-----------------------Virtual Functions (can be implemented)----------------------------------------
		//Card Effect Virtual functions for all cards (Phases)
	//Handles draw phase effects here
	override public void DrawPhaseEffect()
	{
	
	}
	
	//Handles standby phase effects here
	override public void StandbyPhaseEffect()
	{
	
	}
	
	//Handles main phase 1 phase effects here
	override public void MainPhase1Effect()
	{
	
	}
	
	//Handles battle phase effects here
	override public void BattlePhaseEffect()
	{
	
	}
	
	//Handles start step effects here
	override public void StartStepEffect()
	{
	
	}
	
	//Handles battle step effects here
	override public void BattleStepEffect()
	{
	
	}
	
	//Handles damage step effects here
	override public void DamageStepEffect()
	{
	
	}
	
	//Handles end step effects here
	override public void EndStepEffect()
	{
	
	}
	
	//Handles main phase 2 effects here
	override public void MainPhase2Effect()
	{
	
	}
	
	//Handles end phase effects here
	override public void EndPhaseEffect()
	{
	
	}
	
	//Card Effect Virtual functions for all cards (Activations, chains, and others)
		//Outgoing effects (from this card to other cards)
	//Handles GENERAL counters, tokens, spell, trap, monster, and any other effects here (handles outgoing negation, targets, destruction, summons, and other effects)
	override public void ActivationEffect()
	{
		//Responsible for calling other functions when needed, else this function can handle genaric things in the game (the other called functions must check if the correct phase is selected or any other conditions)
	}
	
	//Handles SPECIFIC flip summon effects here (If this card is flip summoned)
	override public void FlipMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC trigger effects here
	override public void TriggerMonaterEffect()
	{
		
	}
	
	//Handles SPECIFIC continuous effects here
	override public void ContinuousMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC quick effects here
	override public void QuickMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC ignition effects here
	override public void IgnitionMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC ritual monster effects here
	override public void RitualMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC fusion monster effects here
	override public void FusionMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC synchro monster effects here
	override public void SynchroMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC xyz monster effects here
	override public void XYZMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC pendulum monster effects here
	override public void PendulumMonsterEffect()
	{
		
	}
	
	//Handles SPECIFIC pendulum spell effects here
	override public void PendulumSpellEffect()
	{
		
	}
	
	//Handles SPECIFIC normal spell effects here
	override public void NormalSpellEffect()
	{
		
	}
	
	//Handles SPECIFIC quick play spell effects here
	override public void QuickPlaySpellEffect()
	{
		
	}
	
	//Handles SPECIFIC continuous spell effects here
	override public void ContinuousSpellEffect()
	{
		
	}
	
	//Handles SPECIFIC equip spell effects here
	override public void EquipSpellEffect()
	{
		
	}
	
	//Handles SPECIFIC field spell effects here
	override public void FieldSpellEffect()
	{
		
	}
	
	//Handles SPECIFIC ritual spell effects here
	override public void RitualSpellEffect()
	{
		
	}
	
	//Handles SPECIFIC normal trap effects here
	override public void NormalTrapEffect()
	{
		
	}
	
	//Handles SPECIFIC continuous trap effects here
	override public void ContinuousTrapEffect()
	{
		
	}
	
	//Handles SPECIFIC counter trap effects here
	override public void CounterTrapEffect()
	{
		
	}
	
		//Incoming effects (from other cards to this card)
	//Handles chaining effects here
	override public void ChainedEffect()
	{
		
	}
	
	//Handles targeted effects here
	override public void TargetedEffect()
	{
		
	}
	
	//Handles destroyed effects here (If this card is destroyed)
	override public void DestroyedEffect()
	{
		
	}
	
	//Handles sent effects here (if this card is sent somewhere (includes the graveyard, hand, and deck))
	override public void SentEffect()
	{
		
	}
	
	//Handles drawing effects here (If this card is drawn)
	override public void DrawnEffect()
	{
		
	}
	
	//Handles discard effects here (If this card is discarded)
	override public void DiscardedEffect()
	{
		
	}
	
	//Handles banish effects here (if this card is banished)
	override public void BanishedEffect()
	{
		
	}
	
	//Handles negation effects here (If this card is negated)
	override public void NegatedEffect()
	{
		
	}
	
	//Handles hand effects here (if this card is in the hand)
	override public void HandEffect()
	{
		
	}
	
		//Summoning effects ("When this card is summoned...")
	//Handles the normal summon effects here (If this card is normal summoned)
	override public void NormalSummonedEffect()
	{
		
	}
	
	//Handles the normal set effects here (If this card is normal set)
	override public void NormalSetEffect()
	{
		
	}
	
	//Handles the special summon effects here (If this card is special summoned)
	override public void SpecialSummonedEffect()
	{
		
	}
	
	//Handles the special set effects here (If this card is special set)
	override public void SpecialSetEffect()
	{
		
	}
	
	//Handles the tribute summon effects here (If this card is tribute summoned)
	override public void TributeSummonedEffect()
	{
		
	}
	
	//Handles the tribute set effects here (If this card is tribute set)
	override public void TributeSetEffect()
	{
		
	}
	
	//Handles the ritual summon effects here (If this card is ritual summoned)
	override public void RitualSummonedEffect()
	{
		
	}
	
	//Handles the fusion summon effects here (If this card is fusion summoned)
	override public void FusionSummonedEffect()
	{
		
	}
	
	//Handles the synchro summon effects here (If this card is synchro summoned)
	override public void SynchroSummonedEffect()
	{
		
	}
	
	//Handles the xyz summon effects here (If this card is xyz summoned)
	override public void XYZSummonedEffect()
	{
		
	}
	
	//Handles the pendulum summon effects here (If this card is pendulum summoned)
	override public void PendulumSummonedEffect()
	{
		
	}
	
	//Handles the flip face-up effects here (If this card is flipped face-up by whatever reason)
	override public void FlipFaceUpEffect()
	{
		
	}
	
		//Other effects (such as in-game mechanics)
	//Handles the effects that activate when a battle position changes this card
	override public void BattlePositionChangeEffect()
	{
		
	}
}