**Unity Voice Command Game** is a PC demo project that demonstrates how to control a **2D/3D** (**evaluating two options**) game character using **voice commands** in **English**.  
The player can say words like **"run"**, **"walk"**, **"jump"**, **"turn left"**, **"turn right"**, **"go forward"**, **"go back"**, **"shoot"**, or **"attack"**, and the character reacts in real time.

In addition to voice control, the character can also be operated using standard **keyboard inputs**.

This project focuses on **Unity**, **C#**, and simple **speech recognition** integration for **desktop (Windows)** environments.

---

## 🎮 Game Control by Voice in Unity

The game demonstrates how to use Unity.

At the moment, evaluating two options for implementing voice control:
1. Running locally on Windows using **System.Speech** (free, offline)
2. Using a cloud-based API such as **Azure Speech Services**, **Google Speech-to-Text**, or **Wit.ai**  
   - Azure: free for 5 hours/month  
   - Google: free for 60 minutes/month  
   - Wit.ai: free for development

### Voice Features
- **Run / Walk** — Change movement speed  
- **Jump** — Jump if grounded  
- **Turn Left / Right / Forward / Back** — Change the character’s direction
- **Shoot / Attack** — Perform combat actions
