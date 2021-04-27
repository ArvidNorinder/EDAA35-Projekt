import java.io.BufferedWriter;
import java.io.FileWriter;
import java.util.Random;

public class GenerateDataFile {
	public static void main(String[] args) {
		int amountOfNumbers = 1000;
		int rangeOfNumbers = 10000;
		String fileName = "Data.txt";
		Random rand = new Random();
		try {
			BufferedWriter bw = new BufferedWriter(new FileWriter(fileName));
			for(int i = 0; i < amountOfNumbers; i++) {
				bw.write(Integer.toString(rand.nextInt(rangeOfNumbers)+1));
				bw.newLine();
			}
			bw.close();
		} catch (Exception e) {}
	}
}
