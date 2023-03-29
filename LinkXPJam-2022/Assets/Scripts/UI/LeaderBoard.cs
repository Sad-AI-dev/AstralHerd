using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] RectTransform labelPrefab;

    [Header("spacing settings")]
    [SerializeField] float spacing;

    private void Start()
    {
        GenerateLabels();
    }

    public void GenerateLabels()
    {
        List<int> scores = JsonInterfacer.instance.GetScores().scores;
        for (int i = 0; i < Mathf.Min(scores.Count, 5); i++) {
            //create object
            RectTransform rt = Instantiate(labelPrefab, transform);
            rt.localPosition += new Vector3(0, -i * spacing);
            //set text
            rt.GetComponent<TMP_Text>().text = $"{i + 1} :      {scores[i]}";
        }
    }
}