using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
abstract public class Item : InteractiveObject
{
    [Header("Item")]
    [SerializeField] protected Vector3 itemPosition;
    [SerializeField] protected Vector3 itemRotation;
    [SerializeField] protected Sprite logo;
    protected Rigidbody rb;
    protected Transform cameraMain;
    protected Collider colision;

    [Header("Target")]
    [SerializeField] protected Transform target;

    [Header("Event Sound")]
    [SerializeField] private float distanceSound;
    public static event Action<Vector3, float> OnEventSound;
    private void Reset()
    {
        distanceSound = 50;
    }
    protected void Awake()
    {
        cameraMain = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        colision = GetComponent<Collider>();
    }
    protected override void Interactive()
    {
        if (target.childCount == 0)
        {
            transform.SetParent(target);
            transform.localPosition = itemPosition;
            transform.localRotation = Quaternion.Euler(itemRotation);
            input = true;
            colision.enabled = false;
            rb.isKinematic = true;
        }
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            OnEventSound?.Invoke(this.transform.position,distanceSound);
        }
    }
}