using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_GeneratedGameDtl : MonoBehaviour, IDialog
{
    public ScrollRect GameScrollRect;
    public GameObject BoxHeader;
    public GameObject GroupView;
    public GameObject ChracterUIPrefab;
    public GameObject SearchUIPrefab;

    public Color ModeHeaderColor;
    public Color GameHeaderColor;
    public Color VillainHeaderColor;
    public Color ChallengeHeaderColor;
    public Color LocationsHeaderColor;
    public Color TeamHeaderColor;
    public Color HerosHeaderColor;
    public Color AddHerosHeaderColor;
    public Color CompanionHeaderColor;
    public Color EquipmentHeaderColor;
    public Color CampaignsHeaderColor;

    protected float openTime = 0.1f;
    protected float closeTime = 0.05f;

    public virtual void SetData(BuildGameData data)
    {
        GameScrollRect.verticalNormalizedPosition = 1f;

        ClearGameObjectChildren(GroupView);

        if (data.Games.Count <= 0) return;

        var buildGameHeaders = data.Games.Count != 1;
        var gameCount = 0;

        if (data.Mode != null && data.Mode.Count > 0)
        {
            Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(ModeHeaderColor, Color.white, "MODE");
            foreach (var m in data.Mode)
            {
                Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(m);
            }
        }

        foreach (var game in data.Games)
        {
            if (buildGameHeaders) Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(GameHeaderColor, Color.white, "GAME " + ++gameCount);

            if (game.Campaigns != null && game.Campaigns.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(CampaignsHeaderColor, Color.white, "CAMPAIGNS");
                foreach (var c in game.Campaigns)
                {
                    Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(c);
                }
            }

            if (game.Villains != null && game.Villains.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(VillainHeaderColor, Color.white, "VILLAIN");
                foreach (var v in game.Villains)
                {
                    Instantiate(ChracterUIPrefab, GroupView.transform).GetComponent<UI_SearchDtlCharacter>().SetData(v);
                }
            }

            if (game.Challenges != null && game.Challenges.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(ChallengeHeaderColor, Color.white, "CHALLENGE");
                foreach (var c in game.Challenges)
                {
                    Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(c);
                }
            }

            if (game.Locations != null && game.Locations.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(LocationsHeaderColor, Color.white, "LOCATIONS");
                foreach (var l in game.Locations)
                {
                    Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(l);
                }
            }

            if (game.Teams != null && game.Teams.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(TeamHeaderColor, Color.white, "TEAM");
                foreach (var t in game.Teams)
                {
                    Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(t);
                }
            }

            if (game.Heros != null && game.Heros.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(HerosHeaderColor, Color.white, "HEROS");
                foreach (var h in game.Heros)
                {
                    Instantiate(ChracterUIPrefab, GroupView.transform).GetComponent<UI_SearchDtlCharacter>().SetData(h);
                }
            }

            if (game.Equipment != null && game.Equipment.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(EquipmentHeaderColor, Color.white, "EQUIPMENT");
                foreach (var e in game.Equipment)
                {
                    Instantiate(SearchUIPrefab, GroupView.transform).GetComponent<UI_SearchDtl>().SetData(e);
                }
            }

            if (game.AdditionalHeros != null && game.AdditionalHeros.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(AddHerosHeaderColor, Color.white, "ADDITIONAL HEROS");
                foreach (var h in game.AdditionalHeros)
                {
                    Instantiate(ChracterUIPrefab, GroupView.transform).GetComponent<UI_SearchDtlCharacter>().SetData(h);
                }
            }

            if (game.Companions != null && game.Companions.Count > 0)
            {
                Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>().SetData(CompanionHeaderColor, Color.white, "COMPANIONS");
                foreach (var c in game.Companions)
                {
                    Instantiate(ChracterUIPrefab, GroupView.transform).GetComponent<UI_SearchDtlCharacter>().SetData(c);
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
        LeanTween.scale(this.gameObject, new Vector3(1f, 1f, 1f), openTime).setOnComplete(OpenComplete);
    }

    public virtual void OpenComplete()
    {
        GameScrollRect.verticalNormalizedPosition = 1f;
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
