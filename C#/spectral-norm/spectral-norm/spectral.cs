/* The Computer Language Benchmarks Game
 https://salsa.debian.org/benchmarksgame-team/benchmarksgame/

 contributed by Jesper Meyer
*/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace SpectralNorm
{
    unsafe class spectral
    {
        //arg 0 antal exec, arg 1 utfil
        public static void Main(string[] args)
        {
            //skriv ut tid för en loop till txt-doc.

            String[] times = new String[int.Parse(args[0])];

            for(int x = 0; x < 600; x++)
            {
                long start = 1000L * Stopwatch.GetTimestamp();


                int n = 100;

                fixed (double* u = new double[n])
                fixed (double* v = new double[n])
                {
                    new Span<double>(u, n).Fill(1);
                    for (var i = 0; i < 10; i++)
                    {
                        mult_AtAv(u, v, n);
                        mult_AtAv(v, u, n);
                    }

                    var result = Math.Sqrt(dot(u, v, n) / dot(v, v, n));
                    Console.WriteLine("{0:f9}", result);
                }
                long stop = 1000L * Stopwatch.GetTimestamp();

                long diff = (stop - start);

                long micro = diff / TimeSpan.TicksPerMillisecond;

                times[x] = micro.ToString();
            }
            File.WriteAllLines("." + args[1], times);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double A(int i, int j)
        {
            return (i + j) * (i + j + 1) / 2 + i + 1;
        }

        private static double dot(double* v, double* u, int n)
        {
            double sum = 0;
            for (var i = 0; i < n; i++)
                sum += v[i] * u[i];
            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void mult_Av(double* v, double* outv, int n)
        {
            Parallel.For(0, n, i =>
            {
                var sum = Vector128<double>.Zero;
                for (var j = 0; j < n; j += 2)
                {
                    var b = Sse2.LoadVector128(v + j);
                    var a = Vector128.Create(A(i, j), A(i, j + 1));
                    sum = Sse2.Add(sum, Sse2.Divide(b, a));
                }

                var add = Sse3.HorizontalAdd(sum, sum);
                var value = Unsafe.As<Vector128<double>, double>(ref add);
                Unsafe.WriteUnaligned(outv + i, value);
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void mult_Atv(double* v, double* outv, int n)
        {
            Parallel.For(0, n, i =>
            {
                var sum = Vector128<double>.Zero;
                for (var j = 0; j < n; j += 2)
                {
                    var b = Sse2.LoadVector128(v + j);
                    var a = Vector128.Create(A(j, i), A(j + 1, i));
                    sum = Sse2.Add(sum, Sse2.Divide(b, a));
                }

                var add = Sse3.HorizontalAdd(sum, sum);
                var value = Unsafe.As<Vector128<double>, double>(ref add);
                Unsafe.WriteUnaligned(outv + i, value);
            });
        }

        private static void mult_AtAv(double* v, double* outv, int n)
        {
            fixed (double* tmp = new double[n])
            {
                mult_Av(v, tmp, n);
                mult_Atv(tmp, outv, n);
            }
        }
    }
}