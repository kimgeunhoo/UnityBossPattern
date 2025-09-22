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

    // Animator ������ �ؼ� SetBool ���� �̵�. Self GameObject Animator�� �����ͼ�, animator ������ �����ϰ�, Update true, Success, false
    [SerializeReference] public BlackboardVariable<Animator> MushroomAnimator;

    protected override Status OnStart()
    {
        if (Vector3.Distance(Self.Value.transform.position, TargetLoaction.Value) < 0.1f)
        {

            return Status.Success;
        }


        // ���Ϳ� rigidbody2D������ status�� fail�� �����
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
            rigidbody2D.linearVelocity = (TargetLoaction.Value - Self.Value.transform.position).normalized * Speed.Value; //�������� �̵�

            return Status.Running;
        }
    }
}

