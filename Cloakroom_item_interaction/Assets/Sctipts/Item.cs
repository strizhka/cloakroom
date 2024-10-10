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
    [SerializeField]  private float interpolationSpeed = 0.15f;

    Outline outline;

    private bool moving = false;
    private Transform player;
    private Rigidbody rb;
    private EventBus _eventBus;
    private int _secondsLeft;

    private void Start()
    {
        EventBus eventBus = FindObjectOfType<EventBus>();
        _eventBus = eventBus;

        if (_eventBus != null)
        {
            _eventBus.Subscribe<TimeChangedSignal>(GetTime);
        }
        else
        {
            Debug.LogError("EventBus component is missing!");
        }

        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).transform;

        outline = gameObject.GetComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false;
    }

    private void Update()
    {
        if (moving){
            if (Vector3.SqrMagnitude(transform.position - player.position - player.forward * 1f - player.up * -0.4f) < 0.01f){
                transform.position = Vector3.Lerp(transform.position, player.position + player.forward * 1f + player.up * -0.4f, 1f);
                moving = false;
                transform.SetParent(player);
            }
            else {
                transform.position = Vector3.Lerp(transform.position, player.position + player.forward * 1f + player.up * -0.4f, interpolationSpeed);
                //Debug.Log(transform.position);
                //Debug.LogWarning(player.position);
            }
        }
    }

    public void Interact(){
        moving = true;
        rb.isKinematic = true;
        outline.enabled = false;
    }

    public void Drop(bool throwed){
        rb.isKinematic = false;
        if (throwed){
            rb.AddForce(transform.parent.transform.forward * throwingForce);
        }
        transform.SetParent(null);
    }

    public void Highlight(bool state){
        if (state && rb.isKinematic)
            return;
        if (!outline.IsDestroyed())
            outline.enabled = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag(tag+"Zone"))
            {
                Debug.Log("dct dtj");
                AddTime(_seconds);
                Destroy(gameObject);
            }
        }
    }

    private void GetTime(TimeChangedSignal signal)
    {
        _secondsLeft = signal.SecondsLeft;
        Debug.Log(_secondsLeft);
        Debug.Log(signal.SecondsLeft);
    }

    private void AddTime(int seconds)
    {
        _secondsLeft = Mathf.Clamp(_secondsLeft + seconds, 0, 100000);
        _eventBus.Invoke(new TimeChangedSignal(_secondsLeft));
    }

    private void OnDestroy()
    {
        if (_eventBus != null)
        {
            _eventBus.Unsubscribe<TimeChangedSignal>(GetTime);
        }
    }
}
