using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms.Impl;

public class Item : MonoBehaviour, IInteractable
{

    [SerializeField] private float outlineWidth;
    [SerializeField] private float throwingForce;
    [SerializeField] private int _seconds;

    [Range(0, 1f)]
    [SerializeField] private float interpolationSpeed = 0.15f;
    [SerializeField] private bool isEventRelated = false;
    [SerializeField] Sprite icon; // null for general items

    Outline outline;

    private bool moving = false;
    private Transform player;
    private Rigidbody rb;
    private EventBus _eventBus;
    private NpcMovement _npcMovement;

    private void Start()
    {
        EventBus eventBus = FindObjectOfType<EventBus>();
        _eventBus = eventBus;


        _npcMovement = GetComponentInParent<NpcMovement>();

        if (!isEventRelated)
            rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).transform;

        outline = gameObject.GetComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        //outline.OutlineColor = Color.red;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false;
    }

    private void Update()
    {
        if (moving){
            if (Vector3.SqrMagnitude(transform.position - player.position - player.forward * 1f) < 0.01f){
                transform.position = Vector3.Lerp(transform.position, player.position + player.forward * 1f, 1f);
                moving = false;
                transform.SetParent(player);
            }
            else {
                transform.position = Vector3.Lerp(transform.position, player.position + player.forward * 1f , interpolationSpeed);
            }
        }
    }

    public void Interact(){
        if (isEventRelated) {
            player.GetComponent<Inventory>().InsertItem(icon, transform.name);
            Destroy(gameObject);
            return;
        }
        moving = true;
        rb.isKinematic = true;
        outline.enabled = false;
    }

    public void Drop(bool throwed){
        if (rb != null)
        {
            rb.isKinematic = false;
            if (throwed){
                rb.AddForce(transform.parent.transform.forward * throwingForce / Time.deltaTime);
            }
            transform.SetParent(null);
        }
    }

    public void Highlight(bool state){
        if (!isEventRelated && state && rb.isKinematic && (!gameObject.CompareTag("Phone") && !gameObject.CompareTag("Baggage") && !gameObject.CompareTag("Coats1") && !gameObject.CompareTag("Coats2")))
            return;
        if (!outline.IsDestroyed())
            outline.enabled = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag(tag + "Zone"))
            {
                AddTime(_seconds);
                if (_npcMovement != null)
                {
                    _npcMovement.IsServed = true;
                }
                else
                {

                }
                Destroy(gameObject);
            }
        }
    }

    private void AddTime(float seconds)
    {
        Timer.SecondsLeft = Mathf.Clamp(Timer.SecondsLeft + seconds, 0, 90);
        _eventBus.Invoke(new TimerChangedSignal(Timer.SecondsLeft));

    }

}
