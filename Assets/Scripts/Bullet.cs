using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : Projectile
{
    private Vector2 direction;//진행 방향
    private float turnSpeed;
    [SerializeField]
    private float speed;
    private BulletMoveType moveType;

    private void Start()
    {
        //Renderer renderer = GetComponent<Renderer>();
        //// 깊이 테스트 비활성화
        //renderer.material.SetInt("_ZWrite", 0);
        //// 알파 블렌딩 사용
        //renderer.material.SetFloat("_Mode", 3); // 설정된 총알 Material의 Rendering Mode가 Transparent 여야 합니다.
        //renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //renderer.material.SetInt("_ZWrite", 0);
        //renderer.material.DisableKeyword("_ALPHATEST_ON");
        //renderer.material.EnableKeyword("_ALPHABLEND_ON");
        //renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //GetComponent<Renderer>().material.renderQueue = 3000; // 총알이 다른 객체 뒤에 렌더링되도록 렌더 큐를 조절합니다.
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