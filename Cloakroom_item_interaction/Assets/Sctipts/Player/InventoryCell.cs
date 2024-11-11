using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    private string _itemName;
    public string itemName {
        get {
            return _itemName;
        }
    }
    public void SendSignal(string name) {
        _itemName = name;

        // send signal to event handler
    }
}
