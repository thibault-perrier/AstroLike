using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotAIController : MonoBehaviour
{

    private Rigidbody _rb;
    public float EnemyMovSpeed = 5.0f;
    [SerializeField] private float EnemyPushForce = 0.1f;
    [SerializeField] private float EnemyPushDist = 2.0f;
    [SerializeField] private float EnemyPushCooldown = 2.0f;
    private float _currentPushCooldown = 0.0f;
    private Vector3 movement;

    public Transform player;
    public Transform corner1, corner2;
    public Vector3 targetPosition;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GetRandomTargetPosition();
    }

    private void FixedUpdate()
    {
        _currentPushCooldown += Time.deltaTime;
        if (_currentPushCooldown >= EnemyPushCooldown)
        {
            _currentPushCooldown = 0.0f;
            CheckIfSeePlayer();
        }

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget < .3f)
            GetRandomTargetPosition();

        movement = (targetPosition - transform.position).normalized * EnemyMovSpeed * Time.deltaTime;
        _rb.MovePosition(_rb.position + movement);
    }

    private void CheckIfSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, EnemyPushDist))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Enemy hit player");

            hit.rigidbody.AddForce(transform.forward * EnemyPushDist, ForceMode.Impulse);
        }
    }

    private void GetRandomTargetPosition()
    {
        if (Random.Range(0f, 1f) < .5f)
        {
            targetPosition = player.position;
            return;
        }

        targetPosition = new(
            Random.Range(corner1.position.x, corner2.position.x),
            transform.position.y,
            Random.Range(corner1.position.z, corner2.position.z)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.SetupGame(true);
    }
}
