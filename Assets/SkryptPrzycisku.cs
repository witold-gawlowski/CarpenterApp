using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkryptPrzycisku : MonoBehaviour {
  public CanvasManager menadzerOkien;
  void Awake()
  {
    menadzerOkien = GetComponentInParent<CanvasManager>();
  }
  public void ChangeScreen()
  {
    menadzerOkien.UstawSekcjeNa(GetComponentInChildren<Text>().text);
  }
}
