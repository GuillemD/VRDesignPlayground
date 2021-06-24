using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private bool canSpawn = true;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnBall()
    {
        if(canSpawn)
        {
            GameObject newBall = Instantiate(prefab, transform.position, transform.rotation);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        canSpawn = false;
    }
    private void OnTriggerExit(Collider other)
    {
        canSpawn = true;
    }
}
