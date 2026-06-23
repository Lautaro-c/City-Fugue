using UnityEngine;

public class OfficerAttack : EnemyAttack
{
    [Header("References")]
    [SerializeField] private Transform spawnPos;
    private LineOfSight lineOfSight;

    [Header("Stats")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private float lastAttackTime;

    private void Start()
    {
        player = GameManager.Instance.GetPlayerTransform();
        lineOfSight = GetComponent<LineOfSight>();
    }

    public override float Attack(float speed)
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return 0f;

        if (!lineOfSight.CanAttack(transform, player))
            return 0f;

        lastAttackTime = Time.time;

        Vector3 origin = spawnPos.position;
        Vector3 direction = (player.position - origin).normalized;

        float rayDistance = (transform.position - player.position).magnitude;

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.red, 0.2f);

            if (hit.transform.CompareTag("Player"))
            {
                HealthManager.Instance.ReceiveDamage(damage);
            }
        }

        return 0f;
    }
}