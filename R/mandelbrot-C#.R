setwd("C:\\Users\\Arvid\\Desktop\\School\\Utvärdering av programvarosystem\\EDAA35-Projekt\\C#\\mandelbrot\\mandelbrot")
file <- ".\\.data.txt"
means <- c()

plotresult <- function(file, start = 1) {
    ExecutionTimeMicroSeconds <- read.csv(file, header = F)
    ExecutionTimeMicroSeconds <- ExecutionTimeMicroSeconds[start:nrow(ExecutionTimeMicroSeconds),]
    print(colnames(ExecutionTimeMicroSeconds))
    plot(ExecutionTimeMicroSeconds, type='l')
}

calculatemean <- function(file, start = 1) {
    data <- read.csv(file, header = F)
    eqdata <- data[start:nrow(data),]
    mean(eqdata[1])
}


for(i in 1:10) {
	system("dotnet run 600 data.txt")
	means <- c(means, calculatemean(file))
}

#plottar efter sista loopen ovan
plotresult(file)

#avkommentera nedan för att få allt efter körning 150
#plotresult(file, 150)

#print(means)

print(mean(means))