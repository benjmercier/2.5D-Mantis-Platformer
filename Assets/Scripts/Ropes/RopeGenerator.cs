using UnityEngine;

namespace Mantis.Scripts.Ropes
{
    public class RopeGenerator : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _hookRB;
        [SerializeField]
        private GameObject _ropeSegmentPrefab;

        [SerializeField]
        private int _totalLinks = 25;
        [SerializeField]
        private float _spawnOffset = 0.41f;

        private Rigidbody _previousRB;
        private Vector3 _spawnPos;
        private GameObject _newSegment;
        private HingeJoint _hingeJoint;

        void Start()
        {
            GenerateRope();
        }

        private void GenerateRope()
        {
            _previousRB = _hookRB;
            _spawnPos = _hookRB.gameObject.transform.position;

            for (int i = 0; i < _totalLinks; i++)
            {
                _newSegment = Instantiate(_ropeSegmentPrefab);
                _newSegment.transform.parent = transform;

                if (i != 0)
                {
                    _spawnPos.y -= _spawnOffset;
                }

                _newSegment.transform.position = _spawnPos;

                if (_newSegment.TryGetComponent(out _hingeJoint))
                {
                    _hingeJoint.connectedBody = _previousRB;
                }
                else
                {
                    Debug.Log("Rope::GenerateRope()::_newSegment HingeJoint is NULL");
                }

                if (_newSegment.TryGetComponent(out Rigidbody rigidbody))
                {
                    _previousRB = rigidbody;
                }
                else
                {
                    Debug.Log("Rope::GenerateRope()::_newSegment Rigidbody is NULL");
                }

                if (_previousRB.gameObject.TryGetComponent(out RopeSegment ropeSegment))
                {
                    ropeSegment.ConnectRopeSegments();
                }
                else
                {
                    Debug.Log("Rope::GenerateRope()::_previousRB RopeSegment is NULL");
                }
            }
        }
    }
}

