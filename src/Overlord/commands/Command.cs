using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Overlord {
    abstract class Command<T> {
        protected Requester requester;
        protected UrlBuilder builder;
        
        protected ILogger logger { get; }
        
        public Command(UrlBuilder builder, Requester requester) {
            this.builder = builder;
            this.requester = requester;
            this.logger = ApplicationLogging.CreateLogger<Command<T>>();
        }
        
        abstract public Task<T> run();
    }
}