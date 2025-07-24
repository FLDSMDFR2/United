using System.Collections;
using TMPro;
using UnityEngine;

public class UI_LoadScreen : MonoBehaviour
{
    public TextMeshProUGUI Loading;
    public TextMeshProUGUI Version;

    protected Coroutine loading;

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
        Version.text = "V" + Application.version;
        loading = StartCoroutine(LoadingCoroutine());
    }

    protected virtual IEnumerator LoadingCoroutine()
    {
        var count = 0;
        while(true)
        {
            yield return new WaitForSeconds(0.3f);
            count++;
            if (count == 1)
            {
                Loading.text = "Loading .";
            }
            else if (count == 2)
            {
                Loading.text = "Loading ..";
            }
            else
            {
                Loading.text = "Loading ...";
                count = 0;
            }
        }
    }

    public virtual void Close()
    {
        if (loading != null) StopCoroutine(loading);
        this.gameObject.SetActive(false);
    }
}
