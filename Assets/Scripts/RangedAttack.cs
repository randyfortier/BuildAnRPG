using System;
using System.Collections;
using UnityEngine;

public class RangedAttack : Attack {
    [SerializeField] float arrowSpawnDelay = 1.0f;
    [SerializeField] GameObject arrowPrefab = null;
    [SerializeField] Transform arrowSpawnPosition = null;
    [SerializeField] float coolDown = 1.5f;
    [SerializeField] string targetLayerName;
    [SerializeField] float projectileLifetime = 3.0f;
    [SerializeField] float arrowDamage = 15.0f;
    [SerializeField] private SoundEffectPlayer soundEffectPlayer = null;

    private Animator animator = null;
    private float lastLaunchTime = 0.0f;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private Vector3 GetAttackDirection() {
        float horizontal = animator.GetFloat("Horizontal");
        float vertical = animator.GetFloat("Vertical");

        if (horizontal < -0.01f) {
            return Vector3.left;
        } else if (horizontal > 0.01f) {
            return Vector3.right;
        } else if (vertical > 0.01f) {
            return Vector3.up;
        } else {
            return Vector3.down;
        }
    }

    private Vector3 GetRotation() {
        float horizontal = animator.GetFloat("Horizontal");
        float vertical = animator.GetFloat("Vertical");

        if (horizontal < -0.01f) {
            return new Vector3(0.0f, 0.0f, 270.0f);
        } else if (horizontal > 0.01f) {
            return new Vector3(0.0f, 0.0f, 90.0f);
        } else if (vertical > 0.01f) {
            return new Vector3(0.0f, 0.0f, 180.0f);
        } else {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    public override void StartAttack() {
        if (lastLaunchTime > 0.0f && Time.time < (lastLaunchTime + coolDown)) {
            // we cannot fire yet, so wait
            return;
        }

        // play the sound effect
        if (soundEffectPlayer != null) {
            soundEffectPlayer.PlayBowSound();
        }

        // remember time for cooldown purposes
        lastLaunchTime = Time.time;

        // show the attack animation
        animator.SetBool("Attack", true);

        // spawn a new arrow
        StartCoroutine(DelayedAction(
            () => {
                // rotate the arrow
                Quaternion rotation = Quaternion.Euler(GetRotation());

                // position the arrow
                Vector3 position = arrowSpawnPosition.position;

                // spawn the arrow
                GameObject arrow = Instantiate(arrowPrefab, position, rotation);
                ArrowMovement arrowMovement = arrow.GetComponent<ArrowMovement>();
                arrowMovement.direction = GetAttackDirection();
                arrowMovement.targetLayerName = targetLayerName;
                arrowMovement.lifetime = projectileLifetime;
                arrowMovement.rangedDamage = arrowDamage;
                arrowMovement.attacker = this;
            },
            arrowSpawnDelay
         ));
    }

    public IEnumerator DelayedAction(Action action, float delay) {
        yield return new WaitForSecondsRealtime(delay);
        action();
    }

    public override void LevelUp() {
        arrowDamage *= 1.2f;
        projectileLifetime *= 1.2f;

        ResetExperience();
    }
}
