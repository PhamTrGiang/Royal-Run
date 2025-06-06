using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float collisionCooldown = 1f;
    [SerializeField] private float adjustChangeMoveSpeedAmount = -2f;

    private const string hitString = "Hit";
    private float cooldownTimer = 0f;

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (cooldownTimer < collisionCooldown) return;

        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        animator.SetTrigger(hitString);
        cooldownTimer = 0f;
    }
}