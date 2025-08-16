using UnityEngine;

/// <summary>
/// Responsible for moving the camera along a target and face it.
/// </summary>
public class CameraBehaviour: MonoBehaviour{
    
    [Tooltip("The object that the camera should look at/follow.")] 
    public Transform target;

    [Tooltip("Camera offset relative to target object.")] 
    public Vector3 offset = new Vector3 (0, 3, -6);

    /// <summary>
    /// Update runs once per frame.
    /// </summary>
    public void Update(){

        // Check if target is a valid object.
        if (target != null){
            // Set camera position to an offset of its target.
            transform.position = target.position + offset;

            //Change rotation so the camera faces its target.
            transform.LookAt(target);
        }

    }

}
