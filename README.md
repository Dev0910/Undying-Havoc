# Undying Havoc

_Survive the day. Defend the night. Protect your lifeline._

---

## 📖 About the Game

**Undying Havoc** is a survival game set in a post-apocalyptic world, developed as a college project by a dedicated team of four. Stranded in a hostile environment, your survival depends on your ability to adapt, strategize, and endure. The game challenges players to survive relentless zombie attacks while managing resources and building a fortified base.

- **☀️ By Day:** Explore the desolate world to gather vital resources. Use what you find to build and reinforce your base, preparing for the terror that comes with sundown.
- **🌙 By Night:** Defend your life-sustaining oxygen generator from waves of zombies. The horde grows stronger and more numerous as the nights go on, pushing your defenses and tactical skills to the limit.

## ✨ Key Gameplay Features

- **🛍️ Shop-Based Enemy Spawning:** Enemies are introduced dynamically through an in-game shop system, adding a layer of strategic depth to combat and resource planning.
- **🎲 Dynamic Resource Spawning:** Resources appear in varied locations and quantities each playthrough, ensuring that no two survival attempts are the same.
- **🏗️ Dynamic Grid System:** A flexible grid system for building allows for creative base construction and strategic placement of defenses.

## 💻 Technical Highlights

- **📦 Extensive Use of Scriptable Objects:** Game data and behaviors are managed efficiently using Unity’s Scriptable Objects, allowing for a modular and scalable architecture.
- **♻️ Object Pooling for Optimization:** To ensure smooth performance, the game uses object pooling to reduce the overhead from frequently creating and destroying game objects like enemies and projectiles.

## 🚀 Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- Unity Engine (see `ProjectVersion.txt` for the recommended version)

### Installation

1.  Clone the repository to your local machine:
    ```sh
    git clone https://github.com/your-repository/undying-havoc.git
    ```
2.  Open the project folder in the Unity Hub.
3.  Unity will automatically install the required packages listed in `Packages/manifest.json`.
4.  Once the project is open, load the main scene from `Assets/Scenes/Main.unity` to begin.

## 📂 Project Structure

The project maintains a clean and organized structure for all game assets and code.

```
Undying-Havoc/
├── Assets/
│   ├── Animations/
│   ├── Prefabs/
│   ├── Scenes/
│   └── Scripts/
├── Packages/
└── ProjectSettings/
```

- **`Assets/`**: Contains all game assets, including scripts, scenes, prefabs, and resources.
- **`Scripts/`**: All core gameplay logic is located here, organized by system (Player, Enemy, UI, etc.).
- **`Scenes/`**: The main Unity scene files for the game.
- **`Prefabs/`**: A collection of reusable game objects like buildings, enemies, and weapons.

## 👥 Development Team

This project was brought to life by a team of dedicated student developers.

- **Lead Developer:** Dev Patel
- **Team Members:** Yash Jadhav, Arpan Chakraborty, Sarthak Nagpure

## ⚖️ License

This project is intended for educational purposes only and is not licensed for commercial distribution.
