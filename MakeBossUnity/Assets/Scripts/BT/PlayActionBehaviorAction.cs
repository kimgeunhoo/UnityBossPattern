using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayActionBehavior", story: "play [ActionBehavior]", category: "Action/Pattern", id: "98dbcfba2315b56ab8569b4bb098e6c3")]
public partial class PlayActionBehaviorAction : Action
{
    [SerializeReference] public BlackboardVariable<ActionBehavior> ActionBehavior;

    protected override Status OnStart()
    {
        ActionBehavior.Value.OnStart();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 패턴이 성공할 때까지 실행
        if(ActionBehavior.Value.isPatternEnd)
        {
            return Status.Success;
        }
        else
        {
            ActionBehavior.Value.OnUpdate();
            return Status.Running;
        }
    }

    protected override void OnEnd()
    {
        ActionBehavior.Value.OnEnd();
    }
}

