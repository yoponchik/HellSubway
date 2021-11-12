using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� �Է¿� ���� �յ��¿�� �̵��ϰ� �ʹ�.
// ����ڰ� ����Ű�� ������ ������ �ٰ�ʹ�.
// LShiftŰ�� ������ ���� �޸��� �ʹ�.
public class PlayerMoveUnity : MonoBehaviour
{
    public float speed = 5;
    float fastSpeed;

    // - Y�ӵ�
    public float yVelocity = 0;
    // - �����ٴ���
    [SerializeField] 
    float jumpPoewr = 10; // [] = attribute  Serialize ��򰡿� ����ȴ�
    // �߷�
    [SerializeField]
    float gravity = -9.81f;
    CharacterController cc;
    

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        fastSpeed = speed * 2;
    }


    // 2�� ������ �ٰ� �ʹ�.
    // ����ĭ��Ʈ
    int jumpCount= 0;
    // �ִ����� ī��Ʈ
    public int maxJumpCount = 2;

    // Fixed 1s - 40�� ȣ�� update 1s-60�� ȣ��
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. ���� �� ������ 
        if (cc.isGrounded)
        {
            // ���� ī��Ʈ�� 0���� �ʱ�ȭ �ؾ��Ѵ�.
            jumpCount = 0;
            yVelocity = 0;
        }
        else     //��������
        {
            // 1. yVelocity�� �߷��� ������ �ް� �ʹ�.     
            yVelocity += gravity * Time.deltaTime;
        }
        // 2. ���� (����ī��Ʈ < �ִ�����ī��Ʈ) �׸��� (���� ��ư�� ������)��
        // (cc.collisionFlags & CollisionFlags.Below) != 0 ==> isgrounded
        // 3. ��, ����ī��Ʈ�� �ִ�����ī��Ʈ���� �۾ƾ��Ѵ�.
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            // 3. yVelocity�� jumpPower�� ������ �����ϰ� �ʹ�.
            yVelocity = jumpPoewr;
            // ���� ī��Ʈ�� 1 �� �����ؾ��Ѵ�.
            jumpCount++;
        }
        
        // ������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // �յ��¿� ������ �����
        Vector3 direction = new Vector3(h, 0, v);
        // ���� �ٶ󺸰� �ִ� ���� ��¥ �չ����� �ǰ� �ϰ� �ʹ�.
        direction = Camera.main.transform.TransformDirection(direction);

        // direction�� ũ�⸦ 1�� ����� �ʹ�. (Normalize)
        direction.Normalize();

        float finalSpeed = speed;
        // LShiftŰ�� ������ fastSpeed�� �������� �ϰ�ʹ�
        // �ȴ����� speed�� �������� �ϰ� �ʹ�.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = fastSpeed;
        }
        //  float finalSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;

        //Vector3 velocity = direction * (Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : speed;);
        Vector3 velocity = direction * finalSpeed;
        // 4. yVelocity�� ���� ���⿡ �����ϰ� �ʹ�.
        velocity.y = yVelocity;
        // �� �������� �̵��ϰ� �ʹ�.
        // transform.position += direction * speed * Time.deltaTime;       // ���������� ���ؼ�
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
