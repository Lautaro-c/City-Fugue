using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum Mode { Pursue, Wander, Attack, AfterAttack, Flee, Dead }

    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float slowRadious = 5f;
    [SerializeField] private float maxPredictionTime = 10f;
    [SerializeField] private float maxAngleChange = 90f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private bool canCrash;
    [SerializeField] private float wanderChangeInterval = 1.5f;

    private DecisionTree decisionTree;
    private float timeSinceLastAttack;
    private Mode mode;
    private Rigidbody enemyRb;
    private DecisionNode tree;
    private EnemyContext context;
    private LineOfSight los;
    private Vector3 wanderDirection;
    private float wanderTimer;
    private EnemyAttack enemyAttack;
    private bool isDead;

    private void Awake()
    {
        isDead = false;
        enemyRb = GetComponent<Rigidbody>();
        wanderDirection = transform.forward;
        los = GetComponent<LineOfSight>();
        decisionTree = GetComponent<DecisionTree>();
        wanderTimer = 0f;

        // Inicializamos el contexto una sola vez para evitar Garbage Collector
        context = new EnemyContext { self = transform };
    }

    private void Start()
    {
        tree = decisionTree.CreateTree();
        player = GameManager.Instance.GetPlayerTransform();
        playerRb = GameManager.Instance.GetPlayerRB();
        enemyAttack = GetComponent<EnemyAttack>();

        // Actualizamos las referencias fijas del contexto
        context.player = player;
        context.los = los;

        timeSinceLastAttack = attackCooldown;
    }

    private void FixedUpdate()
    {
        if (timeSinceLastAttack >= attackCooldown && !isDead)
        {
            tree.Evaluate(this, context);
        }
        else
        {
            timeSinceLastAttack += Time.fixedDeltaTime; // Corregido a fixedDeltaTime por estar en FixedUpdate
        }

        Vector3 dir = Vector3.zero;
        float movementSpeed = 0f;

        switch (mode)
        {
            case Mode.Pursue:
                dir = SteeringBehaviour.Pursue(transform, player, playerRb, maxPredictionTime, slowRadious);
                movementSpeed = speed;
                break;
            case Mode.Wander:
                wanderTimer -= Time.fixedDeltaTime;
                if (wanderTimer <= 0f)
                {
                    wanderDirection = SteeringBehaviour.Wander(wanderDirection, maxAngleChange);
                    wanderTimer = wanderChangeInterval;
                }
                dir = wanderDirection;
                movementSpeed = speed * 0.5f;
                break;
            case Mode.Attack:
                dir = SteeringBehaviour.Seek(transform, player.position);
                movementSpeed = 0f;
                if (!canCrash)
                {
                    mode = Mode.AfterAttack;
                    timeSinceLastAttack = 0f;
                }
                break;
            case Mode.Flee:
                dir = SteeringBehaviour.Flee(transform, player.position);
                movementSpeed = speed * 2f;
                break;
            case Mode.Dead:
            case Mode.AfterAttack:
                movementSpeed = 0f;
                break;
        }
        Move(dir, movementSpeed);
    }

    private void Move(Vector3 dir, float movementSpeed)
    {
        enemyRb.velocity = dir * movementSpeed;
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetMode(Mode mode) => this.mode = mode;

    private void OnDeath()
    {
        mode = Mode.Dead;
        isDead = true;
        enemyRb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canCrash)
            {
                timeSinceLastAttack = 0f;
                HealthManager.Instance.ReceiveDamage(damage);
                mode = Mode.AfterAttack;
            }
            else
            {
                OnDeath();
            }
        }
    }
}