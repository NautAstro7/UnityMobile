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

        var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

            if (Input.GetMouseButton(0)){
                var screenPos = Input.mousePosition;
                horizontalSpeed = CalculateMovement(screenPos);
            }

        #elif UNITY_IOS || UNITY_ANDROID

            // Check input and move horizontally accordingly.
            if (Input.touchCount > 0){

                var firstTouch = Input.touches[0];
                var screenPos = firstTouch.position;

                horizontalSpeed = CalculateMovement(screenPos);

            }   

        #endif

        body.AddForce(horizontalSpeed, 0, rollSpeed);

    }

    public float CalculateMovement (Vector3 scrPos){
        var cam = Camera.main;

        var viewPos = cam.ScreenToViewportPoint(scrPos);

        float xMove = 0;

        if (viewPos.x < 0.5f){
            xMove = -1;
        }else{
            xMove = 1;
        }

        return xMove * dodgeSpeed;
    }

}
