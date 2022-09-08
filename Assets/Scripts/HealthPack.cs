using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField]
    [Tooltip("the amount the player heals")]
    #region HealthPack_variables
    private int healAmount;
    #endregion

    #region Heal_functions
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            collision.transform.GetComponent<PlayerController>().Heal(healAmount);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
