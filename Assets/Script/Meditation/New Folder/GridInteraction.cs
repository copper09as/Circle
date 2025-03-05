using UnityEngine;
using UnityEngine.UI;

public class GridInteraction : MonoBehaviour
{
    public MaterialElement[] availableMaterials;  // 16种材料
    private MaterialElement selectedMaterial;

    public void SelectMaterial(int index)
    {
        selectedMaterial = availableMaterials[index];
    }

    public void PlaceMaterial(GridCell cell)
    {
        if (cell.isPath && cell.material == null)  // 只能填充路径上的格子
        {
            cell.material = selectedMaterial;
            Debug.Log("放置材料：" + selectedMaterial.name);
        }
    }
}
