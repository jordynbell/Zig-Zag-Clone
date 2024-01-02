using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    public GameObject roadPrefab;
    public float offset = 0.707f;
    public Vector3 lastPosition;

    private int roadCount = 0;

    private float baseSpeed = 0.35f;
    private float maxSpeedMultiplier = 0.1f;
    private float speedIncreaseRate = 0.01f;
    private float currentSpeedMultiplier = 1.0f;
    private float timeElapsed = 0.0f;


    //Will possibly sort out spawn rate in future
    public void StartBuilding()
    {
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed < 180.0f) // 3 minutes
        {
            currentSpeedMultiplier = Mathf.Lerp(1.0f, maxSpeedMultiplier, timeElapsed / 180.0f);
            baseSpeed += speedIncreaseRate * Time.fixedDeltaTime;
        }

        InvokeRepeating("CreateNewRoadPart", 0, baseSpeed/3);
        Debug.Log(baseSpeed);
    }

    public void CreateNewRoadPart()
    {
        Vector3 spawnPos = Vector3.zero;
        float chance = Random.Range(0, 100);
        if (chance < 50)
            spawnPos = new Vector3(lastPosition.x + offset, lastPosition.y, lastPosition.z + offset);
        else
           spawnPos = new Vector3(lastPosition.x - offset, lastPosition.y, lastPosition.z + offset);

        GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));
        g.tag = "Road";

        lastPosition = g.transform.position;
        roadCount++;

        if(roadCount % 5 == 0)
        {
            g.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}
