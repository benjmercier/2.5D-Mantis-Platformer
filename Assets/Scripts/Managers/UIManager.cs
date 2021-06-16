using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Mantis.Scripts.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField]
        private TextMeshProUGUI _notification;

        //[SerializeField]
        private string _ledgeClimbTxt = "Press 'E' to Climb";
        public string LedgeGrabTxt { get { return _ledgeClimbTxt; } }
        private string _ropeSwingTxt = "Press 'A' & 'D' to Swing \n 'Space' to Jump";
        public string RopeSwingTxt { get { return _ropeSwingTxt; } }
        private string _wallJumpTxt = "Press 'Space' to Wall Jump";
        public string WallJumpTxt { get { return _wallJumpTxt; } }
        private string _levelCompleteTxt = "Press 'Enter' to Restart";
        public string LevelCompleteTxt { get { return _levelCompleteTxt; } }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ActivateNotification(bool isActive, string text)
        {
            _notification.text = text;

            _notification.gameObject.SetActive(isActive);

            Debug.Log("Called: " + text);
        }
    }
}

