using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] float x = 0;
    [SerializeField] float y = 2;
    [SerializeField] float z = 2;
     float horizontal = 0;
    [SerializeField] GameObject target = null;

    public Vector3 Offset => target.transform.position + new Vector3(x, y, z);
    public bool IsValid => target;

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }
    private void Update()
    {
        if (!IsValid) return;
        transform.position = Vector3.Lerp(transform.position, Offset, Time.deltaTime * 100);
        Quaternion _angle = Quaternion.LookRotation( target.transform.position-transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, _angle, Time.deltaTime * 100);
    }
}
