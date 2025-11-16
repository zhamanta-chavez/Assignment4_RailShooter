using UnityEngine;
using DG.Tweening;

public class PathFollowScript : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    [SerializeField] private int currentWayPointIndex;
    public int numberOfEnemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("MoveToPoint", 1f);
    }

    public void MoveToPoint()
    {
        if (currentWayPointIndex < waypoints.Length)
        {
            transform.DOLookAt(waypoints[currentWayPointIndex].position, .75f, AxisConstraint.Y).SetEase(Ease.Linear);
            transform.DOMove(waypoints[currentWayPointIndex].position, moveSpeed).SetEase(Ease.InOutSine).OnComplete(() => WaypointCheck());
        }
    }

    void WaypointCheck()
    {
        EnemyEncounterScript _encounter = waypoints[currentWayPointIndex].GetComponent<EnemyEncounterScript>(); 
        currentWayPointIndex++;

        if (_encounter != null)
        {
            numberOfEnemies = _encounter._enemyCount;
            transform.DOLookAt(_encounter.lookPoint.position, .5f, AxisConstraint.Y).SetEase(Ease.InSine);
            _encounter.InitiateEncounter();
        }
        else
        {
            MoveToPoint();
        }
    }

    public void SubtractEnemy()
    {
        numberOfEnemies--;
        if (numberOfEnemies <= 0)
        {
            Invoke("MoveToPoint", .75f);
        }
    }
}
