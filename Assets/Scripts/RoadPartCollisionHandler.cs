using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPartCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("DestroyRoadPart", 1.0f); // Destroy the road part when the player runs over it
        }
    }

    void DestroyRoadPart()
    {
        Destroy(gameObject);
    }
}
