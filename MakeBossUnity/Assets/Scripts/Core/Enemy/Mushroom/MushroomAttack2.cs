using UnityEngine;

public class MushroomAttack2 : ActionBehavior
{
    Transform target;

    [SerializeField] float waitTimeForCharging = 1f;    // ���� �ð�
    [SerializeField] GameObject projectilePrefab;       // ����ü
    [SerializeField] float projectileRange = 180f;      // ����ü �߻� ����
    [SerializeField] int loopCount = 2;                 // ������ �ݺ� Ƚ��
    [SerializeField] float StunTime = 2f;               // ���� �ð�

    public override void OnStart()
    {
        Debug.Log("MushroomAttack2 Start");
        isPatternEnd = true;
    }

    public override void OnUpdate()
    {

    }
    public override void OnEnd()
    {

    }

    public override void OnStop()
    {
        base.OnStop();
    }
}
