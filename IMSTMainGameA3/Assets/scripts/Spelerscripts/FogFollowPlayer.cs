using UnityEngine;

public class FogFollowPlayer : MonoBehaviour
{
    public Transform target; // De speler transform

    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
    }
}
