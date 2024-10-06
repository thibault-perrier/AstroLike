using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    StatsManager.BotStats botStats;


    private Vector3 movement;
    Quaternion turn;
    private Rigidbody _rb;
    private Transform _transform;


    void Start()
    {
        botStats = StatsManager.instance.bots[0];

        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        DoRotate();
        DoMove();
        CheckForPush();
    }

    private void DoRotate()
    {
        float rotate = Input.GetKey(KeyCode.D) ? -1f : (Input.GetKey(KeyCode.A) ? 1f : 0f);

        if (rotate != 0.0f)
        {
            float rotation = rotate * StatsManager.instance.BotRotSpeed * botStats.CurrentSteeringLvl * Time.deltaTime;
            _transform.Rotate(Vector3.down * rotation);
        }
    }

    private void DoMove()
    {
        float move = Input.GetKey(KeyCode.W) ? 1f : (Input.GetKey(KeyCode.S) ? -1f : 0f);

        if (move != 0.0f)
        {
            movement = transform.forward * move * StatsManager.instance.BotMovSpeed * botStats.CurrentMotorLvl * Time.deltaTime;
            _rb.MovePosition(_rb.position + movement);
        }
    }

    private void CheckForPush()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Space)) // add 2 seconds of reload time
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, StatsManager.instance.BotPushDist))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Player hit enemy");

                hit.rigidbody?.AddForce(transform.forward * StatsManager.instance.BotPushForce * botStats.CurrentPistonLvl, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.SetupGame(false);
    }
}
