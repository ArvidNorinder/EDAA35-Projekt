source("https://fileadmin.cs.lth.se/cs/Education/EDAA35/R_resources.R")

setwd("C:\\Users\\Arvid\\Desktop\\School\\Utvärdering av programvarosystem\\EDAA35-Projekt\\Java\\Mergesort\\")
file <- "data.txt"
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
	system("java -cp bin Mergesort 600 indata.txt data.txt")
	means <- c(means, calculatemean(file, 300))
}

#plottar efter sista loopen ovan
plotresult(file)

#avkommentera nedan för att få allt efter körning 150
#plotresult(file, 150)
#print(means)
#avkommentera för att få genomsnitt av genomsnitten (mest tillförlitliga värdet)
print(mean(means))
print(confidenceInterval(means))