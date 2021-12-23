﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    public ParticleSystem smoke;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;
    private float boost=1;
    public bool isBoosted = false;


    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        BoostThePlayer();


        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime*boost); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        smoke.transform.position = transform.position + new Vector3(0, -0.6f, -0.6f);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    //If Player press spacebar activate boost 
    void BoostThePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isBoosted == false)
        {
            isBoosted = true;
            boost = 5;
            StartCoroutine(BoostCooldown());
            Debug.Log("Boost");
            smoke.Play();

        }
    }
    // Coroutine to count down boost duration
    IEnumerator BoostCooldown()
    {
        yield return new WaitForSeconds(1);
        isBoosted = false;
        boost = 1;
        smoke.Stop();
        Debug.Log("No More Boost");
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =   other.gameObject.transform.position- transform.position ; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}