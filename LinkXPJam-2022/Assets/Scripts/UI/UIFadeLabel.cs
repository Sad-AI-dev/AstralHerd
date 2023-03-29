using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFadeLabel : MonoBehaviour
{
    public TMP_Text label;

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float lifeTime = 1f;
    float timer = 0f;

    RectTransform rt;
    CanvasGroup group;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime) { Destroy(gameObject); }
        else {
            group.alpha = 1 - (timer / lifeTime);
            rt.anchoredPosition += new Vector2(0, moveSpeed * Time.deltaTime) * 100;
        }
    }
}
