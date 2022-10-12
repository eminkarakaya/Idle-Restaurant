using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectManager : MonoBehaviour
{
    bool isMoved;
    [SerializeField] private Button _geriBtn;
    private GameObject _kapatilacakPanel;
    [SerializeField] private GameObject _selectedObject;
    [SerializeField] private Vector3 _oldPos;
    void Update()
    {
        Select();

    }
    
    public void Select()
    {
        // if(Input.touchCount > 0)    
        // {

            // Touch parmak =  Input.GetTouch(0);
                // if(parmak.phase == TouchPhase.Moved)
                // {
                //     Debug.Log("moved");
                //     isMoved = true;
                // }
                
                // if(parmak.phase == TouchPhase.Ended && _selectedObject == null && !isMoved)
                if(Input.GetMouseButton(0) && _selectedObject == null && !isMoved)
                {
                    RaycastHit hit = CastRay();
                    if(hit.collider == null)
                        return;
                    if(hit.collider.TryGetComponent(out Department department))
                    {
                        department.selectableCollider.enabled = false;
                        
                        if(department.isLocked)
                        {
                            department.lockedPanel.GetComponent<Canvas>().enabled = true;
                            _kapatilacakPanel = department.lockedPanel;
                        }
                        else
                        {
                            _kapatilacakPanel = department.acilacakPanel;
                            department.acilacakPanel.GetComponent<Canvas>().enabled =true;
                        }

                        CameraMove.instance.MoveTarget(department.camPlace.position);
                        department.oldCamPlace = CameraMove.instance.transform;
                        _selectedObject = department.selectableCollider.gameObject;
                        _geriBtn.gameObject.SetActive(true);
                        _oldPos = department.oldCamPlace.position;
                        CameraMove.instance.kilitlen = true;
                    }
                    // isMoved = true;
                }
                // if(parmak.phase == TouchPhase.Ended)
                //     isMoved = false;
        // }
    }
    public void GeriButonu()
    {
        CameraMove.instance.kilitlen = false;
        _selectedObject.GetComponent<Collider>().enabled = true;
        CameraMove.instance.MoveTarget(_oldPos);
        _geriBtn.gameObject.SetActive(false);
        _selectedObject = null;
        _kapatilacakPanel.GetComponent<Canvas>().enabled = false;
        // _kapatilacakPanel.SetActive(false);
    }
    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
        );
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
        );
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear,worldMousePosFar - worldMousePosNear, out hit ,Mathf.Infinity);

        return hit;
    }
}
