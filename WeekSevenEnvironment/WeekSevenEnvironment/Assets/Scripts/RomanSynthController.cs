using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class RomanSynthController : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnNoteOnEvent(Note inputNote)
    {
        anim.SetTrigger("Pick");
    }
}
