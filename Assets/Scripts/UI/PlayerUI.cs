using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerStatSlider _health;
    [SerializeField] private PlayerStatSlider _exp;

    [SerializeField] private Image _combatImage;
    [SerializeField] private TextMeshProUGUI _levelText;

    [SerializeField] private DeathScreen _deathScreen;
    [SerializeField] private CanvasGroup _UICanvas;
    [SerializeField] private MenuUI _menu;

    private Player _player;

    public void SetPlayer(Player player)
    {
         _player = player;
         _health.SetSlider(player.HealthSystem.CurrentHealth, player.UnitInfo.maxHealth);
    }

    public void UpdatePlayerHealth()
    {
        _health.SetSlider(_player.HealthSystem.CurrentHealth, _player.UnitInfo.maxHealth);
    }

    public void UpdatePlayerExperience()
    {
        _exp.SetSlider(_player.LevelSystem.currentExp, _player.LevelSystem.expToNextLevel);
        _levelText.text = _player.LevelSystem.currentLevel.ToString();
    }

    public void HideInterface(bool hide)
    {
        _UICanvas.alpha = hide ? 0 : 1;
    }

    public void SetCombat(bool combat)
    {
        _combatImage.gameObject.SetActive(combat);
    }

    public void ShowDeathScreen()
    {
        if(_deathScreen == null) return;
        HideInterface(true);
//        Debug.Log("Death screen");
        _deathScreen.gameObject.SetActive(true);
    }

    public void ShowMainMenu()
    {
        _menu.ShowMenu();
    }

    public void HideMainMenu()
    {
        _menu.HideMenu();
    }

    public void SwitchMainMenu(bool show)
    {
        if(_menu == null) return;
        _player.inMenu = show;
        if(!show)
            HideMainMenu();
        else
            ShowMainMenu();
    }
}
