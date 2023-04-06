using TMPro;
using UnityEngine;

public class CreateSatelliteButton : MonoBehaviour
{
    [SerializeField] private GameObject _field;

    [SerializeField] private Game _game;

    [SerializeField] private TMP_InputField xCoordinateInput;

    [SerializeField] private TMP_InputField yCoordinateInput;
    
    [SerializeField] private TMP_InputField zCoordinateInput;

    private int x;
    private int y;
    private int z;

    private float xFieldRange => _field.transform.localScale.x / 2;
    private float yFieldRange => _field.transform.localScale.z / 2;
    private float zFieldRange => _field.transform.localScale.z / 2;

    public void OnCreateSatelliteButtonClick()
    {
        if(int.TryParse(xCoordinateInput.text, out x)
           && int.TryParse(yCoordinateInput.text, out y)
           && int.TryParse(zCoordinateInput.text, out z))
        {
            if (Mathf.Abs(x) <= xFieldRange
                && Mathf.Abs(y) <= yFieldRange
                && Mathf.Abs(z) <= zFieldRange)
            {
                _game.CreateSatellite(x, y, z);
            }
        }

    }
}
