using UnityEngine;

public class RandomAnimation : StateMachineBehaviour
{
    [SerializeField] private string parameterName;
    [SerializeField] private int length;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int hashRandom = Animator.StringToHash(parameterName);
        animator.SetInteger(hashRandom,  Random.Range (0, length));
    }
}