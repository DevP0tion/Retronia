using NaughtyAttributes;
using Retronia.Contents.Entities;
using Retronia.Utils;
using Retronia.Utils.UI;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Retronia.Players
{
  public class PlayerController : MonoBehaviour
  {
    public static PlayerController Instance { get; private set; }
    [Button]
    public void Test()
    {
    }

    #region State
    private const string State = "State";
    
    [Foldout(State), SerializeField] private Vector2 direction;
    [Foldout(State), SerializeField] private bool attacking = false;

    #endregion

    #region Binding
    private const string Binding = "Binding";
    
    [Foldout(Binding), SerializeField, GetSet(nameof(Entity))] private Entity character;
    [Foldout(Binding), SerializeField] private new Camera camera;
    [Foldout(Binding), SerializeField] private Transform arrow;
    [Foldout(Binding), SerializeField] private UIGaugeBar healthPointBar;
    [Foldout(Binding), SerializeField] private CinemachineCamera virtualCamera;

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
      if (character) Entity = character;
    }

    private void FixedUpdate()
    {
      if (character)
      {
        // 캐릭터가 화살표를 가르키게 Lerp
        character.PreferredRotation = direction.ToAngle();

        // 커서와 캐릭터의 좌표를 비교하여 방향 설정
        direction = ((Vector2)camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)character.transform.position)
          .normalized * 0.8f;

        // 커서 방향 & 캐릭터 좌표에 비례하여 화살표의 위치 설정
        arrow.position = (Vector3)direction + character.transform.position;

        // 화살표가 커서 방향을 가르키게
        arrow.LookAt2D(character.transform, Vector2.up);
        
        // 마우스 오른쪽 버튼을 누르고 있을시 공격 트리거
        if(attacking && character) character.Shoot(camera.ScreenToWorldPoint(Input.mousePosition));
      }
    }

    private void OnDestroy()
    {
      if (character) character.healthPoint.onChanged.RemoveListener(HealthHook);
      character = null;
      Instance = null;
    }

    #endregion
    
    #region Exports

    public Entity Entity
    {
      get => character;
      set
      {
        if (character) character.healthPoint.onChanged.RemoveListener(HealthHook);
        if (value)
        {
          value.healthPoint.onChanged.AddListener(HealthHook);
          healthPointBar.max = value.healthPoint.Max;
          healthPointBar.Value = value.healthPoint.Value;
          virtualCamera.Follow = value.transform;
          virtualCamera.LookAt = value.transform;
        }
        character = value;
      }
    }

    private void HealthHook(float _)
    {
      if(!character) return;
      
      healthPointBar.max = character.healthPoint.Max;
      healthPointBar.Value = character.healthPoint.Value;
    }

    #endregion
    
    #region Input Action

    public void Attack(InputAction.CallbackContext context)
    {
      attacking = context.performed;
    }

    public void Move(InputAction.CallbackContext context)
    {
      character.IsMove = context.performed;
    }
    
    #endregion
  }
}