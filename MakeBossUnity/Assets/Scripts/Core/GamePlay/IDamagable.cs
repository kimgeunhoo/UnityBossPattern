using UnityEngine;

// A is B �� �ƴѵ�, �������� �޴� ���� �������� ǥ���ϰ� �ʹ� => �÷��̾�, ���� �������� �Դ�. PlayerDamage, EnemyDamage, NPV - Damage
public interface IDamagable
{
    int CurrentHealth { get; }

    void TakeDamage(int damage);
}
