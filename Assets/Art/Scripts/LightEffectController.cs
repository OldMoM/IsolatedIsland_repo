using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffectController : MonoBehaviour
{
    public Light SunSource;
    public float DirectLightIntensity;
    public Vector3 DirectLightRotation;
    public float AmbientIntensity;
    public GameObject Prop_Tent;
    public Light CharacterLight;
    public Light RobotLight;
    public ParticleSystem SmokeParticle;

    // public Motion Anim;

    // private float ControlValue;
    // private float time;
    // Start is called before the first frame update
    void Start()
    {
        // ControlValue = 0;
        // time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // time += Time.deltaTime*2;
        // if(time>24f)time -= 24f;
        // ValueChange(time);
        // 平行光
        SunSource.intensity = DirectLightIntensity;
        Quaternion rotation = Quaternion.Euler(DirectLightRotation);
        SunSource.gameObject.transform.localRotation = rotation;
        // 环境光
        RenderSettings.ambientIntensity = AmbientIntensity;
        // 帐篷
        float inten = (AmbientIntensity + DirectLightIntensity)/2;
        //print(inten);
        Color tentCol = new Color(inten, inten, inten);
        Prop_Tent.GetComponent<MeshRenderer>().material.color = tentCol;
        //点光源
        if(DirectLightIntensity > 0.5)
        {
            CharacterLight.intensity = 0;
            RobotLight.intensity = 0;
        }
        else
        {
            CharacterLight.intensity = 1;
            RobotLight.intensity = 1;
        }
        // 烟雾
        float currentTime = gameObject.GetComponent<Animator>().
                            GetCurrentAnimatorStateInfo(0).normalizedTime;
        currentTime = currentTime - Mathf.Floor(currentTime);
        if(currentTime>0.5f) SmokeParticle.Stop();
        if(currentTime<0.1f) SmokeParticle.Play();
    }
    public void ValueChange(float CV)
    {
        // print(CV);
        //平行光
        // if(CV>6 && CV<=10)
        // {
        //     SunSource.intensity = SmoothStep(6, 10, CV);
        // }
        // else if(CV>10 && CV<14)
        // {
        //     float temp = SmoothStep(0, 2, Mathf.Abs(CV-12f));
        //     // print("temp="+temp);
        //     SunSource.intensity = 0.2f * temp + 1f;
        // }
        // else if(CV>=14 && CV<18)
        // {
        //     SunSource.intensity = SmoothStep(18f, 14f, CV);
        // }
        // else
        // {
        //     SunSource.intensity = 0;
        // }
    } 
    private float SmoothStep(float a, float b, float x)
    {
        float t = (x-a)/(b-a);
        float y = 3*Mathf.Pow(t,2) - 2*Mathf.Pow(t,3);
        return y;
    }
}
