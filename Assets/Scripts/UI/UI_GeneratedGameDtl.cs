using UnityEngine;
using UnityEngine.UI;

public class UI_GeneratedGameDtl : MonoBehaviour, IDialog
{
    public ScrollRect GameScrollRect;
    public GameObject BoxHeader;
    public GameObject GroupView;
    public GameObject ChracterUIPrefab;
    public GameObject SearchUIPrefab;

    public virtual void SetData(BuildGameData data)
    {
        GameScrollRect.verticalNormalizedPosition = 1f;

        ClearGameObjectChildren(GroupView);

        if (data.Games.Count <= 0) return;

        var buildGameHeaders = data.Games.Count != 1;
        var gameCount = 0;

        if (data.Mode != null)
        {
            Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(Color.magenta, Color.white, "MODE");
            Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(data.Mode);
        }

        foreach (var game in data.Games)
        {
            if (buildGameHeaders) Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(Color.cyan, Color.white, "GAME " + ++gameCount);

            Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(Color.red, Color.white, "VILLAIN");
            foreach (var v in game.Villains)
            {
                Instantiate(ChracterUIPrefab, GroupView.transform).GetComponent<UI_Character>().SetData(v);
            }

            if (game.Challenge != null)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(Color.yellow, Color.white, "CHALLENGE");
                if (game.Challenge != null) Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(game.Challenge);
            }

            Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(Color.grey, Color.white, "LOCATIONS");
            foreach (var l in game.Locations)
            {
                Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(l);
            }

            Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(Color.blue, Color.white, "HEROS");
            foreach (var h in game.Heros)
            {
                Instantiate(ChracterUIPrefab, GroupView.transform).GetComponent<UI_Character>().SetData(h);
            }

            if (game.AdditionalHeros.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(Color.blue, Color.white, "ADDITIONAL HEROS");
                foreach (var h in game.AdditionalHeros)
                {
                    Instantiate(ChracterUIPrefab, GroupView.transform).GetComponent<UI_Character>().SetData(h);
                }
            }
        }
    }

    protected virtual void ClearGameObjectChildren(GameObject gameObject)
    {
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    #region IDialog
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
