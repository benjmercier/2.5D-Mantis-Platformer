using UnityEngine;

namespace Mantis.Scripts.Player.States
{
    public class LedgeGrabbingState : BaseState
    {
        public override void EnterState()
        {
            TogglePlayerController(false);
        }

        public override void ExitState()
        {
            
        }

        public override void Update()
        {
            
        }

        private void TogglePlayerController(bool isEnabled)
        {
            _player.Controller.enabled = isEnabled;
        }
    }
}

