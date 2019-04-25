using UnityEngine;

public class HealthPotion : MonoBehaviour {
    [SerializeField] float healthIncrease = 15.0f;

    public string targetLayerName;

    private bool alreadyHit = false;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName)) {
            alreadyHit = true;
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) {
                health.Heal(healthIncrease);
                Destroy(transform.gameObject);
            }
        }
    }
}