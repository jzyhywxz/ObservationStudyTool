
using System.ComponentModel;

namespace ObservationStudyTool.Helpers
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        //protected virtual void OnPropertyChanged<T>(Expression<Func<T>> selectorExpression)
        //{
        //    MemberExpression body = selectorExpression.Body as MemberExpression;
        //    if (body == null) throw new ArgumentException("The body must be a member expression");
        //    OnPropertyChanged(body.Member.Name);
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
