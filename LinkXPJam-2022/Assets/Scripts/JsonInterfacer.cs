using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JsonInterfacer : MonoBehaviour
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
    public static JsonInterfacer instance;
    const string filePath = "/scores.json";

    [System.Serializable]
    public struct Scores {
        public List<int> scores;
    }

    public void AddScore()
    {
        Scores scores = GetScores();
        scores.scores.Add(Scorehandler.instance.score);
        scores.scores.Sort((int a, int b) => -a.CompareTo(b));
        SaveScores(scores);
        //reset score
        Scorehandler.instance.ResetScore();
    }

    public Scores GetScores()
    {
        Scores scores;
        try {
            string input = File.ReadAllText(Application.dataPath + filePath);
            scores = JsonUtility.FromJson<Scores>(input);
        }
        catch { //file doesn't exist
            scores = new Scores { scores = new() };
        }
        return scores;
    }

    void SaveScores(Scores scores)
    {
        string output = JsonUtility.ToJson(scores);
        File.WriteAllText(Application.dataPath + filePath, output);
    }
}
