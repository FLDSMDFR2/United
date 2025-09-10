using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Settings : MonoBehaviour, IDialog
{
    public TextMeshProUGUI Version;

    protected float openTime = 0.1f;
    protected float closeTime = 0.05f;

    public virtual void SetData()
    {
        Version.text = "V" + Application.version;
    }

    #region IDialog
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
        LeanTween.scale(this.gameObject, new Vector3(1f, 1f, 1f), openTime);
    }

    public virtual void Close()
    {
        LeanTween.scale(this.gameObject, new Vector3(0f, 0f, 0f), closeTime).setOnComplete(CloseComplete);
    }
    public virtual void CloseComplete()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
