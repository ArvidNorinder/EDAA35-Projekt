source("https://fileadmin.cs.lth.se/cs/Education/EDAA35/R_resources.R")

setwd("C:\\Users\\Arvid\\Desktop\\School\\Utvärdering av programvarosystem\\EDAA35-Projekt\\Java\\mandelbrot\\")

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
	means <- c(means, calculatemean(paste("data", i, ".txt", sep = ""), 30))
}

plotresult("data10.txt")

print(mean(means))

print(confidenceInterval(means))