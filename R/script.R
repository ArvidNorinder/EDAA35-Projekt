#använd system.time(command) för att mäta tid (ges dock i sekunder)

#function for running the java mergesort
javaSort <- function() {
system("java -cp ../Java/main/bin Mergesort)"
}

#function for running the C# mergesort
cSharpSort <- function() {
system("dotnet run -p ../C#/main")
}