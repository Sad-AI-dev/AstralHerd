using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform target;

    float startZ = 0f;

    private void Start()
    {
        startZ = transform.position.z;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
    }
}
