using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public Transform rayStart;
    public GameObject crystalEffect;

    private Animator anim;
    private Rigidbody rb;
    private bool walkingRight = true;
    private GameManager gameManager;

    private float baseSpeed = 2.0f;
    private float maxSpeedMultiplier = 2.0f;
    private float speedIncreaseRate = 0.01f;
    private float currentSpeedMultiplier = 1.0f;
    private float timeElapsed = 0.0f;


    // Start is called before the first frame update
    [System.Obsolete]
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        else
        {
            anim.SetTrigger("GameStarted");
        }

        // Gradually increase speed over time
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed < 180.0f) // 3 minutes
        {
            currentSpeedMultiplier = Mathf.Lerp(1.0f, maxSpeedMultiplier, timeElapsed / 180.0f);
            baseSpeed += speedIncreaseRate * Time.fixedDeltaTime;
            Debug.Log(baseSpeed);
        }

        // Move the player
        rb.transform.position = transform.position + transform.forward * baseSpeed * currentSpeedMultiplier * Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchDirection();
        }
        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity) && transform.position.y < 0)
        {
            anim.SetTrigger("IsFalling");
        }

        if (Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Road") && gameManager.gameStarted)
            {
                Destroy(hit.collider.gameObject, 1.5f);
            }
        }

            if (transform.position.y < -2)
        {
            gameManager.EndGame();
        }

    }

    private void SwitchDirection()
    {
        if (!gameManager.gameStarted)
            return;

        walkingRight = !walkingRight;

        if(walkingRight)
            transform.rotation = Quaternion.Euler(0, 45, 0);
        else
            transform.rotation = Quaternion.Euler(0, -45, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {       
            gameManager.IncreaseScore();

            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(other.gameObject);
        }
    }

}
