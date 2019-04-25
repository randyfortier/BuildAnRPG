using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour {
    public Vector3 direction = Vector3.zero;
    public float rangedDamage = 10.0f;
    public string targetLayerName;
    public float lifetime = 5.0f;
    public Attack attacker = null;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool moving = false;

    private float spawnTime = 0.0f;
    private bool alreadyHit = false;

    private void Awake() {
        spawnTime = Time.time;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (alreadyHit) {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName)) {
            alreadyHit = true;
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                health.TakeDamage(rangedDamage, attacker);
                Destroy(transform.gameObject);
            }
        }
    }

    void Update() {
        if (Time.time > (spawnTime + lifetime)) {
            Destroy(transform.gameObject);
        } else if (moving) {
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;
            newPosition.z = 0.0f;
            transform.position = newPosition;
        }
    }
}
