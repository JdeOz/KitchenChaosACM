using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float rotationSpeed = 20f;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Clase creada");
    }

    // Update is called once per frame
    void Update() {
        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) {
            moveDir.z += 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            moveDir.z -= 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            moveDir.x -= 1;
        }

        if (Input.GetKey(KeyCode.D)) {
            moveDir.x += 1;
        }

        moveDir = moveDir.normalized;
        transform.position += moveDir * (moveSpeed * Time.deltaTime);
        Debug.Log(moveDir);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }
}