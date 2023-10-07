using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCamera : MonoBehaviour
{
    float dragOrigin;
    [SerializeField] private float groundZ = 0;
    [SerializeField] private SpriteRenderer mapRenderer;
    [SerializeField] private float minX, maxX, minY, maxY;
    private void Start()
    {
        minX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x /2;
        maxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x /2;
        minY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y /2;
        maxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y /2;
    }
    private void Update()
    {
        PanCamera();
    }
    void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = GetWorldPosition(groundZ).x;
        if(Input.GetMouseButton(0))
        {
            float direction = dragOrigin - GetWorldPosition(groundZ).x;
            Vector3 pos = ClampCamera(new Vector3(Camera.main.transform.position.x + direction,transform.position.y,transform.position.z));
            Camera.main.transform.position = pos;
        }
    }
    private Vector3 GetWorldPosition(float z)
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }
    private Vector3 ClampCamera(Vector3 targetPos)
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float _minX = minX + camWidth;
        float _maxX = maxX - camWidth;
        float newX = Mathf.Clamp(targetPos.x, _minX, _maxX);

        return new Vector3(newX, targetPos.y, targetPos.z);
    }
}
