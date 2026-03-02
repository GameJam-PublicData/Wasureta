using UnityEngine;
using VContainer;

namespace MainSystem.Dialog
{
public class DialogTest : MonoBehaviour
{
    [SerializeField] DialogSO dialogSO;
    IDialogSystem _dialogSystem;

    [Inject]
    public void Construct(IDialogSystem dialogSystem)
    {
        Debug.Log("DialogTest");
        _dialogSystem = dialogSystem;
    }

    void Start()
    {
        Debug.Log("DialogTest.Start");
        _dialogSystem.ShowDialog(dialogSO);
    }
}
}