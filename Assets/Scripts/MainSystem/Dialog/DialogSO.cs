using UnityEngine;

namespace MainSystem.Dialog
{
[CreateAssetMenu(fileName = "DialogSO", menuName = "ScriptableObject/DialogSO")]
public class DialogSO : ScriptableObject
{
    public string Content = "Dialog Content";
    public float ViewTime = 0.1f;
}
}