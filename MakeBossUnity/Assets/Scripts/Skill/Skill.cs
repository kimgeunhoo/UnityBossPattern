using UnityEngine;

public class Skill : ScriptableObject
{
     public string Name;

    public virtual void Excute()
    {
        Debug.Log($"{Name} ��ų�� ����Ͽ����ϴ�");
    }
}

// �����丵�� �ϰ� �ֱ� �����Ѵ�. - �ϳ��� ǥ���� �ƴ� ����, �߻������� ǥ��

