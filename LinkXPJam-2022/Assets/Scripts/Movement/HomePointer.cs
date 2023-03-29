using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePointer : MonoBehaviour
{
    [SerializeField] float triggerDistance = 30f;
    [SerializeField] float activateDelay = 1f;
    [SerializeField] Transform player;
    [SerializeField] GameObject visuals;

    Transform trackTarget;
    bool active = false;

    private void Update()
    {
        if (!trackTarget) { trackTarget = PathManager.instance.GetRandomFollower(); if (!trackTarget) { return; } }
        DistanceCheck();
        if (visuals.activeSelf) { UpdateRotation(); }
    }

    void DistanceCheck()
    {
        active = Vector2.Distance(player.position, trackTarget.position) > triggerDistance;
        if (visuals.activeSelf != active) { StartCoroutine(ActivateCo()); }
    }

    IEnumerator ActivateCo()
    {
        yield return new WaitForSeconds(activateDelay);
        visuals.SetActive(active);
    }

    void UpdateRotation()
    {
        Vector2 dir = trackTarget.position - player.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}