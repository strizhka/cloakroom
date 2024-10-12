using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Search;


public class NpcMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private Transform _exit;
    [SerializeField] private Transform[] queuePositions;

    private EventBus _eventBus;
    private int _queuePosition;
    private Animator _animator;

    private GameObject _npc;

    public bool IsServed = false;

    private void Awake()
    {
        _eventBus = FindObjectOfType<EventBus>();

        if (_eventBus != null)
        {
            _eventBus.Subscribe<LineChangedSignal>(SetQueuePosition);
            UnityEngine.Debug.Log($"{gameObject.name} подписан на событие LineChangedSignal");
        }
        else
        {
            UnityEngine.Debug.LogWarning("EventBus не найден!");
        }

        _npc = GetComponent<GameObject>();
        _animator = GetComponent<Animator>();
        _eventBus.Subscribe<LineChangedSignal>(SetQueuePosition);

        QueueManager.Enqueue(this);
    }

    private void Update()
    {
        if (!IsServed)
        {
            MoveToTarget();
        }
        else
        {
            OnServed();
        }
    }

    private void MoveToTarget()
    {
        if (_queuePosition >= 0 && _queuePosition < queuePositions.Length)
        {
            Transform target = queuePositions[_queuePosition];

            if (transform.position != target.position)
            {
                Vector3 direction = (target.position - transform.position).normalized;

                transform.Translate(direction * _speed * Time.deltaTime);
            }
            else
            {
                //_animator.SetTrigger("Idle");
            }
        }

        if (_queuePosition > 0 && _queuePosition <= queuePositions.Length)
        {

        }
    }

    public void SetQueuePosition(LineChangedSignal line)
    {
        UnityEngine.Debug.Log(_queuePosition);
        int position = 0;
        foreach (NpcMovement npc in QueueManager.queue)
        {
            if (npc != null)
            {
                npc._queuePosition = position;
                position++;
                npc.MoveToTarget();
            }
            else
            {
                UnityEngine.Debug.LogWarning("NpcMovement был уничтожен и удален из очереди.");
            }
        }
    }


    public void OnServed()
    {
        //_animator.SetTrigger("Exit");
        //QueueManager.Dequeue();

        QueueManager.RemoveNpc(this);
        QueueManager.UpdateLineCount();

        Transform target = _exit;

        if (transform.position != target.position)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else
        {
            try
            {
                Destroy(gameObject);
            }
            catch 
            {
                UnityEngine.Debug.LogError("Не вышло уничтожить нпс");
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, _exit.position, _speed * Time.deltaTime);
    }

    public void OnDestroy()
    {
        //QueueManager.RemoveNpc(this);
        //QueueManager.UpdateLineCount();
    }

}





