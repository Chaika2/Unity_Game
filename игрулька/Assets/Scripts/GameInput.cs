using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set;}
    
    private PlayerInputActions playerInputAcrions;

    private void Awake(){
        Instance = this;

        playerInputAcrions = new PlayerInputActions();
        playerInputAcrions.Enable();
    }

    public Vector2 GetMovementVector(){
        Vector2 inputVector = playerInputAcrions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
