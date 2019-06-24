using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
  public TMP_Text scoreDisplayRon;
  public TMP_Text scoreDisplayTsumo;
  public RectTransform fuButtonsContainer;
  public RectTransform hanButtonsContainer;
  public Color dealerActiveColor;
  public Button dealerButton;
  public Button nonDealerButton;
  public GameObject fuButtonPrefab;
  public GameObject hanButtonPrefab;
  List<FuButtonController> fuButtons = new List<FuButtonController>();
  List<HanButtonController> hanButtons = new List<HanButtonController>();

  int currentFuIndex = -1;
  int currentHanIndex = -1;
  bool isDealer = false;

  List<string> hans = new List<string>(){
      "1 Han",
      "2 Han",
      "3 Han",
      "4 Han",
      "5 Han",
      "6-7 Han",
      "8-10 Han",
      "11-12 Han",
      "13+ or Yakuman"
    };
  List<int> fus = new List<int>(){
      20,
      25,
      30,
      40,
      50,
      60,
      70,
      80,
      90,
      100,
      110
    };

  void Start()
  {
    foreach (var fu in fus)
    {
      var fuButtonController = Instantiate(fuButtonPrefab, fuButtonsContainer).GetComponent<FuButtonController>();
      fuButtonController.Init(fu, OnFuChanged);
      fuButtons.Add(fuButtonController);
    }
    foreach (var han in hans)
    {
      var hanButtonController = Instantiate(hanButtonPrefab, hanButtonsContainer).GetComponent<HanButtonController>();
      hanButtonController.Init(han, OnHanChanged);
      hanButtons.Add(hanButtonController);
    }

    UpdateDealerButtonState();
  }

  void OnFuChanged(int fu)
  {
    currentFuIndex = fus.IndexOf(fu);
    for (int i = 0; i < fuButtons.Count; i++) fuButtons[i].SetActive(i == currentFuIndex);
    if (currentHanIndex > -1) CalculateScore();
  }

  void OnHanChanged(string han)
  {
    currentHanIndex = hans.IndexOf(han);
    for (int i = 0; i < hanButtons.Count; i++) hanButtons[i].SetActive(i == currentHanIndex);
    if (currentHanIndex > 3 || currentFuIndex > -1) CalculateScore();
  }

  void UpdateDealerButtonState()
  {
    nonDealerButton.image.color = isDealer ? Color.white : dealerActiveColor;
    dealerButton.image.color = isDealer ? dealerActiveColor : Color.white;
  }

  public void SetIsDealer(bool isDealer)
  {
    this.isDealer = isDealer;
    UpdateDealerButtonState();
    if (currentHanIndex > -1 && (currentHanIndex > 3 || currentFuIndex > -1)) CalculateScore();
  }

  void CalculateScore()
  {
    var han = 0;
    if (currentHanIndex == 8) han = 13;
    else if (currentHanIndex == 7) han = 11;
    else if (currentHanIndex == 6) han = 8;
    else if (currentHanIndex == 5) han = 6;
    else if (currentHanIndex == 4) han = 5;
    else if (currentHanIndex == 3) han = 4;
    else if (currentHanIndex == 2) han = 3;
    else if (currentHanIndex == 1) han = 2;
    else if (currentHanIndex == 0) han = 1;

    if (han >= 13)
      ShowScoreWithBasicPoint(8000);
    else if (han >= 11)
      ShowScoreWithBasicPoint(6000);
    else if (han >= 8)
      ShowScoreWithBasicPoint(4000);
    else if (han >= 6)
      ShowScoreWithBasicPoint(3000);
    else if (han == 5)
      ShowScoreWithBasicPoint(2000);
    else
    {
      var fu = fus[currentFuIndex];
      var basicPoint = Mathf.Min(fu * Mathf.Pow(2, 2 + han), 2000);
      ShowScoreWithBasicPoint((int)basicPoint);
    }
  }

  void ShowScoreWithBasicPoint(int basicPoint)
  {
    scoreDisplayRon.text = CeilTo100(basicPoint * (isDealer ? 6 : 4)).ToString();
    scoreDisplayTsumo.text = isDealer ? $"{CeilTo100(basicPoint * 2)} all" : $"{CeilTo100(basicPoint)}/{CeilTo100(basicPoint * 2)}";
  }

  int CeilTo100(int value) => Mathf.CeilToInt((float)value / 100) * 100;
}
