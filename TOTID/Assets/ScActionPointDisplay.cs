using TMPro;
using UnityEngine;

public class ScActionPointDisplay : MonoBehaviour
{
    private ScAction action;

    [SerializeField] TextMeshProUGUI actionPointText;

    private void Awake()
    {
        actionPointText.text = action.actionPoint.ToString();
    }

    public void ActionPointText()
    {
        actionPointText.text  = action.actionPoint.ToString();
    }
}
