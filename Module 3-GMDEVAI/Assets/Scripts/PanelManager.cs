using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private Button[] locationButtons;

    [Header("Tanks")]
    [SerializeField] private GameObject greenTank;
    [SerializeField] private GameObject redTank;
    [SerializeField] private GameObject blueTank;

    [Header("Panels")]
    [SerializeField] private GameObject locationPanel;
    [SerializeField] private GameObject tankSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        RemoveButtonListeners();
        TankSelected("greenTank");
    }

    private void RemoveButtonListeners()
    {
        foreach (Button button in locationButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void ActivatePanel(GameObject panelToBeActivated)
    {
        locationPanel.SetActive(panelToBeActivated.Equals(locationPanel));
        tankSelectionPanel.SetActive(panelToBeActivated.Equals(tankSelectionPanel));
    }

    public void TankSelected(string tankSelected)
    {
        ActivatePanel(locationPanel);

        FollowPath followPath = null;

        RemoveButtonListeners();

        switch (tankSelected)
        {
            case "greenTank":
                followPath = greenTank.GetComponent<FollowPath>();
                break;

            case "redTank":
                followPath = redTank.GetComponent<FollowPath>();
                break;

            case "blueTank":
                followPath = blueTank.GetComponent<FollowPath>();
                break;

            default:
                Debug.LogError("No Tank Selected");
                return;
        }
        

        locationButtons[0].onClick.AddListener(() => followPath.GoToHelipad());
        locationButtons[1].onClick.AddListener(() => followPath.GoToRuins());
        locationButtons[2].onClick.AddListener(() => followPath.GoToFactory());
        locationButtons[3].onClick.AddListener(() => followPath.GoToTwinMountain());
        locationButtons[4].onClick.AddListener(() => followPath.GoToBarracks());
        locationButtons[5].onClick.AddListener(() => followPath.GoToCommandCenter());
        locationButtons[6].onClick.AddListener(() => followPath.GoToRefineryPump());
        locationButtons[7].onClick.AddListener(() => followPath.GoToTankers());
        locationButtons[8].onClick.AddListener(() => followPath.GoToRadar());
        locationButtons[9].onClick.AddListener(() => followPath.GoToCommandPost());
        locationButtons[10].onClick.AddListener(() => followPath.GoToMiddle());
    }

    public void OnBackButton() => ActivatePanel(tankSelectionPanel);

    public void OnSelectLocation() => ActivatePanel(locationPanel);
}
