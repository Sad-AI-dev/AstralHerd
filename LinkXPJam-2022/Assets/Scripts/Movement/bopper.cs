using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bopper : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float stunTimer = 1f;
    [SerializeField] float stunPenalty = 2f;

    [SerializeField] float amplitude = 1f;
    [SerializeField] float bopRange = 1f;

    float timer = 0f;
    bool stunned = false;

    void Update()
    {
        timer += Time.deltaTime;
        Vector2 target = new Vector3(Mathf.Sin(timer * amplitude) * bopRange, Mathf.Cos((timer * amplitude) + 0.5f) * bopRange);
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, (stunned ? speed / stunPenalty : speed) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(HitCo());
    }

    IEnumerator HitCo()
    {
        stunned = true;
        yield return new WaitForSeconds(stunTimer);
        stunned = false;
    }
}
