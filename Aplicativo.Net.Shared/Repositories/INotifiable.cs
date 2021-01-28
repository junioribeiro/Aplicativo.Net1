using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicativo.Net.Shared.Repositories
{
    public interface INotifiable
    {
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}
