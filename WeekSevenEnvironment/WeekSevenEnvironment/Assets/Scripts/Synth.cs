using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class Synth : MonoBehaviour
{
    private HelmController helmController;

    [SerializeField] HelmPatch patch;

    private bool patchLoadedIfNeeded = false;
    private bool isPlaying = false;

    private void Awake()
    {
        helmController = GetComponent<HelmController>();

        StartCoroutine(LoadPatchAsync());
    }

    IEnumerator LoadPatchAsync()
    {
        // requires a pause before loading patch due to some AudioHelm race condition

        yield return new WaitForSeconds(0.5f);
        LoadInitialPatch();
    }

    public void LoadInitialPatch()
    {
        Debug.Log("loading patch...");
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
        if (!isPlaying)
        {
            StartCoroutine(PlayNoteAndHold(note, 0.5f));
        }
    }

    IEnumerator PlayNoteAndHold(int note, float length)
    {
        isPlaying = true;
        helmController.NoteOn(note, 0.8f, length);
        yield return new WaitForSeconds(length);
        isPlaying = false;
    }
}
