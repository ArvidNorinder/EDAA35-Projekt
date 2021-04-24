import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Mergesort{

    static List<Integer> copyArray(List<Integer> list){
        List<Integer> newList = new ArrayList<>();

        for(Integer i: list){
            newList.add(i);
        }

        return newList;
    }

    static void runSort(int n, String infil, String utfil){
        List<Integer> ogList = new ArrayList<>();

        //Läs in listan från fil
        try{
            Scanner reader = new Scanner(new File(infil));
            while(reader.hasNext()) {
                ogList.add(reader.nextInt());
            }

            BufferedWriter bw = new BufferedWriter(new FileWriter(utfil));
            bw.write("Löpnummer,Tid(ns)");
            bw.newLine();

            for(int i = 0; i < n; i++){
                List<Integer> list = copyArray(ogList);
                long t0 = System.nanoTime();
                merge(0, list.size() - 1, list);
                long t1 = System.nanoTime();
                long time = t1 - t0;
                bw.write((i + 1) + "," + time);
                bw.newLine();
            }

            bw.close();
        } catch(Exception exception) {
            System.out.println("Något gick snett: " + exception);
        }
        
    }

    private static void merge(int a, int b, List<Integer> list){
        int mid = (a + b) / 2;
        if(a != b){
            
            merge(a, mid, list);
            merge(mid + 1, b, list);

            int left = a; int right = mid + 1; List<Integer> tmp = new ArrayList<>();
            while(left <= mid && right <= b){
                if(list.get(left) < list.get(right)){
                    tmp.add(list.get(left));
                    left++;
                }else{
                    tmp.add(list.get(right));
                    right++;
                }
            }
            
            if(right > b){
                while(left <= mid){
                    tmp.add(list.get(left));
                    left++;
                }
            }

            for(int i = 0; i < tmp.size(); i++){
                list.set(i + a, tmp.get(i));
            }

        }
    }

    public static void main(String[] args){
        String infil = "Data.txt";
        String utfil = "Utfil.txt";
        String pathToFiles = System.getProperty("user.home") + "/EDAA35-Projekt/Java/main/";	//ersätt sista strängen med vägen till java-projektet
        int repetitions = 600;
        System.out.println(pathToFiles);
        runSort(repetitions, pathToFiles + infil, pathToFiles + utfil);
    }
}