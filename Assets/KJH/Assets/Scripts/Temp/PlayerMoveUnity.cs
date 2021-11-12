using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
// 사용자가 점프키를 누르면 점프를 뛰고싶다.
// LShift키를 누르면 빨리 달리고 싶다.
public class PlayerMoveUnity : MonoBehaviour
{
    public float speed = 5;
    float fastSpeed;

    // - Y속도
    public float yVelocity = 0;
    // - 점프뛰는힘
    [SerializeField] 
    float jumpPoewr = 10; // [] = attribute  Serialize 어딘가에 저장된다
    // 중력
    [SerializeField]
    float gravity = -9.81f;
    CharacterController cc;
    

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        fastSpeed = speed * 2;
    }


    // 2단 점프를 뛰고 싶다.
    // 점프칸운트
    int jumpCount= 0;
    // 최대점프 카운트
    public int maxJumpCount = 2;

    // Fixed 1s - 40번 호출 update 1s-60번 호출
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 땅에 서 있을때 
        if (cc.isGrounded)
        {
            // 점프 카운트를 0으로 초기화 해야한다.
            jumpCount = 0;
            yVelocity = 0;
        }
        else     //떠잇을때
        {
            // 1. yVelocity가 중력의 영향을 받고 싶다.     
            yVelocity += gravity * Time.deltaTime;
        }
        // 2. 만약 (점프카운트 < 최대점프카운트) 그리고 (점프 버튼을 눌렀다)면
        // (cc.collisionFlags & CollisionFlags.Below) != 0 ==> isgrounded
        // 3. 단, 점프카운트가 최대점프카운트보다 작아야한다.
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            // 3. yVelocity를 jumpPower의 값으로 대입하고 싶다.
            yVelocity = jumpPoewr;
            // 점프 카운트를 1 씩 증가해야한다.
            jumpCount++;
        }
        
        // 사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 앞뒤좌우 방향을 만들고
        Vector3 direction = new Vector3(h, 0, v);
        // 내가 바라보고 있는 앞이 진짜 앞방향이 되게 하고 싶다.
        direction = Camera.main.transform.TransformDirection(direction);

        // direction의 크기를 1로 만들고 싶다. (Normalize)
        direction.Normalize();

        float finalSpeed = speed;
        // LShift키를 누르면 fastSpeed가 곱해지게 하고싶다
        // 안누르면 speed가 곱해지게 하고 싶다.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = fastSpeed;
        }
        //  float finalSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;

        //Vector3 velocity = direction * (Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;);
        Vector3 velocity = direction * finalSpeed;
        // 4. yVelocity를 실제 방향에 대입하고 싶다.
        velocity.y = yVelocity;
        // 그 방향으로 이동하고 싶다.
        // transform.position += direction * speed * Time.deltaTime;       // 공평해지기 위해서
        cc.Move(velocity * Time.deltaTime);   // vt
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, yVelocity, 0));
    }

    public void SpeedChange(float speed)
    {
        speed *= speed;
        fastSpeed *= speed;
    }
}
