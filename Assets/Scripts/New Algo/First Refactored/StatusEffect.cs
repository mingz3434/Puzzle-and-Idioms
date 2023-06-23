using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    #region Scripts
    public RoundData roundData;
    #endregion

    #region Game object references
    public EffectBox effectBox;
    #endregion

    #region Status effect data
    private int _effectID;
    public int effectID { get { return _effectID; } set { _effectID = value; } }
    private string _effectName;
    public string effectName { get { return _effectName; } set { _effectName = value; } }
    private string _effectDesc;
    public string effectDesc { get { return _effectDesc; } set { _effectDesc = value; } }
    private string _effectType;
    public string effectType { get { return _effectType; } set { _effectType = value; } }
    private string _effectIconPicName;
    public string effectIconPicName { get { return _effectIconPicName; } set { _effectIconPicName = value; } }
    
    // Damage within turns
    //private float _instantValue;
    //public float instantValue { get { return _instantValue; } set { _instantValue = value; } }
    private float _sustainValue;
    public float sustainValue { get { return _sustainValue; } set { _sustainValue = value; } }

    private Entity _effectTarget;
    public Entity effectTarget { get { return _effectTarget; } set { _effectTarget = value; } }
    private int _effectTurns;
    public int effectTurns { get { return _effectTurns; } set { _effectTurns = value; } }
    private int _effectRemainingTurns;
    public int effectRemainingTurns { get { return _effectRemainingTurns; } set { _effectRemainingTurns = value; } }
    private int _effectLevel;
    public int effectLevel { get { return _effectLevel; } set { _effectLevel = value; } }

    private List<string> _affectedStatsWithModifier;
    public List<string> affectedStatsWithModifier { get { return _affectedStatsWithModifier; } set { _affectedStatsWithModifier = value; } }
    private List<string> _affectedStatsModifierType;
    public List<string> affectedStatsModifierType { get { return _affectedStatsModifierType; } set { _affectedStatsModifierType = value; } }
    private List<int> _affectedStatsModifierOrder;
    public List<int> affectedStatsModifierOrder { get { return _affectedStatsModifierOrder; } set { _affectedStatsModifierOrder = value; } }
    private List<int> _affectedStatsModifierValue;
    public List<int> affectedStatsModifierValue { get { return _affectedStatsModifierValue; } set { _affectedStatsModifierValue = value; } }
    private List<StatModifier> _affectedStatsModifier;
    public List<StatModifier> affectedStatsModifier { get { return _affectedStatsModifier; } set { _affectedStatsModifier = value; } }



    #endregion


    #region Status effect functions
    // Within the caster's move, which giving status effect to the target
    // Called only once
    public virtual void OnInflict()
    {

        // Check if there is the same status effect from the target
        if (HasSameStatusEffect())
        {
            effectTarget.currentStatusEffects[GetOrderOfStatusEffect()].OnRemove();
        }

        // Check if there are more than 5 status effects from the target
        if (effectTarget.currentStatusEffects.Count >= 5)
        {
            // Temp solution, just remove the oldest effect
            effectTarget.currentStatusEffects[0].OnRemove();
        }

        // Add the new status effect into the list
        effectTarget.currentStatusEffects.Add(this);
        effectTarget.hasStatusEffect = true;

        // Get the remaining turns for the status effect
        effectRemainingTurns = effectTurns;

        // Apply new StatModifier to the target's stat
        //if (hasModifiedStats)
        {
            for (int i = 0; i < affectedStatsWithModifier.Count; i++)
            {
                switch (affectedStatsWithModifier[i])
                {
                    case "currentMaxHealthPoint":
                        effectTarget.currentMaxHealthPoint.AddModifier(affectedStatsModifier[i]);
                        break;

                    case "currentAttackPoint":
                        effectTarget.currentAttackPoint.AddModifier(affectedStatsModifier[i]);
                        break;

                    case "currentDefencePoint":
                        effectTarget.currentDefencePoint.AddModifier(affectedStatsModifier[i]);
                        break;

                    case "currentPerceptionPoint":
                        effectTarget.currentPerceptionPoint.AddModifier(affectedStatsModifier[i]);
                        break;

                    case "currentDexterityPoint":
                        effectTarget.currentDexterityPoint.AddModifier(affectedStatsModifier[i]);
                        break;

                    case "currentConstitutionPoint":
                        effectTarget.currentConstitutionPoint.AddModifier(affectedStatsModifier[i]);
                        break;

                    case "currentMaxAttackInterval":
                        effectTarget.GetComponent<Mob>().currentMaxAttackInterval.AddModifier(affectedStatsModifier[i]);
                        break;

                    case "currentMaxActiveAbilityCD":
                        foreach (KeyValuePair<TeammateType, Teammate> teammate in effectTarget.GetComponent<Player>().teammates)
                        {
                            teammate.Value.currentMaxActiveAbilityCD.AddModifier(affectedStatsModifier[i]);
                        }
                        break;
                        
                    // And more...
                }
            }
        }
        
        roundData = GameObject.Find("Round Manager").GetComponent<RoundData>();
        effectBox = roundData.effectBoxFactory.CreateEffectBox(effectTarget, this, GetOrderOfStatusEffect());


    }

    // On every new turn
    public virtual void OnBeforeTurnStart()
    {
        effectRemainingTurns -= 1;
        effectTarget.TakeDamage(sustainValue);
    }

    // On every turn ending public virtual 
    public virtual void OnTurnEnd() { if (effectRemainingTurns <= 0) { OnRemove(); } }

    // On removing itself
    public virtual void OnRemove()
    {

        // Remove icon and CD text in effect box/arena
        effectBox.DestroyEffectBox();

        // Remove the statModifier from the target
        //if (hasModifiedStats)
        {
            for (int i = 0; i < affectedStatsWithModifier.Count; i++)
            {
                switch (this.affectedStatsWithModifier[i])
                {
                    case "currentMaxHealthPoint":
                        effectTarget.currentMaxHealthPoint.RemoveModifier(affectedStatsModifier[i]);
                        break;

                    case "currentAttackPoint":
                        effectTarget.currentAttackPoint.RemoveModifier(affectedStatsModifier[i]);
                        break;

                    case "currentDefencePoint":
                        effectTarget.currentDefencePoint.RemoveModifier(affectedStatsModifier[i]);
                        break;

                    case "currentPerceptionPoint":
                        effectTarget.currentPerceptionPoint.RemoveModifier(affectedStatsModifier[i]);
                        break;

                    case "currentDexterityPoint":
                        effectTarget.currentDexterityPoint.RemoveModifier(affectedStatsModifier[i]);
                        break;

                    case "currentConstitutionPoint":
                        effectTarget.currentConstitutionPoint.RemoveModifier(affectedStatsModifier[i]);
                        break;

                    case "currentMaxAttackInterval":
                        effectTarget.GetComponent<Mob>().currentMaxAttackInterval.RemoveModifier(affectedStatsModifier[i]);
                        break;

                    case "currentMaxActiveAbilityCD":
                        foreach (KeyValuePair<TeammateType, Teammate> teammate in effectTarget.GetComponent<Player>().teammates)
                        {
                            teammate.Value.currentMaxActiveAbilityCD.RemoveModifier(affectedStatsModifier[i]);
                        }
                        break;

                    // And more...
                }
            }
        }

        //effectTarget.currentStatusEffects.Remove(this);
        effectTarget.currentStatusEffects.Remove(this);
        if (effectTarget.currentStatusEffects.Count == 0)
        {
            effectTarget.hasStatusEffect = false;
        }

    }

    public bool HasSameStatusEffect()
    {
        bool hasSameEffect = false;

        //Debug.Log($"$effectTarget.name {effectTarget.name}");

        if (effectTarget != null)
        {
            if (effectTarget.currentStatusEffects != null)
            {
                foreach (StatusEffect statusEffect in effectTarget.currentStatusEffects)
                {
                    if (statusEffect.effectName == this.effectName)
                    {
                        hasSameEffect = true;
                    }
                }
            }
        }
        
        return hasSameEffect;
    }

    public int GetOrderOfStatusEffect()
    {
        int orderOfStatusEffect = -1;
        for (int i = 0; i <= (effectTarget.currentStatusEffects.Count - 1); i++)
        {
            if (effectTarget.currentStatusEffects[i].effectName == this.effectName)
            {
                orderOfStatusEffect = i;
            }
        }

        return orderOfStatusEffect;
    }
    #endregion
}
