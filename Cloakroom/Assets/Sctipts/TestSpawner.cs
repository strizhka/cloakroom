using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private GameObject _npcPrefab;

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_npcPrefab, _spawnPoint.position, _spawnPoint.rotation);
        }
    }


}

