using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        public static PlayerData Instance;

        public GameObject prefab;
        public Vector3 spawnPoint;
        public LayerMask clickGroundMask;
        public float speed;

        private void OnEnable()
        {
            if (Instance != null) return;
            Instance = this;
        }
    }
}