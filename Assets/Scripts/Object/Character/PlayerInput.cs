using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 외부에서 값을 읽어갈 수는 있지만, 수정할 수는 없게(private set) 설정합니다.
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool IsJumpDown { get; private set; }

    void Update()
    {
        // 매 프레임마다 사용자의 입력을 감지해서 변수에 업데이트합니다.
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        // 점프 조건이 있었으니 스페이스바 입력도 미리 만들어 둡니다.
        IsJumpDown = Input.GetKeyDown(KeyCode.Space);
    }
}
