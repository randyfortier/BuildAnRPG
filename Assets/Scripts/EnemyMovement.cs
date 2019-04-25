using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float sensoryRadius = 10.0f;
    [SerializeField] private float attackRadius = 0.5f;

    private Animator animator = null;
    private BoxCollider2D collider = null;
    private Attack attack = null;
    private GameObject player = null;

    void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        attack = GetComponent<Attack>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        // movement

        Vector3 direction = player.transform.position - transform.position;
        direction.z = 0.0f;
        float playerDistance = direction.magnitude;
        direction = direction.normalized;

        bool attacking = false;

        // attacking
        if (playerDistance < attackRadius) {
            attacking = true;

            // start attacking
            attack.StartAttack();
        } else {
            // disable the attack animation
            animator.SetBool("Attack", false);
        }

        if (!attacking && playerDistance < sensoryRadius) {
            // move toward the player
            Vector2 start = transform.position;
            Vector2 move = direction * speed * Time.deltaTime;
            Vector2 end = start + move;
            RaycastHit2D hit = Physics2D.Linecast(start, end, obstacleLayer);

            if (hit) {
                // we can't move because there is an obstacle in the way
                return;
            }

            // move the character
            transform.Translate(move);

            // animate the character
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);

        }
    }
}
