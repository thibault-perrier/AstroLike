using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [HideInInspector] public float EnemyFrameLvl = 1;
    [HideInInspector] public float EnemyMotorLvl = 1;
    [HideInInspector] public float EnemySteeringLvl = 1;
    [HideInInspector] public float EnemyPistonLvl = 1;


    [Header("Push stats")]
    [SerializeField] private float EnemyPushDist = 2.0f;
    [SerializeField] private float EnemyPushCooldown = 2.0f;
    private float _currentPushCooldown = 0.0f;
    private Vector3 movement;
    public Transform player;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        _currentPushCooldown += Time.deltaTime;
        if (_currentPushCooldown >= EnemyPushCooldown)
        {
            _currentPushCooldown = 0.0f;
            CheckIfSeePlayer();
        }

        movement = (player.position - transform.position).normalized * EnemyMotorLvl * StatsManager.instance.BotMovSpeed * Time.deltaTime;
        _rb.MovePosition(_rb.position + movement);
    }

    private void CheckIfSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, EnemyPushDist))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Enemy hit player");

            hit.rigidbody?.AddForce(transform.forward * EnemyPistonLvl * StatsManager.instance.BotPushForce, ForceMode.Impulse);
        }
    }
}
