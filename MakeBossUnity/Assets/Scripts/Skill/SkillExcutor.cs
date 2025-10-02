using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillExcutor : MonoBehaviour
{
    List<Skill> currentSkill = new();

    [SerializeField] Skill[] startSkill;

    private void Start()
    {
        for (int i = 0; i < startSkill.Length; i++)
        {
            currentSkill.Add(startSkill[i]);
        }

        ExcuteSkill(0);
        ExcuteSkill(1);

    }

    // ��ų�� ����, ������... �߰��� �ϰ� �ʹ�.

    public void AddSkill(Skill skill) // ���� ������ Ÿ���� ������ ���� �� �ִ�. -> �ߺ��� ���ϴ� ����� �ڷᱸ���� �������� ��� �ǳ�?
    {
        currentSkill.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        currentSkill.Remove(skill);
    }

    public void ExcuteSkill(int index)
    {
        currentSkill[index].Excute();
    }

}
