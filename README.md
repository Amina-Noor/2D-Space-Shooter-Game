# 2D Space Shooter Game

A desktop-based 2D Space Shooter Game developed using **C#** and **Windows Forms** on **.NET Framework 4.0** as part of the Human Computer Interaction (HCI) Lab course at Fatima Jinnah Women University.

---

## 📌 Project Overview

The player controls a spaceship, shoots incoming enemies, avoids collisions, and tries to survive as long as possible while the difficulty gradually increases. The project demonstrates graphical rendering, event-driven programming, keyboard interaction, collision detection, and HCI principles in a complete gaming application.

---

## 👩‍💻 Developed By

| Name | Roll No |
|------|---------|
| Amina Noor | 2023-BSE-007 |

**Department:** Software Engineering  
**University:** Fatima Jinnah Women University  

---

## 🛠️ Tools and Technologies

| Tool / Technology | Purpose |
|---|---|
| Visual Studio 2010 | IDE for development |
| C# | Programming language |
| .NET Framework 4.0 | Runtime environment |
| Windows Forms | User interface development |
| System.Drawing (GDI+) | Graphics rendering |
| Timer Class | Game loop and updates |
| System.Media.SoundPlayer | Sound effects |
| MSBuild | Building executable files |

---

## 🎮 Features

- Main Menu with Start and Exit options
- Player movement using Arrow Keys or WASD
- Shooting system (Space key)
- Enemy spawning and movement
- Collision detection
- Explosion animations
- Score and Lives system
- Dynamic difficulty (speed and spawn rate increase with score)
- Pause / Resume functionality
- Game Over screen with Restart and Main Menu options
- Sound effects
- Animated starfield background
- Vector graphics rendering (no external assets required)

---

## 🕹️ Controls

| Key | Action |
|-----|--------|
| Arrow Keys / WASD | Move spaceship |
| Space | Shoot |
| P | Pause / Resume |
| Esc | Return to menu |

---

## 🗂️ Project Structure
SpaceShooterGame/
├── Properties/
│   ├── AssemblyInfo.cs
│   ├── Resources.resx
│   └── Settings.settings
├── References/
├── Assets/
├── Sounds/
├── Game/
│   ├── AssetGenerator.cs
│   ├── Bullet.cs
│   ├── CollisionHelper.cs
│   ├── EnemyShip.cs
│   ├── ExplosionEffect.cs
│   ├── GameInputPanel.cs
│   ├── GameObject.cs
│   ├── GameRenderer.cs
│   ├── PlayerShip.cs
│   ├── SoundHelper.cs
│   └── StarParticle.cs
├── GameForm.cs
├── GameOverForm.cs
├── MainMenuForm.cs
└── Program.cs

---

## ⚙️ How to Run

1. Clone or download this repository.
2. Open `SpaceShooterGame.sln` in **Visual Studio 2010** or later.
3. Make sure **.NET Framework 4.0** is installed on your system.
4. Build the solution using **Build → Build Solution** (or press `Ctrl+Shift+B`).
5. Run the project using **Debug → Start Without Debugging** (or press `Ctrl+F5`).

---

## 🧠 HCI Principles Applied

- **Consistency** — Same controls and interface style throughout the game.
- **User Feedback** — Explosion animations, score updates, and sound effects provide immediate feedback.
- **Learnability** — Simple keyboard controls; instructions shown on the main menu.
- **Visibility of System Status** — Score, lives, level, and speed always visible on the HUD.
- **User Control and Freedom** — Pause, restart, and exit options available at all times.
- **Error Prevention** — Boundary limits for player movement, bullet cooldown to prevent spamming, and focus handling for keyboard input.

---

## ✅ Functional Requirements

| ID | Requirement |
|----|-------------|
| FR-01 | User can start the game |
| FR-02 | User can move the spaceship |
| FR-03 | User can shoot bullets |
| FR-04 | Enemies spawn automatically |
| FR-05 | Collision detection works |
| FR-06 | Score updates correctly |
| FR-07 | Lives decrease on collision |
| FR-08 | Game over screen appears |
| FR-09 | User can restart the game |
| FR-10 | Difficulty increases over time |

---

## 🚀 Future Improvements

- Multiplayer mode
- Boss enemies
- Power-ups
- Fullscreen support
- Customizable controls
- Colorblind-friendly mode
- High score saving
- Selectable difficulty levels
- Background music

---

## 📚 References

- [Windows Forms Documentation – Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
- [System.Drawing Namespace (GDI+) – Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.drawing)
- [Timer Class – Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.timer)
- [.NET Framework 4 Documentation – Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/framework/)
- Alan Dix, Janet Finlay, Gregory Abowd, Russell Beale – *Human-Computer Interaction*, 3rd ed., Pearson Education, 2004.





