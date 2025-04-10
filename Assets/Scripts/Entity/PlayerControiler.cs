using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControiller : BaseController
{



    InputAction MoveAction;

    InputAction AttackMode;

    InputAction MovementMode;

    InputAction SkipTick; 


    Direction direction;

    string mode;


    public CollidableObject collider;


    bool ActionLock = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EntityAttributes = GetComponent<EntityAttributes>();
        collider = GetComponent<CollidableObject>();
        IsPlayerControlled = true;
        MoveAction = InputSystem.actions.FindAction("Move");
        MovementMode = InputSystem.actions.FindAction("MovementMode");
        AttackMode = InputSystem.actions.FindAction("AttackMode");
        SkipTick = InputSystem.actions.FindAction("SkipTick"); 

        mode = "movement";

        RegisterTickable(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveAction.WasPressedThisFrame())
            ActionLock = false; 
      
        if (waitingForPlayerinput && !ActionLock)
        {
            var value = MoveAction.ReadValue<Vector2>();
            if (value.x > 0)
            {
                direction = Direction.East;
                waitingForPlayerinput = false;
            }
            else if (value.x < 0)
            {
                direction = Direction.West;
                waitingForPlayerinput = false;
            }
            else if (value.y > 0)
            {
                direction = Direction.North;
                waitingForPlayerinput = false;
            }
            else if (value.y < 0)
            {
                direction= Direction.South;
                waitingForPlayerinput = false;
            }
            else
            {
                direction = Direction.None;
                if (SkipTick.IsPressed())
                    waitingForPlayerinput = false; 
            }

            if (MovementMode.IsPressed())
                mode = "movement";
            else if (AttackMode.IsPressed())
                mode = "attack"; 
            
        }
    }

    public override void RunTick()
    {
        ActionLock = true; 
        if (mode == "movement")
        {     
            if (!GameManager.instance.entityManager.IsEntityPresent(collider.CurrentLocation.GetInDirection(direction)))
                Move(direction);
            else
                Attack(direction);
        }
        if (mode == "attack")
        {
            Attack(direction);
            mode = "movement"; 
        }
    }
}
