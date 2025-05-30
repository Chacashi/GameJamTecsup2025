using System;
using UnityEngine;

abstract public class Item : InteractiveObject
{
    [Header("Item")]
    [SerializeField] protected Vector3 itemPosition;
    [SerializeField] protected Vector3 itemRotation;
    [SerializeField] protected Sprite logo;

    protected Transform cameraMain;

    public static event Action<Transform> OnEventSound;
    protected void Awake()
    {
        cameraMain = Camera.main.transform;
    }
    protected override void Interactive()
    {
        transform.SetParent(cameraMain);
        transform.localPosition = itemPosition;
        transform.localRotation = Quaternion.Euler(itemRotation);
        input = true;
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            OnEventSound?.Invoke(this.transform);
        }
    }
}