using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSkill", menuName = "ScriptableObject/Skill/ShieldSkill")]
public class ShieldSkill : Skill
{
    public override void Excute()
    {
        base.Excute();
        Debug.Log("Deal damage to the target");
    }
}
