using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttack1 : ActionBehavior
{
    Transform target;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] float waitTimeForCharging = 1f;    // ���� �ð�
    [SerializeField] GameObject projectilePrefab;       // ����ü
    [SerializeField] float projectileRange = 180f;      // ����ü �߻� ����
    [SerializeField] int loopCount = 2;                 // ������ �ݺ� Ƚ��

    [SerializeField] float RightAngle = -60f;
    [SerializeField] float LeftAngle = 120f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void OnStart()
    {
        isPatternEnd = false;
        StartCoroutine(ChargingPattern());
    }

    public override void OnUpdate()
    {
        // �÷��̾��� ���� ��ġ �������� flip�ϴ� �ڵ带

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(transform.position.x < player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

    }
    public override void OnEnd()
    {
         // ������ ������ �� �ʱ�ȭ �ؾ��ϴ� �ڵ尡 �ִٸ� ���⼭ �����Ѵ�.
         isPatternEnd = false;
    }

    public override void OnStop()
    {
        StopCoroutine(ChargingPattern());
        base.OnStop();
    }

    IEnumerator ChargingPattern()
    {
        // �⸦ ������.
        // �� ������ �ִϸ��̼� ���� -> �ִ��� ����ϰ� ���� �غ���
        animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(waitTimeForCharging);

        for (int i = 0; i < loopCount; i++)
        {
            // 180�� ������ ����ü�� �߻��Ѵ�.
            Fire();
            yield return new WaitForSeconds(1f);
        }

        animator.SetTrigger("Stun");
        yield return new WaitForSeconds(2f);

        isPatternEnd = true;
    }

    private void Fire()
    {
        // �� ��ġ�� TargetLoaction ��ġ�� ���ؼ� flip
        // �� ��ġ�� TargetLocation ��ġ�� ���ؼ� ���� ���

        float currentAngle = SelectAngleByPlayerPosition();

        float deltaAngle = projectileRange / 10;

        for(int i = 0; i < 10; i++) // 10 - numberofprojectiles
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            float dirx = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float diry = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector2 Spawn = new Vector2(transform.position.x + dirx, transform.position.y + diry);
            Vector2 dir = (Spawn - (Vector2)transform.position).normalized;

            if (projectileInstance.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = dir * 5f;
            }

            currentAngle += deltaAngle;
        }
    }

    private float SelectAngleByPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (transform.position.x < player.transform.position.x)
        {
            return RightAngle;
        }
        else
        {
            return LeftAngle;
        }
    }
}
