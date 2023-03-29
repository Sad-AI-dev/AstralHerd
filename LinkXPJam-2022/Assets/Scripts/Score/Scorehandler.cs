using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scorehandler : MonoBehaviour
{
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public static Scorehandler instance;

    public int score { get; private set; }
    public int lastGainedScore { get; private set; }

    readonly UnityEvent<int> onScoreChange = new();

    [Header("multiplier settings")]
    [SerializeField] float maxMultiplier = 5f;
    [SerializeField] float multiplierFalloffSpeed = 2f;
    [HideInInspector] public float multiplier = 1f;

    public void AddScore(int toAdd)
    {
        lastGainedScore = Mathf.RoundToInt(toAdd * multiplier);
        score += lastGainedScore;
        StartCoroutine(MultiplierCo(score));
        UpdateUI();
    }

    public void AddScoreListener(UnityAction<int> listener)
    {
        onScoreChange.AddListener(listener);
    }

    IEnumerator MultiplierCo(int oldScore)
    {
        yield return null;
        multiplier = maxMultiplier;
        while (multiplier > 1f && oldScore == score) {
            yield return null;
            multiplier = Mathf.Lerp(multiplier, 1f, multiplierFalloffSpeed * Time.deltaTime);
        }
        if (oldScore == score) { multiplier = 1f; } //reset if needed
    }

    //------------reset-----------
    public void ResetScore()
    {
        StartCoroutine(ResetCo());
    }

    IEnumerator ResetCo()
    {
        yield return new WaitForSeconds(1f);
        score = 0;
    }

    //-------------UI-------------
    void UpdateUI()
    {
        onScoreChange?.Invoke(score);
    }
}
