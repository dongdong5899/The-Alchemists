//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SwordUpgrade : MonoBehaviour
//{
//    private EntityStat entityStat;
//    private Stat stat;
//    //private ParticleSystem upgradeParticle;
//    private Enum enumState;

//    private void Awake()
//    {
//        entityStat = FindAnyObjectByType<EntityStat>();
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.LeftAlt))
//        {
//            //upgradeParticle.Play();

//            enumState = EffectEnum.Freeze;

//            switch (enumState)
//            {
//                case EffectEnum.Freeze:
//                    Debug.Log("asdf");
//                    entityStat.GetDamage();
//                    break;
//                case EffectEnum.FreezePrevent:
//                    Debug.Log("asdasd");
//                    entityStat.GetDamage();
//                    break;
//            }
//        }
//    }
//}
