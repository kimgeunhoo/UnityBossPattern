using UnityEngine;

public abstract class ActionBehavior : MonoBehaviour
{
    public bool isPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();

}
