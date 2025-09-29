using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mushroom : MonoBehaviour, IDamagable
{
    // Idle, Run, Stun, A1, A2, Hit, Die �� ���¿� ���� �ڵ带 ���� �����ؾ� �Ѵ�.
    // ������ ���¸� �ڵ�� ������ ���� �����ϴ� ��� ����غ���, ���� ���� �ӽ� finite state machine, Behavior tree

    BehaviorGraphAgent behaviorAgent;
    [SerializeField] EnemyState startState;

    [SerializeField] int maxHealth = 100;
    [field:SerializeField] public int CurrentHealth { get; private set; }
    public Action<int, int> OnHealthbarUpdate { get; set; }

    public Action<bool> OnPatternStart;     // Action, Func �Լ��� ����ó�� �����ؼ� ����ϰڴ�. void �̸�(bool enable)
    public Action<string, bool> OnSomeFuncStart;


    [SerializeField] ParticleSystem rageVFX; // ������ ������ ��尡 �Ǿ��� �� �ߵ��ϴ� ����Ʈ


    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }
    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", startState);
        CurrentHealth = maxHealth;

        OnHealthbarUpdate?.Invoke(CurrentHealth, maxHealth); // ���� �����Ҷ� ����
    }

    private void OnEnable()
    {
        OnPatternStart += HandlePatternStart;
        OnSomeFuncStart += HandleSomeFuncStart;
    }

    private void OnDisable()
    {
        OnPatternStart -= HandlePatternStart;
        OnSomeFuncStart -= HandleSomeFuncStart;
    }

    private void HandlePatternStart(bool enable)
    {
        Debug.Log("HandlePatternStart �Լ� ����");
        behaviorAgent.SetVariableValue<Boolean>("IsPatternTrigger", enable);      // � �̺�Ʈ�� ����Ǿ��� ��

        if (rageVFX.isPlaying) { return; } // �ݺ����� �÷��� ȣ�� ����

        rageVFX.Play();
    }

    private void HandleSomeFuncStart(string methodName, bool enable) // �Լ��� ����ó�� ����ϰ� �ʹ�. -> Action<string, bool>
    {
        Debug.Log("HandleSomeFuncStart �Լ� ����");
        behaviorAgent.SetVariableValue<Boolean>(methodName, enable);
    }


    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        // ������ ���� ����� �ΰ�ȿ�� ���� �ڿ�

        OnHealthbarUpdate?.Invoke(CurrentHealth, maxHealth); // ü���� ������Ʈ�ϴ� �Լ��� �����϶�� ��� Invoke


        if (CurrentHealth < maxHealth * 0.5f)
        {
            OnPatternStart?.Invoke(true);
            //OnSomeFuncStart?.Invoke("IsPatternTrigger", true);
            // ���� ü���� ���� ���ϰ� �ȴٸ� ���� ��ȭ, 2������ ��
            
        }

        if(IsStun())
        {
            StunRaise();
            //return; // ������ �ɷȴٸ� ���� ������ ���� �ʴ´�.
        }

        if (CurrentHealth <= 0)
        {
            // �׾���.
            // animator.SetTrigger("Die");  
            Debug.Log("Mushroom Die");
            behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Die);   

            // ���� bossDeathEvent, ���� �ı�, ���� �̺�Ʈ �߻� -> ���� �׾��� �� � ������ �Ͼ�°�?
        }
    }

    //Bus<IRaiseStunEvent>.Raise()
    private bool IsStun()
    {
        // � ������ �� ������ �ɸ��°�? - ����ȭ ('0')
        // Ư�� ���� Ÿ�Կ� �´� ���·� ������ ���� �� Ȯ���� ���� ������ �ɸ� �� �ִ�.
        // ������ Ư�� ����� �����ϸ� ������ �ɸ���.

        // �ֻ����� ������ ���� ���ں��� ������ ����
        
        int rand = UnityEngine.Random.Range(0, 101); // 0 ~ 100 ��ȯ �ڵ�

        if (rand <= 50)
        {
            return true;
        }
        else 
        {
            return false;
        }

    }

    private void StunRaise()
    {
        // animator.SetTrigger("Stun");
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Stun);
    }

    void Update()
    {
        if (Keyboard.current.tKey.IsPressed())
        {
            TakeDamage(10);
            Debug.Log($"Mushroom Health: {CurrentHealth}");
        }
    }

}
