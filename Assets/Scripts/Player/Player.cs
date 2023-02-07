using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Unit
{
    public PlayerUI PlayerUI { get => _playerUI;}

    [Header("PLAYER")]
    [Header("Components")]
    [SerializeField] private PlayerCamera _camera;
    [SerializeField] private PlayerUI _playerUI;

    [Header("Pointer")]
    [SerializeField] private Pointer _pointerPrefab;
    [SerializeField] private float _pointerLifetime;

    [Header("Raycast")]
    [SerializeField] private LayerMask _groundLayer;    

    public bool inMenu = false;

    private Coroutine _healProcess;
    private Pointer _pointer;

// MONOBEHAVIOUR =============

    void Start() 
    {
        InitUnit();
    }

    void Update() 
    {
        UpdateBehaviour();
    }

// CAMERA =================

    public void MoveCamera(Vector2 mouseScreenPosition) {
        if(inMenu) return;
        _camera.MoveCamera(mouseScreenPosition);
    }

    public void PermanentAttachCameraSwitch() {
        _camera.permanentAttach = !_camera.permanentAttach;
    }

    public void AttachCamera(bool attach) {
        _camera.tempAttach = attach;
    }

//======================================

    public void PlayerClicked(Vector2 mousePosition) 
    {
        if(!IsPlayerClickedInGame(mousePosition)) return;
        if(behaviourCurrent is UnitBehaviourDeath) return;

        ResetPointer();

        var unit = GetClickedUnit(mousePosition);
        if(unit != null)
        {
            SetBehaviourAttack(unit);
        }
        else 
        {
            MovePlayer(GetClickedPosition(mousePosition));
        }
    }

    public void UpdateExperience()
    {
        _playerUI.UpdatePlayerExperience();
    }

    public void SetCombat(bool combat)
    {
        if(!combat)
            _healProcess = StartCoroutine(StartHeal());
        else
            if(_healProcess != null)
            {
                StopCoroutine(_healProcess);
            }
        
        _playerUI.SetCombat(combat);
    }

//=======================================
    public override void SetBehaviourDeath()
    {
        base.SetBehaviourDeath();
        _playerUI.HideInterface(true);
        _playerUI.ShowDeathScreen();
    }

//======================================

    protected override void InitUnit()
    {
        base.InitUnit();
        HealthSystem.damageTrigger = GetDamage; 
        LevelSystem.canReceiveExp = true;

        UnitInfo.threat = EThreat.PLAYER;

        _playerUI.SetPlayer(this);
        _playerUI.UpdatePlayerExperience();
        SetCombat(false);

        InitPointer();      
    }

//=========================================

    private void GetDamage(Unit sender)
    {
        _playerUI.UpdatePlayerHealth();
    }


    private bool IsPlayerClickedInGame(Vector2 mousePosition)
    {
        var rect = new Rect(0, 0, Screen.width, Screen.height);
        if(rect.Contains(mousePosition)) return true;
        return false;
    }

    private void MovePlayer(Vector3? point)
    {   
        if(point == null) return;
        SetupPointer((Vector3)point);
        SetBehaviourWalking((Vector3)point);
    }

    private void InitPointer()
    {
        _pointer = Instantiate<Pointer>(_pointerPrefab);
        ResetPointer();
    }

    private void ResetPointer()
    {
        if(_pointer != null)
            _pointer.gameObject.SetActive(false);
    }

    private void SetupPointer(Vector3 position)
    {
        if(_pointer == null) return;
        _pointer.gameObject.SetActive(true);
        _pointer.CreatePoint(position, _pointerLifetime);
    }

    private Vector3? GetClickedPosition(Vector2 mousePosition) 
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, _groundLayer)) 
            return hitInfo.point;

        return null;
    }

    private Unit GetClickedUnit(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) 
        {
            if(hitInfo.transform.tag == "Unit")
            {
                var unit = hitInfo.transform.GetComponent<Unit>();
                if(unit.UnitInfo.threat == EThreat.ENEMY || unit.UnitInfo.threat == EThreat.NEUTRAL)
                    if(unit.behaviourCurrent is not UnitBehaviourDeath)
                        return unit;
            }
        }
        return null;
    }    

    /// WIP
    private IEnumerator StartHeal()
    {
        while(HealthSystem.CurrentHealth < UnitInfo.maxHealth)
        {
            yield return new WaitForSeconds(1.5f);
            HealthSystem.AddHealth(5);
            _playerUI.UpdatePlayerHealth();
        }
    }
}
