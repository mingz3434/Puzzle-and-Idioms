using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AbilityFactory : MonoBehaviour, IFactory
{
    #region Scripts
    #endregion

    #region Game object references
    #endregion

    #region Factory functions
    // level need to create afterwards?
    public Ability CreateAbility(int id, string name, string desc, string abilityType, int CD, int cost, 
                   int[] selfStatusEffectsID, int[] selfStatusEffectsTurns, string[] selfAffectedStats, int[] selfAffectedStatsValue,
                   int[] enemyStatusEffectsID, int[] enemyStatusEffectsTurns, string[] enemyAffectedStats, int[] enemyAffectedStatsValue)
    {
       
        Ability ability = new Ability();

        ability.abilityID = id;
        ability.abilityName = name;
        ability.abilityDesc = desc;
        ability.abilityType = abilityType;
        ability.baseAbilityCD = CD;
        ability.baseAbilityCost = cost;

        ability.selfStatusEffectsID = new List<int>();
        ability.selfStatusEffectsTurns = new List<int>();
        ability.selfAffectedStats = new List<string>();
        ability.selfAffectedStatsValue = new List<int>();

        ability.enemyStatusEffectsID = new List<int>();
        ability.enemyStatusEffectsTurns = new List<int>();
        ability.enemyAffectedStats = new List<string>();
        ability.enemyAffectedStatsValue = new List<int>();


        ability.selfStatusEffects = new List<StatusEffect>();
        ability.enemyStatusEffects = new List<StatusEffect>();

        // For adding data into the lists
        if (selfStatusEffectsID.Length != 0)
        {
            for (int i = 0; i < selfStatusEffectsID.Length; i++)
            {
                ability.selfStatusEffectsID.Add(selfStatusEffectsID[i]);
            }
        }

        if (selfStatusEffectsTurns.Length != 0)
        {
            for (int i = 0; i < selfStatusEffectsTurns.Length; i++)
            {
                ability.selfStatusEffectsTurns.Add(selfStatusEffectsTurns[i]);
            }
        }

        if (selfAffectedStats.Length != 0)
        {
            for (int i = 0; i < selfAffectedStats.Length; i++)
            {
                ability.selfAffectedStats.Add(selfAffectedStats[i]);
            }
        }
        
        if (selfAffectedStatsValue.Length != 0)
        {
            for (int i = 0; i < selfAffectedStatsValue.Length; i++)
            {
                ability.selfAffectedStatsValue.Add(selfAffectedStatsValue[i]);
            }
        }

        if (enemyStatusEffectsID.Length != 0)
        {
            for (int i = 0; i < enemyStatusEffectsID.Length; i++)
            {
                ability.enemyStatusEffectsID.Add(enemyStatusEffectsID[i]);
            }
        }

        if (enemyStatusEffectsTurns.Length != 0)
        {
            for (int i = 0; i < enemyStatusEffectsTurns.Length; i++)
            {
                ability.enemyStatusEffectsTurns.Add(enemyStatusEffectsTurns[i]);
            }
        }

        if (enemyAffectedStats.Length != 0)
        {
            for (int i = 0; i < enemyAffectedStats.Length; i++)
            {
                ability.enemyAffectedStats.Add(enemyAffectedStats[i]);
            }
        }

        if (enemyAffectedStatsValue.Length != 0)
        {
            for (int i = 0; i < enemyAffectedStatsValue.Length; i++)
            {
                ability.enemyAffectedStatsValue.Add(enemyAffectedStatsValue[i]);
            }
        }

        // For adding data into the self status effects list
        if (selfStatusEffectsID.Length != 0)
        {
            for (int i = 0; i < selfStatusEffectsID.Length; i++)
            {
                ability.selfStatusEffects.Add(ImportData.statusEffectDictionary[selfStatusEffectsID[i]]);
            }
        }

        // For adding data into the enemy status effects list
        if (enemyStatusEffectsID.Length != 0)
        {
            for (int i = 0; i < enemyStatusEffectsID.Length; i++)
            {
                ability.enemyStatusEffects.Add(ImportData.statusEffectDictionary[enemyStatusEffectsID[i]]);
            }
        }

        return ability;
    }

    //public void 
    #endregion
}