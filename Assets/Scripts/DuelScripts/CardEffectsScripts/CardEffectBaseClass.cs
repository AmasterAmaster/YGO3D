using UnityEngine;
using System.Collections;

public class CardEffectBaseClass : MonoBehaviour
{
	//Finds the GameAPI for use for all cards
	virtual public void SetGameAPI(){}			//Sets the Game API

	//Card Effect virtual functions for all cards (Phases)
	virtual public void DrawPhaseEffect(){}		//Handles draw phase effects here
	virtual public void StandbyPhaseEffect(){}	//Handles standby phase effects here
	virtual public void MainPhase1Effect(){}	//Handles main phase 1 phase effects here
	virtual public void BattlePhaseEffect(){}	//Handles battle phase effects here
	virtual public void StartStepEffect(){}		//Handles start step effects here
	virtual public void BattleStepEffect(){}	//Handles battle step effects here
	virtual public void DamageStepEffect(){}	//Handles damage step effects here
	virtual public void EndStepEffect(){}		//Handles end step effects here
	virtual public void MainPhase2Effect(){}	//Handles main phase 2 effects here
	virtual public void EndPhaseEffect(){}		//Handles end phase effects here
	
	//Card Effect virtual functions for all cards (Activations, chains, and others)
		//Outgoing effects (from this card to other cards)
	virtual public void ActivationEffect(){}		//Handles GENERAL counters, tokens, spell, trap, monster, and any other effects here (handles outgoing negation, targets, destruction, summons, and other effects)
	virtual public void FlipMonsterEffect(){}		//Handles SPECIFIC flip summon effects here (If this card is flip summoned)
	virtual public void TriggerMonaterEffect(){}	//Handles SPECIFIC trigger effects here
	virtual public void ContinuousMonsterEffect(){}	//Handles SPECIFIC continuous effects here
	virtual public void QuickMonsterEffect(){}		//Handles SPECIFIC quick effects here
	virtual public void IgnitionMonsterEffect(){}	//Handles SPECIFIC ignition effects here
	virtual public void RitualMonsterEffect(){}		//Handles SPECIFIC ritual monster effects here
	virtual public void FusionMonsterEffect(){}		//Handles SPECIFIC fusion monster effects here
	virtual public void SynchroMonsterEffect(){}	//Handles SPECIFIC synchro monster effects here
	virtual public void XYZMonsterEffect(){}		//Handles SPECIFIC xyz monster effects here
	virtual public void PendulumMonsterEffect(){}	//Handles SPECIFIC pendulum monster effects here
	virtual public void PendulumSpellEffect(){}		//Handles SPECIFIC pendulum spell effects here
	virtual public void NormalSpellEffect(){}		//Handles SPECIFIC normal spell effects here
	virtual public void QuickPlaySpellEffect(){}	//Handles SPECIFIC quick play spell effects here
	virtual public void ContinuousSpellEffect(){}	//Handles SPECIFIC continuous spell effects here
	virtual public void EquipSpellEffect(){}		//Handles SPECIFIC equip spell effects here
	virtual public void FieldSpellEffect(){}		//Handles SPECIFIC field spell effects here
	virtual public void RitualSpellEffect(){}		//Handles SPECIFIC ritual spell effects here
	virtual public void NormalTrapEffect(){}		//Handles SPECIFIC normal trap effects here
	virtual public void ContinuousTrapEffect(){}	//Handles SPECIFIC continuous trap effects here
	virtual public void CounterTrapEffect(){}		//Handles SPECIFIC counter trap effects here
		//Incoming effects (from other cards to this card)
	virtual public void ChainedEffect(){}			//Handles chaining effects here
	virtual public void TargetedEffect(){}			//Handles targeted effects here
	virtual public void DestroyedEffect(){}			//Handles destroyed effects here (If this card is destroyed)
	virtual public void SentEffect(){}				//Handles sent effects here (if this card is sent somewhere (includes the graveyard, hand, and deck))
	virtual public void DrawnEffect(){}				//Handles drawing effects here (If this card is drawn)
	virtual public void DiscardedEffect(){}			//Handles discard effects here (If this card is discarded)
	virtual public void BanishedEffect(){}			//Handles banish effects here (if this card is banished)
	virtual public void NegatedEffect(){}			//Handles negation effects here (If this card is negated)
	virtual public void HandEffect(){}				//Handles hand effects here (if this card is in the hand)
		//Summoning effects ("When this card is summoned...")
	virtual public void NormalSummonedEffect(){}	//Handles the normal summon effects here (If this card is normal summoned)
	virtual public void NormalSetEffect(){}			//Handles the normal set effects here (If this card is normal set)
	virtual public void SpecialSummonedEffect(){}	//Handles the special summon effects here (If this card is special summoned)
	virtual public void SpecialSetEffect(){}		//Handles the special set effects here (If this card is special set)
	virtual public void TributeSummonedEffect(){}	//Handles the tribute summon effects here (If this card is tribute summoned)
	virtual public void TributeSetEffect(){}		//Handles the tribute set effects here (If this card is tribute set)
	virtual public void RitualSummonedEffect(){}	//Handles the ritual summon effects here (If this card is ritual summoned)
	virtual public void FusionSummonedEffect(){}	//Handles the fusion summon effects here (If this card is fusion summoned)
	virtual public void SynchroSummonedEffect(){}	//Handles the synchro summon effects here (If this card is synchro summoned)
	virtual public void XYZSummonedEffect(){}		//Handles the xyz summon effects here (If this card is xyz summoned)
	virtual public void PendulumSummonedEffect(){}	//Handles the pendulum summon effects here (If this card is pendulum summoned)
	virtual public void FlipFaceUpEffect(){}		//Handles the flip face-up effects here (If this card is flipped face-up by whatever reason)
		//Other effects (such as in-game mechanics)
	virtual public void BattlePositionChangeEffect(){}	//Handles the effects that activate when a battle position changes this card
}