using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    //--------------------singleton------------------------
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        CompilePath();
    }
    public static PathManager instance;

    [SerializeField] Transform pathHolder;
    readonly List<Transform> path = new();

    readonly List<Transform> followers = new();

    void CompilePath()
    {
        foreach (Transform t in pathHolder) {
            path.Add(t);
        }
    }

    public Transform GetPoint(int index)
    {
        if (index < 0) { index *= -1; } //make positive
        if (index >= path.Count) { index %= path.Count; } //clamp index
        return path[index];
    }

    //------------------follower management------------------
    public void AddFollower(Transform t)
    {
        followers.Add(t);
    }

    public void RemoveFollower(Transform t)
    {
        followers.Remove(t);
    }

    public Transform GetFollowerAt(int index)
    {
        if (index >= 0 && index < followers.Count) {
            return followers[index];
        }
        return null;
    }

    public Transform GetRandomFollower()
    {
        if (followers.Count > 0) {
            return followers[Random.Range(0, followers.Count)];
        }
        return null;
    }
}
