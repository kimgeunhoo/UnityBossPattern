using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mushroom : MonoBehaviour, IDamagable
{
    // Idle, Run, Stun, A1, A2, Hit, Die 이 상태에 따른 코드를 전부 정의해야 한다.
    // 각각의 상태를 코드로 정의해 놓고 조립하는 방식 사용해보기, 유한 상태 머신 finite state machine, Behavior tree

    BehaviorGraphAgent behaviorAgent;
    [SerializeField] EnemyState startState;

    [SerializeField] int maxHealth = 100;
    [field:SerializeField] public int CurrentHealth { get; private set; }
    public Action<int, int> OnHealthbarUpdate { get; set; }

    public Action<bool> OnPatternStart;     // Action, Func 함수를 변수처럼 저장해서 사용하겠다. void 이름(bool enable)
    public Action<string, bool> OnSomeFuncStart;


    [SerializeField] ParticleSystem rageVFX; // 보스가 레이지 모드가 되었을 때 발동하는 이펙트


    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
    }
    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", startState);
        CurrentHealth = maxHealth;

        OnHealthbarUpdate?.Invoke(CurrentHealth, maxHealth); // 연속 전투할때 갱신
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
        Debug.Log("HandlePatternStart 함수 실행");
        behaviorAgent.SetVariableValue<Boolean>("IsPatternTrigger", enable);      // 어떤 이벤트가 실행되었을 때

        if (rageVFX.isPlaying) { return; } // 반복적인 플레이 호출 방지

        rageVFX.Play();
    }

    private void HandleSomeFuncStart(string methodName, bool enable) // 함수를 변수처럼 사용하고 싶다. -> Action<string, bool>
    {
        Debug.Log("HandleSomeFuncStart 함수 실행");
        behaviorAgent.SetVariableValue<Boolean>(methodName, enable);
    }


    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        // 아이템 버프 등등의 부가효과 적용 뒤에

        OnHealthbarUpdate?.Invoke(CurrentHealth, maxHealth); // 체력을 업데이트하는 함수를 실행하라고 명령 Invoke


        if (CurrentHealth < maxHealth * 0.5f)
        {
            OnPatternStart?.Invoke(true);
            //OnSomeFuncStart?.Invoke("IsPatternTrigger", true);
            // 보스 체력이 일정 이하가 된다면 패턴 강화, 2페이즈 등
            
        }

        if(IsStun())
        {
            StunRaise();
            //return; // 스턴이 걸렸다면 죽음 판정을 하지 않는다.
        }

        if (CurrentHealth <= 0)
        {
            // 죽었다.
            // animator.SetTrigger("Die");  
            Debug.Log("Mushroom Die");
            behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Die);   

            // 게임 bossDeathEvent, 몬스터 파괴, 폭팔 이벤트 발생 -> 보스 죽었을 때 어떤 현상이 일어나는가?
        }
    }

    //Bus<IRaiseStunEvent>.Raise()
    private bool IsStun()
    {
        // 어떤 조건일 때 스턴이 걸리는가? - 무력화 ('0')
        // 특정 무기 타입에 맞는 형태로 공격을 했을 때 확률에 따라 스턴이 걸릴 수 있다.
        // 보스의 특정 기믹을 성공하면 스턴이 걸린다.

        // 주사위를 돌려서 일정 숫자보다 작으면 스턴
        
        int rand = UnityEngine.Random.Range(0, 101); // 0 ~ 100 반환 코드

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
