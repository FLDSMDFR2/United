using System;

[Serializable]
public class BoxAssociationDtl
{
    public Boxs Box;
    public bool Default = true; // default for this box
    public bool Owned;
    public bool IncludeInGameBuild = true;
}
