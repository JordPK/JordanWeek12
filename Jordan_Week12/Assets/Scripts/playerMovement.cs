using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    
    public Vector2 inputDirection,lookDirection;
    Animator anim;

    Vector3 touchStart, touchEnd;
    [SerializeField] Image dpad;
    [SerializeField] float dpadRadius = 10;

    Touch theTouch;

    public int health = 1;

    PlayerInput _playerInput;
    InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //makes the character look down by default
        lookDirection = new Vector2(0, -1);

        _playerInput = GetComponent<PlayerInput>();
        moveAction = _playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        //getting input from keyboard controls
        calculateDesktopInputs();

        //calculateMobileInput();
        //calculateTouchInputs();


        if (!UIManager.Instance.isPaused)
        {
            animationSetup();
        }
        
        //moves the player
        transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
    }


    void calculateDesktopInputs()
    {
        /*float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        inputDirection = new Vector2(x, y).normalized;*/

        inputDirection = moveAction.ReadValue<Vector2>();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

    }


    void animationSetup()
    {
        //checking if the player wants to move the character or not
        if (inputDirection.magnitude > 0.1f)
        {
            //changes look direction only when the player is moving, so that we remember the last direction the player was moving in
            lookDirection = inputDirection;

            //sets "isWalking" true. this triggers the walking blend tree
            anim.SetBool("isWalking", true);
        }
        else
        {
            // sets "isWalking" false. this triggers the idle blend tree
            anim.SetBool("isWalking", false);

        }

        //sets the values for input and lookdirection. this determines what animation to play in a blend tree
        anim.SetFloat("inputX", lookDirection.x);
        anim.SetFloat("inputY", lookDirection.y);
        anim.SetFloat("lookX", lookDirection.x);
        anim.SetFloat("lookY", lookDirection.y);
    }

    public void attack()
    {
        anim.SetTrigger("Attack");
    }

    void calculateMobileInput()
    {
        if (Input.GetMouseButton(0))
        {
            dpad.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
            }

            touchEnd = Input.mousePosition;

            float x = touchEnd.x - touchStart.x;
            float y = touchEnd.y - touchStart.y;

            inputDirection = new Vector2(x, y).normalized;

            if ((touchEnd - touchStart).magnitude > dpadRadius)
            {
                dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
            }
            else
            {
                dpad.transform.position = touchEnd;
            }

            if ((touchEnd - touchStart).magnitude > dpadRadius / 1.5f)
            {
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }
        }
        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive (false);
        }

        }
    void calculateTouchInputs()
    {
      /*  if (Input.touchCount > 0)
        {
            dpad.gameObject.SetActive(true);
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStart = theTouch.position;
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEnd = theTouch.position;

                float x = touchEnd.x - touchStart.x;
                float y = touchEnd.y - touchStart.y;

                inputDirection = new Vector2(x, y).normalized;

                if ((touchEnd - touchStart).magnitude > dpadRadius)
                {
                    dpad.transform.position = touchStart + (touchEnd - touchStart).normalized * dpadRadius;
                }
                else
                {
                    dpad.transform.position = touchEnd;
                }
            }

        }
        else
        {
            inputDirection = Vector2.zero;
            dpad.gameObject.SetActive(false);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Potion")
        {
            PotionScript potion = collision.GetComponent<PotionScript>();
            health += potion.PotionInfo.health;

            Destroy(collision.gameObject);
        }

        if (collision.tag == "BlackHole")
        {
            UIManager.Instance.GameOver();
        }
    }

}
