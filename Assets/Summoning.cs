using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoning : StateMachineBehaviour
{
    public float attackRange = 2.5f;
    bool canSummon;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        canSummon = animator.GetComponent<Invoke>().canSummon;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canSummon==true)
        {
            animator.SetTrigger("Summon");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Summon");
    }

}
