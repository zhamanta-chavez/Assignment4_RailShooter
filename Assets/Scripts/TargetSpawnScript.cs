using UnityEngine;

public class TargetSpawnScript : MonoBehaviour
{
    public int numberOfTargets;

    public GameObject target;

    public void SpawnTargets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            float randomX = Random.Range(-.2f, .2f);
            float randomY = Random.Range(-.2f, .2f);

            Instantiate(target, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
    }
}
