using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int cellSize = 128;
    [SerializeField] private int cellGap = 16;
    [SerializeField] private GameObject cell;
    private RectTransform bar;
    private int currentSize = 0;
    void Start()
    {
        bar = transform.GetChild(0).Find("Inventory").GetChild(0).GetComponent<RectTransform>();
        bar.sizeDelta = new Vector2(0, bar.sizeDelta.y);
    }

    public void InsertItem(Sprite image, string name) {
        currentSize++;

        GameObject newCell = Instantiate(cell, bar);
        if (image == null) {
            Debug.LogWarning("Event related item's icon missing");
            newCell.transform.GetChild(0).GetComponent<Image>().color = Color.magenta;
        }
        else {
            newCell.transform.GetChild(0).GetComponent<Image>().sprite = image;
        }

        CalculateInventoryAppearance();

        newCell.GetComponent<InventoryCell>().SendSignal(name);
    }

    public void DeleteItem(string name) {
        int index = -1;
        for (int i = 0; i < bar.childCount; i++) {
            if (bar.GetChild(i).GetComponent<InventoryCell>().itemName == name) {
                index = i;
                Destroy(bar.GetChild(i).gameObject);
                break;
            }
        }

        if (index >= 0) {
            currentSize--;
            CalculateInventoryAppearance(index);
        }
    }

    private void CalculateInventoryAppearance(int ignore = -1) {
        int barWidth = currentSize * (cellSize + cellGap) + cellGap;
        bar.sizeDelta = new Vector2(barWidth, bar.sizeDelta.y);

        if (currentSize == 0) {
            bar.sizeDelta = new Vector2(0, bar.sizeDelta.y);
        }

        int index = 0;
        for (int i = 0; i < bar.childCount; i++) {
            if (i == ignore) {
                index++;
                continue;
            }
            bar.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(
                cellGap + cellSize / 2 + (cellGap + cellSize) * (i - index) - barWidth / 2, 0
            );
        }
    }
}


