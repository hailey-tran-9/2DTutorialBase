using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Movement_variables
    public float movespeed;
    #endregion

    #region Physics_components
    Rigidbody2D EnemyRB;
    #endregion

    #region Targeting_variables
    public Transform player;
    #endregion

    #region Attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObj;
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region Unity_functions
    // Runs once on creation
    private void Awake() {
        EnemyRB = GetComponent<Rigidbody2D>();

        currHealth = maxHealth;
    }

    // Runs once every frame
    private void Update() {
        if (player == null) {
            return;
        }

        Move();
    }

    #endregion

    #region Movement_functions
    // Moves directly towards the player
    private void Move() {
        // Calculate movement vector player position - enemy position = direction of player relative to enemy
        Vector2 direction = player.position - transform.position;

        // Normalizing the vector ensures that it's magnitude 1
        EnemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion
    
    #region Attack_functions
    // Raycasts box for player, causes damage, spawns explosion prefab
    private void Explode() {
        // Call audioManager for explosion
        FindObjectOfType<AudioManager>().Play("Explosion");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);

        foreach (RaycastHit2D hit in hits) {
            if (hit.transform.CompareTag("Player")) {
                // Cause damage
                Debug.Log("Hit Player with explosion");

                // Spawn explosion prefab
                Instantiate(explosionObj, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerController>().TakeDamage(explosionDamage);
                Destroy(this.gameObject);
            }
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.CompareTag("Player")) {
            Explode();
        }
    }
    #endregion

    #region Health_functions
    // Enemy takes damage based on value param
    public void TakeDamage(float value) {
        // Call hurt sound
        FindObjectOfType<AudioManager>().Play("BatHurt");
        
        // Decrement health
        currHealth -= value;

        Debug.Log("Health is now " + currHealth.ToString());

        // Check for death
        if (currHealth <= 0) {
            // Die
            Die();
        }
    }

    // Destroys game object
    private void Die() {
        // Destroy game object
        Destroy(this.gameObject);
    }
    #endregion
}
