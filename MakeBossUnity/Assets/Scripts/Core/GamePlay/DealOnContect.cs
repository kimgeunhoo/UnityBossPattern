using UnityEngine;

public class DealOnContect : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private int applyDamage = 5;

    [SerializeField] private ParticleSystem[] contactVFXs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // 충돌한 대상이 데미지를 줄 수 있는 컴포넌트여야 한다. Idamageable 인터페이스를 만들지 않았다면 Mushroom.TakeDamage collision,
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamagable>(out var damageable)) // iDamageable 인터페이스를 상속한 모든 클래스는 작동한다 
        {
            SetApplyDamage();
            damageable.TakeDamage(applyDamage);

            if (contactVFXs != null) 
            {
                foreach (var effect in contactVFXs)
                {
                    effect.Play();
                }
            }
            //Destroy(gameObject);
            // 총알의 이미지를 비활성화 하고
            // 파티클 이펙트가 종료되었을 때 게임 오브젝트 파괴
        }
    }

    private void SetApplyDamage()
    {
        
    }
    
}
