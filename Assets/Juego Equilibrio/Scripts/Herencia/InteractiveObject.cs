using UnityEngine;

abstract public class InteractiveObject : MonoBehaviour
{
    protected bool input;
    protected abstract void Interactive();
    public void Input(bool value)
    {
        if (value)
        {
            InputReader.interactive += Interactive;
        }
        else
        {
            InputReader.interactive -= Interactive;
        }
    }
}
