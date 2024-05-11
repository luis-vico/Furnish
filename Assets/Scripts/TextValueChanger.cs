using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextValueChanger : MonoBehaviour
{

    public GameObject SliderX;
    public GameObject SliderY;
    public GameObject SliderZ;

    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;

    public TextMeshProUGUI SliderX_text;
    public TextMeshProUGUI SliderY_text;
    public TextMeshProUGUI SliderZ_text;

    public BoxCollider sizeBoxCollider;

    public void changeXText(){
        SliderX_text.text = (SliderX.GetComponent<Slider>().value + OffsetX).ToString("F2") + "m";
    }
    public void changeYText(){
        SliderY_text.text = (SliderY.GetComponent<Slider>().value + OffsetY).ToString("F2") + "m";
    }
    public void changeZText(){
        SliderZ_text.text = (SliderZ.GetComponent<Slider>().value + OffsetZ).ToString("F2") + "m";
    }

    public void changeColliderSize(){
        sizeBoxCollider.size = new Vector3(
            SliderX.GetComponent<Slider>().value + OffsetX, 
            SliderY.GetComponent<Slider>().value + OffsetY, 
            SliderZ.GetComponent<Slider>().value + OffsetZ);
        sizeBoxCollider.center = new Vector3(0, (SliderY.GetComponent<Slider>().value + OffsetY) / 2, 0);
    }
           

}
