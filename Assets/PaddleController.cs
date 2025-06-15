using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;
    public string actionMapName = "Paddle1"; 

    public float minY = -4.5f; 
    public float maxY = 4.5f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private PlayerControls controls;
    private InputAction moveAction;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        var map = controls.asset.FindActionMap(actionMapName, true);
        map.Enable();

        moveAction = map.FindAction("Move", true);
        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.Disable();
            moveAction.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
            moveAction.canceled -= ctx => moveInput = Vector2.zero;
        }

        var map = controls.asset.FindActionMap(actionMapName, true);
        map.Disable();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, moveInput.y * speed);
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }
}