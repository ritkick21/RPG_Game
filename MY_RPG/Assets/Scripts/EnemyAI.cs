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
    [SerializeField] Animation blueSlimeIdle;
    private State state;
    private EnemyPathfinding enemyPathfinding;
    bool isCurrentlyColliding = false;

    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        slimeCol = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        state = State.Roaming;
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming)
        {
            animator.SetTrigger("Roaming");
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator BattlingRoutine(){
        while(state == State.Battling){
            animator.SetTrigger("Battling");
            Vector2 currentPosition = GetCurrentPosition();
            enemyPathfinding.MoveTo(currentPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        var tag = other.gameObject.tag;
        //Debug.Log("Collision2D Enter Triggered " + tag);
        if (tag == "Player"){
            isCurrentlyColliding = true;
            state = State.Battling;
            StartCoroutine(BattlingRoutine());
        }
    }   

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private Vector2 GetCurrentPosition() {
        return new Vector2(0, 0).normalized;
    }
}
