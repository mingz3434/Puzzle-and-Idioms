using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class ImportData : MonoBehaviour
{
    #region JSON data reference
    // Import data (in json format) to the game
    public TextAsset idiomDataJson;
    public TextAsset popupDataJson;
    public TextAsset mobDataJson;
    public TextAsset teammateDataJson;
    public TextAsset tileEffectDataJson;
    public TextAsset statusEffectDataJson;
    public TextAsset abilityDataJson;
    #endregion

    #region Data array reference
    // Create array for data <-shit way I know
    public static IdiomList idioms;
    public static PopupList popups;
    public static MobList mobs;
    public static TeammateList teammates;

    public static TileEffectList tileEffectsData;
    public static StatusEffectList statusEffectsData;
    public static AbilityList abilitiesData;

    public static Dictionary<int, TileEffect> tileEffectDictionary;
    public static Dictionary<int, StatusEffect> statusEffectDictionary;
    public static Dictionary<int, Ability> abilityDictionary;
    #endregion

    #region Factories
    private TileEffectFactory tileEffectFactory;
    private StatusEffectFactory statusEffectFactory;
    private AbilityFactory abilityFactory;
    #endregion

    #region Flow
    void Awake()
    {
        tileEffectFactory = gameObject.GetComponent<TileEffectFactory>();
        statusEffectFactory = gameObject.GetComponent<StatusEffectFactory>();
        abilityFactory = gameObject.GetComponent<AbilityFactory>();

        InitalizingIdiomData(); InitalizingPopupData(); InitalizingMobData();

        InitalizingTeammateData(); InitalizingTileEffectData(); InitalizingStatusEffectData(); InitalizingAbilityData();

        InitalizingTileEffectDict(); InitalizingStatusEffectDict(); InitalizingAbilityDict();
    }
    #endregion

    public void InitalizingIdiomData() { idioms = JsonUtility.FromJson<IdiomList>(idiomDataJson.text); var go = GameObject.Find("mzDirector").GetComponent<_Director>(); go.i.setLoadedIdioms(); }
    public void InitalizingPopupData() { popups = JsonUtility.FromJson<PopupList>(popupDataJson.text); }
    public void InitalizingMobData() { mobs = JsonUtility.FromJson<MobList>(mobDataJson.text); }
    public void InitalizingTeammateData() { teammates = JsonUtility.FromJson<TeammateList>(teammateDataJson.text); }
    public void InitalizingTileEffectData() { tileEffectsData = JsonUtility.FromJson<TileEffectList>(tileEffectDataJson.text); }
    public void InitalizingStatusEffectData() { statusEffectsData = JsonUtility.FromJson<StatusEffectList>(statusEffectDataJson.text); }
    public void InitalizingAbilityData() { abilitiesData = JsonUtility.FromJson<AbilityList>(abilityDataJson.text); }

    #region Convert data into actual object (for ability/status effect, which is not an actual game object)
    public void InitalizingTileEffectDict()
    {
        tileEffectDictionary = new Dictionary<int, TileEffect>();
        if (tileEffectsData.tileEffectData.Length != 0)
        {
            for (int i = 0; i < tileEffectsData.tileEffectData.Length; i++)
            {
                tileEffectDictionary.Add(tileEffectsData.tileEffectData[i].tileEffectID,
                tileEffectFactory.CreateTileEffect
                (
                tileEffectsData.tileEffectData[i].tileEffectID,
                tileEffectsData.tileEffectData[i].tileEffectName, 
                tileEffectsData.tileEffectData[i].tileEffectDesc,
                tileEffectsData.tileEffectData[i].tileEffectType,
                tileEffectsData.tileEffectData[i].tileEffectIconPicName
                ));
            }
        }
    }
    public void InitalizingStatusEffectDict()
    {
        statusEffectDictionary = new Dictionary<int, StatusEffect>();
        if (statusEffectsData.statusEffectData.Length != 0)
        {
            for (int i = 0; i < statusEffectsData.statusEffectData.Length; i++)
            {
                statusEffectDictionary.Add(statusEffectsData.statusEffectData[i].effectID,
                statusEffectFactory.CreateStatusEffect
                (
                statusEffectsData.statusEffectData[i].effectID,
                statusEffectsData.statusEffectData[i].effectName, 
                statusEffectsData.statusEffectData[i].effectDesc,
                statusEffectsData.statusEffectData[i].effectType,
                statusEffectsData.statusEffectData[i].effectIconPicName,
                statusEffectsData.statusEffectData[i].sustainValue,
                statusEffectsData.statusEffectData[i].affectedStatsWithModifier,
                statusEffectsData.statusEffectData[i].affectedStatsModifierType,
                statusEffectsData.statusEffectData[i].affectedStatsModifierOrder,
                statusEffectsData.statusEffectData[i].affectedStatsModifierValue
                ));
            }
        }
    }

    public void InitalizingAbilityDict()
    {
        abilityDictionary = new Dictionary<int, Ability>();
        if (abilitiesData.abilityData.Length != 0)
        {
            for (int i = 0; i < abilitiesData.abilityData.Length; i++)
            {
                abilityDictionary.Add(abilitiesData.abilityData[i].abilityID,
                abilityFactory.CreateAbility
                (
                abilitiesData.abilityData[i].abilityID,
                abilitiesData.abilityData[i].abilityName, 
                abilitiesData.abilityData[i].abilityDesc,
                abilitiesData.abilityData[i].abilityType,
                abilitiesData.abilityData[i].baseAbilityCD,
                abilitiesData.abilityData[i].baseAbilityCost,
                abilitiesData.abilityData[i].selfStatusEffectsID,
                abilitiesData.abilityData[i].selfStatusEffectsTurns,
                abilitiesData.abilityData[i].selfAffectedStats,
                abilitiesData.abilityData[i].selfAffectedStatsValue,
                abilitiesData.abilityData[i].enemyStatusEffectsID,
                abilitiesData.abilityData[i].enemyStatusEffectsTurns,
                abilitiesData.abilityData[i].enemyAffectedStats,
                abilitiesData.abilityData[i].enemyAffectedStatsValue
                ));
            }
        }
    }
    #endregion
}



