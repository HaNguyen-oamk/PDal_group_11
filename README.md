# 🎮 Unity Voice-Controlled Shooting Game (PC Demo)

This is a Unity PC demo **shooting 2D game** where the player controls a character using
**English voice commands** and **keyboard input**.

The player can move, change speed, and **shoot enemies** using voice commands in real time.

---

## 🗣️ Voice Control (Vosk)

This project uses **Vosk Speech Recognition** for voice input.

- Offline speech recognition
- Free and open-source
- Works on Windows (PC)
- No internet connection required

Voice commands are normalized to reduce recognition errors before being applied to gameplay.

---

## 🎤 Voice Commands

- **Run / Walk** – Change movement speed
- **Go up / down / left / right** – Move character
- **Turn left / Turn right** – Change direction
- **Stop** – Stop movement
- **Shoot / Fire** – shoot weapon

Voice-based movement will auto-stop after a short time if no new command is detected.

---

## ⌨️ Keyboard Support

The game also supports standard keyboard input for movement and shooting.
This allows normal gameplay without voice control.

---

## 🗺️ Game Flow

1. **Main Menu** – Start / Exit
2. **Map Select** – Map 1 / Map 2 / Map 3
3. **Player Select** – Player 1 / Player 2
4. **Gameplay**
   - Defeat enemies using shooting mechanics
   - Player died → Game Over UI
5. **Level Complete UI**
   - Next Level / Restart / Exit

---

## 💻 Platform & Tools

- Unity
- C#
- Vosk Speech Recognition
- Windows (PC)

---

## 📦 Assets & License

This project uses **free assets from Unity Asset Store** and other free sources,
used for **educational purposes** under their respective licenses.
