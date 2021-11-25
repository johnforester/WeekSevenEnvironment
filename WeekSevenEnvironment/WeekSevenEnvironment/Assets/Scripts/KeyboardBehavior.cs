using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardBehavior : MonoBehaviour
{
    [SerializeField] Synth synth;
    [SerializeField] GameObject pointer;
    [SerializeField] Collider pointerTrack;
    [SerializeField] float interactionDistance = 10f;
    [SerializeField] GameObject lowKeyPos;
    [SerializeField] GameObject highKeyPos;

    private GameObject focusedObject;
    private float keyboardWidth;

    private float pointerStartingY = 0f;
    private Vector3 pointerMovementEndposition;
    private bool isPlaying = false;
    private int currentNote;

    private void Start()
    {
        pointerStartingY = pointer.transform.position.y;
        keyboardWidth = Vector3.Distance(lowKeyPos.transform.position, highKeyPos.transform.position);
    }

    private void Update()
    {
        if (isPlaying)
        {
            pointer.transform.position = Vector3.Lerp(pointer.transform.position, pointerMovementEndposition, 1.0f * Time.deltaTime);

            if (pointer.transform.position.y >= pointerMovementEndposition.y)
            {
                pointerMovementEndposition = new Vector3(pointer.transform.position.x, pointerStartingY, pointer.transform.position.z);
            }
        }
    }

    private void FixedUpdate()
    {
        //compute player's forward direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Raycast originating from camera
        RaycastHit hit;

        Ray ray = new Ray(transform.position, forward);

        Debug.DrawRay(transform.position, forward * interactionDistance, Color.magenta);

        int layerMask = 1 << 8; // only detect Keyboards

        // Conduct raycast
        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            focusedObject = hit.collider.gameObject;
            ToggleHighlight(true);

            Vector3 pointerPos = pointer.transform.position;
            Vector3 nextPointerPos = new Vector3(hit.point.x, pointerPos.y, hit.point.z);

            // normal next pointer pos to track
            Vector3 normaledPointerPos = Physics.ClosestPoint(nextPointerPos, pointerTrack, pointerTrack.gameObject.transform.position, pointerTrack.gameObject.transform.rotation);

            float keyDistance = Vector3.Distance(normaledPointerPos, lowKeyPos.transform.position);
            float keyRatio = keyDistance / keyboardWidth;
            float key = keyRatio * 127;
            int nextKey = (int)key;

            if (nextKey != currentNote)
            {
                currentNote = nextKey;
                synth.PlayNote(currentNote);
                isPlaying = true;
                pointerMovementEndposition = new Vector3(normaledPointerPos.x, pointerStartingY - 4f, normaledPointerPos.z);
                pointer.transform.position = new Vector3(normaledPointerPos.x, pointerStartingY, normaledPointerPos.z);

            }
        }

        else
        {
            ToggleHighlight(false);
            focusedObject = null;
            isPlaying = false;
        }
    }

    private void ToggleHighlight(bool isOn)
    {
        if (focusedObject)
        {
            Material material = focusedObject.GetComponent<MeshRenderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, isOn ? 0.3f : 0f);
        }
    }
}
