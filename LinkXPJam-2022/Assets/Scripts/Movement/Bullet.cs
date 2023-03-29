using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 1f;
    [SerializeField] float speed = 1f;
    public float damage = 1f;

    [HideInInspector] public string ownerTag;

    private void Start()
    {
        StartCoroutine(LifeTimeCo());
    }

    private void Update()
    {
        transform.Translate(new Vector2(1, 0) * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag(ownerTag)) {
            if (collision.transform.CompareTag("Protectable") && !ownerTag.Contains("Player")) {
                GuardManager.instance.health.DealDamage(damage);
                Destroy(collision.gameObject);
            }
            else if (collision.transform.TryGetComponent(out HealthManager health)) {
                health.DealDamage(damage);
            }
            //die
            StartCoroutine(DieCo());
        }
    }

    IEnumerator LifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine(DieCo());
    }

    IEnumerator DieCo()
    {
        yield return null;
        Destroy(gameObject);
    }
}
