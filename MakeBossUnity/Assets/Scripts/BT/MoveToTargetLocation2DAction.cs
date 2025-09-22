using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToTargetLocation2D", story: "[Self] move to [TargetLoaction] .", category: "Action/Navigation", id: "3912096c8a9bf7a05a72512190458cb9")]
public partial class MoveToTargetLocation2DAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> TargetLoaction;
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> StoppingDistance;
    Rigidbody2D rigidbody2D;

    // Animator 접근을 해서 SetBool 통해 이동. Self GameObject Animator를 가져와서, animator 변수에 저장하고, Update true, Success, false
    [SerializeReference] public BlackboardVariable<Animator> MushroomAnimator;

    protected override Status OnStart()
    {
        if (Vector3.Distance(Self.Value.transform.position, TargetLoaction.Value) < 0.1f)
        {

            return Status.Success;
        }


        // 몬스터에 rigidbody2D없으면 status를 fail로 만들기
        if (Self.Value.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid)) 
        {
            rigidbody2D = rigid;
            return Status.Running;
        } 
        else 
        {
            return Status.Failure;
        }

        
    }

    protected override Status OnUpdate()
    {
        if (Vector3.Distance(Self.Value.transform.position, TargetLoaction.Value) < StoppingDistance) // StoppingDistance
        {
            //MushroomAnimator.ObjectValue = Self.Value;
            rigidbody2D.linearVelocity = Vector2.zero;
            return Status.Success;
        }
        else 
        {
            rigidbody2D.linearVelocity = (TargetLoaction.Value - Self.Value.transform.position).normalized * Speed.Value; //방향으로 이동

            return Status.Running;
        }
    }
}

