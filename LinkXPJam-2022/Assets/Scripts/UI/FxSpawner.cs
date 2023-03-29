using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSpawner : MonoBehaviour
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
    public static FxSpawner instance;

    public void SpawnFx(GameObject prefab, Vector3 worldPos)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(transform, false);
        obj.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(worldPos);
        obj.GetComponent<UIFadeLabel>().label.text = $"{Scorehandler.instance.lastGainedScore}";
    }
}
