using UnityEngine;
using UnityEngine.UI;

public class GridInteraction : MonoBehaviour
{
    public MaterialElement[] availableMaterials;  // 16�ֲ���
    private MaterialElement selectedMaterial;

    public void SelectMaterial(int index)
    {
        selectedMaterial = availableMaterials[index];
    }

    public void PlaceMaterial(GridCell cell)
    {
        if (cell.isPath && cell.material == null)  // ֻ�����·���ϵĸ���
        {
            cell.material = selectedMaterial;
            Debug.Log("���ò��ϣ�" + selectedMaterial.name);
        }
    }
}
