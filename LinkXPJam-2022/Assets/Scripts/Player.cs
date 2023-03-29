using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    public float damage = 1f;
    public float fireRate = 1f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] List<Transform> shootPoint = new();

    [Header("Events")]
    [SerializeField] UnityEvent onShoot = new();

    Rigidbody2D body;
    bool canShoot = true;
    int lastShot = 0;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Update()
    {
        Shoot();
    }

    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 normalizedInput = new Vector2(xInput, yInput).normalized;
        body.velocity = normalizedInput * speed;
    }

    void Rotate()
    {
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && canShoot) {
            StartCoroutine(ShootCo());
        }
    }

    IEnumerator ShootCo()
    {
        ShootBullet();
        canShoot = false;
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }

    void ShootBullet()
    {
        onShoot?.Invoke();
        //create bullet
        Transform bullet = Instantiate(bulletPrefab).transform;
        bullet.SetPositionAndRotation(shootPoint[lastShot % shootPoint.Count].position, transform.rotation);
        lastShot++;
        if (bullet.TryGetComponent(out Bullet b)) {
            b.ownerTag = transform.tag;
            b.damage = damage;
        }
    }
}
