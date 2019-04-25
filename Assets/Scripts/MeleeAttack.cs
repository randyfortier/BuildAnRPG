using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : Attack {
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private string targetLayerName;
    [SerializeField] private float meleeDamage = 15.0f;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private SoundEffectPlayer soundEffectPlayer = null;

    private Animator animator = null;

    private List<GameObject> collidingObjects = null;

    private float lastAttackTime = 0.0f;

    private void Awake() {
        animator = GetComponent<Animator>();
        collidingObjects = new List<GameObject>();
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName)) {
            collidingObjects.Add(collision.gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName)) {
            if (!collidingObjects.Contains(collision.gameObject)) {
                collidingObjects.Add(collision.gameObject);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision) {
        collidingObjects.Remove(collision.gameObject);
    }

    public override void StartAttack() {
        if (lastAttackTime == 0.0f || Time.time > (lastAttackTime + attackCooldown)) {
            // play the sound effect
            if (soundEffectPlayer != null) {
                soundEffectPlayer.PlayDaggerSound();
            }

            // remember time for cooldown purposes
            lastAttackTime = Time.time;

            // show the attack animation
            animator.SetBool("Attack", true);

            // deal damage
            foreach (GameObject collidingObject in collidingObjects) {
                Health health = collidingObject.GetComponent<Health>();
                if (health != null) {
                    health.TakeDamage(meleeDamage, this);
                }
            }
        }
    }

    public override void LevelUp() {
        meleeDamage *= 1.2f;
        attackCooldown /= 1.2f;

        ResetExperience();
    }
}
