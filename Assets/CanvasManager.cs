using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CanvasManager : MonoBehaviour {
  //layout
  public List<Button> przyciski;
  public List<GameObject> panele;
  public Text tytulSekcji;
  private GameObject obecnyPanel;

  /* Input fields,
   * calling recalculate in this MB.
   * This triggers recalcualtion of all the output fields referenced in this script.
   * Recalculation takes place here.  
   */

  //wooden elements panel
  public InputField[] counts;
  public InputField[] widths;
  public InputField[] heights;
  public InputField[] lengths;
  public GameObject elementsPanel;
  public GameObject extrasPanel;

  //extras panel
  public InputField[] extrasCounts;
  public InputField[] extrasPrices;

  //result panel
  public InputField extraDrewnoProcent;
  public InputField pricePerM3;
  public InputField procentRobociznyI;
  public InputField procentRobociznyII;  

  //output fields
  public Text objetoscDrewna;
  public Text ekstraDrewno;
  public Text razemDrewno;
  public Text cenaDrewnaNetto;
  public Text cenaDrewnaBrutto;
  public Text dodatkiBrutto;
  public Text robociznaI;
  public Text cenaI;
  public Text robociznaII;
  public Text cenaII;

  private void UstawObecynPanel(GameObject panel)
  {
    if (obecnyPanel)
    {
      obecnyPanel.SetActive(false);
    }
    obecnyPanel = panel;
    panel.SetActive(true);
    tytulSekcji.text = panel.name;
  }

  public void UstawSekcjeNa(string s)
  {
    foreach(GameObject panel in panele)
    {
      if (panel.name.CompareTo(s) == 0)
      {
        UstawObecynPanel(panel);
        return;
      }
    }
    print("nie można znaleźć przycisku o tej nazwie");
  }

  void Awake()
  {
    foreach(GameObject panel in panele)
    {
      panel.SetActive(false);
    }
    counts = elementsPanel.GetComponentsInChildren<InputField>(true).Where(s => s.name == "count").ToArray();
    widths = elementsPanel.GetComponentsInChildren<InputField>(true).Where(s => s.name == "width").ToArray();
    heights = elementsPanel.GetComponentsInChildren<InputField>(true).Where(s => s.name == "height").ToArray();
    lengths = elementsPanel.GetComponentsInChildren<InputField>(true).Where(s => s.name == "length").ToArray();

    extrasPrices = extrasPanel.GetComponentsInChildren<InputField>(true).Where(s => s.name == "price").ToArray();
    extrasCounts = extrasPanel.GetComponentsInChildren<InputField>(true).Where(s => s.name == "count").ToArray();
  }

  public void RecalculatePrices()
  {
    //precalculate work prices
    float cenaRobociznyI = (100 - System.Convert.ToSingle(procentRobociznyI.text)) / System.Convert.ToSingle(procentRobociznyI.text)*
      System.Convert.ToSingle(cenaDrewnaBrutto.text);
    float cenaRobociznyII = (100 - System.Convert.ToSingle(procentRobociznyII.text)) / System.Convert.ToSingle(procentRobociznyII.text) *
      System.Convert.ToSingle(cenaDrewnaBrutto.text)*3000 / System.Convert.ToSingle(pricePerM3.text);
    print(System.Convert.ToSingle(cenaDrewnaBrutto.text) * 3000 + "roboII");

    // calculate and fill in work prices and final prices
    robociznaI.text = cenaRobociznyI.ToString();
    robociznaII.text = cenaRobociznyII.ToString();
    cenaI.text = (cenaRobociznyI + System.Convert.ToSingle(dodatkiBrutto.text) + System.Convert.ToSingle(cenaDrewnaBrutto.text)).ToString();
    cenaII.text = (cenaRobociznyII + System.Convert.ToSingle(dodatkiBrutto.text) + System.Convert.ToSingle(cenaDrewnaBrutto.text)).ToString();
  }

  public void RecalculateExtras()
  {
    //calculate dodatkiBrutto
    float result = 0;
    for( int i = 0; i < extrasCounts.Length; i++ )
    {
      int count = 0;
      float cost = 0;
      if (extrasCounts[i].text.CompareTo("")!=0)
        count = System.Convert.ToInt32(extrasCounts[i].text);
      if (extrasPrices[i].text.CompareTo("") != 0)
        cost = System.Convert.ToInt32(extrasPrices[i].text);
      result += count * cost;
    }
    dodatkiBrutto.text = (result * 1.23f).ToString();

    //recalculate final prices
    RecalculatePrices();
  }

  public void RecalculateWoodVolume()
  {
    //recalculate wood volume
    print("recalculate wood");
    float result = 0;
    if(counts.Length != heights.Length || heights.Length != lengths.Length || lengths.Length != widths.Length)
    {
      print("nierówna ilość danych");
      return;
    }
    for (int i = 0; i < counts.Length; i++)
    {
      int count = 0;
      float width = 0, height = 0, length = 0;
      if (counts[i].text != "")
        count = System.Convert.ToInt32(counts[i].text);
      if (widths[i].text != "")
        width = System.Convert.ToSingle(widths[i].text);
      if (heights[i].text != "")
        height = System.Convert.ToSingle(heights[i].text);
      if (lengths[i].text != "")
        length = System.Convert.ToSingle(lengths[i].text);
      result += count * width * height * length;
    }
    result /= 1000000;
    print("changing wood volume");
    objetoscDrewna.text = result.ToString();

    if (extraDrewnoProcent.text != "")
      ekstraDrewno.text = (result * System.Convert.ToSingle(extraDrewnoProcent.text)/100).ToString();

    razemDrewno.text = (System.Convert.ToSingle(ekstraDrewno.text) + System.Convert.ToSingle(objetoscDrewna.text)).ToString();

    if (pricePerM3.text != "")
    {
      cenaDrewnaNetto.text = (System.Convert.ToSingle(pricePerM3.text) * System.Convert.ToSingle(razemDrewno.text)).ToString();
      cenaDrewnaBrutto.text = (System.Convert.ToSingle(cenaDrewnaNetto.text)*1.23f).ToString();
    }

    //recalculate final prices
    RecalculatePrices();
  }

  void Start()
  {
    int i = 0;
    foreach(GameObject go in panele)
    {
      Text tekstPrzycisku = przyciski[i].GetComponentInChildren<Text>();
      tekstPrzycisku.text = go.name;
      i++;
    }
    przyciski[0].onClick.Invoke();
  }

  public void Koniec() {
    Application.Quit();
  }
}
