using UnityEngine;

public class Skill : ScriptableObject
{
     public string Name;

    public virtual void Excute()
    {
        Debug.Log($"{Name} 스킬을 사용하였습니다");
    }
}

// 리펙토링을 하고 있기 시작한다. - 하나씩 표현이 아닌 범주, 추상적으로 표현

