using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float offsetRange = 1f;

    int targetIndex = -1;
    Vector2 target;

    Vector2 randOffset;

    private void Start()
    {
        GetComponent<HealthManager>().onDeath.AddListener(Die);
        PathManager.instance.AddFollower(transform);
        //set offset
        randOffset = new Vector2(Random.Range(-offsetRange, offsetRange), Random.Range(-offsetRange, offsetRange));
        ReachPoint();
        transform.position = target; //startPos
    }

    private void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target) < 0.001f) {
            ReachPoint();
        }
    }

    public void ReachPoint()
    {
        targetIndex++;
        target = (Vector2)PathManager.instance.GetPoint(targetIndex).position + randOffset;
    }

    void Die()
    {
        GuardManager.instance.health.DealDamage(1f);
        //remove self from list
        PathManager.instance.RemoveFollower(transform);
    }
}
