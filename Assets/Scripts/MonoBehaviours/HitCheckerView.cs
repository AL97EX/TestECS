using System;
using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace MonoBehaviours
{
    public class HitCheckerView : MonoBehaviour
    {
        public EcsWorld ecsWorld { get; set; }
        private int hit;
        private EcsPool<HitComponent> hitPool;
        private EcsPool<ButtonComponent> buttonPool;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.BUTTON_TAG))
            {
                Debug.Log("OnButtonEnter");
            }
            
            hit = ecsWorld.NewEntity();
            hitPool = ecsWorld.GetPool<HitComponent>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);
            hitComponent.hittedObjectTag = other.gameObject.tag;
            
            buttonPool = ecsWorld.GetPool<ButtonComponent>();
            buttonPool.Add(hit);
            ref var buttonComponent = ref buttonPool.Get(hit);
            buttonComponent.isClicked = true;
        }

        private void OnTriggerExit(Collider other)
        {
            hitPool.Del(hit);
            buttonPool.Del(hit);
        }
    }
}