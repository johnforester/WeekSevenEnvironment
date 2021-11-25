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

        // Conduct raycast
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            //ToggleHighlight(false);

            focusedObject = hit.collider.gameObject;

            ToggleHighlight(true);
        }
        else
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
            //heldObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        else if (focusedObject.CompareTag("Interactable"))
        {
            // pick up
            focusedObject.transform.parent = pickupSlot.transform;
            focusedObject.transform.position = pickupSlot.transform.position;
          //  focusedObject.GetComponent<Rigidbody>().isKinematic = true;
            holding = true;
            heldObject = focusedObject;
            focusedObject = null;
        }
    }

    public void OnPlayInstrument()
    {
        Debug.Log("attempt play");

        if (focusedObject.CompareTag("Playable"))
        {
            Debug.Log("play");

            Playable playableObj = focusedObject.GetComponent<Playable>();
            playableObj.PlayNote();
        }
    }

    private void ToggleHighlight(bool isOn)
    {
        if (focusedObject.CompareTag("Playable") || focusedObject.CompareTag("Interactable"))
        {
            Material material = focusedObject.GetComponent<MeshRenderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, isOn ? 0.4f : 0f);
        }
    }
}
