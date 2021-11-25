using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumBehavior : MonoBehaviour
{
    [SerializeField] float interactionDistance = 10f;
    private Playable focusedDrumable;

    private void FixedUpdate()
    {
        //compute player's forward direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // Raycast originating from camera
        RaycastHit hit;

        Ray ray = new Ray(transform.position, forward);

        Debug.DrawRay(transform.position, forward * interactionDistance, Color.magenta);

        int layerMask = 1 << 9; // only detect Drums

        // Conduct raycast
        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            ToggleHighlight(false);
            focusedDrumable = hit.collider.gameObject.GetComponent<Playable>();
            ToggleHighlight(true);

        }
        else
        {
            ToggleHighlight(false);
        }
    }

    public void OnPlayInstrument()
    {
        if (focusedDrumable)
        {
            focusedDrumable.PlayNote();
        }
    }

    private void ToggleHighlight(bool isOn)
    {
        if (focusedDrumable)
        {
            Material material = focusedDrumable.GetComponent<MeshRenderer>().material;
            material.color = new Color(material.color.r, material.color.g, material.color.b, isOn ? 0.4f : 0f);
        }
    }
}
