using Content.Shared.Guidebook;
using Content.Shared._NF.Shipyard.Prototypes;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;
using Content.Client.Guidebook;
using Robust.Shared.Prototypes;

namespace Content.Client._NF.Shipyard.UI;

[GenerateTypedNameReferences]
public sealed partial class VesselRow : PanelContainer
{
    [Dependency] private readonly IEntitySystemManager _entitySystem = default!;
    private readonly GuidebookSystem _guidebook = default!;
    public VesselPrototype? Vessel;
    public VesselRow()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);
        _guidebook = _entitySystem.GetEntitySystem<GuidebookSystem>();

        Guidebook.OnPressed += LoadVesselGuidebook;
    }

    private void LoadVesselGuidebook(BaseButton.ButtonEventArgs args)
    {
        if (Vessel?.GuidebookPage == null)
            return;

        List<ProtoId<GuideEntryPrototype>> guidebookEntries = new() { Vessel.GuidebookPage.Value };
        _guidebook.OpenHelp(guidebookEntries);
    }
}
