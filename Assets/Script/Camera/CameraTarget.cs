using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CameraTarget : MonoBehaviour {

        private float horizontalinput;
        private float Verticalinput;

        [Header("限制移动边缘")]
        [SerializeField] private Transform LeftDownCorner;
        [SerializeField] private Transform RightUpCorner;

    private bool isDragging = false;
    private Vector3 dragOrigin;

    public void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // 记录拖拽开始的位置
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // 计算鼠标移动的偏移量
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(dragOrigin);

            // 更新对象的位置
            transform.position += new Vector3(-difference.x, -difference.y, 0);

            // 限制移动范围
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, LeftDownCorner.position.x, RightUpCorner.position.x),
                Mathf.Clamp(transform.position.y, LeftDownCorner.position.y, RightUpCorner.position.y),
                transform.position.z
            );

            // 更新拖拽起始点为当前鼠标位置
            dragOrigin = Input.mousePosition;
        }
    }
        public void MoveWithImport(float moveSpeed = 100.0f) {
            //限制移动范围
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

            //获取移动输入
            horizontalinput = Input.GetAxis("Horizontal");
            Verticalinput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.right * horizontalinput * Time.deltaTime * moveSpeed);
            transform.Translate(Vector3.up * Verticalinput * Time.deltaTime * moveSpeed);
        }
    }
