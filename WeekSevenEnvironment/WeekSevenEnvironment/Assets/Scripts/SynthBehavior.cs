using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthBehavior : MonoBehaviour
{
    [SerializeField] GameObject synthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.visible = false;
            synthController.SetActive(true);
            GetComponentInChildren<Synth>().LoadInitialPatch();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.visible = true;
            synthController.SetActive(false);
        }
    }
}
