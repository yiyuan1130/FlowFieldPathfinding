using System;

public class CellTypeChangeCommand : RenderCommand
{
    public Guid guid;
    public CellType cellType;
    public override void Execute()
    {
        base.Execute();
        var cellRender = CellRenderManager.Instance.GetCellRender(guid);
        cellRender.OnCellTypeChange(cellType);
    }
}
