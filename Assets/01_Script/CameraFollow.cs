using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    private void LateUpdate()
    {
        //seguir al jugador

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

    }
}
