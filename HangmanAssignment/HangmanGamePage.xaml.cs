namespace HangmanAssignment;


public partial class HangmanGamePage : ContentPage
{
    private string wordToGuess = "KEYBOARD"; // Set the word to be guessed
    private char[] currentGuess; // Tracks current displayed word
    private int attemptsLeft = 7; // Tracks remaining attempts

    public HangmanGamePage()
    {
        InitializeComponent();
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Initialize the current guess with underscores
        currentGuess = new char[wordToGuess.Length];
        for (int i = 0; i < wordToGuess.Length; i++)
        {
            currentGuess[i] = wordToGuess[i] == ' ' ? ' ' : '_';
        }
        UpdateDisplayedWord();
        HangmanImage.Source = "hang1.png"; // Reset to the initial hangman image
    }

    private void OnGuessClicked(object sender, EventArgs e)
    {
        string input = GuessEntry.Text?.ToUpper();
        if (string.IsNullOrWhiteSpace(input) || input.Length != 1)
        {
            DisplayAlert("Invalid Input", "Please enter a single letter.", "OK");
            return;
        }

        char guessedLetter = input[0];
        bool isCorrect = false;

        for (int i = 0; i < wordToGuess.Length; i++)
        {
            if (wordToGuess[i] == guessedLetter)
            {
                currentGuess[i] = guessedLetter;
                isCorrect = true;
            }
        }

        if (!isCorrect)
        {
            attemptsLeft--;
            UpdateHangmanImage();
        }

        UpdateDisplayedWord();

        if (new string(currentGuess) == wordToGuess)
        {
            DisplayAlert("You survived!", $"Congratulations! The word was '{wordToGuess}'.", "OK");
            InitializeGame();
        }
        else if (attemptsLeft == 0)
        {
            DisplayAlert("You died!", $"Game Over! The word was '{wordToGuess}'.", "OK");
            InitializeGame();
        }

        GuessEntry.Text = string.Empty;
    }

    private void UpdateDisplayedWord()
    {
        // Update the displayed word with spaces between letters
        DisplayedWord.Text = string.Join(" ", currentGuess);
    }

    private void UpdateHangmanImage()
    {
        // Dynamically update the hangman image based on remaining attempts
        int imageIndex = 7 - attemptsLeft;
        HangmanImage.Source = $"hang{imageIndex + 1}.png";
    }
}
