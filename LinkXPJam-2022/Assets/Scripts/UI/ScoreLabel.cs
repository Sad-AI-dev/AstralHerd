using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class ScoreLabel : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] TMP_Text multiplierLabel;
    RectTransform rt;

    [Header("anim settings")]
    [SerializeField] float maxSize = 1.5f;
    [SerializeField] float growSpeed = 1f;
    [SerializeField] float shrinkSpeed = 0.8f;
    int count = 0;

    [Header("multiplier settings")]
    [SerializeField] float multiplierLabelLifetime = 1f;

    private void Start()
    {
        Scorehandler.instance.AddScoreListener(StartGrow);
        rt = GetComponent<RectTransform>();
    }

    void StartGrow(int newScore)
    {
        label.text = $"Score: {newScore}";
        multiplierLabel.text = $"x{FormatMultiplier()}";
        count++;
        StartCoroutine(GrowCo(count));
        if (Scorehandler.instance.multiplier > 1.1f) {
            StartCoroutine(MultiplierCo(count));
        }
    }

    IEnumerator GrowCo(int oldCount)
    {
        while (rt.localScale.x < maxSize && oldCount == count) {
            rt.localScale += new Vector3(1, 1) * (growSpeed * Time.deltaTime);
            yield return null;
        }
        while (rt.localScale.x > 1f && oldCount == count) {
            rt.localScale = Vector3.Lerp(rt.localScale, new Vector3(0.99f, 0.99f), shrinkSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MultiplierCo(int oldCount)
    {
        multiplierLabel.alpha = 1;
        float timer = 0f;
        while (multiplierLabel.alpha > 0f && oldCount == count) {
            yield return null;
            timer += Time.deltaTime;
            multiplierLabel.alpha = 1 - (timer / multiplierLabelLifetime);
        }
        if (oldCount == count) { multiplierLabel.alpha = 0f; } //reset if needed
    }

    string FormatMultiplier()
    {
        float input = Scorehandler.instance.multiplier * 10;
        float decim = Mathf.Round(input) / 10;
        return decim.ToString();
    }
}
