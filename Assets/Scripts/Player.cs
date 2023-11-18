using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWalking;
    private Vector3 lastDir = Vector3.zero;
    private ClearCounter selectedCounter;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Mas de un Player");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    // Update is called once per frame
    private void Update() {
        Vector3 moveDir = gameInput.GetMoveVectorNormalized();
        HandleMovement(moveDir);
        HandleInteractions(moveDir);
    }

    private void HandleInteractions(Vector3 inputVec) {
        if (inputVec != Vector3.zero) {
            lastDir = inputVec;
        }

        float interactDistance = 2f;
        selectedCounter = null;
        if (Physics.Raycast(transform.position, lastDir, out RaycastHit raycastHit, interactDistance,
                counterLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                selectedCounter = clearCounter;
            }
        }

        SetSelectedCounter();
        // Debug.Log(selectedCounter);
    }

    private void HandleMovement(Vector3 inputVec) {
        Vector3 moveDir = new Vector3(0, 0, 0);
        Vector3 moveX = new Vector3(inputVec.x, 0, 0);
        Vector3 moveZ = new Vector3(0, 0, inputVec.z);

        var position = transform.position;

        float playerRadious = .6f;
        float playerHeight = .7f;

        bool canMoveX = !Physics.CapsuleCast(position, position + Vector3.up * playerHeight,
            playerRadious, moveX, moveSpeed * Time.deltaTime);
        if (canMoveX) {
            moveDir += moveX;
        }

        bool canMoveZ = !Physics.CapsuleCast(position, position + Vector3.up * playerHeight,
            playerRadious, moveZ, moveSpeed * Time.deltaTime);
        if (canMoveZ) {
            moveDir += moveZ;
        }

        isWalking = moveDir != Vector3.zero;
        transform.position += moveDir * (moveSpeed * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, inputVec, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
    
    private void SetSelectedCounter() {
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }
}