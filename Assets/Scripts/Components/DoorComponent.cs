using UnityEngine;

namespace Components
{
    public struct DoorComponent
    {
        public Transform doorTransform;
        public string doorTag;
        public float speedOpening;
        public Vector3 openedPosition;
    }
}