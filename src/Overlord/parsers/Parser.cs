
namespace Overlord.Parser {
    interface Parser<T> {
        T Parse(string pageContent);
    }
}