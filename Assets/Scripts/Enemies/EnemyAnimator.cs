using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private float deathTime = 1f;
    private Animator animator;

    // Hasheamos los parámetros del Animator (es mucho más rápido que usar strings en cada frame)
    private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
    private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    private static readonly int IsShootingHash = Animator.StringToHash("IsShooting");
    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDeathAnamiation()
    {
        animator.SetBool(IsDeadHash, true);
        StartCoroutine(DestroyAfterDelay());
    }

    public void PlayWalkingAnamiation()
    {
        animator.SetBool(IsWalkingHash, true);
        animator.SetBool(IsShootingHash, false);
        animator.SetBool(IsRunningHash, false);
    }

    public void PlayRunningAnamiation()
    {
        animator.SetBool(IsWalkingHash, false);
        animator.SetBool(IsShootingHash, false);
        animator.SetBool(IsRunningHash, true);
    }

    public void PlayAttackAnamiation()
    {
        animator.SetBool(IsWalkingHash, false);
        animator.SetBool(IsShootingHash, true);
        animator.SetBool(IsRunningHash, false);
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}