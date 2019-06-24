using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SpiralInput : MonoBehaviour
{

    public GameObject buttonPrefab;
    public GameObject currentMarkPrefab;
    public GameObject cameraObject;
    [Range(0,360)]
    public int angleStepDegrees = 12;
    public float depthOffset = 10.0f;
    public float radius = 50f;
    public float markRadius = 80f;
    [Range(0,360)]
    public float angleOffsetDegrees = 90f;
    public CharacterSet characters;
    public List<Button> buttonPool = new List<Button>();
    public GameObject mark;
    public int forwardCount = 3;
    public int backwardCount = 5;
    public bool flipRightVector = false;

    private float _rotationAngleDegrees = 0f;
    private int _current = 0;


    public void ChangeCharacterSet(CharacterSet characters)
    {
        this.characters = characters;
        RemoveChildren();
        CreateButtons();
        PlaceButtons();
    }

    public void OnCharacterPressed()
    {
        buttonPool[_current].onClick.Invoke();
    }

    public void RemoveChildren()
    {
        int count = transform.childCount;
        buttonPool = null;
#if UNITY_EDITOR
        for (int i = count - 1; i >= 0; --i)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        DestroyImmediate(mark);
#else
        for (int i = count - 1; i >= 0; --i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Destroy(mark);
#endif
        mark = null;
    }

    public void CreateButtons()
    {
        // If there are no buttons, create them and save them to the pool
        if (buttonPool == null || buttonPool.Count == 0)
        {
            buttonPool = new List<Button>();
            var forward = (transform.position - cameraObject.transform.position).normalized;
            var right = Vector3.Cross(forward, Vector3.up);
            var up = Vector3.Cross(right, forward);

            for (int i = 0; i < characters.Count; ++i)
            {
                var c = characters[i];
                GameObject go = GameObject.Instantiate(buttonPrefab);
                var rt = go.GetComponent<RectTransform>();
                var parent = gameObject.GetComponent<RectTransform>();
                rt.SetParent(parent, false);
                Button b = go.GetComponent<Button>();
                b.GetComponentInChildren<TextMeshProUGUI>().text = c.displayName;
                buttonPool.Add(b);
            }

        }
        if (mark == null)
        {
            mark = GameObject.Instantiate(currentMarkPrefab);
            mark.transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 90);
            mark.transform.SetParent(transform, false);
        }

        // For all buttons, add their listeners for their respective actions
        // Preserve the actions, if specified using editor
        for (int i = 0; i < buttonPool.Count; ++i)
        {
            var b = buttonPool[i];
            var c = characters[i];
            b.onClick.AddListener(new UnityAction(() => 
            {
                c.DoVirtualKeyAction();
            }));
        }
    }

    public void PlaceButtons()
    {
        if (buttonPrefab == null || cameraObject == null)
        {
            Debug.LogError("Button prefab or camera is not set!");
            return;
        }

        if (characters.Count == 0)
        {
            return;
        }

        var forward = (transform.position - cameraObject.transform.position).normalized;
        var right = Vector3.Cross(forward, Vector3.up);
        var up = Vector3.Cross(right, forward);
        float rightModifier = flipRightVector ? -1 : 1;

        float stepAlpha = angleStepDegrees * Mathf.Deg2Rad;
        float angleOffset = (angleOffsetDegrees + _rotationAngleDegrees) * Mathf.Deg2Rad;

        foreach (var b in buttonPool)
        {
            b.gameObject.SetActive(false);
        }

        for (int i = backwardCount; i >= -forwardCount; --i)
        {
            var idx = _current - i;
            float depth = -depthOffset * i - depthOffset * _rotationAngleDegrees / angleStepDegrees;
            idx = idx % characters.Count;
            idx = idx < 0 ? idx + characters.Count : idx;
            float alpha = stepAlpha * i + angleOffset;
            float x = Mathf.Cos(alpha);
            float y = Mathf.Sin(alpha);

            buttonPool[idx].gameObject.SetActive(true);
            var rt = buttonPool[idx].GetComponent<RectTransform>();
            var img = buttonPool[idx].GetComponent<Image>();
            rt.localPosition = (rightModifier * x * right + y * up) * radius + forward * depth;
            rt.localRotation = Quaternion.LookRotation(forward, up);

            if (i == 0)
                img.color = Color.green;
            else
                img.color = Color.white;
        }

        mark.transform.localPosition = (rightModifier * Mathf.Cos(angleOffsetDegrees * Mathf.Deg2Rad) * right + Mathf.Sin(angleOffsetDegrees * Mathf.Deg2Rad) * up) * markRadius;
        mark.transform.localRotation = Quaternion.LookRotation(forward, up);

        float angle = -angleOffsetDegrees;
        if (flipRightVector)
        {
            if (angle > 180f)
            {
                angle = 360f + (180f - angle);
            }
            else
            {
                angle = 180f - angle;
            }
        }
        mark.transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    public void FlipRightVector()
    {
        flipRightVector = !flipRightVector;
    }

    private void Start()
    {
        RemoveChildren();
        CreateButtons();
        if (buttonPool.Count > 0)
        {
            PlaceButtons();
            buttonPool[_current].GetComponent<Image>().color = Color.green;
        }
    }

    public void Rotate(float angleDeg)
    {
        _rotationAngleDegrees += angleDeg;
        if (_rotationAngleDegrees > angleStepDegrees * 0.5f)
        {
            _rotationAngleDegrees = _rotationAngleDegrees - angleStepDegrees;
            _current += 1;
            _current = _current % characters.Count;
        }

        if (_rotationAngleDegrees < -angleStepDegrees * 0.5f)
        {
            _rotationAngleDegrees = _rotationAngleDegrees + angleStepDegrees;
            _current -= 1;
            _current = _current < 0 ? _current + characters.Count : _current;
            _current = _current % characters.Count;
        }
        PlaceButtons();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (cameraObject != null)
        {
            var forward = (transform.position - cameraObject.transform.position).normalized;
            var right = Vector3.Cross(forward, Vector3.up);
            var up = Vector3.Cross(right, forward);

            Gizmos.DrawRay(transform.position, right * radius);
            Gizmos.DrawRay(transform.position, forward * radius);
            Gizmos.DrawRay(transform.position, up * radius);
        }
    }
#endif

}
