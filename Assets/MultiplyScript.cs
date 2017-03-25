using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplyScript : MonoBehaviour {
  public Text result;
  public Text multiplier1;
  public Text multiplier2;
  public void OnPress(){
    result.text = (int.Parse(multiplier1.text)*int.Parse(multiplier2.text)).ToString();
  }
}
