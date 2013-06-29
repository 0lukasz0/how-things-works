using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace HowThingsWorks.Debugging
{
    //note: Project Properties -> Build -> Advanced -> Debug Info
    //change: pdb-only/none and observe stack trace difference
    class PdbExceptionsImpact
    {
        [Test]
    	public void ExceptionImpactTest()
        {
            long timeTicks = 0L;
            long timeMs = 0L;
    	    var stopwatch = new Stopwatch();
            stopwatch.Start();
    	    
    		try
    		{
                var p = new PdbTestClass(3);
                p.DoCoolStuff();
    		}
    		catch (Exception e)
    		{
                timeTicks = stopwatch.ElapsedTicks;
                timeMs = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(e.StackTrace);
    		}

            Console.WriteLine(timeTicks);
            Console.WriteLine(timeMs);
    	}

        class PdbTestClass
        {
            private int MaxDepth { get; set; }

            public PdbTestClass(int maxDepth)
            {
                if(maxDepth < 1)
                    throw new ArgumentOutOfRangeException("maxDepth");
                
                MaxDepth = maxDepth;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public void DoCoolStuff()
            {
                StartSteppingDown();
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public void StartSteppingDown()
            {
                StepDown();
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public void StepDown(int x=0)
            {
                if (x < MaxDepth)
                    StepDown(x + 1);
                else
                    throw new Exception("blah!");
            }
        }
    }
}
