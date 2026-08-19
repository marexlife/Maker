namespace Maker.CreateModule;

internal sealed class ModuleCreator(ModuleCreationInfo moduleCreationInfo)
{
    internal void TryCreateModule()
    {
        try
        {
            DoCreateModule();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    private void DoCreateModule()
    {
        new ModuleTree(moduleCreationInfo.ModuleName).GetModuleTree().Create();
    }
}