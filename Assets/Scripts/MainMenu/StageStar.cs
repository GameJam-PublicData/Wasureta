using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MainMenu
{
public class StageStar : MonoBehaviour
{
    [SerializeField] Sprite onStar;
    [SerializeField] Sprite offStar;
    
    [SerializeField] Image[] stars;

    public void SetStar(int star)
    {
        for (int i = 0; i < star; i++)
        {
            stars[i].sprite = onStar;
        }
        for(int i = star; i < stars.Length; i++)
        {
            stars[i].sprite = offStar;
        }
    }
}
}
