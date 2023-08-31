using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControllerNoMaterial : MonoBehaviour
{
    public GameObject InactiveObject;
    public GameObject GazedAtObject;

    private const float _minObjectDistance = 2.5f;
    private const float _maxObjectDistance = 3.5f;
    private const float _minObjectHeight = 0.5f;
    private const float _maxObjectHeight = 3.5f;

    private Vector3 _startingPosition;
    private bool _isSwitching = false;
    private GameObject _originalObject;

    public void Start()
    {
        _startingPosition = transform.parent.localPosition;
        _originalObject = gameObject;
        SetObject(false);
    }

    public void TeleportRandomly()
    {
        int sibIdx = transform.GetSiblingIndex();
        int numSibs = transform.parent.childCount;
        sibIdx = (sibIdx + Random.Range(1, numSibs)) % numSibs;
        GameObject randomSib = transform.parent.GetChild(sibIdx).gameObject;

        float angle = Random.Range(-Mathf.PI, Mathf.PI);
        float distance = Random.Range(_minObjectDistance, _maxObjectDistance);
        float height = Random.Range(_minObjectHeight, _maxObjectHeight);
        Vector3 newPos = new Vector3(Mathf.Cos(angle) * distance, height,
                                     Mathf.Sin(angle) * distance);

        transform.parent.localPosition = newPos;

        randomSib.SetActive(true);
        gameObject.SetActive(false);
        SetObject(false);
    }

    public void OnPointerEnter()
    {
        SetObject(true);
    }

    public void OnPointerExit()
    {
        SetObject(false);
    }

    public void OnPointerClick()
    {
        if (!_isSwitching)
        {
            _isSwitching = true;
            StartCoroutine(SwitchObjectsAndBack());
        }
    }

    private IEnumerator SwitchObjectsAndBack()
    {
        // Switch to the other object
        SetObject(true);
        yield return new WaitForSeconds(5.0f);

        // Switch back to the original object
        SetObject(false);
        _isSwitching = false;
    }

    private void SetObject(bool gazedAt)
    {
        if (InactiveObject != null && GazedAtObject != null)
        {
            InactiveObject.SetActive(!gazedAt);
            GazedAtObject.SetActive(gazedAt);
        }
    }
}
