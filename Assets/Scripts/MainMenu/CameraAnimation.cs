using UnityEngine;
using UnityEngine.Serialization;

namespace MainMenu
{
public class CameraAnimation : MonoBehaviour
{ 
    [SerializeField]
    float speed = 1f;
    
    void Update()
    {
        //ずっと回転
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
}
