using UnityEngine;

public class CameraSort : MonoBehaviour
{
    void Start()
    {
        Camera.main.transparencySortMode = TransparencySortMode.CustomAxis;

        Camera.main.transparencySortAxis = new Vector3(0, 1, 0);
    }
}
