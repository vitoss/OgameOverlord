using System;
using Xunit;

namespace Overlord.Tests {
    public class ProcrastinatorTests {
        
       [Fact]
       public void ShouldHaveSleepMethodWithSomeDuration() {
           var watch = System.Diagnostics.Stopwatch.StartNew();
           
           var procrastinator = new Procrastinator(101, 105);
           procrastinator.Sleep();
           
           watch.Stop();
           
           Assert.True(watch.ElapsedMilliseconds > 100, "There should be some delay");
       }
    }
}