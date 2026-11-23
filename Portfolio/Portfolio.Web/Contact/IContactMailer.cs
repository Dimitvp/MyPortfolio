using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Web.Contact
{
    public interface IContactMailer
    {
        bool IsAvailable { get; }

        Task SendAsync(ContactMessage message, CancellationToken cancellationToken);
    }
}
