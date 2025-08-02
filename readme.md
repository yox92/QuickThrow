# 🎯 QuickThrowGrenade

This mod for **SPT** restores the deprecated "Fast Grenade Throw" mechanic from earlier Tarkov versions (≈ <0.12), allowing you to **instantly throw a grenade** without having to equip it first.

## 🔧 Key Features

- **Fast throw grenade**: Pressing the grenade key will throw the grenade instantly, skipping equip animations.
- **Low throw override**: Hold a configurable 'Left Control' key to **force a short throw** with QuickThrow feature.
- **Restore Vanilla**: Hold a configurable key 'Shift' to **to equip it first**. Vanilla feature.

## 🔌 Installation

Drop `QuickThrow.dll`, `QuickThrow_log.txt`, `debug.cfg` folder into your `BepInEx/plugins/QuickThrow` directory.

## 🧠 How It Works

This mod uses Harmony patches on two key methods:
- `Player.SetInHands(ThrowWeapItemClass, Callback<IHandsThrowController>)`  
  → Replaced with `SetInHandsForQuickUse` unless a bypass key is held.
- `Player.BaseGrenadeHandsController.vmethod_1(float timeSinceSafetyLevelRemoved, bool low)`  
  → Dynamically enforces **low throw** if the appropriate key is pressed.

## 🖱️ Keybindings

| Action              | Default Key                            |
|---------------------|----------------------------------------|
| Force low throw     | `Left Control` or any configurable key |
| Force Vanilla throw | `Left Shift` or any configurable key   |

## ⚙️ Debug
A detailed log is printed to `QuickThrow_log.txt` when file `debug.cfg` true.