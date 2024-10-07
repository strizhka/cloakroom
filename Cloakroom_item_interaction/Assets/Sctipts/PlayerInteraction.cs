using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable{
    public void Interact();
    public void Highlight(bool state);

    public void Drop(bool throwed);
}

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactRange;

    private IInteractable lastInteractedObject, heldItem;
    //private float counter = 0;

    void Update()
    {
        if (heldItem != null && Input.GetKeyDown(KeyCode.E)){
            heldItem.Drop(false);
            heldItem = null;
        }
        else if (heldItem != null && Input.GetKeyDown(KeyCode.Q)){
            heldItem.Drop(true);
            heldItem = null;
        }
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange)){
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactedObject)){
                if (Input.GetKeyDown(KeyCode.E)){
                    interactedObject.Interact();
                    heldItem = interactedObject;
                }
                if (lastInteractedObject == null){
                    interactedObject.Highlight(true);
                }
                if (lastInteractedObject != null && lastInteractedObject != interactedObject){
                    interactedObject.Highlight(true);
                    lastInteractedObject.Highlight(false);
                }
                lastInteractedObject = interactedObject;
            }
        }
        else {
            if (lastInteractedObject != null){
                //counter += Time.deltaTime;
                //if (counter > 1f){
                    lastInteractedObject.Highlight(false);
                    lastInteractedObject = null;
                    //counter = 0;
                //}
            }
        }
    }
}
