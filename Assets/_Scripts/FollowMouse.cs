using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Update()
    {
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPos.z = 0;
        transform.position = screenPos;

        if(Input.GetMouseButtonDown(0))
        {
            GetComponent<TurretAttack>().enabled = true;
            Destroy(this);
        }
    }
}
