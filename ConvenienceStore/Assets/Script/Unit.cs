using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Unit : MonoBehaviour
{
    public enum UnitType
    {
        Angle,
        Elf,
        BigZombie,
        Dwarf,
        Lizard,
        Boom
    }


    [Header("유닛 설정")]
    [SerializeField] private UnitType uType;
    public UnitType UType => uType;

    [SerializeField] protected float moveSpeed = 5f;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private int unitScore;
    public int UnitScore { get{return unitScore;} } //이걸 호출해서 사용할것이다. 
    [SerializeField] private float followThreshold = 1.5f; // 플레이어와의 최소 거리 (데드존 역할)
    [SerializeField] private float followRange = 5f; // Gizmo용 시각화

    private Coroutine followCoroutine = null;
    private AnimationHandler aniHandler;
    private SpriteRenderer sprite;

    private void Awake()
    {
        aniHandler = GetComponentInChildren<AnimationHandler>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
    
        SettingScore(this.UType);
    }

    //타겟 설정
    public void SetFollowTarget(Transform targetTransform)
    {
        if (followCoroutine != null)
        {
            StopCoroutine(followCoroutine);
        }
        GetComponent<Collider2D>().enabled = false;
        followCoroutine = StartCoroutine(FollowRoutine(targetTransform));
    }

    // 일정 거리 이상일 때만 따라가며 애니메이션 적용
    private IEnumerator FollowRoutine(Transform targetTransform)
    {
        while (targetTransform != null)
        {
            Vector3 direction = targetTransform.position - transform.position;
            float distance = direction.magnitude;

            if (distance > followThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
                aniHandler?.Move(direction);

                if (sprite != null && Mathf.Abs(direction.x) > 0.01f)
                {
                    sprite.flipX = direction.x < 0;
                }
            }
            else
            {
                aniHandler?.Move(Vector2.zero); // 애니메이션 멈춤
            }

            yield return null;
        }

        //정지
        aniHandler?.Move(Vector2.zero);
    }

    // Scene 뷰에서 범위 표시 (디버깅용)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }


    private void SettingScore(UnitType type)
    {
        switch (type)
        {
            case UnitType.Angle:
                unitScore = 100;
                break;
            case UnitType.BigZombie:
                unitScore = 200;
                break;
            case UnitType.Lizard:
                unitScore = 50;
                break;
            case UnitType.Boom:
                unitScore = -300; //음수
                break;

        }

    }


    // Unit.cs 안에서
    void OnDestroy()
    {
        //근데 이건 삭제를 할때 호출하는거 아닌가? 
        UnitSpawner spawner = FindObjectOfType<UnitSpawner>();
        if (spawner != null)
        {
            spawner.UnregisterUnit(transform.position);
        }
    }

}
