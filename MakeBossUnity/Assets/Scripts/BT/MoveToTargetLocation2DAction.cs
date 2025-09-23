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
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Animator ������ �ؼ� SetBool ���� �̵�. Self GameObject Animator�� �����ͼ�, animator ������ �����ϰ�, Update true, Success, false
    

    protected override Status OnStart()
    {
        if (Self.Value.TryGetComponent<Animator>(out var anim))
        {
            animator = anim;
        }

        if(Self.Value.TryGetComponent<SpriteRenderer>(out var _spriteRenderer))
        {
            this.spriteRenderer = _spriteRenderer;
        }

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
        if(Self.Value.transform.position.x < TargetLoaction.Value.x) // player ���� �ڵ�
        {
            spriteRenderer.flipX = true; // �׻� ����
        } 
        else
        {
            spriteRenderer.flipX = false;// �׻� ������
        }  

        if (Vector3.Distance(Self.Value.transform.position, TargetLoaction.Value) < StoppingDistance) // StoppingDistance
        {
            animator.SetBool("IsRun", false);
          
            rigidbody2D.linearVelocity = Vector2.zero;
            return Status.Success;
        }
        else 
        {
            animator.SetBool("IsRun", true);
            rigidbody2D.linearVelocity = (TargetLoaction.Value - Self.Value.transform.position).normalized * Speed.Value; //�������� �̵�
            return Status.Running;
        }
    }
}

