using TMPro;
using UnityEngine;

public class RepairManager : MonoBehaviour
{
    public int totalParts = 3;
    public int collectedParts = 0;

    [Header("UI")]
    public TMP_Text partsText;
    public TMP_Text objectiveText;

    [Header("Final")]
    public GameObject winPanel;

    private void Start()
    {
        UpdateUI();

        if (winPanel != null)
            winPanel.SetActive(false);
    }

    public void CollectPart()
    {
        collectedParts++;
        UpdateUI();

        if (collectedParts >= totalParts && objectiveText != null)
        {
            objectiveText.text = "Objetivo: Regresa al auto y repáralo.";
        }
    }

    public bool HasAllParts()
    {
        return collectedParts >= totalParts;
    }

    public void RepairCar()
    {
        if (!HasAllParts())
        {
            if (objectiveText != null)
                objectiveText.text = "Aún faltan piezas para reparar el auto.";
            return;
        }

        if (objectiveText != null)
            objectiveText.text = "Lograste reparar el auto. Escapaste del Tec.";

        if (winPanel != null)
            winPanel.SetActive(true);
    }

    private void UpdateUI()
    {
        if (partsText != null)
            partsText.text = $"Piezas: {collectedParts}/{totalParts}";

        if (objectiveText != null && collectedParts < totalParts)
            objectiveText.text = "Objetivo: Busca piezas para reparar el auto.";
    }
}