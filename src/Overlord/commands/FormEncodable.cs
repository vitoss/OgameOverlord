using System.Net.Http;

namespace Overlord {
    interface FormEncodable {
        FormUrlEncodedContent Encoded();
    }
}