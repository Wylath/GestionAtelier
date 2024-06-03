using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace GestionAtelier.ToolBox
{
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged()
        {
            RaisePropertyChanged(string.Empty);
        }

        protected void RaisePropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(PropertyName));
            }

        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            PropertyChangedEventHandler handler = PropertyChanged;

            if (memberExpression != null && handler != null)
            {
                string PropertyName = memberExpression.Member.Name;
                handler(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
