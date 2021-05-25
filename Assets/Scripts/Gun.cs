using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float shotDistance = 15f;
    [SerializeField] KeyCode fireKey = KeyCode.J;
    [SerializeField] LayerMask shootingMask = 0;
    [SerializeField] float shootingRate = 0.5f;
    [SerializeField] Transform gunTip = null;
    [SerializeField] float bulletVisualizerTime = 0.1f;
    [SerializeField] float bulletRandomRecoil = 0.1f;
    [SerializeField] GameObject muzzleFlash = null;
    float shootingCooldown = 0f;
    Vector3 shootEndPoint = new Vector3();
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(fireKey) && Time.time >= shootingCooldown)
        {
            StartCoroutine(Fire());
            shootingCooldown = Time.time + (1f / shootingRate);
        }
    }

    IEnumerator Fire()
    {
        CameraMovement.INSTANCE.ShakeCam(transform.right * -1, 0.3f, 0.05f);

        Vector3 temp = gunTip.position;
        temp.Set(gunTip.localPosition.x, Random.Range(-bulletRandomRecoil, bulletRandomRecoil), gunTip.localPosition.y);
        gunTip.localPosition = temp;

        RaycastHit2D hit = Physics2D.Raycast(gunTip.position, transform.right, shotDistance, shootingMask);

        if(hit.collider == null)
        {
            shootEndPoint.Set(gunTip.position.x + (shotDistance * transform.right.x), gunTip.position.y, gunTip.position.z);
        }
        else
        {
            shootEndPoint = hit.point;
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            enemy?.Attacked(1);
        }

        lineRenderer.enabled = true;
        muzzleFlash.SetActive(true);

        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, shootEndPoint);

        yield return new WaitForSecondsRealtime(bulletVisualizerTime);

        lineRenderer.enabled = false;
        muzzleFlash.SetActive(false);
    }
}
