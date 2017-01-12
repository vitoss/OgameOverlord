using System;
using System.Threading.Tasks;

namespace Overlord {
    class Procrastinator {
        private int minDelay, maxDelay;
        
        public Procrastinator(int minDelay, int maxDelay) {
            this.minDelay = minDelay;
            this.maxDelay = maxDelay;
        }
        
        public void Sleep() {
            Task.Delay(GetRandomWaitTime()).Wait();
        }
        
        private int GetRandomWaitTime() {
            Random r = new Random();
            return r.Next(minDelay, maxDelay);
        }
    }
}