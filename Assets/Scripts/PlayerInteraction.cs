using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private LayerMask obstacleLayer;

    private Animator animator = null;
    private BoxCollider2D collider = null;
    private Attack attack = null;

    void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        attack = GetComponent<Attack>();
    }

    void Update() {
        // movement

        // check if the user wants to go right/left or up/down
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // play correct walk animation
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        // attacking

        if (Input.GetButton("Fire1")) {
            // start attacking
            attack.StartAttack();
        } else {
            // disable the attack animation
            animator.SetBool("Attack", false);

            // determine the desired direction
            Vector3 direction = new Vector3(horizontal, vertical, 0.0f);

            // check if there is a collider in the way before moving
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
        }

    }
}
