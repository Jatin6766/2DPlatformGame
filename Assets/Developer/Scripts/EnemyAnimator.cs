using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetTrigger(string _name)
    {
        animator.SetTrigger(_name);
    }

}
