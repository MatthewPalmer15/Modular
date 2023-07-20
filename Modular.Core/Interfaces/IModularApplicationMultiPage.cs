namespace Modular.Core.Interfaces
{
    public interface IModularApplicationMultiPage : IModularApplicationPage
    {
        public new string Title { get; set; }

        public new void OnLoad();

        public bool CanShow();

        public bool OnNext();

        public bool OnPrev();

        public new bool OnSave();

        public new bool OnCancel();

        public new void HandlePanels();

        public new void RegisterEvents();

        public new void OnDestroy();

        public new void OnNavigatedAway();

        public new void OnNavigatedTo();

        public new void GetValues();

        public new void SetValues();
    }
}
