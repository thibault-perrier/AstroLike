using System;
using System.Collections;
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


    [Header("IA variables")]
    [SerializeField] private Transform _playerTransform;
    private Transform _selfTransform;
    [SerializeField] private float _botFOV;
    private Vector3 _playerPos;
    private Vector3 _selfPos;
    private Vector3 _target;
    private bool _targetInFOV;
    private bool _canPush = true;
    private bool _isRotating = false;
    private Vector3 vecToPlayer;
    private Vector3 vecToTarget;


    private Rigidbody _rb;
    private Rigidbody _playerRB;
    private Vector3 movement;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _selfTransform = transform;

        _playerRB = _playerTransform.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        _playerPos = _playerTransform.position;
        _selfPos = _selfTransform.position;
    }

    void FixedUpdate()
    {
        // check if see player -> target = player
        CheckIfSeePlayer();

        // turn around until target is in fov

        if (!_targetInFOV)
        {
            // if (!_isRotating) StartCoroutine(RotateToTarget());

            float angle = Vector3.Angle(_selfTransform.forward, vecToTarget);
            float step = angle * EnemySteeringLvl * StatsManager.instance.BotRotSpeed * Time.deltaTime * 0.01f;
            _selfTransform.Rotate(Vector3.down, step);
        }
        else // if vec pos -> target == transform.forward -> move forward
        {
            movement = (_target - _selfPos).normalized * EnemyMotorLvl * StatsManager.instance.BotMovSpeed * Time.deltaTime;
            _rb.MovePosition(_rb.position + movement);
        }

        // check if can push
        if (_canPush && Vector3.Distance(_playerPos, _selfPos) <= EnemyPushDist && IsTargetInFOV(vecToPlayer))
        {
            // -> push by EnemyPistonLvl * StatManager.instance.BotPushForce
            _playerRB?.AddForce(_selfTransform.forward * EnemyPistonLvl * StatsManager.instance.BotPushForce, ForceMode.Impulse);
            _canPush = false;
            StartCoroutine(ResetCanPush());
        }
    }

    private IEnumerator ResetCanPush()
    {
        yield return new WaitForSeconds(EnemyPushCooldown);
        _canPush = true;
    }

    private void CheckIfSeePlayer()
    {
        vecToPlayer = _playerPos - _selfPos;
        vecToTarget = _target - _selfPos;

        if (IsTargetInFOV(_playerPos))
        {
            _target = _playerPos;
        }
        else if (Vector3.Distance(_selfPos, _target) < .5)
        {
            _target = GetRandomPointOnArena();
        }

        _targetInFOV = IsTargetInFOV(_target);
    }

    private bool IsTargetInFOV(Vector3 targetPos)
    {
        Vector3 dirToTarget = (targetPos - _selfPos).normalized;

        return Mathf.Abs(Vector3.Angle(_selfTransform.forward, dirToTarget)) <= _botFOV;
    }

    private Vector3 GetRandomPointOnArena()
    {
        Vector3 firstCorner = ShopManager.instance._bounds[0].position;
        Vector3 secondCorner = ShopManager.instance._bounds[1].position;
        Vector2 randPos = MathUtils.GetRandomPosOnAreaWithCorners(firstCorner.x, firstCorner.z, secondCorner.x, secondCorner.z);

        return new Vector3(randPos.x, firstCorner.y, randPos.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bounds")
            GameManager.instance.SetupGame(true);
    }
}
