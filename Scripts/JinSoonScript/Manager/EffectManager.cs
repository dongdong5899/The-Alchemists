//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class EffectManager : Singleton<EffectManager>
//{
//    public List<Effect> effects;
//    public Dictionary<EffectEnum, Effect> effectDic;

//    private void Awake()
//    {
//        effectDic = new Dictionary<EffectEnum, Effect>();
//        foreach (EffectEnum effect in Enum.GetValues(typeof(EffectEnum)))
//        {
//            string effectName = effect.ToString();
//            try
//            {
//                Type t = Type.GetType($"{effectName}Effect");
//                Effect nEffect = Activator.CreateInstance(t) as Effect;

//                effectDic.Add(effect, nEffect);
//            }
//            catch (Exception ex)
//            {
//                Debug.LogError($"{effectName} is loading error!");
//                Debug.LogError(ex);
//            }
//        }
//    }

//    public Effect GetEffect(EffectEnum effect) => effectDic[effect];
//}
