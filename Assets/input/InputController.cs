using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }


    public InputAction directionActionX { get; private set; }
    public InputAction directionActionY { get; private set; }
    public InputAction moveAction { get; private set; }
    public InputAction rotateAction { get; private set; }
    public InputAction shootAction { get; private set; }
    public InputAction confirmAction { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        moveAction = GetComponent<PlayerInput>().actions.FindActionMap("Player", true).FindAction("Move", true);
        rotateAction = GetComponent<PlayerInput>().actions.FindActionMap("Player", true).FindAction("Rotate", true);
//        directionAction = GetComponent<PlayerInput>().actions.FindActionMap("Player", true)
//            .FindAction("Direction", true);
        directionActionX = GetComponent<PlayerInput>().actions.FindActionMap("Player", true)
            .FindAction("DirectionX", true);
        directionActionY = GetComponent<PlayerInput>().actions.FindActionMap("Player", true)
            .FindAction("DirectionY", true);
        shootAction = GetComponent<PlayerInput>().actions.FindActionMap("Player", true)
            .FindAction("Shoot", true);
        confirmAction = GetComponent<PlayerInput>().actions.FindActionMap("Player", true)
            .FindAction("Confirm", true);
    }
}