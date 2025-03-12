using MainGame;
using UnityEngine;

/// <summary>
/// 相机的控制器，实现相机的wasd移动、鼠标缩放视野、拖拽移动、鼠标放置在边缘移动etc
/// 需要挂接在MainCamera上
/// </summary>
public class CameraController : MonoBehaviour
{

    public static CameraController Instance;

    [Header("限制移动边缘")]
    CameraTarget cameraTarget;
    [SerializeField] private Transform target;
    [Tooltip("相机到目标的距离")]
    [SerializeField] private float distance;
    //[SerializeField] private float addDistanceOffset = 0;
    [SerializeField] private float minOrthoSize = 2f;
    [SerializeField] private float maxOrthoSize = 10f;
    [SerializeField] private float scrollSensitivity = 5f;
    [SerializeField] private float zoomSmoothing = 5f;
    private float targetDistance;
    private float targetOrthoSize;

    //位置偏移


    [SerializeField] private float smoothing = 10.0f;

    [SerializeField] private float moveSpeed = 10.0f;


    private void Start()
    {
        Instance = this;
        cameraTarget = target.GetComponent<CameraTarget>();
    }

    private void Update()
    {
        if (State.Instance.currentState != GameState.Map)
            return;
        cameraTarget.HandleMouseInput();
        cameraTarget.MoveWithImport(moveSpeed);
        ScrollView();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetCameraPos(new Vector3(0, 0, -1));
            Debug.Log("已经返回初始位置");
        }
            
    }

    /*private void LateUpdate() {
        if (target != null) {

            if ((target.position.x != transform.position.x) 
                || (target.position.y != transform.position.y)) {
                //仅判断x y方向,跟随之

                //NOTICE:这里是 减去 distance，注意场景！
                Vector3 targetPos = new Vector3(
                    target.position.x, 
                    target.position.y, 
                    target.position.z
                );
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }*/

    void ScrollView()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetOrthoSize -= scroll * scrollSensitivity;
        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthoSize, maxOrthoSize);
        GetComponent<Camera>().orthographicSize =
            Mathf.Lerp(
            GetComponent<Camera>().orthographicSize,
            targetOrthoSize,
            Time.deltaTime * zoomSmoothing
        );

    }

    /// <summary>
    /// 为相机定位
    /// </summary>
    public void SetCameraPos(Vector3 pos,float moveSpeed = 100f)
    {
        transform.position  = new Vector3(
                Mathf.Lerp(
                    transform.position.x,
                    pos.x,
                    Time.deltaTime * moveSpeed
                    ),
                Mathf.Lerp(
                    transform.position.y,
                    pos.y,
                    Time.deltaTime * moveSpeed
                    ),
                transform.position.z
            );
        GetComponent<Camera>().orthographicSize =
        Mathf.Lerp(
        GetComponent<Camera>().orthographicSize,
        16.5f,
        Time.deltaTime * zoomSmoothing
);
        //Debug.Log("move camera");
    }

}
