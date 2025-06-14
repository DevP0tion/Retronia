using NaughtyAttributes;
using Retronia.Contents;
using Retronia.Contents.Entities;
using Retronia.Utils;
using Retronia.Utils.UI;
using Retronia.Worlds;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Retronia.Scenes.World
{
  public class PlayerController : MonoBehaviour
  {
    public static PlayerController Instance { get; private set; }
    [Button]
    public void Test()
    {
      BulletManager.Get("BasicBullet");
    }

    #region State
    [Header("Player States")]
    
    [SerializeField, GetSet(nameof(Entity))] private Entity entity;
    private Rigidbody2D body => entity.Body;
    private Vector2 direction
    {
      get => entity.direction;
      set => entity.direction = value;
    }

    [SerializeField] private bool attacking = false;

    #endregion

    #region Binding
    [Header("Player Bindings")]
    
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform arrow;
    [SerializeField] private UIGaugeBar healthPointBar;

    #endregion

    #region Unity Event

#if UNITY_EDITOR
    private void Reset()
    {
      camera = Camera.main;
    }
#endif

    private void Awake()
    {
      if (Instance) Destroy(gameObject);
      else Instance = this;
      // 엔티티가 미리 설정되어있을시 값 초기화용도
      if (entity) Entity = entity;
    }

    private void FixedUpdate()
    {
      if (entity)
      {
        if (body)
          // 캐릭터가 화살표를 가르키게 Lerp
          body.rotation = Mathf.LerpAngle(body.rotation, direction.ToAngle(), Time.fixedDeltaTime * entity.rotateSpeed);

        // 커서와 캐릭터의 좌표를 비교하여 방향 설정
        direction = ((Vector2)camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)entity.transform.position)
          .normalized * 0.8f;

        // 커서 방향 & 캐릭터 좌표에 비례하여 화살표의 위치 설정
        arrow.position = (Vector3)direction + entity.transform.position;

        // 화살표가 커서 방향을 가르키게
        arrow.LookAt2D(entity.transform, Vector2.up);

        // 마우스 왼쪽 버튼을 누를시 해당 방향으로 이동
        if (Input.GetMouseButton(0)) body.AddForce(body.rotation.ToDirection() * entity.speed, ForceMode2D.Impulse);
        
        // 마우스 오른쪽 버튼을 누르고 있을시 공격 트리거
        if(attacking && entity) entity.Shoot(camera.ScreenToWorldPoint(Input.mousePosition));
      }
    }

    private void OnDestroy()
    {
      if (entity) entity.healthPoint.onChanged.RemoveListener(HealthHook);
      entity = null;
      Instance = null;
    }

    #endregion
    
    #region Exports

    public Entity Entity
    {
      get => entity;
      set
      {
        if (entity) entity.healthPoint.onChanged.RemoveListener(HealthHook);
        if (value) value.healthPoint.onChanged.AddListener(HealthHook);
        entity = value;
      }
    }

    private void HealthHook(float _)
    {
      if(!entity) return;
      
      healthPointBar.max = entity.healthPoint.Max;
      healthPointBar.Value = entity.healthPoint.Value;
    }

    #endregion
    
    #region Input Action

    public void Attack(InputAction.CallbackContext context)
    {
      attacking = context.performed;
    }
    
    #endregion
  }
}