using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector2 direction;//���� ����
    private float turnSpeed;
    [SerializeField]
    private float speed;
    private BulletMoveType moveType;

    private void Start()
    {
        //Renderer renderer = GetComponent<Renderer>();
        //// ���� �׽�Ʈ ��Ȱ��ȭ
        //renderer.material.SetInt("_ZWrite", 0);
        //// ���� ���� ���
        //renderer.material.SetFloat("_Mode", 3); // ������ �Ѿ� Material�� Rendering Mode�� Transparent ���� �մϴ�.
        //renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //renderer.material.SetInt("_ZWrite", 0);
        //renderer.material.DisableKeyword("_ALPHATEST_ON");
        //renderer.material.EnableKeyword("_ALPHABLEND_ON");
        //renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //GetComponent<Renderer>().material.renderQueue = 3000; // �Ѿ��� �ٸ� ��ü �ڿ� �������ǵ��� ���� ť�� �����մϴ�.
    }
    public void Initialize(BulletMoveType moveType, Vector2 dir, float speed, float turnSpeed=0, float lifeTime = 0)
    {
        this.moveType = moveType;
        direction = dir;
        this.speed = speed;
        this.turnSpeed = turnSpeed;
        if (lifeTime > 0)
        {
            Destroy(gameObject, lifeTime);
        }
        transform.Translate(Vector3.back);
    }

    private void Update()
    {
        if (direction==null)
        {
            return;
        }
        switch (moveType)
        {
            case BulletMoveType.straight:
                transform.Translate(GameManager.Instance.bulletSpeed * GameManager.Instance.gameSpeed * speed * Time.deltaTime * direction);
                break;
            case BulletMoveType.curve:
                transform.Translate(GameManager.Instance.bulletSpeed * GameManager.Instance.gameSpeed * speed * Time.deltaTime * direction);
                Quaternion quat = Quaternion.Euler(0, 0, turnSpeed * GameManager.Instance.gameSpeed * GameManager.Instance.bulletSpeed * Time.deltaTime);
                direction = quat * direction;
                break;
            default:
                break;
        }
    }
}

public enum BulletMoveType
{
    straight,
    curve,
}