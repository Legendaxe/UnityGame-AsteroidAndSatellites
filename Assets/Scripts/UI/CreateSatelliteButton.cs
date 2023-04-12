using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateSatelliteButton : MonoBehaviour
{
    [FormerlySerializedAs("_field")] [SerializeField] private GameObject field;

    [FormerlySerializedAs("_game")] [SerializeField] private Game game;

    [SerializeField] private TMP_InputField xCoordinateInput;

    [SerializeField] private TMP_InputField yCoordinateInput;
    
    [SerializeField] private TMP_InputField zCoordinateInput;

    private int _x;
    private int _y;
    private int _z;

    private float XFieldRange => field.transform.localScale.x / 2;
    private float YFieldRange => field.transform.localScale.z / 2;
    private float ZFieldRange => field.transform.localScale.z / 2;

    public void OnCreateSatelliteButtonClick()
    {
        if(int.TryParse(xCoordinateInput.text, out _x)
           && int.TryParse(yCoordinateInput.text, out _y)
           && int.TryParse(zCoordinateInput.text, out _z))
        {
            if (Mathf.Abs(_x) <= XFieldRange
                && Mathf.Abs(_y) <= YFieldRange
                && Mathf.Abs(_z) <= ZFieldRange)
            {
                game.CreateSatellite(_x, _y, _z);
            }
        }

    }
}
