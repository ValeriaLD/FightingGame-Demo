using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] 
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 5, -7);

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
