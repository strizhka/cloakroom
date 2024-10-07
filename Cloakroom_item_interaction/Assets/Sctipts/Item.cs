using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private float outlineWidth;
    [SerializeField] private float throwingForce;
    [Range(0, 1f)]
    [SerializeField]  private float interpolationSpeed = 0.15f;
    Outline outline;
    private bool moving = false;
    private Transform player;
    private Rigidbody rb;
    public void Interact(){
        moving = true;
        rb.isKinematic = true;
        outline.enabled = false;
    }
    public void Drop(bool throwed){
        rb.isKinematic = false;
        if (throwed){
            rb.AddForce(transform.parent.transform.forward * throwingForce / Time.deltaTime);
        }
        transform.SetParent(null);
    }
    public void Highlight(bool state){
        if (state && rb.isKinematic)
            return;
        outline.enabled = state;
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).transform;

        outline = gameObject.GetComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = outlineWidth;
        outline.enabled = false;
    }

    void Update()
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
}
