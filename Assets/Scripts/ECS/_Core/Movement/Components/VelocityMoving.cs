using UnityEngine;

namespace Client
{
    internal struct VelocityMoving
    {
        public Transform Target;
        public float Speed;
        public float Accuracy;
        public Vector3 Offset;
    }
    
    internal struct VelocityPositionMoving
    {
        public Vector3 Target;
        public float Speed;
        public float Accuracy;
    }
}