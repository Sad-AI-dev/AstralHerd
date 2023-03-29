using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    struct PricedAction {
        public UnityEvent onBuy;
        public int price;
    }

    [SerializeField] List<PricedAction> spawnActions = new();

    //budget
    [Header("budget")]
    [SerializeField] int budget = 10;
    [SerializeField] int budgetGain = 5;
    [SerializeField] int rampupSpeed = 5;
    [SerializeField] int rampupFrequency = 2;
    int spawnedCount = 0;
    [SerializeField] float budgetGainDelay = 1f;
    int lowestPrice;

    [Header("Spawning")]
    [SerializeField] float minSpawnDelay = 0f;
    [SerializeField] float maxSpawnDelay = 1f;
    [SerializeField] float enemySpawnDelay = 0.2f;

    private void Start()
    {
        lowestPrice = GetLowestPrice();
        StartCoroutine(GainBudgetCo());
        StartCoroutine(SpawnEnemiesCo());
    }

    IEnumerator GainBudgetCo()
    {
        yield return new WaitForSeconds(budgetGainDelay);
        budget += budgetGain;
        //rampup
        spawnedCount++;
        if (spawnedCount % rampupFrequency == 0) {
            budgetGain += rampupSpeed;
        }
        StartCoroutine(GainBudgetCo());
    }

    IEnumerator SpawnEnemiesCo()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        while (budget >= lowestPrice) { 
            TrySpawnEnemy();
            yield return new WaitForSeconds(enemySpawnDelay);
        }
        StartCoroutine(SpawnEnemiesCo());
    }

    void TrySpawnEnemy()
    {
        List<PricedAction> options = GetAllAffordableActions();
        if (options.Count > 0) {
            int chosenIndex = Random.Range(0, options.Count);
            budget -= options[chosenIndex].price;
            options[chosenIndex].onBuy?.Invoke();
        }
    }

    //--------------------action selection-----------------------
    int GetLowestPrice()
    {
        int lowest = spawnActions[0].price;
        foreach (PricedAction priceAction in spawnActions) {
            if (lowest > priceAction.price) {
                lowest = priceAction.price;
            }
        }
        return lowest;
    }

    List<PricedAction> GetAllAffordableActions()
    {
        List<PricedAction> affordables = new();
        foreach (PricedAction priceAction in spawnActions) {
            if (priceAction.price <= budget) {
                affordables.Add(priceAction);
            }
        }
        return affordables;
    }
}