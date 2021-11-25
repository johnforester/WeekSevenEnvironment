using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class Synth : MonoBehaviour
{
    private HelmController helmController;

    [SerializeField] HelmPatch patch;

    private bool patchLoadedIfNeeded = false;

    private void Awake()
    {
        helmController = GetComponent<HelmController>();
    }

    public void LoadInitialPatch()
    {
        if (!patchLoadedIfNeeded)
        {
            patchLoadedIfNeeded = true;

            if (patch)
            {
                Debug.Log("loading patch");
                helmController.LoadPatch(patch);
            }
        }
        else
        {
            Debug.Log("patch already loaded");
        }
    }

    public void PlayNote(int note)
    {
        helmController.NoteOn(note, 0.8f, 0.5f);
    }
}
