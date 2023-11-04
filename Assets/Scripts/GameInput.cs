using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;

    private void Start() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector3 GetMoveVectorNormalized() {
        Vector3 moveDir = playerInputActions.Player.Move.ReadValue<Vector3>();
        return moveDir.normalized;
    }
}
