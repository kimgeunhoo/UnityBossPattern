using UnityEngine;

public class MushroomAttack2 : ActionBehavior
{
    Transform target;

    [SerializeField] float waitTimeForCharging = 1f;    // 차지 시간
    [SerializeField] GameObject projectilePrefab;       // 투사체
    [SerializeField] float projectileRange = 180f;      // 투사체 발사 각도
    [SerializeField] int loopCount = 2;                 // 패턴의 반복 횟수
    [SerializeField] float StunTime = 2f;               // 스턴 시간

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
