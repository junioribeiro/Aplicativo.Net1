using Aplicativo.Net.Shared.Repositories;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicativo.Net.Shared.Commands
{
    public abstract class AbstractCommand<T> : Notifiable
    {
        public abstract T Execute();

        protected void AddNotifications(INotifiable notifiable) => base.AddNotifications(notifiable.Notifications);
    }

    public abstract class AbstractAsyncCommand<T> : Notifiable
    {
        public abstract Task<T> Execute();
        protected void AddNotifications(INotifiable notifiable) => base.AddNotifications(notifiable.Notifications);
    }
}
