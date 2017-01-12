using System;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Overlord {
    class WebProxy : IWebProxy
    {
        private readonly Uri _uri;

        public WebProxy(string uri)
        {
            _uri = new Uri(uri);
        }

        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination)
        {
            return _uri;
        }

        public bool IsBypassed(Uri host)
        {
            return host.IsLoopback;
        }
    }
    
    class Requester {
        private CookieContainer cookiesContainer = new CookieContainer();
        private bool _debug;
        private string _referrer = "";
        
        private Procrastinator procrastinator = new Procrastinator(2000, 8000);
        
        static ILogger Logger { get; } = ApplicationLogging.CreateLogger<Requester>();
        
        public Requester(bool debug) {
            this._debug = debug;
        }
        
        public async Task<string> Get(string page, string debugName) {
            procrastinator.Sleep();
            
            using(var client = CreateClient()) {
                AddHeaders(client);
                
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    _referrer = page;
                    
                    string result = await content.ReadAsStringAsync();

                    if(_debug) {
                        var cookies =  cookiesContainer.GetCookies(new Uri(page));
                        if(cookies.Count == 0) {
                            Logger.LogDebug("No cookies found in storage");
                        } else {
                            Logger.LogDebug($"Cookies found for {page}");
                        }
                        
                        foreach (Cookie cookie in cookies) {
                            Logger.LogDebug(cookie.Name + ": " + cookie.Value);
                        }
                    }

                    if (result != null)
                    {
                        if(_debug) {
                            PrintPage(debugName, result);
                        }
                        
                        return result;
                    }
                    
                    return "";
                }
            }
        } 
        
       public async Task<string> PostForm(string page, FormUrlEncodedContent payload, string debugName) {
           procrastinator.Sleep();
           
           using(var client = CreateClient()) {
               AddHeaders(client);
               client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
               client.DefaultRequestHeaders.TryAddWithoutValidation("Origin", "https://pl.ogame.gameforge.com");
               
               using (HttpResponseMessage response = await client.PostAsync(page, payload))
               using (HttpContent content = response.Content)
               {
                   _referrer = page;
                   
                   string result = await content.ReadAsStringAsync();
                   var cookies =  cookiesContainer.GetCookies(new Uri(page));
                   
                   if(_debug) {
                       if(cookies.Count == 0) {
                           Logger.LogDebug("No cookies found in storage");
                       } else {
                           Logger.LogDebug($"Cookies found for {page}");
                       }
                       
                       foreach (Cookie cookie in cookies) {
                           Logger.LogDebug(cookie.Name + ": " + cookie.Value);
                       }
                   }
                   
                   if (result != null)
                   {
                       if(_debug) {
                           PrintPage(debugName, result);
                       }
                       return result;
                   }
                   
                   return "";
                }
            }
        }
        
        private void PrintPage(string filename, string content) {
            File.WriteAllText("pages/" + filename, content);
            Logger.LogDebug($"Downloaded page having size: {content.Length}"); //Substring(0,1500)
        }
        
        private HttpClient CreateClient() {
            var handler = new HttpClientHandler()
            {
                Proxy = new WebProxy("https://localhost:8080"),
                UseProxy = false,
                CookieContainer = cookiesContainer,
                AllowAutoRedirect = false
            };
            
            return new HttpClient(handler);
        }
        
        private void AddHeaders(HttpClient client) {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, sdch");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.8");
            
            if(_referrer.Length > 0) {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Referrer", _referrer);
            }
        }
    }
}