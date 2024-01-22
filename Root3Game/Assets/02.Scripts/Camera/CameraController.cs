using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 5f;

    public float speed = 10f;//easing speed

    Vector3 hit_position = Vector3.zero;
    Vector3 current_position = Vector3.zero;
    Vector3 camera_position = Vector3.zero;

    bool flag = false;
    Vector3 target_position;

    void Update()
    {
        if (GameManager.Instance._isShowSystemLevelUpPanel) return;

        if (Input.GetMouseButtonDown(0))
        {
            hit_position = Input.mousePosition;
            camera_position = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            current_position = Input.mousePosition;
            LeftMouseDrag();
            flag = true;
        }

        if (flag)
        {
            transform.position = Vector3.Lerp(transform.position, target_position, Time.deltaTime * speed);
            float gap = Mathf.Abs(transform.position.x - target_position.x);
            if (gap < 0.1f)
            {
                flag = false;
            }
        }
    }

    void LeftMouseDrag()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);
        direction = direction * -1;

        Vector3 pos = new Vector3(Mathf.Clamp(camera_position.x + direction.x, minX, maxX), transform.position.y, transform.position.z);
        target_position = pos;
    }
}
