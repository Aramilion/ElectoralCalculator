using System.ComponentModel;

namespace Model
{
    public class SelectableCandidate : INotifyPropertyChanged
    {
        public DataLayer.Candidate candidate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isSelected;   

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
