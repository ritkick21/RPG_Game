using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State {
        Roaming,
        Battling
    }

    [SerializeField] Collider2D slimeCol;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D playerCol;
    private State state;
    private EnemyPathfinding enemyPathfinding;
    bool isCurrentlyColliding;

    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        slimeCol = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        playerCol = GetComponent<Collider2D>();
        state = State.Roaming;
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            OnTriggerEnter2D(playerCol);
            if(isCurrentlyColliding == true){
                state = State.Battling;
                StartCoroutine(BattlingRoutine());               
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator BattlingRoutine(){
        
        yield return new WaitForSeconds(2f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        isCurrentlyColliding = true;
    }   

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
