# Markov
This works by looking at the previous two words it has generated (or you have provided), then searching the lexicon to find all possible next words, then picking a random one weighted by the number of instances of each word. 
One common error that can occur is if the user inputs a phrase that does not occur in the text, or for some other reason does not have any possible next words (either because the user inputted words that are never adjacent in the source text, or the user inputs the last two words of the source, or the user inputs two words followed by a null character)
This should be compatible with windows filepaths, if there is an issue I will try to fix it
