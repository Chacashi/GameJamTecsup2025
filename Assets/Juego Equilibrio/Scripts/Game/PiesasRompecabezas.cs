using UnityEngine;

public class PiesasRompecabezas : InteractiveObject
{
    protected override void Interactive()
    {
        GameManager.instance.CheckRompeCabezas();
        Destroy(gameObject);
    }
}
