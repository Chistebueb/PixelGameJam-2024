using UnityEngine;

public class YAxisRotator : MonoBehaviour
{
    // Serialize this field so you can assign the target GameObject in the Unity Editor
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Look at target
            transform.LookAt(target);

            // Restrict rotation to only the Y axis
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
}
