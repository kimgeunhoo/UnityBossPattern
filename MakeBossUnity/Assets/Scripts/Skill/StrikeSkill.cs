using UnityEngine;

[CreateAssetMenu(fileName = "StrikeSkill", menuName = "ScriptableObject/Skill/StrikeSkill")]
public class StrikeSkill : Skill
{
    // strike 스킬을 사용하기 위해 필요한 것...
    // 사운드, 이펙트, 효과, 작용 방식?

    public override void Excute()
    {
        base.Excute();
        Debug.Log("Deal damage to the target");
    }
}
