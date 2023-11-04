using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float rotationSpeed = 20f;

    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    // Start is called before the first frame update
    private void Start() {
        Debug.Log("Clase creada");
    }

    // Update is called once per frame
    private void Update() {
        Vector3 moveDir = gameInput.GetMoveVectorNormalized();
        
        isWalking = moveDir != Vector3.zero;

        transform.position += moveDir * (moveSpeed * Time.deltaTime);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
    
}