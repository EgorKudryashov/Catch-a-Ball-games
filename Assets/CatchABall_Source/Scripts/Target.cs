using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorgue = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public ParticleSystem explosionPartical;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorgue(), RandomTorgue(), RandomTorgue(), ForceMode.Impulse);

        transform.position =RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive && !gameManager.isPauseGame)
        {
            Instantiate(explosionPartical, transform.position, explosionPartical.transform.rotation);
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!gameObject.CompareTag("Bad") && gameManager.lives > 0)
        {
            gameManager.UpdateLives(-1);
        }

        if (!gameObject.CompareTag("Bad") && gameManager.lives == 0)
        {
            gameManager.GameOver();
        }

        Destroy(gameObject);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorgue()
    {
        return Random.Range(-maxTorgue, maxTorgue);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

}

