using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshPro _levelText;
    private Material _objectMaterial;

    void Start()
    {
        _objectMaterial = new Material(gameObject.GetComponent<Renderer>().sharedMaterial);
        SetHealth(1);
    }

    void FixedUpdate()
    {
        AlignCamera();
    }

    public void SetLevel(int level)
    {
        if(_levelText != null)
            _levelText.text = level.ToString();
    }

    public void SetHealth(float healthRate)
    {
        _objectMaterial.SetFloat("_HealthValue", healthRate);
        UpdateMaterial();
    }

    public void ChangeColor(Color color)
    {
        _objectMaterial.SetColor("_HealthColor", color);
        UpdateMaterial();
    }

    private void AlignCamera()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.down);
    }
    
    private void UpdateMaterial()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial = _objectMaterial;
    }
}