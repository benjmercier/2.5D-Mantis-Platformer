using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mantis.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private Transform _startPos;
        public Transform StartPos { get { return _startPos; } }

        [SerializeField]
        private GameObject _levelEnd;

        private float _minPos = -10f;
        public float MinPos { get { return _minPos; } }

        private bool _levelComplete = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (_levelComplete)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

        public void LevelComplete(bool isComplete)
        {
            _levelComplete = isComplete;

            Debug.Log("level complete: " + _levelComplete);
            UIManager.Instance.ActivateNotification(isComplete, UIManager.Instance.LevelCompleteTxt);
            //UIManager.Instance.ActivateNotification(_levelComplete, UIManager.Instance.LevelCompleteTxt);
        }
    }
}

