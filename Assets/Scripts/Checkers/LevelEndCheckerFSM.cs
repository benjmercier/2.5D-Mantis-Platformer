using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mantis.Scripts.Managers;

namespace Mantis.Scripts.Checkers
{
    public class LevelEndCheckerFSM : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.LevelComplete(true);
                //UIManager.Instance.ActivateNotification(true, UIManager.Instance.LevelCompleteTxt);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.LevelComplete(false);
                //UIManager.Instance.ActivateNotification(false, UIManager.Instance.LevelCompleteTxt);
            }
        }
    }
}

