using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene

public class ObstacleBehaviour : MonoBehaviour
{
    public float waitTime = 1.0f;

    private void OnCollisionEnter(Collision collision){

        if (collision.gameObject.GetComponent<PlayerBehaviour>()){

            Destroy(collision.gameObject);

            Invoke("ResetGame", waitTime);

        }

    }

    private void ResetGame(){

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);

    }

}
