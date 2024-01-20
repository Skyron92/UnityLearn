using System.Collections;
using System.Linq;
using UnityEngine;

public class CubeRanger : CubeUnit
{
    [SerializeField, Range(0f, 40f)] private float fieldOfFear;
    [SerializeField, Range(0f, 40f)] private float safeDistance;
    private Vector3 _direction;
    
    protected override IEnumerator CheckEnemiesInRange() {
        if (EnemiesList.Exists(x => Vector3.Distance(transform.position, x.transform.position) <= fieldOfFear)) {
            currentState = CubeState.Run;
        }
        yield return new WaitForSeconds(0.2f);
    }

    protected override IEnumerator SetDestination() {
        EnemiesList.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
        _direction = EnemiesList[0] == this ? EnemiesList[0].transform.position - transform.position : EnemiesList[1].transform.position - transform.position;
        Destination = -_direction.normalized * safeDistance;
        Debug.Log(Destination);
        Run();
        yield return new WaitForSeconds(0.2f);
    }

    protected override void Run() {
        StopCoroutine(CheckEnemiesInRange());
        NavMeshAgent.SetDestination(Destination);
    }
}