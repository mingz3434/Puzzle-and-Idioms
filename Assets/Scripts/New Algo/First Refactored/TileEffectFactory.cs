using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TileEffectFactory : MonoBehaviour, IFactory
{
    #region Factory functions
    public TileEffect CreateTileEffect(int id, string name, string desc, string effectType, string effectIconPicName)
    {


        TileEffect tileEffect = new TileEffect();

        tileEffect.tileEffectID = id;
        tileEffect.tileEffectName = name;
        tileEffect.tileEffectDesc = desc;
        tileEffect.tileEffectType = effectType;
        tileEffect.tileEffectIconPicName = effectIconPicName;

        return tileEffect;
    }

    #endregion
}