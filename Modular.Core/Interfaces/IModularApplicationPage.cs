namespace Modular.Core.Interfaces
{
    public interface IModularApplicationPage
    {

        public string Title { get; set; }

        public void OnLoad();

        public bool OnSave();

        public bool OnCancel();

        public void HandlePanels();

        public void RegisterEvents();

        public void OnDestroy();

        public void OnNavigatedAway();

        public void OnNavigatedTo();

        public void GetValues();

        public void SetValues();
    }
}
