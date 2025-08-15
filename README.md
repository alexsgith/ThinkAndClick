# ThinkAndClick

**Author:** Alex Savi (alex.savi.kv@gmail.com) 
**Created for:** Assessment test for Alantrix Geim Studio

---

##  Game Overview
At the start of each level, all cards are briefly revealed. The player must then flip two cards at a time, trying to find matching pairs. If the cards match, they remain open; if not, they flip back over. As mentioned in the doc before the game start you can chose any of the desired grid count for the cards 2*2,2*3,5*6 etc if the symbool and requrements matches. You can add more symbol in the cardSymbols list of CardManager script in Mainscene.

---

## Features
- **Dynamic Grid Setup** Player can choose the number of rows and columns before starting.
- **Score Tracking** Keeps track of matches, turns, and levels.
- **Level Progression** Each new level resets the score (optional) and increases the challenge.
- **Audio Feedback** Card flip, match, and wrong guess sounds.
- **UI Screens**:
  - Main Menu
  - Game Screen
  - Level Complete Screen
- **Popup Notifications** For invalid grid sizes or game messages.

---

## Technical Details
- **Engine:** Unity version 2021.3.45f1 (As mentioned in Doc)
- **Language:** C#
- **Core Scripts:**
  - `CardManager.cs` Handles card instantiation, shuffling, and matching logic.
  - `CardPrefabScript.cs` Controls card flipping animations and state.
  - `ScoreManager.cs` Tracks matches, turns, and levels.
  - `UiManager.cs` Manages screen transitions, UI updates, and player inputs.
  - `SoundManager.cs` Manages all in-game audio.
  - `PopupManager.cs` Displays messages and alerts.

---

## How to Play
1. **Start the Game** from the main menu (add or remove symbols if needed from the card manager script).
2. **Set Rows & Columns** (must result in an even number of total cards, with enough symbols available).
3. **Memorize the Cards** when they are briefly shown.
4. **Click Cards to Flip** and find matching pairs.
5. **Match All Pairs** to complete the level.
6. **Advance to Next Level** or return to the menu.

---

## Audio
- **Card Flip Sound**
- **Match Sound**
- **Wrong Guess Sound**
- **Background music**

You can toggle the **master volume** in the sound settings.

---

## Future Improvements
- Total score tracking across levels(commented inside code for better understanding).

---

## License
This project is created as part of an **assessment test** for Alantrix Geim Studio.  
Using the project for Commerial use without permission is restricted.