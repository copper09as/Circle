using UnityEngine;
using UnityEngine.UI;
public class ItemDisplay : MonoBehaviour
{
    public Image image;
    public ItemData itemData;
    public Button Button;
    private void Start()
    {
        image = GetComponent<Image>();
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Use);
    }
    public void Use()
    {
        itemData.Func();
    }
}