using System;
using UnityEngine;


// ����� �� ���ΰ�, �������̽��� ���� ���ΰ�     "A is B" ���(A)��(is) ����(B)�̴�. => Ŭ���� ���, Human Animal
//                                                "������ �ݵ�� �����Ѵ�" => A is B? �ƴ� ��찡 �����Ѵ�. �������̽� ����Ͻÿ�

// ���� �� �ֳ�? -> interace IStoppable
// Stop �� �� �־���Ѵ�. ��� �׼���. ->

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
        // ������ ������ �ִ� �ڷ�ƾ�� ���߱� ���ؼ�
    }

}
