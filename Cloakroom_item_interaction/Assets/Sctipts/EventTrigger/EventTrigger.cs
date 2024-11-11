using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private string acceptedItem;

    public void UseItem(Inventory inventory) {
        inventory.DeleteItem(acceptedItem);
    }
}
