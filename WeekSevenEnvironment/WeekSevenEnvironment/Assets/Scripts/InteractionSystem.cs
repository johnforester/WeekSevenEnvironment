using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private GameObject focusedObject;
    private GameObject heldObject;

    [SerializeField] GameObject pickupSlot;
    [SerializeField] float interactionDistance;

    private bool holding = false;

    private void FixedUpdate()
    {
        if (holding)
        {
            return;
        }

        //compute player's forward direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Raycast originating from camera
        RaycastHit hit;

        Ray ray = new Ray(transform.position, forward);

        Debug.DrawRay(transform.position, forward * interactionDistance, Color.green);

        int layerMask = 1 << 6; // only detect Interactables

        // Conduct raycast
        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            ToggleHighlight(false);

            focusedObject = hit.collider.gameObject;

            ToggleHighlight(true);
        }
        else if (focusedObject)
        {
            ToggleHighlight(false);
            focusedObject = null;
        }
    }

    public void OnInteract()
    {
        if (holding && heldObject != null)
        {
            // drop what we're holding
            holding = false;
            heldObject.transform.parent = null;
            focusedObject = null;
        }

        else if (focusedObject.CompareTag("Interactable"))
        {
            // pick up
            ToggleHighlight(false);

            focusedObject.transform.parent = pickupSlot.transform;
            focusedObject.transform.position = pickupSlot.transform.position;
            holding = true;
            heldObject = focusedObject;
        }
    }

    public void OnPlayInstrument()
    {
        if (!focusedObject)
        {
            return;
        }

        Playable playableObj = focusedObject.GetComponent<Playable>();

        if (playableObj)
        {
            playableObj.PlayNote();
        }
    }

    private bool IsPlayableOrInteractable(GameObject obj)
    {
        if (obj == null)
        {
            return false;
        }

        return obj.CompareTag("Playable") || obj.CompareTag("Interactable");
    }

    private void ToggleHighlight(bool isOn)
    {
        if (IsPlayableOrInteractable(focusedObject))
        {
            Material material = focusedObject.GetComponent<MeshRenderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, isOn ? 0.4f : 0f);
        }
    }
}
