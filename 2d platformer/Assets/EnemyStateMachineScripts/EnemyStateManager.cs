using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    
    EnemyBaseState currentState;
  public EnemyPatrolState PatrolState = new EnemyPatrolState();
   public EnemyAlertState AlertState = new EnemyAlertState();

    // Start is called before the first frame update
    void Start()
    {
        //starting state for the state machine
        currentState = PatrolState;
        //"This" is a reference to the context(this EXACt monobehavior script)
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
   public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
