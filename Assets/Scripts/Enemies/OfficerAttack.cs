using UnityEngine;

public class OfficerAttack : EnemyAttack
{
    [Header("References")]
    [SerializeField] private Transform spawnPos;
    [SerializeField] private LineOfSight lineOfSight;

    [Header("Stats")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private float lastAttackTime;

    private void Start()
    {
        player = GameManager.Instance.GetPlayerTransform();
        lineOfSight = this.GetComponent<LineOfSight>();
    }

    public override float Attack(float speed)
    {
        // ⏱️ Cooldown
        if (Time.time < lastAttackTime + attackCooldown)
            return 0f;

        // 👁️ Chequeo LOS completo (visión + rango + sin obstáculos)
        if (!lineOfSight.CanAttack(transform, player))
            return 0f;

        lastAttackTime = Time.time;

        Vector3 origin = spawnPos.position;
        Vector3 direction = (player.position - origin).normalized;

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, lineOfSightDistance()))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.red, 0.2f);

            if (hit.transform.CompareTag("Player"))
            {
                HealthManager.Instance.ReceiveDamage(damage);
            }
        }

        return 0f;
    }

    // 🔧 Usamos el attackDis del LOS como rango del disparo
    private float lineOfSightDistance()
    {
        return Vector3.Distance(transform.position, player.position);
    }
}