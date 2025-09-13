using UnityEngine;

/// <summary>
/// Responsible for moving the player automatically and receiving input.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour: MonoBehaviour{
    
    /// <summary>
    /// A reference to the Rigidbody component.
    /// </summary>
    private Rigidbody body;

    [Tooltip("How fast the ball moves left/right.")] 
    public float dodgeSpeed = 5;
    [Tooltip("How fast the ball moves forward automatically.")]
    [Range(0, 10)]
    public float rollSpeed = 5;

    public float horizontalSpeed;


    public void Start(){

        // Get access to our Rigidbody component.
        body = GetComponent<Rigidbody>();

    }

    /// <summary>
    /// FixedUpdate is a prime place to put physics calculations happening over a period of time.
    /// </summary>
    public void FixedUpdate(){

        // Check input and move horizontally accordingly.
        if (Input.touchCount > 0){

            var cam = Camera.main;

            var firstTouch = Input.touches[0];

            var screenPos = firstTouch.position;
            var viewPos = cam.ScreenToViewportPoint(screenPos);

            float xMove = 0;

            if (viewPos.x < 0.5f){
                xMove = -1;
            }else{
                xMove = 1;
            }

            horizontalSpeed = xMove * dodgeSpeed;

        }

        body.AddForce(horizontalSpeed, 0, rollSpeed);

    }

}
