namespace Maker.CreateModule;

internal sealed class ModuleCreator(ModuleCreationInfo moduleCreationInfo)
{
    private ModuleTree _moduleTree = new(moduleCreationInfo.ModuleName);

    internal void TryCreateModule()
    {
        try
        {
            _moduleTree.GetModuleTree().Create();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}