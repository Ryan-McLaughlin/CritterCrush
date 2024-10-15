using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler: MonoBehaviour
{
    private Camera mainCamera;
    GameManager gameManager;

    void Awake()
    {
        mainCamera = Camera.main;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log($"InputHandler.OnClick() context: {context}");

        // is used to check if the input action has just been started.
        // This prevents the code within the OnClick method from executing if the input action is already in progress or has been released
        if(!context.started) { return; }

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(pos: (Vector3)Mouse.current.position.ReadValue()));
        
        // Summon mallet
        gameManager.Click(rayHit.point);
        gameManager.Hit(rayHit.collider);
        
        if(!rayHit.collider) { return; }

        Debug.Log(rayHit.collider.gameObject.name);
    }
}
