using UnityEngine;

[CreateAssetMenu(fileName = "StrikeSkill", menuName = "ScriptableObject/Skill/StrikeSkill")]
public class StrikeSkill : Skill
{
    // strike ��ų�� ����ϱ� ���� �ʿ��� ��...
    // ����, ����Ʈ, ȿ��, �ۿ� ���?

    public override void Excute()
    {
        base.Excute();
        Debug.Log("Deal damage to the target");
    }
}
