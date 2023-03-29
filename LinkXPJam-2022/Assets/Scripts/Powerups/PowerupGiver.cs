using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGiver : MonoBehaviour
{
    [SerializeField] float effectStrength = 0f;

    public void DamageUp(Player player)
    {
        player.damage += effectStrength;
    }

    public void AttackSpeedUp(Player player)
    {
        player.fireRate += effectStrength;
    }

    public void PlaySound(AudioClip clip)
    {
        FXManager.instance.PlaySound(clip);
    }
}
