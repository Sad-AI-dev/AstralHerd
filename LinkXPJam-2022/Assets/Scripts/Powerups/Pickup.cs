using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] UnityEvent<Player> onPickup = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.transform.TryGetComponent(out Player player)) {
            onPickup?.Invoke(player);
            Destroy(gameObject);
        }
    }
}
