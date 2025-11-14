
🧠 Voktrain — Console Vocabulary Trainer (C#)

Voktrain is a console-based vocabulary trainer written entirely in C#.
It helps you learn and manage German ↔ English vocabulary through interactive tests, progress tracking, and a lightweight text-based UI.

✨ Features
🎯 Test Mode

Choose difficulty level (Leicht, Mittel, Schwer, or Alle).

Set question count and translation direction (DE→EN or EN→DE).

Each question presents 1 correct and 3 random incorrect options.

Fully keyboard-controlled with ↑ ↓ for navigation and ENTER to answer.

Tracks right/wrong answers and updates both word and test statistics automatically.

📚 Vocabulary Manager

Manage your word lists directly from the console:

Add new words interactively:

Input Deutsch, Englisch, and Schwierigkeitsgrad.

Duplicate detection prevents accidental re-entry.

Saves words automatically to difficulty-based JSON files.

View and edit existing words:

Paginated lists (20 per page).

Navigate with ↑ ↓, ← →, or select entries to edit/delete.

Search function to quickly find any word regardless of difficulty.

📈 Statistics

Analyze your learning progress:

Per Word — track how often you practiced and how many times it was correct.

Per Test — see test summaries (questions, correct %, and difficulty).

Persistent across sessions via automatic JSON save files.

🗂️ File Structure
📦 Voktrain
 ┣ 📂 data
 ┃ ┣ leicht.json
 ┃ ┣ mittel.json
 ┃ ┣ schwer.json
 ┃ ┣ stats_tests.json
 ┃ ┗ stats_words.json
 ┣ 📜 Program.cs
 ┣ 📜 VokabelTrainer.cs
 ┣ 📜 DataManager.cs
 ┣ 📜 TestConfigStart.cs
 ┣ 📜 TestStarten.cs
 ┣ 📜 VokabelManager.cs
 ┣ 📜 VokabelnAnsehenBearbeiten.cs
 ┣ 📜 VokabelnHinzu.cs
 ┣ 📜 SeeStats.cs
 ┗ 📜 Menu.cs

⌨️ Controls
Key	Action
↑ / ↓	Move through menu options
← / →	Change page or switch category
ENTER	Confirm / Select
ESC or “Zurück”	Return to previous menu
💾 Data Persistence

All progress and vocabulary are saved automatically as JSON files in /data.
The system separates:

Vocabulary by difficulty level

Statistics by tests and words

This makes backups and manual edits simple.

🧩 Tech Overview
Component	Purpose
Menu.cs	Handles vertical & horizontal console navigation
DataManager.cs	Loads, saves, and serializes data
TestStarten.cs	Runs vocabulary tests
TestConfigStart.cs	Configures test settings
VokabelManager.cs	Main vocabulary management menu
VokabelnAnsehenBearbeiten.cs	Word listing, paging, editing
VokabelnHinzu.cs	Adds new words interactively
SeeStats.cs	Displays statistics

🛠️ Requirements
.NET 8 SDK or newer
Windows Console (UTF-8 support recommended)


🚀 Run It

From the project root:

dotnet build
dotnet run


Then simply follow the on-screen menus!

🧑‍💻 Author

Developed by Bianca Krause
for educational and personal learning purposes (Fachinformatiker AE).
Built with ❤️ in C#.
