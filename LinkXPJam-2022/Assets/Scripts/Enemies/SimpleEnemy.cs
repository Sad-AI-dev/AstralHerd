using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform visuals;
    [SerializeField] int score = 100;

    Transform target;

    private void Start()
    {
        GetNewTarget();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (target == null) { 
            GetNewTarget();
            if (target == null) { Destroy(gameObject); return; }
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        Rotate();
    }
    void Rotate()
    {
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        visuals.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void GetNewTarget()
    {
        target = PathManager.instance.GetRandomFollower();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Protectable")) {
            if (collision.transform.parent.TryGetComponent(out HealthManager health)) {
                health.DealDamage(1f);
            }
            Destroy(gameObject);
        }
    }

    public void SpawnUILabel(GameObject onDeathLabel)
    {
        FxSpawner.instance.SpawnFx(onDeathLabel, transform.position);
    }

    public void GiveScore()
    {
        Scorehandler.instance.AddScore(score);
    }

    public void PlaySound(AudioClip clip)
    {
        FXManager.instance.PlaySound(clip);
    }
}