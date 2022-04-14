using UnityEngine;
using UnityEngine.AI;

namespace Components
{
    public struct PlayerComponent
    {
        public Transform playerTransform;
        public float rotateSpeed;
        public Vector3 destinationPosition;
        public CapsuleCollider collider;
        public Rigidbody rb;
    }
}