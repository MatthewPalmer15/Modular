namespace Modular.Core
{
    public interface IModularTabPage
    {

        bool IsDirty { get; }
        bool IsValid { get; }
        bool IsSaveable { get; }

        // Methods that can be called to a form that is an MDI tab page to save the underlying business object
        void Save();
        void Close();
        void Close(bool forceSave);
        void Cancel();


    }
}
