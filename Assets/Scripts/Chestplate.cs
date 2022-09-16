using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestplate : MonoBehaviour
{
    #region Equip_Functions
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            collision.transform.GetComponent<PlayerController>().Equip();
            Destroy(this.gameObject);
        }
    }
    #endregion
}
