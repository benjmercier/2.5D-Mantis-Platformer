using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbUpLedgeBehavior : StateMachineBehaviour
{
    private PlayerController_Old _player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //   
    //}
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = animator.gameObject.transform.GetComponentInParent<PlayerController_Old>();

        if (_player != null)
        {
            _player.CompleteLedgeClimb();

            /*
            Vector3 endPos = new Vector3();
            endPos.x = 0.75f;
            endPos.y = 1f;
            endPos.z = _player.transform.localPosition.z;

            _player.transform.localPosition = endPos;

            _player.transform.SetParent(null);

            _player.ToggleCharacterControllerState();
            */
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
