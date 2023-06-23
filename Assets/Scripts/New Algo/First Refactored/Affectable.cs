using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Affectable : MonoBehaviour 
{
    //[SerializeField] public Dictionary<StatusEffectName, StatusEffect> currentStatusEffects = new Dictionary<StatusEffectName, StatusEffect>();
    public List<StatusEffect> currentStatusEffects = new List<StatusEffect>();
    public bool hasStatusEffect = false;
    public bool isSkipTurn = false;


}

