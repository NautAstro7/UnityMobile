using UnityEngine;

public class TileEndBehaviour : MonoBehaviour
{

    public float destroyTime = 1.5f;

    private void OnTriggerEnter(Collider other){

        if(other.gameObject.GetComponent<PlayerBehaviour>()){

            var gm = FindFirstObjectByType<GameManager>();
            gm.SpawnNextTile();

            Destroy(transform.parent.gameObject, destroyTime);

        }

    }

}
