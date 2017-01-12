
namespace Overlord {
    class UrlBuilder {
        private string _protocol = "https";
        private string _baseDomain;
        
        public UrlBuilder(string baseDomain, bool isSecure = true) {
            _baseDomain = baseDomain;
            
            if(!isSecure) {
                _protocol = "http";
            }
        }
        
        public string Create(string path) {
            return _protocol + "://" + _baseDomain + path;
        }
    }
}