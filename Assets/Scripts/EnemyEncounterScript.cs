using UnityEngine;

public class EnemyEncounterScript : MonoBehaviour
{
    public GameObject[] _enemies;
    public int _enemyCount;
    public Transform lookPoint;

    private void Awake()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemyCount++;
        }
    }

    private void Start()
    {
        foreach (GameObject enemy in _enemies)
        {
            enemy.SetActive(false);
        }
    }

    public void InitiateEncounter()
    {
        foreach (GameObject enemy in _enemies)
        {
            enemy.SetActive(true);  
        }
    }
}
