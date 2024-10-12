using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnNpc();
        }
    }

    private void SpawnNpc()
    {
        // ����� NPC
        /*GameObject spawnedNpc = */Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);

        //// �������� ��������� NpcItems � ����������������� NPC � �������� ����� ��� ��������� ��������
        //NpcItems npcItems = spawnedNpc.GetComponent<NpcItems>();
        //if (npcItems != null)
        //{
        //    npcItems.SpawnRandomItem();
        //}
    }
}

