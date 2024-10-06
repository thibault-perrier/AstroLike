using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _enemyTransform;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckBotsRot());
    }

    IEnumerator CheckBotsRot()
    {
        if (!(_playerTransform.rotation.x.Equals(0.0f) && _playerTransform.rotation.z.Equals(0.0f)))
            _playerTransform.rotation = new Quaternion(0.0f, _playerTransform.rotation.y, 0.0f, _playerTransform.rotation.w);
        yield return new WaitForSeconds(1.0f);
        CheckBotsRot();
    }
}
