using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] private float dis = 10f;
    [SerializeField] private float fleeDis = 5f;
    private float attackDis = 2f;
    [SerializeField] private float angle = 90f;
    [SerializeField] private LayerMask layerMask;


    private float disSqr;
    private float fleeDisSqr;
    private float attackDisSqr;

    private void Awake()
    {
        disSqr = dis * dis;
        fleeDisSqr = fleeDis * fleeDis;
        attackDisSqr = attackDis * attackDis;
    }

    public bool CanBeSeen(Transform self, Transform target)
    {
        return isInRange(self, target) && isInAngle(self, target) && hasLineOfSight(self, target);
    }

    public bool CanAttack(Transform self, Transform target)
    {
        return CanBeSeen(self, target) && isInAttackRange(self, target);
    }

    public bool CanFlee(Transform self, Transform target)
    {
        return (self.position - target.position).sqrMagnitude <= fleeDisSqr;
    }

    public bool isInRange(Transform self, Transform target)
    {
        return (self.position - target.position).sqrMagnitude <= disSqr;
    }

    public bool isInAngle(Transform self, Transform target)
    {
        Vector3 dir = (target.position - self.position).normalized;
        return Vector3.Angle(self.forward, dir) <= angle * 0.5f;
    }

    public bool hasLineOfSight(Transform self, Transform target)
    {
        Vector3 dir = target.position - self.position;
        return !Physics.Raycast(self.position, dir, dir.magnitude, layerMask);
    }

    public bool isInAttackRange(Transform self, Transform target)
    {
        return (self.position - target.position).sqrMagnitude <= attackDisSqr;
    }

    public float GetAttackDis() => attackDis;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dis);
        Vector3 leftBoundary = Quaternion.AngleAxis(-angle * 0.5f, Vector3.up) * transform.forward;
        Vector3 rightBoundary = Quaternion.AngleAxis(angle * 0.5f, Vector3.up) * transform.forward;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * dis);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * dis);
    }
}