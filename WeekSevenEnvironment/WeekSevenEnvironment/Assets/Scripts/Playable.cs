using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class Playable : MonoBehaviour
{
    [SerializeField] GameObject NoteHandler;
    [SerializeField] int Note;

    private NoteHandler noteHandler;

    void Start()
    {
        noteHandler = NoteHandler.GetComponent<NoteHandler>();
    }

    public void PlayNote()
    {
        noteHandler.NoteOn(Note);
        StartCoroutine(StopNote());
    }

    IEnumerator StopNote()
    {
        yield return new WaitForSeconds(0.5f);

        noteHandler.AllNotesOff();
    }
}
