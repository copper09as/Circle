using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CameraTarget : MonoBehaviour {

        private float horizontalinput;
        private float Verticalinput;

        [Header("�����ƶ���Ե")]
        [SerializeField] private Transform LeftDownCorner;
        [SerializeField] private Transform RightUpCorner;

    private bool isDragging = false;
    private Vector3 dragOrigin;

    public void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // ��¼��ק��ʼ��λ��
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // ��������ƶ���ƫ����
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(dragOrigin);

            // ���¶����λ��
            transform.position += new Vector3(-difference.x, -difference.y, 0);

            // �����ƶ���Χ
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, LeftDownCorner.position.x, RightUpCorner.position.x),
                Mathf.Clamp(transform.position.y, LeftDownCorner.position.y, RightUpCorner.position.y),
                transform.position.z
            );

            // ������ק��ʼ��Ϊ��ǰ���λ��
            dragOrigin = Input.mousePosition;
        }
    }
        public void MoveWithImport(float moveSpeed = 100.0f) {
            //�����ƶ���Χ
            transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    LeftDownCorner.transform.position.x,
                    RightUpCorner.transform.position.x
                    ),
                Mathf.Clamp(
                    transform.position.y,
                    LeftDownCorner.transform.position.y,
                    RightUpCorner.transform.position.y
                    ),
                transform.position.z
            );

            //��ȡ�ƶ�����
            horizontalinput = Input.GetAxis("Horizontal");
            Verticalinput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.right * horizontalinput * Time.deltaTime * moveSpeed);
            transform.Translate(Vector3.up * Verticalinput * Time.deltaTime * moveSpeed);
        }
    }
