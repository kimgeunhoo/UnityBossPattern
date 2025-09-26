using UnityEngine;

public class DealOnContect : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private int applyDamage = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // �浹�� ����� �������� �� �� �ִ� ������Ʈ���� �Ѵ�. Idamageable �������̽��� ������ �ʾҴٸ� Mushroom.TakeDamage collision,
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamagable>(out var damageable)) // iDamageable �������̽��� ����� ��� Ŭ������ �۵��Ѵ� 
        {
            SetApplyDamage();
            damageable.TakeDamage(applyDamage);

            Destroy(gameObject);
        }
    }

    private void SetApplyDamage()
    {
        
    }

}
