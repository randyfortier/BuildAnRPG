using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private float zPosition;

    private Transform target;

    private void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        Vector3 newPosition;
        newPosition.x = target.transform.position.x;
        newPosition.y = target.transform.position.y;
        newPosition.z = zPosition;
        transform.position = newPosition;
    }
}
