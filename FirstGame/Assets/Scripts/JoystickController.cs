using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBackground;  // 摇杆背景
    private RectTransform joystickHandle;      // 摇杆可拖动的前景
    private Vector2 inputVector;               // 输入向量

    public static JoystickController instance;  // 单例实例

    private void Awake()
    {
        // 确保只存在一个实例
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        // 获取 RectTransform 组件
        joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>(); // 摇杆的前景
    }

    // 当用户拖动时
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        // 将触摸位置转换为相对于背景的位置
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackground, eventData.position, eventData.pressEventCamera, out pos))
        {
            // 计算输入的相对位置（在 -1 到 1 之间）
            pos.x = (pos.x / joystickBackground.sizeDelta.x);
            pos.y = (pos.y / joystickBackground.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // 移动摇杆前景
            joystickHandle.anchoredPosition = new Vector2(inputVector.x * (joystickBackground.sizeDelta.x / 2), inputVector.y * (joystickBackground.sizeDelta.y / 2));
        }
    }

    // 当用户按下摇杆时
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // 调用拖动逻辑
    }

    // 当用户释放摇杆时
    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;  // 将摇杆恢复到中心位置
    }

    // 获取水平输入
    public float GetHorizontal()
    {
        return inputVector.x;
    }

    // 获取垂直输入
    public float GetVertical()
    {
        return inputVector.y;
    }
}
