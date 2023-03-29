using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static GuardManager instance;

    public HealthManager health;
    public bool active = true;

    public void SaveScore()
    {
        if (active) { JsonInterfacer.instance.AddScore(); }
    }
}
