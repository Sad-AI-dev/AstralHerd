using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreShower : MonoBehaviour
{
    [SerializeField] TMP_Text label;

    private void Start()
    {
        label.text = Scorehandler.instance.score.ToString();
    }
}