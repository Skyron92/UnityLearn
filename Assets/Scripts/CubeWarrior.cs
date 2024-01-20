using System.Collections;
using System.Linq;
using UnityEngine;

public class CubeWarrior : CubeUnit
{
    

    protected override IEnumerator CheckEnemiesInRange() {
        if (EnemiesList.Exists(x => Vector3.Distance(transform.position, x.transform.position) <= fieldOfView)) {
            currentState = CubeState.Run;
        }
        yield return new WaitForSeconds(0.2f);
    }

    protected override IEnumerator SetDestination()
    {
        EnemiesList.OrderBy(x => Vector3.Distance(x.transform.position, transform.position));
        Destination = EnemiesList[0] == this ? EnemiesList[0].transform.position : EnemiesList[1].transform.position;
        Run();
        yield return new WaitForSeconds(0.2f);
    }

    protected override void Run() {
        StopCoroutine(CheckEnemiesInRange());
        NavMeshAgent.SetDestination(Destination);
    }
}