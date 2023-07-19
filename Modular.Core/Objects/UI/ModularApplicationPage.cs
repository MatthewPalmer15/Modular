using Modular.Core.Interfaces;

namespace Modular.Core.UI
{
    public class ApplicationPage : IModularApplicationPage
    {

        #region "  Properties  "

        public string Title { get; set; } = string.Empty;

        #endregion

        #region "  Methods  "

        public void OnLoad()
        {
            throw new NotImplementedException();
        }

        public bool OnSave()
        {
            throw new NotImplementedException();
        }

        public bool OnCancel()
        {
            throw new NotImplementedException();
        }

        public void HandlePanels()
        {
            throw new NotImplementedException();
        }

        public void RegisterEvents()
        {
            throw new NotImplementedException();
        }

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedAway()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo()
        {
            throw new NotImplementedException();
        }

        public void GetValues()
        {
            throw new NotImplementedException();
        }

        public void SetValues()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
