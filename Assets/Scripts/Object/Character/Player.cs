using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float _moveSpeed = 2f; // 이동 속도

    [Header("연결된 컴포넌트")]
    [SerializeField] private FujimaruAnimationController AnimatorController_Entity;

    private Rigidbody2D _rigidBody;
    private PlayerInput _input;
    private bool _lookRight = true;

    void Awake()
    {
  
        _rigidBody = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();

        if (AnimatorController_Entity == null)
            AnimatorController_Entity = GetComponent<FujimaruAnimationController>();

        _rigidBody.gravityScale = 0f;
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        HandleFlip();

        bool isMoving = (_input.Horizontal != 0 || _input.Vertical != 0);
        AnimatorController_Entity.SetState(isMoving ? Player_AnimState.Walk : Player_AnimState.Idle);
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveDirection = new Vector2(_input.Horizontal, _input.Vertical).normalized;

        _rigidBody.linearVelocity = moveDirection * _moveSpeed;

        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
        {
            _rigidBody.linearVelocity = Vector2.zero;

            return;
        }
    }

    private void HandleFlip()
    {

        if (_input.Horizontal > 0 && !_lookRight) Flip();
        else if (_input.Horizontal < 0 && _lookRight) Flip();
    }

    private void Flip()
    {
        _lookRight = !_lookRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}