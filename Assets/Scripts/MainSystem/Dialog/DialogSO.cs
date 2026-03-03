using UnityEngine;

namespace MainSystem.Dialog
{
[CreateAssetMenu(fileName = "DialogSO", menuName = "ScriptableObject/DialogSO")]
public class DialogSO : ScriptableObject
{
    public string DialogContent = "Dialog Content";
    public string RushIntroContent = "Forget Content";
    public float CharInterval = 0.1f;
    public float LineEndDelay = 1f;
}
}