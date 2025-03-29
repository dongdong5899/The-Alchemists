using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    private GameObject cam;
    public bool isFastenY;  //y���� �������ٰų�?
    
    //���� offset�̶�� �����ϸ� �Ǵµ� ���������� ��������
    //0�̸� �� �ȿ����̰�, 1�̸� ī�޶�ӵ��� �� ���缭 1���� ũ�� �� ������, �� ������ �� ������
    public float parallaxEffect;
    [SerializeField] private float height = 0;

    private void Awake()
    {
        startPos = transform.position.x;
        cam = Camera.main.gameObject;
        length = transform.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        //���� ��ġ�� �̵������� ���� ����x ��ġ + �� ī�޶���ġ�� ������ ��������ذ�
        Vector3 targetPos = new Vector3(startPos + dist, height, transform.position.z);

        if (isFastenY)
            targetPos.y = cam.transform.position.y;

        transform.position = targetPos;

        //���Ⱑ �� �̹��� 3�� �����Ƹ鼭 ������ ���ִ� �κ���www
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
