using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player movement, defaults to controller input when a controller is connected (configured for a PlayStation 4 controller)
/// </summary>
public class PlayerMove : MonoBehaviour {

    public float MoveSpeed = 10.0f;
    public float RotateSpeed = 80.0f;

    private bool isMoving = false;
    private bool isRotating = false;
    public bool UseMouseKeyboard = true;

    Vector3 Velocity;
    private Vector3 rotateDirection;

    void Start()
    {
        int joyCount = Input.GetJoystickNames().Length;
        Debug.Log(joyCount + " controller(s) detected.");

        if (joyCount > 0)
            UseMouseKeyboard = false;
    }

    void FixedUpdate()
    {
        LeftStickInput();
        RightStickInput();

        RotateOnClick();

        if(rotateDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rotateDirection);
    }

    void RightStickInput()
    {
        float x = Input.GetAxis("HorizontalRight");
        float y = Input.GetAxis("VerticalRight");

        #region old unused player in direction of mouse cursor code
        /*
        //use mouse instead
        if (UseMouseKeyboard)
        {

            var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition); //0-1
            var playerScreen = Camera.main.WorldToViewportPoint(this.transform.position); //0-1

            //point from player to target
            Vector3 dir = mousePos - playerScreen;
            x = dir.x;
            y = dir.y;

        }
        */
        #endregion

        if (Mathf.Abs(x) + Mathf.Abs(y) > 0.1f)
        {
            Vector3 rot = new Vector3(x, 0, y);
            RotateTowards(rot);
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }
        

    }

    void RotateOnClick()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition); //0-1
            var playerScreen = Camera.main.WorldToViewportPoint(this.transform.position); //0-1

            //point from player to target
            Vector3 dir = mousePos - playerScreen;

            Vector3 rot = new Vector3(dir.x, 0, dir.y);
            RotateTowards(rot);
        }
    }

    void LeftStickInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x == 0 && y == 0)
        {
            isMoving = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }

        if(!isMoving)
        {
            //rotate once
            //RotateTowards(new Vector3(x, 0, y));
            isMoving = true;
        }

        //rotation
        Vector3 rot = new Vector3(x, 0, y);
        RotateTowards(rot);

        Velocity = new Vector3(x * MoveSpeed * Time.deltaTime * 100, 0, y * MoveSpeed * Time.deltaTime * 100);

        //consider doing this in FixedUpdate
        GetComponent<Rigidbody>().velocity = Velocity;
    }

    void RotateTowards(Vector3 direction)
    {
        if (rotateDirection == direction)
            return;

        rotateDirection = Vector3.RotateTowards(transform.forward, direction, RotateSpeed * Time.deltaTime, 0.0f);
      
    }
}
