/*
The Computer Language Benchmarks Game
https://salsa.debian.org/benchmarksgame-team/benchmarksgame/

contributed by Ziad Hatahet
based on the Go entry by K P anonymous
*/

import java.io.BufferedWriter;
import java.io.FileWriter;

public class spectralnorm {
	private static final int NCPU = Runtime.getRuntime().availableProcessors();

	public static void main(String[] args) throws InterruptedException {
		String utfil = "spectralnorm-data.txt";
		int reps = args.length > 0 ? Integer.parseInt(args[0]) : 600;
		try {
			BufferedWriter bw = new BufferedWriter(new FileWriter(utfil));
			bw.write("Löpnummer,Tid(mikrosekunder)");
			bw.newLine();

			for (int iteration = 0; iteration < reps; iteration++) {
				long t0 = System.nanoTime()/1000;
				final int n = 100;
				final var u = new double[n];
				for (int i = 0; i < n; i++)
					u[i] = 1.0;
				final var v = new double[n];
				for (int i = 0; i < 10; i++) {
					aTimesTransp(v, u);
					aTimesTransp(u, v);
				}

				double vBv = 0.0, vv = 0.0;
				for (int i = 0; i < n; i++) {
					final var vi = v[i];
					vBv += u[i] * vi;
					vv += vi * vi;
				}
				var value = Math.sqrt(vBv / vv);
				long t1 = System.nanoTime()/1000;
				bw.write((iteration + 1) + "," + (t1 - t0));
				bw.newLine();
			}
			bw.close();
		} catch (Exception e) {
		}
	}

	private static void aTimesTransp(double[] v, double[] u) throws InterruptedException {
		final var x = new double[u.length];
		final var t = new Thread[NCPU];
		for (int i = 0; i < NCPU; i++) {
			t[i] = new Times(x, i * v.length / NCPU, (i + 1) * v.length / NCPU, u, false);
			t[i].start();
		}
		for (int i = 0; i < NCPU; i++)
			t[i].join();

		for (int i = 0; i < NCPU; i++) {
			t[i] = new Times(v, i * v.length / NCPU, (i + 1) * v.length / NCPU, x, true);
			t[i].start();
		}
		for (int i = 0; i < NCPU; i++)
			t[i].join();
	}

	private final static class Times extends Thread {
		private final double[] v, u;
		private final int ii, n;
		private final boolean transpose;

		public Times(double[] v, int ii, int n, double[] u, boolean transpose) {
			this.v = v;
			this.u = u;
			this.ii = ii;
			this.n = n;
			this.transpose = transpose;
		}

		@Override
		public void run() {
			final var ul = u.length;
			for (int i = ii; i < n; i++) {
				double vi = 0.0;
				for (int j = 0; j < ul; j++) {
					if (transpose)
						vi += u[j] / a(j, i);
					else
						vi += u[j] / a(i, j);
				}
				v[i] = vi;
			}
		}

		private static int a(int i, int j) {
			return (i + j) * (i + j + 1) / 2 + i + 1;
		}
	}
}