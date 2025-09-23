using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetPlayerToTarget", story: "Find player to [TargetLocation] .", category: "Action/Find", id: "9bbe5bad21161f22feae824f922dfdde")]
public partial class SetPlayerToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> TargetLocation;

    protected override Status OnStart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Player 태그의 객체가 없습니다.");
            return Status.Failure;
        }
        else
        {
            TargetLocation.Value = player.transform.position;
            return Status.Success;
        }   
    }
}

