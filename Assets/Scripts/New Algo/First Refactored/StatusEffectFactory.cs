using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatusEffectFactory : MonoBehaviour, IFactory
{
    #region Scripts
    #endregion

    #region Game object references
    #endregion

    #region Factory functions
    // turns, level, target, modifier need to create afterwards?
    public StatusEffect CreateStatusEffect(int ID, string name, string desc, string type, string iconPicName, 
                        float susValue, string[] statsWithModifier, string[] statsModifierType, int[] statsModifierOrder, int[] statsModifierValue)
    {


        StatusEffect statusEffect = new StatusEffect();

        statusEffect.effectID = ID;
        statusEffect.effectName = name;
        statusEffect.effectDesc = desc;
        statusEffect.effectType = type;
        statusEffect.effectIconPicName = iconPicName;
        //this.instantValue = insValue;
        statusEffect.sustainValue = susValue;

        //statusEffect.effectTurns = turns;
        //statusEffect.effectLevel = level;
        //statusEffect.effectTarget = target;

        statusEffect.affectedStatsWithModifier = new List<string>();
        statusEffect.affectedStatsModifierType = new List<string>();
        statusEffect.affectedStatsModifierOrder = new List<int>();
        statusEffect.affectedStatsModifierValue = new List<int>();
        statusEffect.affectedStatsModifier = new List<StatModifier>();

        // For adding data into the list
        if (statsWithModifier.Length != 0)
        {
            for (int i = 0; i < statsWithModifier.Length; i++)
            {
                statusEffect.affectedStatsWithModifier.Add(statsWithModifier[i]);
            }
        }

        if (statsModifierOrder.Length != 0)
        {
            for (int i = 0; i < statsModifierOrder.Length; i++)
            {
                statusEffect.affectedStatsModifierOrder.Add(statsModifierOrder[i]);
            }
        }

        if (statsModifierValue.Length != 0)
        {
            for (int i = 0; i < statsModifierValue.Length; i++)
            {
                statusEffect.affectedStatsModifierValue.Add(statsModifierValue[i]);
            }
        }

        for (int i = 0; i < statsModifierType.Length; i++)
        {
            StatModifierType statModType = new StatModifierType();

            switch (statsModifierType[i])
            {
                case "Flat":
                    statModType = StatModifierType.Flat;
                    break;

                case "PercentStack":
                    statModType = StatModifierType.PercentStack;
                    break;
                
                case "PercentMultiple":
                    statModType = StatModifierType.PercentMultiple;
                    break;

                case "Equal":
                    statModType = StatModifierType.Equal;
                    break;
            }

            statusEffect.affectedStatsModifier.Add(new StatModifier(statusEffect.affectedStatsModifierValue[i], statModType,statusEffect.affectedStatsModifierOrder[i]));
            
        } 

        //statusEffect.effectRemainingTurns = turns + 1;
    
        return statusEffect;
    }

    //public void 
    #endregion
}