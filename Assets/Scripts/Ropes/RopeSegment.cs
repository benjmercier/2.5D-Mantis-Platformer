using UnityEngine;

namespace Mantis.Scripts.Ropes
{
    public class RopeSegment : MonoBehaviour
    {
        public GameObject connectedAbove, connectedBelow;

        private HingeJoint _hingeJoint;
        private RopeSegment _aboveSegment;

        void Start()
        {
            ConnectRopeSegments();
        }

        public void ConnectRopeSegments()
        {
            if (TryGetComponent(out _hingeJoint))
            {
                connectedAbove = _hingeJoint.connectedBody.gameObject;
            }
            else
            {
                Debug.Log("RopeSegment::ConnectRopeSegments()::_hingeJoint is NULL");
            }

            if (connectedAbove.TryGetComponent(out RopeSegment ropeSegment))
            {
                _aboveSegment = ropeSegment;
            }
            else
            {
                Debug.Log("RopeSegment::ConnectRopeSegments()::connectedAbove RopeSegment is NULL");
            }

            if (_aboveSegment != null)
            {
                _aboveSegment.connectedBelow = gameObject;
            }
            else
            {
                if (_hingeJoint != null)
                {
                    _hingeJoint.connectedAnchor = Vector3.zero;
                }
            }
        }
    }
}

