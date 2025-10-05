using UnityEngine;

/// <summary>
/// Responsible for moving the player automatically and receiving input.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour: MonoBehaviour{

    public float minScale = 0.5f;
    public float maxScale = 3.0f;

    private float currentScale = 1;

    
    public float swipeMove = 2f;
    public float minSwipeDistance = 0.25f;

    private float minSwipeDistancePixels;

    private Vector2 touchStart;

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

        minSwipeDistancePixels = minSwipeDistance + Screen.dpi;

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

    public void Update(){

        #if UNITY_IOS || UNITY_ANDROID

            if(Input.touchCount > 0){
                Touch first = Input.touches[0];

                SwipeTeleport(first);
                ScalePlayer();
            }
            
        #endif

    }

    private void ScalePlayer(){

        if (Input.touchCount != 2){
            return;
        }
        else{

            Touch t0 = Input.touches[0];
            Touch t1 = Input.touches[1];

            Vector2 t0pos = t0.position;
            Vector2 t0delta = t0.deltaPosition;

            Vector2 t1pos = t1.position;
            Vector2 t1delta = t1.deltaPosition;

            Vector2 t0prev = t0pos - t0delta;
            Vector2 t1prev = t1pos - t1delta;

            float prevTDeltaMag = (t0prev - t1prev).magnitude;
            float tDeltaMag = (t0pos - t1pos).magnitude;

            float deltaMagDiff = prevTDeltaMag - tDeltaMag;

            float newScale = currentScale;
            newScale -= Mathf.Clamp(newScale, minScale, maxScale);

            transform.localScale = Vector3.one * newScale;

            currentScale = newScale;

        }

    }

    private void SwipeTeleport(Touch touch){

        if (touch.phase == TouchPhase.Began){
            touchStart = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended){
            Vector2 touchEnd = touch.position;

            float x = touchEnd.x - touchStart.x;

            if(Mathf.Abs(x) < minSwipeDistancePixels){
                return;
            }

            Vector3 moveDirection;

            if (x < 0){
                moveDirection = Vector3.left;
            }
            else{
                moveDirection = Vector3.right;
            }

            RaycastHit hit;

            if (!body.SweepTest(moveDirection, out hit, swipeMove)){

                var movement = moveDirection * swipeMove;
                var newPos = body.position + movement;

                body.MovePosition(newPos);

            }
        }
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
