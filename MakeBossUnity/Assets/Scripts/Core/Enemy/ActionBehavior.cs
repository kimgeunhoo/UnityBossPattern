using System;
using UnityEngine;


// 상속을 할 것인가, 인터페이스로 만들 것인가     "A is B" 사람(A)은(is) 동물(B)이다. => 클래스 상속, Human Animal
//                                                "유닛은 반드시 공격한다" => A is B? 아닌 경우가 존재한다. 인터페이스 사용하시오

// 멈출 수 있나? -> interace IStoppable
// Stop 할 수 있어야한다. 모든 액션이. ->

public interface IStoppableActionBehavior
{
    void OnStop();
}

public abstract class ActionBehavior : MonoBehaviour, IStoppableActionBehavior
{
    public bool isPatternEnd;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();
    public virtual void OnStop()
    {
        isPatternEnd = false;
        // 각각이 가지고 있는 코루틴을 멈추기 위해서
    }

}
