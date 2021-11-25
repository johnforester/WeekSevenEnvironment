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

        // Conduct raycast
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            focusedObject = hit.collider.gameObject;
        }
        else
        {
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
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        else if (focusedObject.CompareTag("Interactable"))
        {
            // pick up
            focusedObject.transform.parent = pickupSlot.transform;
            focusedObject.transform.position = pickupSlot.transform.position;
            focusedObject.GetComponent<Rigidbody>().isKinematic = true;
            holding = true;
            heldObject = focusedObject;
            focusedObject = null;
        }
    }
}
