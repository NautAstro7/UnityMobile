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


    public void Start(){

        // Get access to our Rigidbody component.
        body = GetComponent<Rigidbody>();

    }

    /// <summary>
    /// FixedUpdate is a prime place to put physics calculations happening over a period of time.
    /// </summary>
    public void FixedUpdate(){

        // Check input and move horizontally accordingly.
        var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
        body.AddForce(horizontalSpeed, 0, rollSpeed);

    }

}
