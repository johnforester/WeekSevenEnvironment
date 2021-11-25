using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class SynthBehavior : MonoBehaviour
{
    // maybe get rid of this
    [SerializeField] GameObject synthController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            synthController.SetActive(true);

            Synth synth = GetComponentInChildren<Synth>();

            if (synth != null)
            {
                synth.LoadInitialPatch();

                HelmSequencer seq = GetComponentInChildren<HelmSequencer>();

                if (seq)
                {
                    seq.enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HelmSequencer seq = GetComponentInChildren<HelmSequencer>();

            if (seq)
            {
                seq.enabled = false;
            }
        }
    }
}
