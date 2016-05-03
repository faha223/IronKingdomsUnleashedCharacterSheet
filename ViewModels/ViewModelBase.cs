using System.ComponentModel;
using IronKingdomsUnleashedCharacterSheet.BaseClasses;

namespace IronKingdomsUnleashedCharacterSheet.ViewModels
{
    public abstract class ViewModelBase : PropertyChangedNotifier
    {
        protected ViewModelBase()
        {
            PropertyChanged += _internalOnPropertyChanged;
        }

        ~ViewModelBase()
        {
            PropertyChanged -= _internalOnPropertyChanged;
        }

        protected virtual void _internalOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {

        }
    }
}