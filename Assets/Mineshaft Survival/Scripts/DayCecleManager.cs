using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCecleManager : MonoBehaviour
{
    [Range(0,1)]
    public float TimeofDay;
    public float DayDuration = 30f;

    public AnimationCurve DirectionalLightCurve;

    public Light DirectionalLight;

    private float directionalLightIntensity;
    // Start is called before the first frame update
    private void Start()
    {
        directionalLightIntensity = DirectionalLight.intensity;
    }

    // Update is called once per frame
    private void Update()
    {
        TimeofDay += Time.deltaTime / DayDuration;
        if (TimeofDay >= 1) TimeofDay -= 1;

        DirectionalLight.transform.localRotation = Quaternion.Euler(TimeofDay * 360f, 180, 0);
        DirectionalLight.intensity = directionalLightIntensity * DirectionalLightCurve.Evaluate(TimeofDay);
    }
}
