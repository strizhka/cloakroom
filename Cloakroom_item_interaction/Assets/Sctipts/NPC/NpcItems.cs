using UnityEngine;

public class NpcItems : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;
    //[SerializeField] private Transform _handPosition;

    private void Start()
    {
        SpawnRandomItem();
    }

    public void SpawnRandomItem()
    {
        if (_items.Length == 0) return;

        // ������� ����� �������
        int randomIndex = Random.Range(0, _items.Length);
        GameObject selectedItem = Instantiate(_items[randomIndex]);

        // ������������� ������� � ����� NPC
        selectedItem.transform.position = new Vector3(
            gameObject.transform.position.x - 1f,
            gameObject.transform.position.y,
            gameObject.transform.position.z
        );

        // ������ ������ �������� ������� NPC
        selectedItem.transform.SetParent(gameObject.transform);
    }


}

