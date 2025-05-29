using System;
using UnityEngine;

abstract public class Item : InteractiveObject
{
    [Header("Item")]
    [SerializeField] protected Vector3 itemPosition;
    [SerializeField] protected Vector3 itemRotation;
    [SerializeField] protected Sprite logo;

    protected Transform cameraMain;

    static public event Action<GameObject, Sprite> eventInformation;
    static public event Action<GameObject> eventRemove;
    protected void Awake()
    {
        cameraMain = Camera.main.transform;
    }
    protected override void Interactive()
    {
        transform.SetParent(cameraMain);
        transform.localPosition = itemPosition;
        transform.localRotation = Quaternion.Euler(itemRotation);
        eventInformation?.Invoke(this.gameObject, logo);
        input = true;
    }
    protected virtual void ActiveEventRemove(GameObject go)
    {
        eventRemove?.Invoke(go);
    }
}