# Markov
This works by looking at the previous two words it has generated (or you have provided), then searching the lexicon to find all possible next words, then picking a random one weighted by the number of instances of each word. 
One common error that can occur is if the user inputs a phrase that does not occur in the text, or for some other reason does not have any possible next words (either because the user inputted words that are never adjacent in the source text, or the user inputs the last two words of the source, or the user inputs two words followed by a null character)
This should be compatible with windows filepaths, if there is an issue I will try to fix it

# Example of use
Has lexicon been created? (Y/n)\
n\
Please enter the filepath to your lexicon, or where you want it if it has not been created yet NOTE THIS MUST BE A CSV\
"C:\Users\[you]\Downloads\lexicon.csv" (the program will create this file if necessary)\
Please enter the filepath to your data\
"C:\Users\[you]\Downloads\Markov Data.txt" (this file must already exist, the program does not create it)\
# Help! It isn't working!
Common errors you may encounter
* The lexicon file is not a CSV. This will completely break the program, and when you specify the filepath for the lexicon you should ensure it is a csv
* The text you have entered as a seed never appears in the original document. If you struggle to find a place to begin text generation, either use the first two words, or open the CSV and pick two random words from the first column
* Either the data (if you are trying to create the lexicon) or the lexicon itself is open in another app, eg notepad, notepad ++, excel. Check both of these files are only being used by the program or it may fail to read data from them, resulting in a crash
