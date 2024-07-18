using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject characterChoicePanel;
    [SerializeField] private GameObject itemChoicePanel;
    [SerializeField] private GameObject weaponChoicePanel;
    [SerializeField] private GameObject weaponInfoPanel;
    [SerializeField] private GameObject commonMagicCircleChoicePanel;
    [SerializeField] private GameObject commonMagicInfoPanel;
    [SerializeField] private GameObject uniqueMagicCircleChoicePanel;
    [SerializeField] private GameObject uniqueMagicInfoPanel;
    
    private Stack<GameObject> panelStack = new Stack<GameObject>();

    void Start()
    {
        ShowStart();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowStart()
    {
        ShowPanel(StartPanel);
    }
    
    public void ShowMainMenu()
    {
        ShowPanel(mainMenuPanel);
    }

    public void ShowCharacterChoice()
    {
        ShowPanel(characterChoicePanel);
    }

    public void ShowItemChoice()
    {
        ShowPanel(itemChoicePanel);
    }

    public void ShowWeaponChoice()
    {
        ShowPanel(weaponChoicePanel);
    }
    
    public void ShowWeaponInfo()
    {
        ShowPanel(weaponInfoPanel);
    }
    
    public void ShowCommonMagicCircle()
    {
        ShowPanel(commonMagicCircleChoicePanel);
    }
    
    public void ShowCommonMagicInfo()
    {
        ShowPanel(commonMagicInfoPanel);
    }

    public void ShowUniqueMagicCircle()
    {
        ShowPanel(uniqueMagicCircleChoicePanel);
    }
    
    public void ShowUniqueMagicInfo()
    {
        ShowPanel(uniqueMagicInfoPanel);
    }


   

    private void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
        panelStack.Push(panel);
    }

    private void HideAllPanels()
    {
        StartPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        characterChoicePanel.SetActive(false);
        itemChoicePanel.SetActive(false);
        weaponChoicePanel.SetActive(false);
        weaponInfoPanel.SetActive(false);
        commonMagicCircleChoicePanel.SetActive(false);
        commonMagicInfoPanel.SetActive(false);
        uniqueMagicCircleChoicePanel.SetActive(false);
        uniqueMagicInfoPanel.SetActive(false);
        
        
    }

    public void GoBack()
    {
        if (panelStack.Count > 1)
        {
            // 현재 패널을 스택에서 제거하고 숨김
            panelStack.Pop().SetActive(false);

            // 이전 패널을 스택에서 가져와서 활성화
            GameObject previousPanel = panelStack.Peek();
            previousPanel.SetActive(true);
        }
    }
}
