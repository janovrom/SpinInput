using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

[ExecuteInEditMode]
public class RadialInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public float angleStepDeg = 5f;
    public FloatEvent rotate;

    private Vector2 _prevVector;
    private Vector2 _angleDelta;
    private Vector2 _center;
    private Vector2 _currentVector;
    private GameObject _selected;


    private void Start()
    {
        _center = Camera.main.WorldToScreenPoint(GetComponent<RectTransform>().TransformPoint(Vector3.zero));
        //_center.y = Screen.height - _center.y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _selected = EventSystem.current.currentSelectedGameObject;
        _prevVector = eventData.position - _center;
        _prevVector.Normalize();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var v = eventData.position;
        v -= _center;
        v.Normalize();
        var angle = Mathf.Round(Vector2.Angle(_prevVector, v));
        float sign = Mathf.Sign(_prevVector.x * v.y - _prevVector.y * v.x);
        _currentVector = v;

        if (angle >= angleStepDeg)
        {
            _prevVector = v;
            rotate.Invoke(sign * angle);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        var p = cam.ScreenToWorldPoint(new Vector3(_center.x, _center.y, 100));
        var p1 = cam.ScreenToWorldPoint(new Vector3(_center.x + _prevVector.x*5f, _center.y + _prevVector.y * 5f, 100));
        var p2 = cam.ScreenToWorldPoint(new Vector3(_center.x + _currentVector.x * 5f, _center.y + _currentVector.y * 5f, 100));
        //p.z = -1
        Gizmos.DrawSphere(p, 0.5f);
        Gizmos.DrawSphere(p1, 0.5f);
        Gizmos.DrawSphere(p2, 0.5f);
    }

}
