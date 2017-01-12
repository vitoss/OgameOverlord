using System;

using Overlord.Model;

namespace Overlord {
    class TargetElector {
        private Coordinates[] coordinates;
        private int roundSize;
        
        public TargetElector(Coordinates[] coordinates, int roundSize) {
            this.coordinates = coordinates;
            this.roundSize = roundSize;
        }
        
        public Coordinates[] SelectRound(int roundNumber) {
            
            var startIndex = roundNumber * roundSize;
            var endIndex = Math.Min(startIndex + roundSize, coordinates.Length);
            var amount = Math.Max(0, endIndex-startIndex);
            var targets = new Coordinates[amount];
            
            var destIndex = 0;
            var srcIndex = startIndex;
            while(srcIndex < endIndex) {
                targets[destIndex++] = coordinates[srcIndex++];
            }
            
            return targets;
        }
    }
}