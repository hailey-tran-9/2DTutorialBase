using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    #region GameObject_variables
    [SerializeField]
    [Tooltip("health pack")]
    private GameObject healthpack;
    [SerializeField]
    [Tooltip("armor")]
    private GameObject chestplate;
    #endregion

    #region Chest_functions

    IEnumerator DestroyChest() {
        yield return new WaitForSeconds(0.3f);
        Instantiate(healthpack, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    IEnumerator GiveArmor() {
        yield return new WaitForSeconds(0.3f);
        Instantiate(chestplate, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void Interact() {
        float which = Random.Range(0.0f, 100.0f);
        if (which <= 75.0f) {
            StartCoroutine("DestroyChest");
        } else {
            StartCoroutine("GiveArmor");
        }
        
    }

    #endregion
}
