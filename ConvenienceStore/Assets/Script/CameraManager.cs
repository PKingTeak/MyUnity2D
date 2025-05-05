using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    [Header("DeadZone")]
    [SerializeField]
    private float deadX;
    [SerializeField]
    private float deadY;
    [SerializeField]
    private float CamSpeed;

    private void LateUpdate()
    {
        //�÷��̾��� �������� ������ ī�޶� �������� �ϱ� ������ ����� ����Ƽ���� ������ LateUpdate();
        
        MoveCam();
    }

    void MoveCam()
    {
        if (player == null)
        {
            return;
        }
        
        Vector3 camPos = transform.position;
        Vector3 playerpos = player.position;

        float dx = playerpos.x - camPos.x;
        float dy = playerpos.y - camPos.y;

        Vector3 targetPos = camPos;


        if (Mathf.Abs(dx) > deadX)
        {
            targetPos.x = playerpos.x - Mathf.Sign(dx) * deadX;
            

        }
        if (Mathf.Abs(dy) > deadY)
        { 
            targetPos.y = playerpos.y - Mathf.Sign(dy) * deadY;
        }


        targetPos.z = camPos.z;

        this.transform.position = Vector3.Lerp(camPos, targetPos, CamSpeed * Time.deltaTime); //�̵�
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 drawPos = new Vector3(transform.position.x, transform.position.y, 0f);

        Gizmos.DrawWireCube(drawPos, new Vector3(deadX*2f,deadY*2f,0f));
    }
}
