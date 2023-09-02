using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControllerNoMaterial : MonoBehaviour
{
    public GameObject InactiveObject;
    public GameObject GazedAtObject;

    private bool _isGazedAt = false;

    private void OnPointerEnter()
    {
        _isGazedAt = true;
        SetObject(true);
    }

    private void OnPointerExit()
    {
        _isGazedAt = false;
        SetObject(false);
    }

    private void Update()
    {
        if (_isGazedAt && Input.GetMouseButtonDown(1)) // Cambiamos a Input.GetMouseButtonDown(1) para clic derecho
        {
            // Comprobar si se está mirando el objeto con un Raycast
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // Solo cambia el objeto si se hizo clic derecho en el objeto
                    StartCoroutine(SwitchObjectsAndBack());
                }
            }
        }
    }

    private void SetObject(bool gazedAt)
    {
        if (InactiveObject != null && GazedAtObject != null)
        {
            InactiveObject.SetActive(!gazedAt);
            GazedAtObject.SetActive(gazedAt);
        }
    }

    private IEnumerator SwitchObjectsAndBack()
    {
        // Switch to the other object
        SetObject(true);
        yield return new WaitForSeconds(5.0f);

        // Switch back to the original object
        SetObject(false);
    }
}
