using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour {
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 offset;

    void Update() {
        transform.position = new Vector3(parent.position.x + offset.x, parent.position.y + offset.y, 0.0f);
    }
}
