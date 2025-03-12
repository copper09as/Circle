using MainGame;
using UnityEngine;

/// <summary>
/// ����Ŀ�������ʵ�������wasd�ƶ������������Ұ����ק�ƶ����������ڱ�Ե�ƶ�etc
/// ��Ҫ�ҽ���MainCamera��
/// </summary>
public class CameraController : MonoBehaviour
{

    public static CameraController Instance;

    [Header("�����ƶ���Ե")]
    CameraTarget cameraTarget;
    [SerializeField] private Transform target;
    [Tooltip("�����Ŀ��ľ���")]
    [SerializeField] private float distance;
    //[SerializeField] private float addDistanceOffset = 0;
    [SerializeField] private float minOrthoSize = 2f;
    [SerializeField] private float maxOrthoSize = 10f;
    [SerializeField] private float scrollSensitivity = 5f;
    [SerializeField] private float zoomSmoothing = 5f;
    private float targetDistance;
    private float targetOrthoSize;

    //λ��ƫ��


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
            Debug.Log("�Ѿ����س�ʼλ��");
        }
            
    }

    /*private void LateUpdate() {
        if (target != null) {

            if ((target.position.x != transform.position.x) 
                || (target.position.y != transform.position.y)) {
                //���ж�x y����,����֮

                //NOTICE:������ ��ȥ distance��ע�ⳡ����
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
    /// Ϊ�����λ
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
